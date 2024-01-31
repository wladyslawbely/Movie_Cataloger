using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WindowsFormsApp1.ServiceReference1;

namespace WindowsFormsApp1
{
    public partial class RegistrationForm : Form
    {
        private Service1Client client = new Service1Client();
        private MainForm form3;
        private AuthorizationForm form1;
        private ToolTip toolTip1 = new ToolTip();

        public RegistrationForm(MainForm form3, AuthorizationForm form1)
        {
            InitializeComponent();
            this.form3 = form3;
            this.form1 = form1;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var username = textBox4.Text.Trim();
            var password = textBox3.Text.Trim();
            string result = client.Register(username, password);
            if (result == "Успешно")
            {
                MessageBox.Show("Вы успешно зарегистрировались!");
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(result);
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.UseSystemPasswordChar = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
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

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            form1.Show();
        }
    }
}
