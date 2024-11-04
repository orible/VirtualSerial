using Win32 = Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO.Ports;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static VirtualSerial.ColourScheme;
using System.Runtime.InteropServices;

namespace VirtualSerial
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            RefreshPorts();
            comboBoxStopBits.DataSource = new StopBits[] {
                StopBits.None,
                StopBits.One,
                StopBits.Two,
                StopBits.OnePointFive };
            comboBoxParity.DataSource = new Parity[] {
                Parity.Odd,
                Parity.None,
                Parity.Mark,
                Parity.Space,
                Parity.Even };
            comboBoxDataBit.DataSource = new DataBits[] {
                DataBits.Five,
                DataBits.Six,
                DataBits.Seven,
                DataBits.Eight
            };
            comboBoxDataBit.DisplayMember = "Item1";
            comboBoxHandshake.DataSource = new Handshake[] { Handshake.None, Handshake.XOnXOff, Handshake.RequestToSend, Handshake.RequestToSendXOnXOff };
            comboBoxSendAs.DataSource = new Message.SendAsEncoding[] { Message.SendAsEncoding.ASCII, Message.SendAsEncoding.ASCII_utf8, Message.SendAsEncoding.ASCII_UTF7 };
            toolStripStatusLabelActivity.Text = "NOT CONNECTED";
            toolStripStatusLabelVersion.Text = AssemblyInfo.GetGitHash();
            comboBoxReadMode.DataSource = new ReadMode[] {
                ReadMode.RAW,
                ReadMode.LINE_BUFFERED,
                ReadMode.STOP_BUFFERED
            };
        }

        public enum ReadMode
        {
            LINE_BUFFERED = 1,
            STOP_BUFFERED = 2,
            RAW = 3,
        }

        private byte[] GetStopCode()
        {
            byte[] buf = Parse.ParseBadHexStringLiteral(this.textBoxStopCode.Text, 2);
            return buf.Take(buf.Length - 1).ToArray();
        }

        [DllImport("user32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int DestroyCaret();

        [DllImport("user32.dll")]
        extern static int GetCaretBlinkTime();

        [DllImport("user32.dll")]
        extern static int SetCaretBlinkTime(int wMSeconds);


        ColourSchemeData themeData;

        public void DrawCaret(Control ctrl)
        {
            var nHeight = 0;
            var nWidth = 10;

            nHeight = Font.Height;

            CreateCaret(ctrl.Handle, IntPtr.Zero, nWidth, nHeight);
            ShowCaret(ctrl.Handle);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //Font font1 = new Font(FontFamily.GenericMonospace, richTextBoxInput.Font.Size);
            Font font1 = new Font("Lucida Console", richTextBoxInput.Font.Size * (float)1.1);

            richTextBoxInput.Font = font1;
            richTextBoxInputHex.Font = font1;
            richTextBoxInputLog.Font = font1;
            richTextBoxOutputLog.Font = font1;
            richTextBoxConsole.Font = font1;

            //this.richTextBoxConsole.GotFocus += _gainedFocus;
            //this.richTextBoxConsole.LostFocus += _lostFocus;

            //DrawCaret(richTextBoxConsole);
            //DrawCaret(richTextBoxInput

            themeData = new ColourSchemeData(this.BackColor, this.ForeColor);
            labelProg2.Text = "";
            labelSpinnerPoll.Text = "";
            labelSpinnerRead.Text = "";
        }
        private void UpdateInfo()
        {
            toolStripStatusLabelReadWrite.Text = $"IO rx {0 / 1024} KB / tx {0 / 1024} KB";
        }
        private void _lostFocus(object? sender, EventArgs e)
        {
            DestroyCaret();
        }
        private void _gainedFocus(object? sender, EventArgs e)
        {
            DrawCaret(richTextBoxConsole);
        }

        BindingList<String> ports;
        private static Mutex mutVmList = new Mutex();
        Dictionary<string, VM> scripts = new Dictionary<string, VM>();

        private void RefreshPorts()
        {
            // read from registry apparently!
            ports = new BindingList<string>(SerialPort.GetPortNames());
            this.comboBox1.DataSource = ports;
        }

        private void buttonPortRefresh_Click(object sender, EventArgs e) => RefreshPorts();

        SerialPort _serialPort;
        public enum DataBits { Five = 5, Six = 6, Seven = 7, Eight = 8 };

        string GetTimecode(DateTime time)
        {
            return $"[{time.ToString("dd/MM/yy HH:mm:ss")}";
        }

        // thread locking queues
        BlockingCollection<Message> queueMessageRead = new BlockingCollection<Message>();
        BlockingCollection<Message> queueMessageWrite = new BlockingCollection<Message>();
        BlockingCollection<Message> queueMessageScriptInput = new BlockingCollection<Message>();

        // owned by _WriteOpenPort thread
        List<string> writeMessages = new List<string>();
        volatile bool WriteThreadRunning;

        void _WriteOpenPort(object state)
        {
            var initState = (State)state;
            int writeBytes = 0; int readBytes = 0;
            richTextBoxInputLog.Invoke(() => { richTextBoxInputLog.AppendText("[Thread] Start\n"); });
            WriteThreadRunning = true;
            bool _stop = false;
            while (!_stop)
            {
                Message msg = queueMessageWrite.Take();
                if (msg.Code == Message.MessageCode.STOP) break;
                try
                {
                    writeBytes += msg.Buf.Length;
                    _serialPort.Write(msg.Buf, 0, msg.Buf.Length);
                    string timecode = GetTimecode(DateTime.Now);
                    richTextBoxInputLog.Invoke((Message msg, string timecode) =>
                    {
                        UpdateInfo();
                        //writeMessages.Add(msg.UserRepresentation);
                        richTextBoxInputLog.AppendText($">> {System.Text.Encoding.ASCII.GetString(msg.Buf)}\n");
                        richTextBoxInputLog.AppendText($">> {string.Join(", ", msg.Buf)}\n");
                        richTextBoxConsole.AppendText(System.Text.Encoding.ASCII.GetString(msg.Buf));
                    }, msg, timecode);
                }
                catch (Exception e)
                {
                    switch (e)
                    {
                        case System.TimeoutException:
                            break;
                        default:
                            this.richTextBoxInputLog.Invoke((string data) =>
                            {
                                richTextBoxInputLog.AppendText(data);
                            }, (e.ToString()));
                            _stop = true;
                            break;
                    }
                }
            }

            richTextBoxInputLog.Invoke(() => { richTextBoxInputLog.AppendText("[Thread] Signalling read to stop...\n"); });

            // shut down read thread
            queueMessageRead.Add(new Message(Message.MessageCode.STOP));

            richTextBoxInputLog.Invoke(() => { richTextBoxInputLog.AppendText("[Thread] Stopped\n"); });

            WriteThreadRunning = false;
        }
        // end ownership of _WriteOpenPort Thread


        readonly string[] spinner = new string[] { "|", "/", "-", "\\" };

        // owned by _readOpenPort thread
        List<string> readMessages = new List<string>();
        volatile bool ReadThreadRunning;
        const int ReadChunkSize = 512;

        // Poll open port and control message thread
        void _ReadOpenPort(object state)
        {
            var initState = (State)state;
            int spinnerActivity = 0;
            int spinnerMsgBuf = 0;
            int readBytes = 0;

            ReadMode readMode = initState.buffermode;
            ReadThreadRunning = true;


            StopBuffer _stopBuffer = new StopBuffer();
            //MemoryStream mem = new MemoryStream();
            byte[] stopcode = { 0x0D };
            stopcode = GetStopCode();

            string stopcodelit = BitConverter.ToString(stopcode, 0, stopcode.Length).Replace("-", " ");

            richTextBoxOutputLog.Invoke((string stopcodelit) =>
            {
                richTextBoxOutputLog.AppendText("[Thread] Start\n");
                richTextBoxOutputLog.AppendText($"[Thread] Stopcode: <<{stopcodelit}>>\n");
            }, stopcodelit);

            bool _stop = false;
            while (true)
            {
                if (_stop) break;
                Message msg;
                labelProg2.Invoke(() => labelProg2.Text = "Checking message buffer");
                if (queueMessageRead.TryTake(out msg))
                //msg = queueMessageRead.Take();
                {
                    if (msg.Code == Message.MessageCode.STOP) break;
                }

                labelSpinnerPoll.Invoke(() =>
                {
                    if (++spinnerActivity >= spinner.Length)
                    {
                        spinnerActivity = 0;
                    }
                    labelSpinnerPoll.Text = spinner[spinnerActivity];
                });

                try
                {
                    labelProg2.Invoke(() => labelProg2.Text = "Checking port buffer");
                    byte[] buf = new byte[1024];
                    string message;
                    int read;
                    switch (readMode)
                    {
                        case ReadMode.STOP_BUFFERED:
                            int sz = _serialPort.Read(buf, 0, 1024);
                            _stopBuffer.Add(buf, sz);
                            byte[] outbuf;
                            read = _stopBuffer.GetMessage(stopcode, out outbuf);
                            if (read < 1) continue;
                            message = "\n" + Encoding.ASCII.GetString(outbuf, 0, read);
                            break;
                        case ReadMode.LINE_BUFFERED:
                            message = _serialPort.ReadLine();
                            read = message.Count() * 1;
                            break;
                        case ReadMode.RAW:
                            read = _serialPort.Read(buf, 0, 1024);
                            if (read < 1) continue;
                            message = Encoding.ASCII.GetString(buf, 0, read);
                            break;
                        default:
                            this.richTextBoxOutputLog.Invoke(() => richTextBoxOutputLog.AppendText("error: no buffer mode selected, discarding buffer"));
                            read = 0;
                            continue;
                    }
                    readBytes += read;


                    string timecode = GetTimecode(DateTime.Now);
                    this.richTextBoxOutputLog.Invoke((string data, string timecode) =>
                    {
                        UpdateInfo();
                        if (++spinnerMsgBuf >= spinner.Length)
                        {
                            spinnerMsgBuf = 0;
                        }
                        labelSpinnerRead.Text = spinner[spinnerMsgBuf];
                        readMessages.Add(data);
                        richTextBoxConsole.AppendText(data);
                        richTextBoxOutputLog.AppendText(data);
                        richTextBoxOutputLog.ScrollToCaret();
                    }, message, timecode);
                    mutVmList.WaitOne();
                    foreach (var kv in this.scripts)
                    {
                        // invoked in the VM thread context - guaranteed to be a FIFO queue
                        kv.Value.Invoke((VM vm, object[] ctx) =>
                        {
                            var readMode = ctx[1];
                            var message = ctx[0];
                            vm.UnsafeCall("RECEIVE", message);
                            return null;
                        }, message, readMode);
                    }
                    mutVmList.ReleaseMutex();
                    //ListenersEmitMessage(new Message(Message.MessageCode.READ, buf));
                }
                catch (Exception e)
                {
                    switch (e)
                    {
                        case TimeoutException:
                            break;
                        default:
                            this.richTextBoxOutputLog.Invoke((string data) =>
                            {
                                MessageBox.Show(data);
                                richTextBoxOutputLog.AppendText("ERROR");
                            }, (e.ToString()));
                            _stop = true;
                            break;
                    }
                }
            }

            // signal writer to stop
            queueMessageWrite.Add(new Message(Message.MessageCode.STOP));
            Invoke((Delegate)(() =>
            {
                toolStripStatusLabelReadWrite.Text = $"IO rx {readBytes} / tx 0";
                labelProg2.Text = "Shutting down...";
                richTextBoxOutputLog.AppendText("\n[Thread] Shutdown signal...\n");
            }));

            // empty input thread
            while (true)
            {
                Message itm;
                if (!queueMessageRead.TryTake(out itm))
                    break;
            }

            // close port
            _serialPort.Close();
            this.Invoke(() =>
            {
                this.buttonConnect.Enabled = true;
                this.buttonDisconnect.Enabled = false;
                richTextBoxOutputLog.AppendText("[Thread] Stopped\n");
                labelProg2.Text = "Not connected";
            });

            ReadThreadRunning = false;

            initState.IsWriteRunning = WriteThreadRunning;
            initState.IsReadRunning = ReadThreadRunning;

            ListenersEmitServiceStatus(initState);
        }
        // end ownership of _ReadOpenPort Thread
        public ScriptTerminalForm GetActiveScript()
        {
            return this.UITermList.First();
        }

        Port GetUISettingsToPort()
        {
            int baud, _databits, readtimeout, writetimeout;
            if (!int.TryParse(this.textBoxInputBaud.Text, out baud))
                throw new System.ArgumentException("Input Baud bad input");

            if (!int.TryParse(this.textBoxReadTimeout.Text, out readtimeout))
                throw new System.ArgumentException("Input read timeout bad input");

            if (!int.TryParse(this.textBoxWriteTimeout.Text, out writetimeout))
                throw new System.ArgumentException("Input write timeout bad input");

            Parity parity = (Parity)comboBoxParity.SelectedItem;
            StopBits stops = (StopBits)comboBoxStopBits.SelectedItem;
            DataBits databits = (DataBits)comboBoxDataBit.SelectedItem;

            Port port = new Port();
            port.BaudRate = baud;
            port.Parity = parity;
            port.PortName = (string)this.comboBox1.SelectedValue;
            port.DataBits = databits;
            port.StopBits = stops;
            port.ReadTimeout = readtimeout;
            port.WriteTimeout = writetimeout;
            port.StopCode = this.textBoxStopCode.Text;
            return port;
        }
        void SetUIFromPortSettings(Port p)
        {
            this.textBoxInputBaud.Text = p.BaudRate.ToString();
            this.textBoxReadTimeout.Text = p.ReadTimeout.ToString();
            this.textBoxWriteTimeout.Text = p.WriteTimeout.ToString();
            this.comboBox1.SelectedItem = p.PortName;
            this.comboBoxParity.SelectedItem = p.Parity;
            this.comboBoxDataBit.SelectedItem = p.DataBits;
            this.comboBoxStopBits.SelectedItem = p.StopBits;
            this.comboBoxReadMode.SelectedItem = p.BufferMode;
            this.textBoxStopCode.Text = p.StopCode?.ToString() ?? "";
            // TODO:
        }
        int GetPortSettings(ref SerialPort port, ref State state)
        {
            int baud, _databits, readtimeout, writetimeout;
            if (!int.TryParse(this.textBoxInputBaud.Text, out baud))
                throw new System.ArgumentException("Input Baud bad input");

            if (!int.TryParse(this.textBoxReadTimeout.Text, out readtimeout))
                throw new System.ArgumentException("Input read timeout bad input");

            if (!int.TryParse(this.textBoxWriteTimeout.Text, out writetimeout))
                throw new System.ArgumentException("Input write timeout bad input");

            Parity parity = (Parity)comboBoxParity.SelectedItem;
            StopBits stops = (StopBits)comboBoxStopBits.SelectedItem;
            DataBits databits = (DataBits)comboBoxDataBit.SelectedItem;

            port.BaudRate = baud;
            port.Parity = parity;
            port.PortName = (string)this.comboBox1.SelectedValue;
            port.DataBits = (int)databits;
            port.StopBits = stops;
            port.ReadTimeout = readtimeout;
            port.WriteTimeout = writetimeout;

            state.PortID = (string)this.comboBox1.SelectedValue;
            state.bits = databits;
            state.readtimeout = readtimeout;
            state.writetimeout = writetimeout;
            state.parity = parity;
            return 1;
        }

        Mutex mutState = new Mutex();
        State state = new State(false);

        void InitPort()
        {
            toolStripStatusLabelActivity.Text = "STARTING";
            Thread readThread = new Thread(new ParameterizedThreadStart(_ReadOpenPort));
            Thread writeThread = new Thread(new ParameterizedThreadStart(_WriteOpenPort));

            // eat message buffer
            while (queueMessageRead.TryTake(out _)) { }
            while (queueMessageWrite.TryTake(out _)) { }

            // open new port
            _serialPort = new SerialPort();

            //state = new State(true);
            // apply UI settings
            GetPortSettings(ref _serialPort, ref state);
            _serialPort.Handshake = Handshake.None;

            // open threads and start port
            _serialPort.Open();

            state.Connected = true;
            state.buffermode = (ReadMode)comboBoxReadMode.SelectedItem;
            readThread.Start(state);
            writeThread.Start(state);
            ListenersEmitServiceStatus(new State(true));
        }

        volatile bool flagIsRunning = false;
        void InitPortFromUI()
        {
            if (WriteThreadRunning || ReadThreadRunning) return;
            try
            {
                InitPort();
                this.buttonConnect.Enabled = false;
                this.buttonDisconnect.Enabled = true;
                toolStripStatusLabelActivity.Text = "OPEN";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Fatal Error");
            }
        }

        void ShutDownThreads()
        {
            // send thread stop events
            queueMessageWrite.Add(new Message(Message.MessageCode.STOP));
        }

        bool skipTextBoxUpdate = false;
        private void richTextBoxInputHex_TextChanged(object sender, EventArgs e) { }
        private void richTextBoxInput_TextChanged(object sender, EventArgs e)
        {
            // convert ascii to text
            if (skipTextBoxUpdate) { skipTextBoxUpdate = false; return; }

            InputTextHistory.UpdateBuffer(this.richTextBoxInput.Text);

            byte[] buf = Encoding.ASCII.GetBytes(this.richTextBoxInput.Text);
            string hexbuf = BitConverter.ToString(buf, 0, buf.Length).Replace("-", " ");
            richTextBoxInputHex.Text = hexbuf;
        }

        private void buttonConvText_Click(object sender, EventArgs e)
        {
            // send this event to the main text box which will then update the hex box with the correct formatting
            richTextBoxInput_TextChanged(sender, e);
        }

        private void buttonConvHex_Click(object sender, EventArgs e)
        {
            skipTextBoxUpdate = true;
            string str = richTextBoxInputHex.Text;
            byte[] buffer = Parse.ParseBadHexStringLiteral(str, 2);

            // convert back to text to show it in the text representation box
            string text = System.Text.Encoding.ASCII.GetString(buffer);
            richTextBoxInput.Text = text;
        }

        //converters
        private void richTextBoxConvHEX_TextChanged(object sender, EventArgs e) { }

        private void richTextBoxConvASCII_TextChanged(object sender, EventArgs e) { }

        private void buttonConnect_Click(object sender, EventArgs e) => InitPortFromUI();

        private void buttonInputSend_Click(object sender, EventArgs e)
        {
            if (richTextBoxInput.Text == "") return;
            string data = richTextBoxInput.Text;

            // commit and clear buffer
            //if (!UIIgnoreTextCursorUpdate)
            //{
            InputTextHistory.UpdateBuffer(richTextBoxInput.Text);
            UIIgnoreTextCursorUpdate = false;
            //}

            InputTextHistory.Commit();

            Message.SendAsEncoding enc = (Message.SendAsEncoding)this.comboBoxSendAs.SelectedItem;

            byte[] buf;
            switch (enc)
            {
                case Message.SendAsEncoding.ASCII:
                    buf = Encoding.ASCII.GetBytes(data);
                    break;
                case Message.SendAsEncoding.ASCII_utf8:
                    buf = Encoding.UTF8.GetBytes(data);
                    break;
                case Message.SendAsEncoding.ASCII_UTF7:
                    buf = Encoding.UTF7.GetBytes(data);
                    break;
                default:
                    MessageBox.Show("Bad SendAsEncoding combo box selection");
                    return;
            }

            queueMessageWrite.Add(new Message(data, buf, enc));
            richTextBoxInput.Clear();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e) => ShutDownThreads();

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => (new AboutForm()).ShowDialog();

        private void buttonInputHexSend_Click(object sender, EventArgs e)
        {
            if (richTextBoxInputHex.Text == "") return;
            byte[] buf = Parse.ParseBadHexStringLiteral(richTextBoxInputHex.Text, 2);
            queueMessageWrite.Add(new Message(richTextBoxInputHex.Text, buf, Message.SendAsEncoding.ASCII));
            richTextBoxInput.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBoxInput.AppendText("\r");
        }

        private static Mutex mutUITermList = new Mutex();
        // UI table of child script windows
        List<ScriptTerminalForm> UITermList = new List<ScriptTerminalForm>();

        public class MessageEventArgs : EventArgs
        {
            public Message Data;
            public MessageEventArgs(Message data) : base()
            {
                Data = data;
            }
        }

        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler TerminalEventMsgOut;

        // emit service control status
        private void ListenersEmitServiceStatus(State state)
        {
            mutUITermList.WaitOne();
            foreach (var e in UITermList)
            {
                e.Invoke(() => e.OnStateUpdate(this, state));
            }
            mutUITermList.ReleaseMutex();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newScriptingInstance();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) => this.buttonDisconnect_Click(sender, e);

        bool UIColorScheme = false;

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIColorScheme = !UIColorScheme;
            darkModeToolStripMenuItem.Text = UIColorScheme ? "Light Mode" : "Dark Mode";
            ColourScheme.ChangeTheme(UIColorScheme ? ColourScheme.dark : themeData, this);
        }

        InputHistory<string> InputTextHistory = new InputHistory<string>();
        bool UIIgnoreTextCursorUpdate = false;

        private void richTextBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    InputTextHistory.Back();
                    richTextBoxInput.Text = InputTextHistory.GetCursor();
                    UIIgnoreTextCursorUpdate = true;
                    break;
                case Keys.Down:
                    InputTextHistory.Forward();
                    richTextBoxInput.Text = InputTextHistory.GetCursor();
                    UIIgnoreTextCursorUpdate = true;
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.ShowDialog();
        }

        Session GetSession()
        {
            return new Session();
        }
        private void buttonLoadPreset_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Path.GetFullPath("./configs");
            dlg.ShowDialog();
        }
        private void presetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Config|*.cfg";
            dlg.InitialDirectory = Path.GetFullPath("./configs");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Port p = Config.DeserializePort(File.ReadAllText(dlg.FileName));
                SetUIFromPortSettings(p);
            }
        }
        private void savePesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Config|*.cfg";
            dlg.InitialDirectory = Path.GetFullPath("./configs");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Port p = GetUISettingsToPort();
                File.WriteAllText(dlg.FileName, Config.SerializePort(p));
            }
        }
        void ChildTermFormClosed_Event(object sender, FormClosedEventArgs e)
        {
            ScriptTerminalForm inst = (ScriptTerminalForm)sender;
            //var term = UITermList.Find((e) => e.GUID = inst.GUID);
            //UITermList.RemoveAll((e) => e.GUID == inst.GUID);
            //activeToolStripMenuItem.DropDownItems.RemoveByKey(inst.GUID);
        }
        private void newScriptingInstance()
        {
            var vm = new VM();
            vm.RegisterFunctionInvokeListener("SETBUFFER", (vm, ctx) => { this.comboBoxReadMode.SelectedItem = (ReadMode)ctx[0]; return null; });
            vm.RegisterFunctionInvokeListener("SEND", (vm, ctx) =>
            {
                string str = (string)ctx[0];
                byte[] buf = Encoding.ASCII.GetBytes(str);
                queueMessageWrite.Add(new Message(Message.MessageCode.WRITE, buf, Message.SendAsEncoding.ASCII));
                return null;
            });
            vm.RegisterFunctionInvokeListener("CONNECT", (vm, ctx) =>
            {
                return Invoke(() =>
                {
                    var port = new Port();
                    port.PortName = (string)ctx[0];
                    port.BufferMode = ReadMode.RAW;
                    port.BaudRate = (int)ctx[2];
                    //TODO
                    port.WriteTimeout = (int)ctx[6];
                    port.ReadTimeout = (int)ctx[7];
                    SetUIFromPortSettings(port);
                    InitPortFromUI();
                    return vm.Ret(null);
                });
            });

            ScriptTerminalForm term = new ScriptTerminalForm();
            vm.RunAsThreaded();
            term.SetVM(vm);

            var uid = Guid.NewGuid().ToString();

            mutVmList.WaitOne();
            scripts[uid] = vm;
            mutVmList.ReleaseMutex();

            ToolStripMenuItem newChild = new ToolStripMenuItem();
            newChild.Text = $"Script Instance <{uid}>";
            newChild.Tag = uid;
            newChild.Name = uid;
            newChild.Click += terminalToolStripClick;
            activeToolStripMenuItem.DropDownItems.Add(newChild);

            //term.Register(ref queueMessageScriptInput, ref queueMessageWrite);
            term.FormClosed += new FormClosedEventHandler(ChildTermFormClosed_Event);
            UITermList.Add(term);
            term.Tag = uid;
            term.Show();
            term.OnStateUpdate(this, new State(WriteThreadRunning || ReadThreadRunning));
            ListenersEmitServiceStatus(state);
        }

        private void terminalToolStripClick(object? sender, EventArgs e)
        {
            string uId = (string)((ToolStripMenuItem)sender).Tag;
            ScriptTerminalForm form = UITermList.Find((e) => e.Tag == uId);

            if (form == null || form.IsDisposed)
            {
                ScriptTerminalForm term = new ScriptTerminalForm();

                VM value;
                scripts.TryGetValue(uId, out value);
                term.Show();

                // attach VM after showing window because otherwise it will complain the vm handle doesn't exist yet
                // when the callbacks are attached and then trying to immediately access window controls
                // which is valid
                term.SetVM(value);

                UITermList.RemoveAll((e) => e.GUID == uId);
                // overwrite terminal ref destroying the previous one
                UITermList.Add(term);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newScriptingInstance();
        }

        string consoleLinebuffer;
        private void richTextBoxConsole_TextChanged(object sender, EventArgs e)
        {
            //ShowCaret(this.richTextBoxConsole.Handle);

            // if not user input discard it
            if (!richTextBoxConsole.Modified)
            {
                // redraw linebuffer: 
                // the full buffer
                this.richTextBoxConsole.Text += consoleLinebuffer;
                return;
            }

            var text = this.richTextBoxConsole.Text;
            /*var diff = Math.Max(0, text.Length - consoleText.Length);
            if (diff > 0)
            {
                // we typed in this substring
                var buffer = text.Substring(consoleText.Length, diff);
                //consoleLinebuffer = buffer;
            }*/
        }

        private void richTextBoxConsole_KeyPress(object sender, KeyPressEventArgs e)
        {
            DrawCaret(this.richTextBoxConsole);
            if (this.richTextBoxConsole.SelectionStart != this.richTextBoxConsole.Text.Length)
            {
                e.Handled = true;
            }

            switch (e.KeyChar)
            {
                case '\b':
                    if (this.consoleLinebuffer.Length > 0)
                    {
                        this.consoleLinebuffer = this.consoleLinebuffer.Remove(this.consoleLinebuffer.Length - 1);
                    }
                    break;
                case '\r':
                    if (this.consoleLinebuffer.Length < 1) break;
                    Message.SendAsEncoding enc = (Message.SendAsEncoding)this.comboBoxSendAs.SelectedItem;
                    string data = consoleLinebuffer;
                    byte[] buf;
                    switch (enc)
                    {
                        case Message.SendAsEncoding.ASCII:
                            buf = Encoding.ASCII.GetBytes(data);
                            break;
                        case Message.SendAsEncoding.ASCII_utf8:
                            buf = Encoding.UTF8.GetBytes(data);
                            break;
                        case Message.SendAsEncoding.ASCII_UTF7:
                            buf = Encoding.UTF7.GetBytes(data);
                            break;
                        default:
                            MessageBox.Show("Bad SendAsEncoding combo box selection");
                            return;
                    }
                    queueMessageWrite.Add(new Message(data, buf, enc));
                    consoleLinebuffer = "";
                    break;
                default:
                    this.consoleLinebuffer += e.KeyChar;
                    break;
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            DrawCaret(richTextBoxConsole);
        }

        private void richTextBoxConsole_Enter(object sender, EventArgs e)
        {
            DrawCaret(richTextBoxConsole);
        }

        private void richTextBoxConsole_Leave(object sender, EventArgs e)
        {
            DestroyCaret();
        }
    }
}