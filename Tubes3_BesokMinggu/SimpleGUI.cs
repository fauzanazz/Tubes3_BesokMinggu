using System;
using System.Windows.Forms;

namespace SimpleGUI
{
    public class SimpleGUIForm : Form
    {
        private Button button;
        private Label label;

        public SimpleGUIForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            button = new Button();
            button.Text = "Click Me!";
            button.Click += Button_Click;

            label = new Label();
            label.Text = "Hello, World!";
            label.TextAlign = ContentAlignment.MiddleCenter;

            // Set form properties
            this.Text = "Simple GUI";
            this.Size = new System.Drawing.Size(300, 150);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Controls.Add(button);
            this.Controls.Add(label);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            label.Text = "Button Clicked!";
        }
    }

    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SimpleGUIForm());
        }
    }
}
