using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Model;
using System.Diagnostics;
using WindowsFormsApp1.ServiceReference1;
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class ListFilmsForm : Form
    {
        private List<Movie> moviesList;
        private List<Movie> sortedList;
        private List<Movie> searchResults;
        private bool sorted = true;
        private MovieCatalog movieCatalog = new MovieCatalog();

        public ListFilmsForm(List<Movie> movies)
        {
            InitializeComponent();
            moviesList = movies;
            movieCatalog.AddImage(moviesList, flowLayoutPanel1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Movie> searchResults = new List<Movie>();
            if (sortedList != null)
            {
                searchResults = movieCatalog.Search(textBox1.Text, sortedList);
            }
            else
            {
                searchResults = movieCatalog.Search(textBox1.Text, moviesList);
            }
            if (textBox1.Text == "")
            {
                movieCatalog.itemsPerPage = 18;
            }
            movieCatalog.currentPage = 0;
            movieCatalog.AddImage(searchResults, flowLayoutPanel1);
            this.searchResults = searchResults;
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            List<Movie> sortedMovies = movieCatalog.SortMovies(button2,searchResults,sorted,moviesList);
            sorted = !sorted;
            this.sortedList = sortedMovies;
            movieCatalog.AddImage(sortedMovies, flowLayoutPanel1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (movieCatalog.currentPage > 0)
            {
                movieCatalog.currentPage--;
                if (sortedList != null)
                {
                    movieCatalog.AddImage(sortedList, flowLayoutPanel1);
                }
                else
                {
                    movieCatalog.AddImage(moviesList, flowLayoutPanel1);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                if (movieCatalog.currentPage < (moviesList.Count / movieCatalog.itemsPerPage))
                {
                    movieCatalog.currentPage++;
                    if (sortedList != null)
                    {
                        movieCatalog.AddImage(sortedList, flowLayoutPanel1);
                    }
                    else
                    {
                        movieCatalog.AddImage(moviesList, flowLayoutPanel1);
                    }
                }
            }
        }
    }
}
