using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookServiceSample.Models
{
    public class Books
    {
        public Books(int id,string title,params BookChapter[] chapter)
        {
            this.Id = id;
            this.Title = title;
            this.Chapters = chapter;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public ICollection<BookChapter> Chapters { get; private set; }
    }
}