using System;
using System.Drawing;
using System.Windows.Forms;
using PBUCONClient;

namespace WinFormPBUCON
{
    class ServerTab
    {
        private TabPage tab;
        private ServerManager manager;
        private PBUCONServer server;
        public TabPage Tab
        {
            get { return tab; }
        }
        private RichTextBox rtb;
        private Button sendButton;
        private TextBox commandBox;

        public ServerTab(ServerManager manager, PBUCONServer server)
        {
            this.manager = manager;
            this.server = server;

            // Create console box
            rtb = new RichTextBox();
            rtb.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            rtb.BackColor = SystemColors.ControlLight;
            rtb.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            rtb.ForeColor = SystemColors.WindowText;
            rtb.Location = new Point(7, 7);
            rtb.ReadOnly = true;
            rtb.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtb.Size = new Size(839, 354);
            rtb.TabIndex = 0;

            // Create send button
            sendButton = new Button();
            sendButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            sendButton.Font = new Font("Segoe UI", 9F);
            sendButton.Location = new Point(771, 367);
            sendButton.Size = new Size(75, 23);
            sendButton.TabIndex = 2;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += new EventHandler(sendButton_Click);

            // Create command bar
            commandBox = new TextBox();
            commandBox.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left) | AnchorStyles.Right)));
            commandBox.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            commandBox.Location = new Point(7, 367);
            commandBox.Size = new Size(758, 23);
            commandBox.TabIndex = 1;
            commandBox.KeyPress += new KeyPressEventHandler(commandBox_KeyPress);

            // Create tab page that holds everything
            tab = new TabPage();
            tab.ImageIndex = 0;
            tab.Location = new Point(4, 23);
            tab.Name = server.Name;
            tab.Padding = new Padding(3);
            tab.Size = new Size(852, 396);
            tab.TabIndex = 0;
            tab.Text = server.Name;
            tab.UseVisualStyleBackColor = true;

            tab.Controls.Add(rtb);
            tab.Controls.Add(commandBox);
            tab.Controls.Add(sendButton);

            // Add events to the server
            server.NewMessage += NewMessage;
            server.ServerChallengeChanged += ChallengeChange;

            // Show the client challenge
            rtb.AppendText("Client challenge: " + server.ClientChallenge.ToString("X8") + "\n");
        }

        private void commandBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // 13 is enter. TODO: 13 is ugly.
            {
                RunCommandbox();
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            RunCommandbox();
        }

        private void RunCommandbox()
        {
            string command = this.commandBox.Text;
            if (command.Length == 0)
            {
                return;
            }

            this.rtb.AppendText(command + "\n", Color.Green);
            ScrollRTB();

            this.manager.SendCommand(server, command);
            this.commandBox.Text = "";
        }

        // TODO: More hackery
        private void ScrollRTB()
        {
            if (manager.AutoScroll)
            {
                rtb.SelectionStart = rtb.Text.Length - 1; // -1 for "\n"
                rtb.ScrollToCaret();
            }
        }

        private void NewMessage(Object sender, UCONMessageEventArgs e)
        {
            PBUCONServer server = sender as PBUCONServer;

            if (rtb.InvokeRequired)
            {
                rtb.Invoke(new MethodInvoker(
                    delegate {
                        rtb.AppendText(e.Message + "\n");
                        ScrollRTB();
                    }
                ));
            }
        }

        private void ChallengeChange(Object sender, ServerChallengeChangedEventArgs e)
        {
            PBUCONServer server = sender as PBUCONServer;
            if (rtb.InvokeRequired)
            {
                rtb.Invoke(new MethodInvoker(
                    delegate {
                        tab.ImageIndex = 1; // Tick the tab. TODO: Still needs improvement if there's a disconnection (which is near impossible to detect)
                        rtb.AppendText("Server challenge now " + e.NewServerChallenge.ToString("X8") + ", was " + e.OldServerChallenge.ToString("X8") + "\n");
                        ScrollRTB();
                    }
                ));
            }
        }
    }
}
