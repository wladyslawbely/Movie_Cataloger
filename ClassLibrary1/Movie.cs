using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ClassLibrary1
{
    public class Movie
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string[] Genres { get; set; }
        public string CoverPath { get; set; }
        public string Link { get; set; }

    }
}
