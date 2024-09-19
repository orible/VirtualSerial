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
using System.Windows.Forms.Integration;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Serialization.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static VirtualSerial.Form1;

namespace VirtualSerial
{
    public partial class ScriptTerminal : Form
    {
        public string GUID = Guid.NewGuid().ToString();

        public ScriptTerminal()
        {
            InitializeComponent();
        }
        private void ScriptTerminal_Load(object sender, EventArgs e)
        {
            //var edit = new ICSharpCode.AvalonEdit.TextEditor();

            //ElementHost host = new ElementHost();
            //host.Dock = System.Windows.Forms.DockStyle.Fill;
            //host.Child = edit;
            //this.groupBox1.Controls.Add(host);

        }
        bool flagIsAttached = false;

        string loadedScript;
        VM virtualMachine = new VM();
        BlockingCollection<Message> writeOut;

        public void Register(
            ref BlockingCollection<Message> _in1,
            ref BlockingCollection<Message> writeOut)
        {
            this.writeOut = writeOut;
        }

        int vmFuncPrint(string str)
        {
            Log(str);
            return 1;
        }

        int vmFuncSend(string str)
        {
            byte[] buf = Encoding.ASCII.GetBytes(str);
            this.writeOut.Add(new Message(Message.MessageCode.NULL, buf, Message.SendAsEncoding.ASCII));
            return 0;
        }

        public void OnMessage(object sender, MessageEventArgs args)
        {
            if (args.Data == null || args.Data.Buf == null) return;
            string data = Encoding.ASCII.GetString(args.Data.Buf);
            virtualMachine.CallOnMessage(this, args);
        }

        State state;
        public void OnStateUpdate(object sender, State _stat)
        {
            state = _stat;
            flagIsAttached = _stat.Connected;
            runToolStripMenuItem.Enabled = _stat.Connected;
        }

        void ShowError(string s)
        {
            Log("ERROR: " + s);
        }

        void Log(string s)
        {
            this.richTextBoxLog.AppendText(s);
            this.richTextBoxLog.ScrollToCaret();
        }

        bool FlagScriptChanged = false;
        bool FlagScriptSaved = false;

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                virtualMachine.SetScript(this.richTextBoxScriptInput.Text);
                virtualMachine.Compile();
            }
            catch (Exception ex)
            {
                ShowError($"fatal: {ex.Message}\n");
            }
        }

        void TryRecompile()
        {
            if (!FlagScriptChanged) return;
            virtualMachine.SetScript(this.richTextBoxScriptInput.Text);
            virtualMachine.Compile();
        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Script|*.lua";
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                FlagScriptSaved = true;
                File.WriteAllText(dlg.FileName, this.richTextBoxScriptInput.Text);
            }
        }

        private void rUNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log($"[vm] running script...\n");
            try
            {
                TryRecompile();
                virtualMachine.Run();
                Log("\n");
            }
            catch (Exception ex)
            {
                ShowError($"fatal: {ex.Message}\n");
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
            if (loadedScript == null || loadedScript.Length < 1)
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
            // send test message
            this.writeOut.Add(new Message("__TEST__", new byte[] { 49, 49 }, Message.SendAsEncoding.ASCII));
        }

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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
