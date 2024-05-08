using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CSharpGUI {
    public class WinFormExample : Form {

        private Button button1;
        private Button button2;
        private Button button3;
        private Button settingsButton;

        public WinFormExample() {
            DisplayGUI();
        }

        private void DisplayGUI() {
            this.Name = "WinForm Example";
            this.Text = "WinForm Example";
            this.Size = new Size(500, 150);
            this.StartPosition = FormStartPosition.CenterScreen;

            button1 = new Button();
            button1.Name = "button1";
            button1.Text = "Program 1 (10.0.0.1)";
            button1.Size = new Size(150, 30);
            button1.Location = new Point(20, 20);
            button1.Click += new EventHandler(Button1_Click);

            button2 = new Button();
            button2.Name = "button2";
            button2.Text = "Program 2 (192.168.1.100)";
            button2.Size = new Size(150, 30);
            button2.Location = new Point(180, 20);
            button2.Click += new EventHandler(Button2_Click);

            button3 = new Button();
            button3.Name = "button3";
            button3.Text = "Program 3 (172.16.0.1)";
            button3.Size = new Size(150, 30);
            button3.Location = new Point(340, 20);
            button3.Click += new EventHandler(Button3_Click);

            settingsButton = new Button();
            settingsButton.Name = "settingsButton";
            settingsButton.Text = "Custom Settings";
            settingsButton.Size = new Size(100, 30);
            settingsButton.Location = new Point(20, 70);
            settingsButton.Click += new EventHandler(SettingsButton_Click);

            this.Controls.Add(button1);
            this.Controls.Add(button2);
            this.Controls.Add(button3);
            this.Controls.Add(settingsButton);
        }

        private void Button1_Click(object sender, EventArgs e) {
            ExecuteCommand("netsh interface ipv4 set address name=\"Ethernet\" static 10.0.0.1 255.255.255.0 10.0.0.254");
        }

        private void Button2_Click(object sender, EventArgs e) {
            ExecuteCommand("netsh interface ipv4 set address name=\"Ethernet\" static 192.168.1.100 255.255.255.0 192.168.1.1");
        }

        private void Button3_Click(object sender, EventArgs e) {
            ExecuteCommand("netsh interface ipv4 set address name=\"Ethernet\" static 172.16.0.1 255.255.0.0 172.16.0.254");
        }

        private void SettingsButton_Click(object sender, EventArgs e) {
            using (var form = new CustomSettingsForm()) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {
                    string ip = form.IP;
                    string subnet = form.Subnet;
                    string gateway = form.Gateway;

                    string command = "netsh interface ipv4 set address name=\"Ethernet\"";
                    if (!string.IsNullOrEmpty(ip))
                        command += " static " + ip;
                    if (!string.IsNullOrEmpty(subnet))
                        command += " " + subnet;
                    if (!string.IsNullOrEmpty(gateway))
                        command += " " + gateway;

                    ExecuteCommand(command);
                }
            }
        }

        private void ExecuteCommand(string command) {
            ProcessStartInfo startInfo = new ProcessStartInfo {
                FileName = @"C:\WINDOWS\system32\cmd.exe",
                Verb = "runas",
                Arguments = "/C \"" + command + "\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            Process process = new Process {
                StartInfo = startInfo
            };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            Console.WriteLine(output);

            Environment.Exit(0);
        }

        public static void Main(String[] args) {
            Application.Run(new WinFormExample());
        }
    }

    public class CustomSettingsForm : Form {

        private Label ipLabel;
        private TextBox ipTextBox;
        private Label subnetLabel;
        private TextBox subnetTextBox;
        private Label gatewayLabel;
        private TextBox gatewayTextBox;
        private Button saveButton;

        public string IP { get { return ipTextBox.Text; } }
        public string Subnet { get { return subnetTextBox.Text; } }
        public string Gateway { get { return gatewayTextBox.Text; } }

        public CustomSettingsForm() {
            InitializeForm();
        }

        private void InitializeForm() {
            this.Name = "Custom Settings Form";
            this.Text = "Custom Settings";
            this.Size = new Size(300, 180);
            this.StartPosition = FormStartPosition.CenterScreen;

            ipLabel = new Label();
            ipLabel.Text = "IP Address:";
            ipLabel.Location = new Point(20, 20);

            ipTextBox = new TextBox();
            ipTextBox.Location = new Point(120, 20);
            ipTextBox.Size = new Size(150, 20);

            subnetLabel = new Label();
            subnetLabel.Text = "Subnet Mask:";
            subnetLabel.Location = new Point(20, 50);

            subnetTextBox = new TextBox();
            subnetTextBox.Location = new Point(120, 50);
            subnetTextBox.Size = new Size(150, 20);

            gatewayLabel = new Label();
            gatewayLabel.Text = "Default Gateway:";
            gatewayLabel.Location = new Point(20, 80);

            gatewayTextBox = new TextBox();
            gatewayTextBox.Location = new Point(120, 80);
            gatewayTextBox.Size = new Size(150, 20);

            saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Location = new Point(120, 120);
            saveButton.DialogResult = DialogResult.OK;

            this.Controls.Add(ipLabel);
            this.Controls.Add(ipTextBox);
            this.Controls.Add(subnetLabel);
            this.Controls.Add(subnetTextBox);
            this.Controls.Add(gatewayLabel);
            this.Controls.Add(gatewayTextBox);
            this.Controls.Add(saveButton);
        }
    }
}
