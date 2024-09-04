using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO.Ports;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VirtualSerial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RefreshPorts();
            comboBoxStopBits.DataSource = new (String, StopBits)[] {
                ("None", StopBits.None),
                ("1", StopBits.One),
                ("2", StopBits.Two),
                ("1.5", StopBits.OnePointFive) };
            comboBoxParity.DataSource = new (String, Parity)[] {
                ("1 (Odd)", Parity.Odd),
                ("NONE", Parity.None),
                ("Mark", Parity.Mark),
                ("0 (Space)", Parity.Space),
                ("2 (Odd)", Parity.Even) };
            comboBoxDataBit.DataSource = new (String, DataBits)[] {
                ("5", DataBits.Five),
                ("6", DataBits.Six),
                ("7", DataBits.Seven),
                ("8", DataBits.Eight)
            };
            comboBoxDataBit.DisplayMember = "Item1";
            comboBoxHandshake.DataSource = new Handshake[] { Handshake.None, Handshake.XOnXOff, Handshake.RequestToSend, Handshake.RequestToSendXOnXOff };
            comboBoxSendAs.DataSource = new Message.SendAsEncoding[] { Message.SendAsEncoding.ASCII, Message.SendAsEncoding.ASCII_utf8, Message.SendAsEncoding.ASCII_UTF7 };
            toolStripStatusLabelActivity.Text = "NOT CONNECTED";
            toolStripStatusLabelVersion.Text = AssemblyInfo.GetGitHash();
        }

        private byte[] GetStopCode()
        {
            byte[] buf = ParseBadHexStringLiteral(this.textBoxStopCode.Text, 2);
            return buf.Take(buf.Length - 1).ToArray();
        }
        private void Form1_Load(object sender, EventArgs e) { }

        BindingList<String> ports;

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

        void _WriteOpenPort()
        {
            richTextBoxInputLog.Invoke(() => { richTextBoxInputLog.AppendText("[Thread] Start\n"); });
            WriteThreadRunning = true;
            bool _stop = false;
            while (!_stop)
            {
                Message msg = queueMessageWrite.Take();
                if (msg.Code == Message.MessageCode.STOP) break;
                try
                {
                    _serialPort.Write(msg.Buf, 0, msg.Buf.Length);
                    string timecode = GetTimecode(DateTime.Now);
                    richTextBoxInputLog.Invoke((Message msg, string timecode) =>
                    {
                        //writeMessages.Add(msg.UserRepresentation);
                        richTextBoxInputLog.AppendText($">> {System.Text.Encoding.ASCII.GetString(msg.Buf)}\n");
                        richTextBoxInputLog.AppendText($">> {string.Join(", ", msg.Buf)}\n");
                    }, msg, timecode);
                }
                catch (Exception e)
                {
                    switch (e)
                    {
                        case TimeoutException:
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
        void _ReadOpenPort()
        {
            int spinnerActivity = 0;
            int spinnerMsgBuf = 0;

            ReadThreadRunning = true;
            MemoryStream mem = new MemoryStream();
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

                labelProg3.Invoke(() =>
                {
                    if (++spinnerActivity >= spinner.Length)
                    {
                        spinnerActivity = 0;
                    }
                    labelProg3.Text = spinner[spinnerActivity];
                });

                try
                {
                    //byte[] buf;
                    //if (ReadBufferLinestop(_serialPort, ChunkSize, ref mem, stopcode, out buf) < 1)
                    //{
                    //    continue;
                    //}

                    labelProg2.Invoke(() => labelProg2.Text = "Checking port buffer");
                    byte[] buf = new byte[1024];
                    int read = _serialPort.Read(buf, 0, 1024);
                    if (read < 1) continue;

                    string message = Encoding.ASCII.GetString(buf, 0, read);
                    //string message = _serialPort.ReadLine();
                    //ParseHexString(message, 2);
                    string timecode = GetTimecode(DateTime.Now);
                    this.richTextBoxOutputLog.Invoke((string data, string timecode) =>
                    {
                        if (++spinnerMsgBuf >= spinner.Length)
                        {
                            spinnerMsgBuf = 0;
                        }
                        labelProg1.Text = spinner[spinnerMsgBuf];
                        readMessages.Add(data);
                        richTextBoxOutputLog.AppendText(data);
                        richTextBoxOutputLog.ScrollToCaret();
                    }, message, timecode);
                    ListenersEmitMessage(new Message(buf));
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

            labelProg2.Invoke(() => labelProg2.Text = "Shutting down...");

            richTextBoxOutputLog.Invoke(() => { richTextBoxOutputLog.AppendText("\n[Thread] Shutdown signal...\n"); });

            _serialPort.Close();

            while (true)
            {
                Message itm;
                if (!queueMessageRead.TryTake(out itm))
                    break;
            }

            this.BeginInvoke(() => { this.buttonConnect.Enabled = true; this.buttonDisconnect.Enabled = false; });

            richTextBoxOutputLog.Invoke(() => { richTextBoxOutputLog.AppendText("[Thread] Stopped\n"); labelProg2.Text = "Not connected"; });
            ReadThreadRunning = false;

            ListenersEmitServiceStatus(new State(false));
        }
        // end ownership of _ReadOpenPort Thread

        Regex isNumber = new Regex(@"^\d$");

        int GetPortSettings(ref SerialPort port)
        {
            //if (!isNumber.IsMatch(this.textBoxInputBaud.Text))
            //    throw new System.ArgumentException("Input Baud bad input");

            int baud, parity, _databits, readtimeout, writetimeout;
            if (!int.TryParse(this.textBoxInputBaud.Text, out baud))
                throw new System.ArgumentException("Input Baud bad input");

            //int.TryParse(this.textBoxInputParity.Text, out parity);
            //if (!int.TryParse(this.textBoxInputDataBits.Text, out databits))
            //    throw new System.ArgumentException("Input databits bad input");

            if (!int.TryParse(this.textBoxReadTimeout.Text, out readtimeout))
                throw new System.ArgumentException("Input read timeout bad input");

            if (!int.TryParse(this.textBoxWriteTimeout.Text, out writetimeout))
                throw new System.ArgumentException("Input write timeout bad input");
            //int.TryParse(this.textBoxInputStopBits.Text, out stopbits);

            (String s, Parity parity) combo = (((String s, Parity parity))comboBoxParity.SelectedItem);
            (String s, StopBits stopBits) stops = (((String s, StopBits stopBits))comboBoxStopBits.SelectedItem);
            (String s, DataBits bits) databits = (((String s, DataBits bits))comboBoxDataBit.SelectedItem);

            _serialPort.BaudRate = baud;
            _serialPort.Parity = combo.parity;
            _serialPort.PortName = (string)this.comboBox1.SelectedValue;
            _serialPort.Parity = combo.parity;
            _serialPort.DataBits = (int)databits.bits;
            _serialPort.StopBits = stops.stopBits;
            _serialPort.ReadTimeout = readtimeout;
            _serialPort.WriteTimeout = writetimeout;
            //_serialPort.Encoding = System.Text.Encoding;
            //System.Text.Encoding.GetEncoding(1252);
            return 1;
        }
        void InitPort()
        {
            toolStripStatusLabelActivity.Text = "STARTING";
            Thread readThread = new Thread(_ReadOpenPort);
            Thread writeThread = new Thread(_WriteOpenPort);

            // eat message buffer
            while (queueMessageRead.TryTake(out _)) { }
            while (queueMessageWrite.TryTake(out _)) { }

            // open new port
            _serialPort = new SerialPort();

            // apply UI settings
            GetPortSettings(ref _serialPort);
            _serialPort.Handshake = Handshake.None;

            // open threads and start port
            _serialPort.Open();

            readThread.Start();
            writeThread.Start();
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

        private static readonly Regex matchBase16 = new Regex("[^0-9ABCDEF]");
        private static readonly Regex matchWhitespace = new Regex(@"\s+");

        byte[] ParseHexString(string str, int n)
        {
            byte[] buf = new byte[str.Length * n];
            int shunt = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ((i % n) == 0 && i < str.Length - 1)
                {
                    string substr = str.Substring(i, Math.Min(n, str.Length));
                    string padleft = substr.PadLeft(2, '0');
                    byte byt = Convert.ToByte(padleft, 16); ;
                    buf[shunt++] = byt;
                }
            }
            return buf;
        }

        // clean and parse a user input hex string
        // It's probably complete junk so transform it to upper
        // strip bad characters
        byte[] ParseBadHexStringLiteral(string str, int n)
        {
            str = str.ToUpper();
            str = matchBase16.Replace(str, "");
            str = matchWhitespace.Replace(str, "");

            byte[] buf = new byte[(str.Length / 2) + 1];
            int shunt = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ((i % n) == 0 && i < str.Length - 1)
                {
                    string substr = str.Substring(i, Math.Min(n, str.Length));
                    string padleft = substr.PadLeft(2, '0');
                    byte byt = Convert.ToByte(padleft, 16); ;
                    buf[shunt++] = byt;
                }
            }
            return buf;
        }

        bool skipTextBoxUpdate = false;
        private void richTextBoxInputHex_TextChanged(object sender, EventArgs e) { }

        private void richTextBoxInput_TextChanged(object sender, EventArgs e)
        {
            // convert ascii to text
            if (skipTextBoxUpdate) { skipTextBoxUpdate = false; return; }
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
            byte[] buffer = ParseBadHexStringLiteral(str, 2);

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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void buttonInputHexSend_Click(object sender, EventArgs e)
        {
            if (richTextBoxInputHex.Text == "") return;
            byte[] buf = ParseBadHexStringLiteral(richTextBoxInputHex.Text, 2);
            queueMessageWrite.Add(new Message(richTextBoxInputHex.Text, buf, Message.SendAsEncoding.ASCII));
            richTextBoxInput.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.ShowDialog();
        }

        private void buttonLoadPreset_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBoxInput.AppendText("\r");
        }

        // UI table of child script windows
        List<ScriptTerminal> UITermList = new List<ScriptTerminal>();

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

        // emit messages to child windows on their UI thread :)
        private void ListenersEmitMessage(Message msg)
        {
            foreach(var e in UITermList)
            {
                e.Invoke(() => e.OnMessage(this, new MessageEventArgs(msg)));
            }
        }

        // emit service control status
        private void ListenersEmitServiceStatus(State _stat)
        {
            foreach (var e in UITermList)
            {
                e.Invoke(() => e.OnStateUpdate(this, _stat));
            }
        }

        void ChildTermFormClosed(object sender, FormClosedEventArgs e)
        {
            ScriptTerminal inst = (ScriptTerminal)sender;
            UITermList.RemoveAll((e) => e.GUID == inst.GUID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ScriptTerminal term = new ScriptTerminal();
            term.Register(ref queueMessageScriptInput, ref queueMessageWrite);
            term.FormClosed += new FormClosedEventHandler(ChildTermFormClosed);
            UITermList.Add(term);
            term.Show();
            term.OnStateUpdate(this, new State(WriteThreadRunning || ReadThreadRunning));
            //ListenersEmitServiceStatus(new State(WriteThreadRunning || ReadThreadRunning));
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
    }
}