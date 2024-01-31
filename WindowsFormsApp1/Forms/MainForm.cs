using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Model;
using WindowsFormsApp1.ServiceReference1;

namespace WindowsFormsApp1
{
    
    public partial class MainForm : Form 
    {
        private Service1Client client = new Service1Client();
        private MovieCatalog movieCatalog = new MovieCatalog();

        public MainForm()  
        {
            InitializeComponent();
            this.Shown += new EventHandler(Form3_Shown);
            AuthorizationForm form1 = new AuthorizationForm(this);
            form1.Show();
            listBox1.DataSource = movieCatalog.Genres(client.ReadMovies().ToList());
            listBox2.DataSource = movieCatalog.Countries(client.ReadMovies().ToList());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Movie[] movies = client.ReadMovies();
            List<Movie> list = new List<Movie>(movies);            
            ListFilmsForm form4 = new ListFilmsForm(list);
            form4.Show();
        }
        private void Form3_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            List<Movie> list = movieCatalog.FilterGenre(listBox1.SelectedItem.ToString());
            ListFilmsForm form4 = new ListFilmsForm(list);
            form4.Show();
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            List<Movie> list = movieCatalog.FilterCountry(listBox2.SelectedItem.ToString());
            ListFilmsForm form4 = new ListFilmsForm(list);
            form4.Show();
        }
    }
}
