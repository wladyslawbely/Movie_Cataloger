using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WindowsFormsApp1.ServiceReference1;

namespace WindowsFormsApp1.Model
{
    public class MovieCatalog
    {
        private List<Movie> moviesList;
        private Service1Client client = new Service1Client();
        public int currentPage { get; set; } = 0;
        public int itemsPerPage { get; set; } = 18;


        public MovieCatalog()
        {

        }

        public List<Movie> SortMovies(Button button2, List<Movie> movies, bool sorted, List<Movie> movies1)
        {
            if (movies == null)
            {
                movies = movies1;
            }
            List<Movie> sortedMovies = new List<Movie>(movies);
            if (sorted)
            {
                sortedMovies.Sort((m1, m2) => m1.Title.CompareTo(m2.Title));
                button2.Text = "Сортировка по названию A-Z ↓";
            }
            else
            {
                sortedMovies.Sort((m1, m2) => m2.Title.CompareTo(m1.Title));
                button2.Text = "Сортировка по названию Z-A ↑";
            }
            return sortedMovies;
        }

        public List<Movie> FilterCountry(string country)
        {
            Movie[] movies = client.ReadMovies();
            List<Movie> list = new List<Movie>();
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i].Country == country)
                {
                    list.Add(movies[i]);
                }
            }
            return list;
        }

        public List<Movie> FilterGenre(string genre)
        {
            Movie[] movies = client.ReadMovies();
            List<Movie> list = new List<Movie>();
            for (int i = 0; i < movies.Length; i++)
            {
                if (movies[i].Genres.Contains(genre))
                {
                    list.Add(movies[i]);
                }
            }
            return list;
        }

        public List<string> Genres(List<Movie> movies)
        {
            List<string> genres = new List<string>();
            foreach (Movie movie in movies)
            {
                foreach (string genre in movie.Genres)
                {
                    if (!genres.Contains(genre))
                    {
                        genres.Add(genre);
                    }
                }
            }
            return genres;
        }

        public List<string> Countries(List<Movie> movies)
        {
            List<string> countries = new List<string>();
            foreach (Movie movie in movies)
            {
                if (!countries.Contains(movie.Country))
                {
                    countries.Add(movie.Country);
                }
            }
            return countries;
        }

        public void AddImage(List<Movie> moviesList, FlowLayoutPanel flowLayoutPanel)
        {
            this.moviesList = moviesList;
            flowLayoutPanel.Controls.Clear();
            int startIndex = currentPage * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, moviesList.Count);
            for (int i = startIndex; i < endIndex; i++)
            {
                try
                {
                    FileStream fs = new FileStream(moviesList[i].CoverPath, FileMode.Open);
                    Image img = Image.FromStream(fs);
                    fs.Close();

                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
                    {
                        ColumnCount = 1,
                        RowCount = 2,
                        Width = 200,
                        Height = 300
                    };


                    PictureBox pictureBox = new PictureBox
                    {
                        Image = img,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Width = 200,
                        Height = 200,
                        Tag = i
                    };
                    pictureBox.Click += new EventHandler(pictureBox_Click);
                    pictureBox.Cursor = Cursors.Hand;

                    Label label = new Label
                    {
                        Text = moviesList[i].Title,
                        Anchor = AnchorStyles.None,
                        MaximumSize = new Size(200, 0),
                        AutoSize = true,
                        Font = new Font("Georgia", 9),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.Bisque,
                        BorderStyle = BorderStyle.FixedSingle
                    };

                    tableLayoutPanel.Controls.Add(pictureBox, 0, 0);
                    tableLayoutPanel.Controls.Add(label, 0, 1);

                    flowLayoutPanel.VerticalScroll.Visible = true;
                    flowLayoutPanel.Controls.Add(tableLayoutPanel);
                }
                catch
                {
                    break;
                }
            }
        }
        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            int number = (int)pictureBox.Tag;
            var linkAddress = moviesList[number].Link;
            Process.Start(linkAddress);
        }

        public List<Movie> Search(string searchText, List<Movie> moviesList)
        {
            searchText = searchText.ToLower();
            List<Movie> searchResults = new List<Movie>();
            foreach (Movie movie in moviesList)
            {
                if (movie.Title.ToLower().Contains(searchText))
                {
                    searchResults.Add(movie);
                }
            }
            itemsPerPage = searchResults.Count;
            return searchResults;
        }
    }
}
