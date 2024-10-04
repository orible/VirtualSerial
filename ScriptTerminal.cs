﻿using System;
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
using System.Windows;
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
        VM vm;

        public ScriptTerminal()
        {
            InitializeComponent();
        }
        private void ScriptTerminal_Load(object sender, EventArgs e)
        {
            stopToolStripMenuItem.Enabled = false;
            runToolStripMenuItem.Enabled = false;
        }
        public void SetVM(VM vmRef)
        {
            this.vm = vmRef;
            this.vm.RegisterFunctionInvokeListener("disconnect", (vm, dat) => Log($"Disconnect: {dat}"));
            this.vm.RegisterFunctionInvokeListener("connect", (vm, dat) => Log($"Connect: {dat}"));
            this.vm.RegisterFunctionInvokeListener("_func", (vm, dat) => Log($"Func: {dat}"));
            this.vm.RegisterFunctionInvokeListener("_log", (vm, dat) => Log((string)dat[0]));
            this.vm.RegisterFunctionInvokeListener("_start", (vm, dat) =>
            {
                this.Invoke(() =>
                {
                    this.stopToolStripMenuItem.Enabled = true;
                    this.runToolStripMenuItem.Enabled = !this.stopToolStripMenuItem.Enabled;
                    Log($"Running: {this.stopToolStripMenuItem.Enabled}");
                });
                return null;
            });
            this.vm.RegisterFunctionInvokeListener("_stop", (vm, dat) =>
            {
                this.Invoke(() =>
                {
                    this.stopToolStripMenuItem.Enabled = false;
                    this.runToolStripMenuItem.Enabled = !this.stopToolStripMenuItem.Enabled;
                    Log($"Running: {this.stopToolStripMenuItem.Enabled}");
                });
                return null;
            });
        }                                                        

        void Error(string s)
        {
            Log("ERROR: " + s);
        }

        bool FlagScriptChanged = false;
        bool FlagScriptSaved = false;
        bool FlagScriptIsAttached = false;
        string loadedScript;

        State state;
        public void OnStateUpdate(object sender, State _stat)
        {
            state = _stat;
            FlagScriptIsAttached = _stat.Connected;
            runToolStripMenuItem.Enabled = _stat.Connected;
        }

        object Log(string s)
        {
            Invoke(() =>
            {
                this.richTextBoxLog.AppendText(s + "\n");
                this.richTextBoxLog.ScrollToCaret();
            });
            return null;
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.runToolStripMenuItem.Enabled = true;
            vm.Invoke((vm, args) =>
            {
                vm.SetScript(this.richTextBoxScriptInput.Text);
                vm.Compile();
                return null;
            });
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

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log($"[vm] running script...\n");
            vm.Invoke((vm, args) =>
            {
                vm.SetScript(this.richTextBoxScriptInput.Text);
                vm.Compile();
                var ret = vm.RunAsThreaded();
                return null;
            });
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.InitialDirectory = Path.Join(System.Windows.Forms.Application.StartupPath, "lua");
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
                this.Text = "LScripter | {no script loaded}";

            this.Text = $"LScripter | {loadedScript} {(!FlagScriptSaved ? "[Unsaved]" : "")}";
        }

        private void richTextBoxScriptInput_TextChanged(object sender, EventArgs e)
        {
            UpdateTitle();
            FlagScriptChanged = true;
        }

        private void attachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // classic flipflop :)
            FlagScriptIsAttached = !FlagScriptIsAttached;
            attachToolStripMenuItem.Text = state.Connected ? "Detach" : "Attach";
        }

        private void ScriptTerminal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                saveToolStripMenuItem_Click(sender, e);
            }
        }

        private async void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vm.Stop();
        }
    }
}
