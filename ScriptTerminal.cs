using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MoonSharp.Interpreter;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static VirtualSerial.Form1;

namespace VirtualSerial
{
    public partial class ScriptTerminal : Form
    {
        public ScriptTerminal()
        {
            InitializeComponent();
        }

        private void ScriptTerminal_Load(object sender, EventArgs e)
        {

        }

        public string GUID = Guid.NewGuid().ToString();

        Script _script;
        DynValue _fnMain;

        string loadedScript;

        void LoadScript()
        {
            Log($"[vm] compiling script...\n");
            Script script = new Script();
            Script.DefaultOptions.DebugPrint = s => Log(s);
            //Script.DefaultOptions.Stderr = s => richTextBoxLog.AppendText(s);
            DynValue fnMain = script.LoadString(GetScript());
            script.Globals["send"] = (Func<string, int>)lfuncSend;
            script.Globals["connect"] = (Func<int, string, float, int, int, int, int>)lfuncConnect;
            script.Globals["disconnect"] = (Func<string, int>)lfuncSend;
            script.Globals["print"] = (Func<string, int>)lfuncPrint;
            script.Globals["debug"] = (Func<string, int>)lfuncPrint;
            _fnMain = fnMain;
            _script = script;
        }

        BlockingCollection<Message> _in;
        BlockingCollection<Message> _out;
        public void Register(
            ref BlockingCollection<Message> _in1,
            ref BlockingCollection<Message> writeOut)
        {
            _in = _in1;
            _out = writeOut;
        }
        int lfuncConnect(
            int baud,
            string parity,
            float stop,
            int data,
            int readtimeout,
            int writetimeout)
        {
            return -1;
        }
        int lfuncDisconnect()
        {
            return -1;
        }
        int lfuncPrint(string str)
        {
            Log(str);
            return 1;
        }
        int lfuncSend(string str)
        {
            byte[] buf = Encoding.ASCII.GetBytes(str);
            this._out.Add(new Message(Message.MessageCode.NULL, buf, Message.SendAsEncoding.ASCII));
            return 0;
        }

        State state;
        public void OnStateUpdate(object sender, State _stat)
        {
            state = _stat;
            flagIsAttached = _stat.Connected;
            runToolStripMenuItem.Enabled = _stat.Connected;
        }

        public void OnMessage(object sender, MessageEventArgs args)
        {
            if (_script == null) return;
            if (args.Data == null || args.Data.Buf == null) return;
            string data = Encoding.ASCII.GetString(args.Data.Buf);
            try
            {
                var fn = _script.Globals["OnReceive"];
                if (fn == null) return;
                _script.Call(fn, data);
            }
            catch (Exception e)
            {
                Error(e.Message);
            }
        }

        bool FlagScriptChanged = false;
        bool FlagScriptSaved = false;

        void Error(string s)
        {
            Log("ERROR: " + s);
        }
        void Log(string s)
        {
            this.richTextBoxLog.AppendText(s);
            this.richTextBoxLog.ScrollToCaret();
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadScript();
            }
            catch (Exception ex)
            {
                Error($"fatal: {ex.Message}\n");
            }
        }
        void TryRecompile()
        {
            if (!FlagScriptChanged) return;
            LoadScript();
        }
        string GetScript()
        {
            return this.richTextBoxScriptInput.Text;
        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                FlagScriptSaved = true;
                File.WriteAllText(dlg.FileName, this.richTextBoxScriptInput.Text);
                //using (Stream stream = dlg.OpenFile())
                //using (StreamWriter sw = new StreamWriter(stream))
                //{
                //    sw.Write(this.richTextBox1.Text);
                //    sw.Flush();
                //}
            }
        }

        private void rUNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log($"[vm] running script...\n");
            try
            {
                TryRecompile();
                _fnMain.Function.Call();
                Log("\n");
            }
            catch (Exception ex)
            {
                Error($"fatal: {ex.Message}\n");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = Path.Join(Application.StartupPath, "lua");
            if (dlg.ShowDialog() != DialogResult.OK) return;
            Stream file = dlg.OpenFile();
            using (StreamReader streamReader = new StreamReader(file))
            {
                loadedScript = dlg.FileName;
                this.Text = "LScripter | " + loadedScript;
                this.richTextBoxScriptInput.Text = streamReader.ReadToEnd();
            }
            FlagScriptChanged = true;
        }
        void UpdateTitle()
        {
            if (loadedScript.Length < 1)
                this.Text = "LScripter";

            this.Text = $"LScripter | {loadedScript} {(!FlagScriptSaved ? "[Unsaved]" : "")}";
        }

        private void richTextBoxScriptInput_TextChanged(object sender, EventArgs e)
        {
            if (!FlagScriptChanged)
            {
                UpdateTitle();
            }
            FlagScriptChanged = true;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._out.Add(new Message("__TEST__", new byte[] { 49, 49 }, Message.SendAsEncoding.ASCII));
        }

        bool flagIsAttached = false;
        private void attachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // classic flipflop :)
            flagIsAttached = !flagIsAttached;
            attachToolStripMenuItem.Text = state.Connected ? "Detach" : "Attach";
        }

        private void ScriptTerminal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
               saveToolStripMenuItem_Click(sender, e);
            }
        }
    }
}
