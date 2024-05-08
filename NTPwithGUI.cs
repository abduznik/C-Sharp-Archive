using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CSharpGUI
{
    public class WinFormExample : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button settingsButton;

        // Program settings
        private string program1IP = "10.0.0.1";
        private string program1Subnet = "255.255.255.0";
        private string program1Gateway = "10.0.0.254";
        private string program1NtpIP = "time.windows.com";

        private string program2IP = "192.168.1.100";
        private string program2Subnet = "255.255.255.0";
        private string program2Gateway = "192.168.1.1";
        private string program2NtpIP = "time.windows.com";

        private string program3IP = "172.16.0.1";
        private string program3Subnet = "255.255.0.0";
        private string program3Gateway = "172.16.0.254";
        private string program3NtpIP = "time.windows.com";

        public WinFormExample()
        {
            DisplayGUI();
        }

        private void DisplayGUI()
        {
            this.Name = "WinForm Example";
            this.Text = "WinForm Example";
            this.Size = new Size(525, 150);
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
            settingsButton.Location = new Point(210, 70);
            settingsButton.Click += new EventHandler(SettingsButton_Click);

            this.Controls.Add(button1);
            this.Controls.Add(button2);
            this.Controls.Add(button3);
            this.Controls.Add(settingsButton);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Button 1 clicked.");
            UpdateRegistryIP(program1NtpIP);
            string command = "netsh interface ipv4 set address name=\"Ethernet\" static " + program1IP + " " + program1Subnet + " " + program1Gateway;
            ExecuteCommand(command);
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Button 2 clicked.");
            UpdateRegistryIP(program2NtpIP);
            string command = "netsh interface ipv4 set address name=\"Ethernet\" static " + program2IP + " " + program2Subnet + " " + program2Gateway;
            ExecuteCommand(command);
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Button 3 clicked.");
            UpdateRegistryIP(program3NtpIP);
            string command = "netsh interface ipv4 set address name=\"Ethernet\" static " + program3IP + " " + program3Subnet + " " + program3Gateway;
            ExecuteCommand(command);
            
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Settings button clicked.");
            using (var form = new CustomSettingsForm())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string ip = form.IP;
                    string subnet = form.Subnet;
                    string gateway = form.Gateway;
                    string ntpIP = form.NtpIP;

                    string command = "netsh interface ipv4 set address name=\"Ethernet\"";
                    if (!string.IsNullOrEmpty(ip))
                        command += " static " + ip;
                    if (!string.IsNullOrEmpty(subnet))
                        command += " " + subnet;
                    if (!string.IsNullOrEmpty(gateway))
                        command += " " + gateway;

                    ExecuteCommand(command);
                    UpdateRegistryIP(ntpIP);
                }
            }
        }

        private void ExecuteCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\WINDOWS\system32\cmd.exe",
                Verb = "runas",
                Arguments = "/C \"" + command + "\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = false
            };

            Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            Console.WriteLine(output);

            Environment.Exit(0);
        }

        private void UpdateRegistryIP(string ip)
        {
            Console.WriteLine("Updating registry IP to: " + ip);
            string keyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\DateTime\Servers";
            string valueName = "0";
            string valueData = ip;

            RegistryKey key = null;

            try
            {
                Console.WriteLine("Trying to open registry key...");
                key = Registry.LocalMachine.OpenSubKey(keyPath, true);
                if (key == null)
                {
                    Console.WriteLine("Key does not exist, creating...");
                    key = Registry.LocalMachine.CreateSubKey(keyPath);
                }

                Console.WriteLine("Setting registry value...");
                key.SetValue(valueName, valueData, RegistryValueKind.String);

                Console.WriteLine("Registry IP updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                }
            }
        }

        public static void Main(string[] args)
        {
            Application.Run(new WinFormExample());
        }
    }

    public class CustomSettingsForm : Form
    {
        private Label ipLabel;
        private TextBox ipTextBox;
        private Label subnetLabel;
        private TextBox subnetTextBox;
        private Label gatewayLabel;
        private TextBox gatewayTextBox;
        private Label ntpIPLabel;
        private TextBox ntpIPTextBox;
        private Button saveButton;

        public string IP { get { return ipTextBox.Text; } }
        public string Subnet { get { return subnetTextBox.Text; } }
        public string Gateway { get { return gatewayTextBox.Text; } }
        public string NtpIP { get { return ntpIPTextBox.Text; } }

        public CustomSettingsForm()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.Name = "Custom Settings Form";
            this.Text = "Custom Settings";
            this.Size = new Size(300, 220);
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

            ntpIPLabel = new Label();
            ntpIPLabel.Text = "NTP IP:";
            ntpIPLabel.Location = new Point(20, 110);

            ntpIPTextBox = new TextBox();
            ntpIPTextBox.Location = new Point(120, 110);
            ntpIPTextBox.Size = new Size(150, 20);

            saveButton = new Button();
            saveButton.Text = "Save";
            saveButton.Location = new Point(120, 140);
            saveButton.DialogResult = DialogResult.OK;

            this.Controls.Add(ipLabel);
            this.Controls.Add(ipTextBox);
            this.Controls.Add(subnetLabel);
            this.Controls.Add(subnetTextBox);
            this.Controls.Add(gatewayLabel);
            this.Controls.Add(gatewayTextBox);
            this.Controls.Add(ntpIPLabel);
            this.Controls.Add(ntpIPTextBox);
            this.Controls.Add(saveButton);
        }
    }
}
