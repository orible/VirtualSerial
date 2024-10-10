namespace VirtualSerial
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            comboBox1 = new ComboBox();
            buttonPortRefresh = new Button();
            richTextBoxOutputLog = new RichTextBox();
            richTextBoxInputLog = new RichTextBox();
            label3 = new Label();
            richTextBoxInputHex = new RichTextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            buttonInputSend = new Button();
            groupBox2 = new GroupBox();
            richTextBoxInput = new TextBox();
            buttonInputHexSend = new Button();
            label1 = new Label();
            button1 = new Button();
            buttonConvText = new Button();
            buttonConvHex = new Button();
            label15 = new Label();
            comboBoxSendAs = new ComboBox();
            groupBox3 = new GroupBox();
            splitContainer2 = new SplitContainer();
            labelProg2 = new Label();
            labelProg1 = new Label();
            labelProg3 = new Label();
            buttonConnect = new Button();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            buttonLoadPreset = new Button();
            buttonSavePreset = new Button();
            textBoxInputBaud = new TextBox();
            textBoxInputDataBits = new TextBox();
            comboBoxStopBits = new ComboBox();
            comboBoxParity = new ComboBox();
            comboBoxHandshake = new ComboBox();
            textBoxReadTimeout = new TextBox();
            textBoxWriteTimeout = new TextBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelActivity = new ToolStripStatusLabel();
            toolStripStatusLabelVersion = new ToolStripStatusLabel();
            buttonDisconnect = new Button();
            splitContainer1 = new SplitContainer();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            darkModeToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            presetToolStripMenuItem = new ToolStripMenuItem();
            savePesetToolStripMenuItem = new ToolStripMenuItem();
            loadSessionToolStripMenuItem = new ToolStripMenuItem();
            saveSessionToolStripMenuItem = new ToolStripMenuItem();
            scriptingToolStripMenuItem = new ToolStripMenuItem();
            activeToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            groupBox4 = new GroupBox();
            label16 = new Label();
            comboBoxReadMode = new ComboBox();
            button2 = new Button();
            label2 = new Label();
            textBoxStopCode = new TextBox();
            comboBoxDataBit = new ComboBox();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(84, 13);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(162, 23);
            comboBox1.TabIndex = 1;
            // 
            // buttonPortRefresh
            // 
            buttonPortRefresh.Location = new Point(253, 13);
            buttonPortRefresh.Name = "buttonPortRefresh";
            buttonPortRefresh.Size = new Size(75, 23);
            buttonPortRefresh.TabIndex = 2;
            buttonPortRefresh.Text = "Refresh";
            buttonPortRefresh.UseVisualStyleBackColor = true;
            buttonPortRefresh.Click += buttonPortRefresh_Click;
            // 
            // richTextBoxOutputLog
            // 
            richTextBoxOutputLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxOutputLog.BackColor = SystemColors.Window;
            richTextBoxOutputLog.BorderStyle = BorderStyle.None;
            richTextBoxOutputLog.ForeColor = Color.Black;
            richTextBoxOutputLog.Location = new Point(3, 20);
            richTextBoxOutputLog.Name = "richTextBoxOutputLog";
            richTextBoxOutputLog.ReadOnly = true;
            richTextBoxOutputLog.Size = new Size(393, 190);
            richTextBoxOutputLog.TabIndex = 3;
            richTextBoxOutputLog.Text = "";
            // 
            // richTextBoxInputLog
            // 
            richTextBoxInputLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxInputLog.BackColor = SystemColors.Window;
            richTextBoxInputLog.BorderStyle = BorderStyle.None;
            richTextBoxInputLog.ForeColor = Color.Black;
            richTextBoxInputLog.Location = new Point(6, 20);
            richTextBoxInputLog.Name = "richTextBoxInputLog";
            richTextBoxInputLog.ReadOnly = true;
            richTextBoxInputLog.Size = new Size(384, 190);
            richTextBoxInputLog.TabIndex = 9;
            richTextBoxInputLog.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 31);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 11;
            label3.Text = "ASCII (TX)";
            // 
            // richTextBoxInputHex
            // 
            richTextBoxInputHex.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxInputHex.BorderStyle = BorderStyle.None;
            richTextBoxInputHex.Location = new Point(406, 49);
            richTextBoxInputHex.Name = "richTextBoxInputHex";
            richTextBoxInputHex.Size = new Size(395, 81);
            richTextBoxInputHex.TabIndex = 12;
            richTextBoxInputHex.Text = "";
            richTextBoxInputHex.TextChanged += richTextBoxInputHex_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(748, 30);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 13;
            label4.Text = "HEX (TX)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 2);
            label5.Name = "label5";
            label5.Size = new Size(95, 15);
            label5.TabIndex = 14;
            label5.Text = "Sent (TX) History";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(4, 2);
            label6.Name = "label6";
            label6.Size = new Size(120, 15);
            label6.TabIndex = 15;
            label6.Text = "Received (RX) History";
            // 
            // buttonInputSend
            // 
            buttonInputSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonInputSend.Location = new Point(6, 136);
            buttonInputSend.Name = "buttonInputSend";
            buttonInputSend.Size = new Size(75, 23);
            buttonInputSend.TabIndex = 16;
            buttonInputSend.Text = "Send Text";
            buttonInputSend.UseVisualStyleBackColor = true;
            buttonInputSend.Click += buttonInputSend_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(richTextBoxInput);
            groupBox2.Controls.Add(buttonInputHexSend);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(buttonConvText);
            groupBox2.Controls.Add(buttonConvHex);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(comboBoxSendAs);
            groupBox2.Controls.Add(buttonInputSend);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(richTextBoxInputHex);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(0, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(807, 163);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "Console";
            // 
            // richTextBoxInput
            // 
            richTextBoxInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            richTextBoxInput.BorderStyle = BorderStyle.None;
            richTextBoxInput.Location = new Point(8, 49);
            richTextBoxInput.Multiline = true;
            richTextBoxInput.Name = "richTextBoxInput";
            richTextBoxInput.Size = new Size(392, 81);
            richTextBoxInput.TabIndex = 24;
            richTextBoxInput.TextChanged += richTextBoxInput_TextChanged;
            richTextBoxInput.KeyDown += richTextBoxInput_KeyDown;
            // 
            // buttonInputHexSend
            // 
            buttonInputHexSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonInputHexSend.Location = new Point(726, 133);
            buttonInputHexSend.Name = "buttonInputHexSend";
            buttonInputHexSend.Size = new Size(75, 23);
            buttonInputHexSend.TabIndex = 23;
            buttonInputHexSend.Text = "Send Hex";
            buttonInputHexSend.UseVisualStyleBackColor = true;
            buttonInputHexSend.Click += buttonInputHexSend_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(106, 140);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 22;
            label1.Text = "Insert:";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(149, 133);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(32, 25);
            button1.TabIndex = 21;
            button1.Text = "CR";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // buttonConvText
            // 
            buttonConvText.Location = new Point(281, 21);
            buttonConvText.Margin = new Padding(2);
            buttonConvText.Name = "buttonConvText";
            buttonConvText.Size = new Size(120, 23);
            buttonConvText.TabIndex = 20;
            buttonConvText.Text = "Convert to Hex→";
            buttonConvText.UseVisualStyleBackColor = true;
            buttonConvText.Click += buttonConvText_Click;
            // 
            // buttonConvHex
            // 
            buttonConvHex.Location = new Point(406, 21);
            buttonConvHex.Margin = new Padding(2);
            buttonConvHex.Name = "buttonConvHex";
            buttonConvHex.Size = new Size(120, 23);
            buttonConvHex.TabIndex = 19;
            buttonConvHex.Text = "← Convert to ASCII";
            buttonConvHex.UseVisualStyleBackColor = true;
            buttonConvHex.Click += buttonConvHex_Click;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label15.AutoSize = true;
            label15.Location = new Point(221, 140);
            label15.Name = "label15";
            label15.Size = new Size(105, 15);
            label15.TabIndex = 18;
            label15.Text = "Send As Encoding:";
            // 
            // comboBoxSendAs
            // 
            comboBoxSendAs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            comboBoxSendAs.FormattingEnabled = true;
            comboBoxSendAs.Location = new Point(332, 136);
            comboBoxSendAs.Name = "comboBoxSendAs";
            comboBoxSendAs.Size = new Size(69, 23);
            comboBoxSendAs.TabIndex = 17;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(splitContainer2);
            groupBox3.Location = new Point(0, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(807, 236);
            groupBox3.TabIndex = 18;
            groupBox3.TabStop = false;
            groupBox3.Text = "I/O History";
            // 
            // splitContainer2
            // 
            splitContainer2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer2.Location = new Point(6, 17);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(label5);
            splitContainer2.Panel1.Controls.Add(richTextBoxInputLog);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(richTextBoxOutputLog);
            splitContainer2.Panel2.Controls.Add(labelProg2);
            splitContainer2.Panel2.Controls.Add(labelProg1);
            splitContainer2.Panel2.Controls.Add(labelProg3);
            splitContainer2.Panel2.Controls.Add(label6);
            splitContainer2.Size = new Size(795, 213);
            splitContainer2.SplitterDistance = 392;
            splitContainer2.TabIndex = 19;
            // 
            // labelProg2
            // 
            labelProg2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelProg2.AutoSize = true;
            labelProg2.Location = new Point(303, 2);
            labelProg2.Name = "labelProg2";
            labelProg2.Size = new Size(27, 15);
            labelProg2.TabIndex = 17;
            labelProg2.Text = "text";
            // 
            // labelProg1
            // 
            labelProg1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelProg1.AutoSize = true;
            labelProg1.Location = new Point(336, 2);
            labelProg1.Name = "labelProg1";
            labelProg1.Size = new Size(27, 15);
            labelProg1.TabIndex = 16;
            labelProg1.Text = "text";
            // 
            // labelProg3
            // 
            labelProg3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelProg3.AutoSize = true;
            labelProg3.Location = new Point(369, 2);
            labelProg3.Name = "labelProg3";
            labelProg3.Size = new Size(27, 15);
            labelProg3.TabIndex = 18;
            labelProg3.Text = "text";
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(647, 99);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(75, 23);
            buttonConnect.TabIndex = 23;
            buttonConnect.Text = "Connect";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 46);
            label7.Name = "label7";
            label7.Size = new Size(37, 15);
            label7.TabIndex = 24;
            label7.Text = "Baud:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(5, 76);
            label8.Name = "label8";
            label8.Size = new Size(40, 15);
            label8.TabIndex = 25;
            label8.Text = "Parity:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(191, 75);
            label9.Name = "label9";
            label9.Size = new Size(51, 15);
            label9.TabIndex = 26;
            label9.Text = "Data Bit:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(191, 47);
            label10.Name = "label10";
            label10.Size = new Size(56, 15);
            label10.TabIndex = 27;
            label10.Text = "Stop Bits:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(5, 103);
            label11.Name = "label11";
            label11.Size = new Size(69, 15);
            label11.TabIndex = 28;
            label11.Text = "Handshake:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(5, 18);
            label12.Name = "label12";
            label12.Size = new Size(37, 15);
            label12.TabIndex = 30;
            label12.Text = "Ports:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(360, 48);
            label13.Name = "label13";
            label13.Size = new Size(110, 15);
            label13.TabIndex = 31;
            label13.Text = "Read Timeout (ms):";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(360, 75);
            label14.Name = "label14";
            label14.Size = new Size(112, 15);
            label14.TabIndex = 32;
            label14.Text = "Write Timeout (ms):";
            // 
            // buttonLoadPreset
            // 
            buttonLoadPreset.Location = new Point(619, 11);
            buttonLoadPreset.Name = "buttonLoadPreset";
            buttonLoadPreset.Size = new Size(103, 23);
            buttonLoadPreset.TabIndex = 35;
            buttonLoadPreset.Text = "Load Preset";
            buttonLoadPreset.UseVisualStyleBackColor = true;
            buttonLoadPreset.Visible = false;
            buttonLoadPreset.Click += buttonLoadPreset_Click;
            // 
            // buttonSavePreset
            // 
            buttonSavePreset.Location = new Point(727, 12);
            buttonSavePreset.Name = "buttonSavePreset";
            buttonSavePreset.Size = new Size(75, 23);
            buttonSavePreset.TabIndex = 36;
            buttonSavePreset.Text = "Save Preset";
            buttonSavePreset.UseVisualStyleBackColor = true;
            buttonSavePreset.Visible = false;
            buttonSavePreset.Click += button4_Click;
            // 
            // textBoxInputBaud
            // 
            textBoxInputBaud.Location = new Point(84, 43);
            textBoxInputBaud.Name = "textBoxInputBaud";
            textBoxInputBaud.Size = new Size(100, 23);
            textBoxInputBaud.TabIndex = 37;
            // 
            // textBoxInputDataBits
            // 
            textBoxInputDataBits.Location = new Point(253, 100);
            textBoxInputDataBits.Name = "textBoxInputDataBits";
            textBoxInputDataBits.Size = new Size(100, 23);
            textBoxInputDataBits.TabIndex = 41;
            textBoxInputDataBits.Visible = false;
            // 
            // comboBoxStopBits
            // 
            comboBoxStopBits.FormattingEnabled = true;
            comboBoxStopBits.Location = new Point(253, 44);
            comboBoxStopBits.Name = "comboBoxStopBits";
            comboBoxStopBits.Size = new Size(101, 23);
            comboBoxStopBits.TabIndex = 42;
            // 
            // comboBoxParity
            // 
            comboBoxParity.FormattingEnabled = true;
            comboBoxParity.Location = new Point(84, 72);
            comboBoxParity.Name = "comboBoxParity";
            comboBoxParity.Size = new Size(100, 23);
            comboBoxParity.TabIndex = 43;
            // 
            // comboBoxHandshake
            // 
            comboBoxHandshake.FormattingEnabled = true;
            comboBoxHandshake.Location = new Point(84, 101);
            comboBoxHandshake.Name = "comboBoxHandshake";
            comboBoxHandshake.Size = new Size(100, 23);
            comboBoxHandshake.TabIndex = 44;
            // 
            // textBoxReadTimeout
            // 
            textBoxReadTimeout.Location = new Point(476, 45);
            textBoxReadTimeout.Name = "textBoxReadTimeout";
            textBoxReadTimeout.Size = new Size(100, 23);
            textBoxReadTimeout.TabIndex = 45;
            textBoxReadTimeout.Text = "100";
            // 
            // textBoxWriteTimeout
            // 
            textBoxWriteTimeout.Location = new Point(476, 74);
            textBoxWriteTimeout.Name = "textBoxWriteTimeout";
            textBoxWriteTimeout.Size = new Size(100, 23);
            textBoxWriteTimeout.TabIndex = 46;
            textBoxWriteTimeout.Text = "100";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelActivity, toolStripStatusLabelVersion });
            statusStrip1.Location = new Point(0, 590);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(826, 22);
            statusStrip1.TabIndex = 47;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelActivity
            // 
            toolStripStatusLabelActivity.Name = "toolStripStatusLabelActivity";
            toolStripStatusLabelActivity.Size = new Size(118, 17);
            toolStripStatusLabelActivity.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabelVersion
            // 
            toolStripStatusLabelVersion.Name = "toolStripStatusLabelVersion";
            toolStripStatusLabelVersion.Size = new Size(150, 17);
            toolStripStatusLabelVersion.Text = "toolStripStatusLabelVersion";
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Enabled = false;
            buttonDisconnect.Location = new Point(727, 99);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(75, 23);
            buttonDisconnect.TabIndex = 48;
            buttonDisconnect.Text = "Disconnect";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(8, 160);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox2);
            splitContainer1.Size = new Size(807, 430);
            splitContainer1.SplitterDistance = 243;
            splitContainer1.TabIndex = 49;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem, settingsToolStripMenuItem, loadToolStripMenuItem, scriptingToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(826, 24);
            menuStrip1.TabIndex = 50;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 22);
            fileToolStripMenuItem.Text = "File";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { darkModeToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 22);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // darkModeToolStripMenuItem
            // 
            darkModeToolStripMenuItem.Name = "darkModeToolStripMenuItem";
            darkModeToolStripMenuItem.Size = new Size(132, 22);
            darkModeToolStripMenuItem.Text = "Dark Mode";
            darkModeToolStripMenuItem.Click += darkModeToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Checked = true;
            loadToolStripMenuItem.CheckState = CheckState.Checked;
            loadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { presetToolStripMenuItem, savePesetToolStripMenuItem, loadSessionToolStripMenuItem, saveSessionToolStripMenuItem });
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(58, 22);
            loadToolStripMenuItem.Text = "Session";
            // 
            // presetToolStripMenuItem
            // 
            presetToolStripMenuItem.Name = "presetToolStripMenuItem";
            presetToolStripMenuItem.Size = new Size(142, 22);
            presetToolStripMenuItem.Text = "Load Preset";
            presetToolStripMenuItem.Click += presetToolStripMenuItem_Click;
            // 
            // savePesetToolStripMenuItem
            // 
            savePesetToolStripMenuItem.Name = "savePesetToolStripMenuItem";
            savePesetToolStripMenuItem.Size = new Size(142, 22);
            savePesetToolStripMenuItem.Text = "Save Preset";
            savePesetToolStripMenuItem.Click += savePesetToolStripMenuItem_Click;
            // 
            // loadSessionToolStripMenuItem
            // 
            loadSessionToolStripMenuItem.Name = "loadSessionToolStripMenuItem";
            loadSessionToolStripMenuItem.Size = new Size(142, 22);
            loadSessionToolStripMenuItem.Text = "Load Session";
            // 
            // saveSessionToolStripMenuItem
            // 
            saveSessionToolStripMenuItem.Name = "saveSessionToolStripMenuItem";
            saveSessionToolStripMenuItem.Size = new Size(142, 22);
            saveSessionToolStripMenuItem.Text = "Save Session";
            // 
            // scriptingToolStripMenuItem
            // 
            scriptingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { activeToolStripMenuItem, newToolStripMenuItem });
            scriptingToolStripMenuItem.Name = "scriptingToolStripMenuItem";
            scriptingToolStripMenuItem.Size = new Size(66, 22);
            scriptingToolStripMenuItem.Text = "Scripting";
            // 
            // activeToolStripMenuItem
            // 
            activeToolStripMenuItem.Name = "activeToolStripMenuItem";
            activeToolStripMenuItem.Size = new Size(107, 22);
            activeToolStripMenuItem.Text = "Active";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Size = new Size(107, 22);
            newToolStripMenuItem.Text = "New";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(comboBoxReadMode);
            groupBox4.Controls.Add(button2);
            groupBox4.Controls.Add(label2);
            groupBox4.Controls.Add(textBoxStopCode);
            groupBox4.Controls.Add(comboBoxDataBit);
            groupBox4.Controls.Add(buttonPortRefresh);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(buttonDisconnect);
            groupBox4.Controls.Add(comboBoxStopBits);
            groupBox4.Controls.Add(textBoxInputBaud);
            groupBox4.Controls.Add(comboBox1);
            groupBox4.Controls.Add(buttonConnect);
            groupBox4.Controls.Add(buttonLoadPreset);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(buttonSavePreset);
            groupBox4.Controls.Add(textBoxInputDataBits);
            groupBox4.Controls.Add(comboBoxParity);
            groupBox4.Controls.Add(comboBoxHandshake);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(textBoxReadTimeout);
            groupBox4.Controls.Add(textBoxWriteTimeout);
            groupBox4.Location = new Point(8, 22);
            groupBox4.Margin = new Padding(2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(2);
            groupBox4.Size = new Size(807, 133);
            groupBox4.TabIndex = 19;
            groupBox4.TabStop = false;
            groupBox4.Text = "Settings";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(360, 21);
            label16.Name = "label16";
            label16.Size = new Size(71, 15);
            label16.TabIndex = 54;
            label16.Text = "Read Buffer:";
            // 
            // comboBoxReadMode
            // 
            comboBoxReadMode.FormattingEnabled = true;
            comboBoxReadMode.Location = new Point(476, 15);
            comboBoxReadMode.Name = "comboBoxReadMode";
            comboBoxReadMode.Size = new Size(100, 23);
            comboBoxReadMode.TabIndex = 53;
            // 
            // button2
            // 
            button2.Location = new Point(726, 68);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 52;
            button2.Text = "Scripter";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(360, 103);
            label2.Name = "label2";
            label2.Size = new Size(92, 15);
            label2.TabIndex = 51;
            label2.Text = "Stopcode (Hex):";
            // 
            // textBoxStopCode
            // 
            textBoxStopCode.Location = new Point(476, 103);
            textBoxStopCode.Name = "textBoxStopCode";
            textBoxStopCode.Size = new Size(100, 23);
            textBoxStopCode.TabIndex = 50;
            textBoxStopCode.Text = "0D";
            // 
            // comboBoxDataBit
            // 
            comboBoxDataBit.FormattingEnabled = true;
            comboBoxDataBit.Location = new Point(253, 72);
            comboBoxDataBit.Margin = new Padding(2);
            comboBoxDataBit.Name = "comboBoxDataBit";
            comboBoxDataBit.Size = new Size(100, 23);
            comboBoxDataBit.TabIndex = 49;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(826, 612);
            Controls.Add(groupBox4);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(842, 612);
            Name = "Form1";
            Text = "Quick Virtual Serial (TX/RX)";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox comboBox1;
        private Button buttonInputSend;
        private RichTextBox richTextBoxOutputLog;
        private Button buttonPortRefresh;
        private RichTextBox richTextBoxInputLog;
        private Label label3;
        private RichTextBox richTextBoxInputHex;
        private Label label4;
        private Label label5;
        private Label label6;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Button buttonConnect;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Button buttonLoadPreset;
        private Button buttonSavePreset;
        private TextBox textBoxInputBaud;
        private TextBox textBoxInputDataBits;
        private ComboBox comboBoxStopBits;
        private ComboBox comboBoxParity;
        private ComboBox comboBoxHandshake;
        private TextBox textBoxReadTimeout;
        private TextBox textBoxWriteTimeout;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabelActivity;
        private Label label15;
        private ComboBox comboBoxSendAs;
        private Button buttonDisconnect;
        private SplitContainer splitContainer1;
        private Button buttonConvText;
        private Button buttonConvHex;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabelVersion;
        private GroupBox groupBox4;
        private ComboBox comboBoxDataBit;
        private Button button1;
        private Label label1;
        private Button buttonInputHexSend;
        private TextBox richTextBoxInput;
        private Label label2;
        private TextBox textBoxStopCode;
        private Label labelProg1;
        private Label labelProg2;
        private Label labelProg3;
        private Button button2;
        private ComboBox comboBoxReadMode;
        private Label label16;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem darkModeToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem presetToolStripMenuItem;
        private ToolStripMenuItem savePesetToolStripMenuItem;
        private ToolStripMenuItem loadSessionToolStripMenuItem;
        private ToolStripMenuItem saveSessionToolStripMenuItem;
        private ToolStripMenuItem scriptingToolStripMenuItem;
        private ToolStripMenuItem activeToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private SplitContainer splitContainer2;
    }
}