namespace VirtualSerial
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            splitContainer3 = new SplitContainer();
            richTextBoxInput = new TextBox();
            label1 = new Label();
            button1 = new Button();
            buttonConvText = new Button();
            label15 = new Label();
            comboBoxSendAs = new ComboBox();
            buttonConvHex = new Button();
            buttonInputHexSend = new Button();
            groupBox3 = new GroupBox();
            splitContainer2 = new SplitContainer();
            labelProg2 = new Label();
            labelSpinnerRead = new Label();
            labelSpinnerPoll = new Label();
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
            comboBoxStopBits = new ComboBox();
            comboBoxParity = new ComboBox();
            comboBoxHandshake = new ComboBox();
            textBoxReadTimeout = new TextBox();
            textBoxWriteTimeout = new TextBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabelActivity = new ToolStripStatusLabel();
            toolStripStatusLabelVersion = new ToolStripStatusLabel();
            toolStripStatusLabelReadWrite = new ToolStripStatusLabel();
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
            label17 = new Label();
            comboBoxRecvEncoding = new ComboBox();
            label16 = new Label();
            comboBoxReadMode = new ComboBox();
            button2 = new Button();
            label2 = new Label();
            textBoxStopCode = new TextBox();
            comboBoxDataBit = new ComboBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            richTextBoxConsole = new TextBox();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
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
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
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
            richTextBoxOutputLog.Size = new Size(409, 185);
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
            richTextBoxInputLog.Size = new Size(395, 185);
            richTextBoxInputLog.TabIndex = 9;
            richTextBoxInputLog.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 7);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 11;
            label3.Text = "ASCII (TX)";
            // 
            // richTextBoxInputHex
            // 
            richTextBoxInputHex.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxInputHex.BorderStyle = BorderStyle.None;
            richTextBoxInputHex.Location = new Point(3, 30);
            richTextBoxInputHex.Name = "richTextBoxInputHex";
            richTextBoxInputHex.Size = new Size(374, 200);
            richTextBoxInputHex.TabIndex = 12;
            richTextBoxInputHex.Text = "";
            richTextBoxInputHex.TextChanged += richTextBoxInputHex_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(83, 6);
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
            buttonInputSend.Location = new Point(3, 244);
            buttonInputSend.Name = "buttonInputSend";
            buttonInputSend.Size = new Size(75, 23);
            buttonInputSend.TabIndex = 16;
            buttonInputSend.Text = "Send Text";
            buttonInputSend.UseVisualStyleBackColor = true;
            buttonInputSend.Click += buttonInputSend_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(splitContainer3);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(834, 289);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "Console";
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(3, 19);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(richTextBoxInput);
            splitContainer3.Panel1.Controls.Add(label1);
            splitContainer3.Panel1.Controls.Add(button1);
            splitContainer3.Panel1.Controls.Add(buttonConvText);
            splitContainer3.Panel1.Controls.Add(label15);
            splitContainer3.Panel1.Controls.Add(comboBoxSendAs);
            splitContainer3.Panel1.Controls.Add(buttonInputSend);
            splitContainer3.Panel1.Controls.Add(label3);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(buttonConvHex);
            splitContainer3.Panel2.Controls.Add(label4);
            splitContainer3.Panel2.Controls.Add(buttonInputHexSend);
            splitContainer3.Panel2.Controls.Add(richTextBoxInputHex);
            splitContainer3.Size = new Size(828, 267);
            splitContainer3.SplitterDistance = 441;
            splitContainer3.TabIndex = 25;
            // 
            // richTextBoxInput
            // 
            richTextBoxInput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxInput.BorderStyle = BorderStyle.None;
            richTextBoxInput.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            richTextBoxInput.Location = new Point(1, 30);
            richTextBoxInput.Multiline = true;
            richTextBoxInput.Name = "richTextBoxInput";
            richTextBoxInput.Size = new Size(438, 200);
            richTextBoxInput.TabIndex = 24;
            richTextBoxInput.TextChanged += richTextBoxInput_TextChanged;
            richTextBoxInput.KeyDown += richTextBoxInput_KeyDown;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(132, 244);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 22;
            label1.Text = "Insert:";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(175, 238);
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
            buttonConvText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonConvText.Location = new Point(323, 2);
            buttonConvText.Margin = new Padding(2);
            buttonConvText.Name = "buttonConvText";
            buttonConvText.Size = new Size(116, 23);
            buttonConvText.TabIndex = 20;
            buttonConvText.Text = "Convert to Hex→";
            buttonConvText.UseVisualStyleBackColor = true;
            buttonConvText.Visible = false;
            buttonConvText.Click += buttonConvText_Click;
            // 
            // label15
            // 
            label15.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label15.AutoSize = true;
            label15.Location = new Point(257, 244);
            label15.Name = "label15";
            label15.Size = new Size(60, 15);
            label15.TabIndex = 18;
            label15.Text = "Encoding:";
            // 
            // comboBoxSendAs
            // 
            comboBoxSendAs.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            comboBoxSendAs.FormattingEnabled = true;
            comboBoxSendAs.Location = new Point(323, 240);
            comboBoxSendAs.Name = "comboBoxSendAs";
            comboBoxSendAs.Size = new Size(69, 23);
            comboBoxSendAs.TabIndex = 17;
            // 
            // buttonConvHex
            // 
            buttonConvHex.Location = new Point(2, 2);
            buttonConvHex.Margin = new Padding(2);
            buttonConvHex.Name = "buttonConvHex";
            buttonConvHex.Size = new Size(76, 23);
            buttonConvHex.TabIndex = 19;
            buttonConvHex.Text = "← Convert to ASCII";
            buttonConvHex.UseVisualStyleBackColor = true;
            buttonConvHex.Click += buttonConvHex_Click;
            // 
            // buttonInputHexSend
            // 
            buttonInputHexSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonInputHexSend.Location = new Point(3, 240);
            buttonInputHexSend.Name = "buttonInputHexSend";
            buttonInputHexSend.Size = new Size(75, 23);
            buttonInputHexSend.TabIndex = 23;
            buttonInputHexSend.Text = "Send Hex";
            buttonInputHexSend.UseVisualStyleBackColor = true;
            buttonInputHexSend.Click += buttonInputHexSend_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(splitContainer2);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(834, 231);
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
            splitContainer2.Panel2.Controls.Add(labelSpinnerRead);
            splitContainer2.Panel2.Controls.Add(labelSpinnerPoll);
            splitContainer2.Panel2.Controls.Add(label6);
            splitContainer2.Size = new Size(822, 208);
            splitContainer2.SplitterDistance = 403;
            splitContainer2.TabIndex = 19;
            // 
            // labelProg2
            // 
            labelProg2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelProg2.AutoSize = true;
            labelProg2.Location = new Point(270, 2);
            labelProg2.Name = "labelProg2";
            labelProg2.Size = new Size(27, 15);
            labelProg2.TabIndex = 17;
            labelProg2.Text = "text";
            // 
            // labelSpinnerRead
            // 
            labelSpinnerRead.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelSpinnerRead.AutoSize = true;
            labelSpinnerRead.Location = new Point(326, 2);
            labelSpinnerRead.Name = "labelSpinnerRead";
            labelSpinnerRead.Size = new Size(27, 15);
            labelSpinnerRead.TabIndex = 16;
            labelSpinnerRead.Text = "text";
            // 
            // labelSpinnerPoll
            // 
            labelSpinnerPoll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelSpinnerPoll.AutoSize = true;
            labelSpinnerPoll.Location = new Point(385, 2);
            labelSpinnerPoll.Name = "labelSpinnerPoll";
            labelSpinnerPoll.Size = new Size(27, 15);
            labelSpinnerPoll.TabIndex = 18;
            labelSpinnerPoll.Text = "text";
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
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelActivity, toolStripStatusLabelVersion, toolStripStatusLabelReadWrite });
            statusStrip1.Location = new Point(0, 729);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(868, 22);
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
            // toolStripStatusLabelReadWrite
            // 
            toolStripStatusLabelReadWrite.Name = "toolStripStatusLabelReadWrite";
            toolStripStatusLabelReadWrite.Size = new Size(19, 17);
            toolStripStatusLabelReadWrite.Text = "IO";
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
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
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
            splitContainer1.Size = new Size(834, 524);
            splitContainer1.SplitterDistance = 231;
            splitContainer1.TabIndex = 49;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem, settingsToolStripMenuItem, loadToolStripMenuItem, scriptingToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(868, 24);
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
            groupBox4.Controls.Add(label17);
            groupBox4.Controls.Add(comboBoxRecvEncoding);
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
            groupBox4.Controls.Add(comboBoxParity);
            groupBox4.Controls.Add(comboBoxHandshake);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(textBoxReadTimeout);
            groupBox4.Controls.Add(textBoxWriteTimeout);
            groupBox4.Location = new Point(8, 26);
            groupBox4.Margin = new Padding(2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(2);
            groupBox4.Size = new Size(849, 133);
            groupBox4.TabIndex = 19;
            groupBox4.TabStop = false;
            groupBox4.Text = "Settings";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(191, 107);
            label17.Name = "label17";
            label17.Size = new Size(60, 15);
            label17.TabIndex = 56;
            label17.Text = "Encoding:";
            // 
            // comboBoxRecvEncoding
            // 
            comboBoxRecvEncoding.FormattingEnabled = true;
            comboBoxRecvEncoding.Location = new Point(253, 103);
            comboBoxRecvEncoding.Margin = new Padding(2);
            comboBoxRecvEncoding.Name = "comboBoxRecvEncoding";
            comboBoxRecvEncoding.Size = new Size(100, 23);
            comboBoxRecvEncoding.TabIndex = 55;
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
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(8, 164);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(848, 558);
            tabControl1.TabIndex = 51;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(splitContainer1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(840, 530);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "I/O Console";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(richTextBoxConsole);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(840, 530);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "TTY Console";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBoxConsole
            // 
            richTextBoxConsole.BorderStyle = BorderStyle.None;
            richTextBoxConsole.Dock = DockStyle.Fill;
            richTextBoxConsole.Location = new Point(3, 3);
            richTextBoxConsole.Multiline = true;
            richTextBoxConsole.Name = "richTextBoxConsole";
            richTextBoxConsole.Size = new Size(834, 524);
            richTextBoxConsole.TabIndex = 0;
            richTextBoxConsole.TextChanged += richTextBoxConsole_TextChanged;
            richTextBoxConsole.KeyPress += richTextBoxConsole_KeyPress;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(868, 751);
            Controls.Add(tabControl1);
            Controls.Add(groupBox4);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(842, 612);
            Name = "MainForm";
            Text = "Quick Virtual Serial (TX/RX)";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            groupBox2.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
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
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
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
        private Label labelSpinnerRead;
        private Label labelProg2;
        private Label labelSpinnerPoll;
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
        private ToolStripStatusLabel toolStripStatusLabelReadWrite;
        private SplitContainer splitContainer3;
        private ContextMenuStrip contextMenuStrip1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox richTextBoxConsole;
        private Label label17;
        private ComboBox comboBoxRecvEncoding;
    }
}