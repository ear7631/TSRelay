﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using System.IO;
using System.Reflection;
using System.Xml;
using TS3QueryLib.Core;
using TS3QueryLib.Core.Server;
using TS3QueryLib.Core.CommandHandling;
using TS3QueryLib.Core.Common.Responses;
using TS3QueryLib.Core.Server.Entities;


namespace ACT_Plugin
{
    public class TSRelayPlugin : UserControl, IActPluginV1
    {

        #region Plugin Constants (Fill in your own here)
        private LinkedList<string> checkedEvents;
        #endregion
        private TextBox queryIDTextBox;
        private TextBox queryPasswordTextBox;
        private TextBox hostTextBox;
        private TextBox portTextBox;
        private Label portLabel;
        private Label hostLabel;
        private Label queryIDLabel;
        private Label queryPasswordLabel;
        private Button connectButton;
        private Button saveButton;
        private OpenFileDialog eventFileDialog;
        private TextBox fileNameLabel;
        private Button browseButton;


        #region Designer Created Code (Avoid editing)
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.queryIDTextBox = new System.Windows.Forms.TextBox();
            this.queryPasswordTextBox = new System.Windows.Forms.TextBox();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.hostLabel = new System.Windows.Forms.Label();
            this.queryIDLabel = new System.Windows.Forms.Label();
            this.queryPasswordLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.eventFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileNameLabel = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // queryIDTextBox
            // 
            this.queryIDTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.queryIDTextBox.Location = new System.Drawing.Point(129, 95);
            this.queryIDTextBox.Name = "queryIDTextBox";
            this.queryIDTextBox.Size = new System.Drawing.Size(100, 20);
            this.queryIDTextBox.TabIndex = 0;
            this.queryIDTextBox.Text = "Query Login ID";
            // 
            // queryPasswordTextBox
            // 
            this.queryPasswordTextBox.Location = new System.Drawing.Point(129, 121);
            this.queryPasswordTextBox.Name = "queryPasswordTextBox";
            this.queryPasswordTextBox.Size = new System.Drawing.Size(100, 20);
            this.queryPasswordTextBox.TabIndex = 1;
            this.queryPasswordTextBox.Text = "Password";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(129, 43);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(100, 20);
            this.hostTextBox.TabIndex = 2;
            this.hostTextBox.Text = "TS Host";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(129, 69);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(100, 20);
            this.portTextBox.TabIndex = 3;
            this.portTextBox.Text = "TS Query Port";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(3, 72);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(116, 13);
            this.portLabel.TabIndex = 4;
            this.portLabel.Text = "Teamspeak Query Port";
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(3, 46);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(114, 13);
            this.hostLabel.TabIndex = 5;
            this.hostLabel.Text = "Teamspeak Hostname";
            // 
            // queryIDLabel
            // 
            this.queryIDLabel.AutoSize = true;
            this.queryIDLabel.Location = new System.Drawing.Point(3, 98);
            this.queryIDLabel.Name = "queryIDLabel";
            this.queryIDLabel.Size = new System.Drawing.Size(108, 13);
            this.queryIDLabel.TabIndex = 6;
            this.queryIDLabel.Text = "Teamspeak Query ID";
            // 
            // queryPasswordLabel
            // 
            this.queryPasswordLabel.AutoSize = true;
            this.queryPasswordLabel.Location = new System.Drawing.Point(3, 124);
            this.queryPasswordLabel.Name = "queryPasswordLabel";
            this.queryPasswordLabel.Size = new System.Drawing.Size(120, 13);
            this.queryPasswordLabel.TabIndex = 7;
            this.queryPasswordLabel.Text = "Teamspeak Query Pass";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(129, 156);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(100, 23);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "Connect Bot";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(6, 156);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(111, 23);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save Settings";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // eventFileDialog
            // 
            this.eventFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.eventFileDialog_FileOk);
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Location = new System.Drawing.Point(257, 43);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.ReadOnly = true;
            this.fileNameLabel.Size = new System.Drawing.Size(218, 20);
            this.fileNameLabel.TabIndex = 10;
            this.fileNameLabel.Text = "No events file selected";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(481, 41);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(73, 23);
            this.browseButton.TabIndex = 11;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // TSRelayPlugin
            // 
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.queryPasswordLabel);
            this.Controls.Add(this.queryIDLabel);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.queryPasswordTextBox);
            this.Controls.Add(this.queryIDTextBox);
            this.Name = "TSRelayPlugin";
            this.Size = new System.Drawing.Size(557, 194);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        #endregion


        private QueryRunner qr;
        private uint honeypot_cid;
        private uint bot_clid;
        private ushort queryPort;
        private string loginID;
        private string loginPassword;
        private string queryHost;

        private SettingsSerializer xmlSettings; // Used for saving the settings of the plugin

        private Label lblStatus; // reference to ACT's status label


        public TSRelayPlugin()
        {
            InitializeComponent();
        }

        private string settingsFile = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\TSRelay.config.xml");


        #region IActPluginV1 Members
        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            this.lblStatus = pluginStatusText;

            checkedEvents = new LinkedList<string>();
            // This is the GUI related stuff
            pluginScreenSpace.Controls.Add(this);
            this.Dock = DockStyle.Fill;


            // Settings stuff
            xmlSettings = new SettingsSerializer(this);
            LoadSettings();


            // Register the log read event
            ActGlobals.oFormActMain.OnLogLineRead += new LogLineEventDelegate(oFormActMain_LogLineEvent);

            lblStatus.Text = "Plugin Started";
        }


        public void DeInitPlugin()
        {
            // Unsubscribe from any events you listen to when exiting!
            ActGlobals.oFormActMain.AfterCombatAction -= oFormActMain_AfterCombatAction;
            ActGlobals.oFormActMain.OnLogLineRead -= oFormActMain_LogLineEvent;

            lblStatus.Text = "Plugin Exited";
        }
        #endregion


        /**
         * This is the callback for checking lines against custom events.
         **/
        void oFormActMain_LogLineEvent(bool isImport, LogLineEventArgs logInfo)
        {
            string entry = logInfo.logLine;
            foreach (string check in checkedEvents)
            {
                if (entry.Contains(check))
                {
                    qr.SendTextMessage(MessageTarget.Channel, this.honeypot_cid, "speak:" + check);
                    break;
                }
            }

        }

        void oFormActMain_AfterCombatAction(bool isImport, CombatActionEventArgs actionInfo)
        {
            throw new NotImplementedException();
        }



        /**
         * This is where we save the settings.
         **/
        void SaveSettings()
        {
            FileStream fs = new FileStream(settingsFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            XmlTextWriter xWriter = new XmlTextWriter(fs, Encoding.UTF8);
            xWriter.Formatting = Formatting.Indented;
            xWriter.Indentation = 1;
            xWriter.IndentChar = '\t';
            xWriter.WriteStartDocument(true);
            xWriter.WriteStartElement("Config");	// <Config>
            xWriter.WriteStartElement("SettingsSerializer");	// <Config><SettingsSerializer>
            xmlSettings.ExportToXml(xWriter);	// Fill the SettingsSerializer XML
            xWriter.WriteEndElement();	// </SettingsSerializer>
            xWriter.WriteEndElement();	// </Config>
            xWriter.WriteEndDocument();	// Tie up loose ends (shouldn't be any)
            xWriter.Flush();	// Flush the file buffer to disk
            xWriter.Close();

            lblStatus.Text = "Plugin settings saved.";
        }



        void LoadSettings()
        {
            xmlSettings.AddControlSetting(this.hostTextBox.Name, hostTextBox);
            xmlSettings.AddControlSetting(this.portTextBox.Name, portTextBox);
            xmlSettings.AddControlSetting(this.queryIDTextBox.Name, queryIDTextBox);
            xmlSettings.AddControlSetting(this.queryPasswordTextBox.Name, queryPasswordTextBox);
            xmlSettings.AddControlSetting(this.fileNameLabel.Name, fileNameLabel);


            if (File.Exists(settingsFile))
            {
                FileStream fs = new FileStream(settingsFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlTextReader xReader = new XmlTextReader(fs);

                try
                {
                    while (xReader.Read())
                    {
                        if (xReader.NodeType == XmlNodeType.Element)
                        {
                            if (xReader.LocalName == "SettingsSerializer")
                            {
                                xmlSettings.ImportFromXml(xReader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //lblStatus.Text = "Error loading settings: " + ex.Message;
                }
                xReader.Close();
            }
        }


        private void connectButton_Click(object sender, EventArgs e)
        {
            this.loginID = this.queryIDTextBox.Text;
            this.loginPassword = this.queryPasswordTextBox.Text;
            this.queryHost = this.hostTextBox.Text;
            this.queryPort = Convert.ToUInt16(this.portTextBox.Text);

            // Connect to the TS Server
            SyncTcpDispatcher dispatcher = new SyncTcpDispatcher(queryHost, queryPort);
            qr = new QueryRunner(dispatcher);
            dispatcher.Connect();

            // Select the default Virtual Server (1)
            qr.SelectVirtualServerById(1);

            // Log in as the bot
            qr.Login(loginID, loginPassword);

            // Get our client ID (we don't know if this changes or not!)
            var response = qr.SendCommand(new Command("clientlist", new string[] { }));
            string[] entries = response.Split('|');
            foreach (string entry in entries)
            {
                if (entry.Contains("ParseBot") && entry.Contains("client_type=1"))
                {
                    int start = entry.IndexOf("clid=") + 5;
                    int end = entry.IndexOf(" ", start);
                    bot_clid = Convert.ToUInt32(entry.Substring(start, end - start));
                }
            }


            // Get our channel ID (we don't know if this changes or not!)
            response = qr.SendCommand(new Command("channellist", new string[] { }));
            entries = response.Split('|');
            foreach (string entry in entries)
            {
                if (entry.Contains("Bot\\sHoneypot"))
                {
                    int start = entry.IndexOf("cid=") + 4;
                    int end = entry.IndexOf(" ", start);
                    honeypot_cid = Convert.ToUInt32(entry.Substring(start, end - start));
                }
            }

            // Move to the honeypot channel
            qr.MoveClient(bot_clid, honeypot_cid);

            // Send a message to the honeypot channel
            qr.SendTextMessage(MessageTarget.Channel, honeypot_cid, "ParseBot is connected to the honeypot.");

            lblStatus.Text = "Plugin connected to TS query service.";
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void eventFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            this.fileNameLabel.Text = eventFileDialog.FileName;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {

            DialogResult result = eventFileDialog.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                this.checkedEvents = new LinkedList<string>();
                StreamReader reader = null;
                reader = new StreamReader(eventFileDialog.OpenFile());
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    this.checkedEvents.AddLast(line);
                }
            }
        }

        
    }
}
