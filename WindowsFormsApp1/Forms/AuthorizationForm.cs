using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1.ServiceReference1;

namespace WindowsFormsApp1
{
    public partial class AuthorizationForm : Form
    {
        private MainForm form3;
        private Service1Client client = new Service1Client();
        private ToolTip toolTip1 = new ToolTip();


        public AuthorizationForm(MainForm form3)
        {
            InitializeComponent();
            this.form3 = form3;
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
        }

        private void label_Click(object sender, EventArgs e)
        {
            RegistrationForm form2 = new RegistrationForm(form3,this);
            form2.Show();
            this.Hide();
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            Label theLabel = (Label)sender;
            theLabel.ForeColor = Color.SkyBlue;
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            Label theLabel = (Label)sender;
            theLabel.ForeColor = Color.Black;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            var username = textBox1.Text.Trim();
            var password = textBox2.Text.Trim();
            if (client.Login(username, password))
            {
                MessageBox.Show("Вы успешно авторизовались!");
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль.");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            form3.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                toolTip1.SetToolTip(this.checkBox1, "Показать пароль");
            }
            else
            {
                toolTip1.SetToolTip(this.checkBox1, "Скрыть пароль");
            }
        }
    }
}
