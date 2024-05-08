using System;
using System.Drawing;
using System.Windows.Forms;

namespace CSharpGUI {
    public class WinFormExample : Form {

        private Button button1;
        private Button button2;
        private Button button3;

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
            button1.Text = "Click Me 1!";
            button1.Size = new Size(100, 30);
            button1.Location = new Point(20, 20);
            button1.Click += new EventHandler(Button1_Click);

            button2 = new Button();
            button2.Name = "button2";
            button2.Text = "Click Me 2!";
            button2.Size = new Size(100, 30);
            button2.Location = new Point(130, 20);
            button2.Click += new EventHandler(Button2_Click);

            button3 = new Button();
            button3.Name = "button3";
            button3.Text = "Click Me 3!";
            button3.Size = new Size(100, 30);
            button3.Location = new Point(240, 20);
            button3.Click += new EventHandler(Button3_Click);

            this.Controls.Add(button1);
            this.Controls.Add(button2);
            this.Controls.Add(button3);
        }

        private void Button1_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 1 clicked!");
        }

        private void Button2_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 2 clicked!");
        }

        private void Button3_Click(object sender, EventArgs e) {
            MessageBox.Show("Button 3 clicked!");
        }

        public static void Main(String[] args) {
            Application.Run(new WinFormExample());
        }
    }
}
