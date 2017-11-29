using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JournalProjectWebApp.Models
{
    public class PocoArticles
    {
        public int serial { get; set; }
        public string title { get; set; }
        public string subject { get; set; }
        public int authorId { get; set; }
        public string authorFname { get; set; }
        public string authorLname { get; set; }
        public Nullable<int> authorBirthYear { get; set; }
        public Nullable<int> authorWorkYears { get; set; }

    }
}