using BookServiceSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookServiceSample.Controllers
{
    public class BookChaptersController : ApiController
    {
        private static List<BookChapter> chapters;
        static BookChaptersController()
        {
            chapters = new List<BookChapter> {
                new BookChapter() {Number=1,Title="悲惨世界",Pages=100 },
                new BookChapter(){Number=2,Title="挪威的森林",Pages=387},
                new BookChapter(){Number=3,Title="这世界我来过",Pages=228},
                new BookChapter(){Number=4,Title="我们的天空",Pages=555},
                new BookChapter(){ Number=5,Title="C#高级编程",Pages=1000}
            };
        }
        // GET: api/BookChapters
        public IEnumerable<BookChapter> GetBookChapters()
        {
            return chapters;
        }

        // GET: api/BookChapters/5
        public BookChapter GetBookChapter(int id)
        {
            return chapters.Where(b => b.Number == id).SingleOrDefault();
        }

        // POST: api/BookChapters
        public void PostBookChapter([FromBody]BookChapter value)
        {
            chapters.Add(value);
        }

        // PUT: api/BookChapters/5
        //public void PutBookChapter(int id, [FromBody]BookChapter value)
        //{
        //    chapters.Remove(chapters.Where(b => b.Number == id).SingleOrDefault());
        //    chapters.Add(value);
        //}
        public IHttpActionResult PutBookChapter(int id, [FromBody]BookChapter value)
        {
            try {
                chapters.Remove(chapters.Where(b => b.Number == id).SingleOrDefault());
                chapters.Add(value);
                return Ok();
            }
            catch(InvalidOperationException)
            {
                return BadRequest();
            }
            
        }
        // DELETE: api/BookChapters/5
        public void DeleteBookChapter(int id)
        {
            chapters.Remove(chapters.Where(b => b.Number == id).SingleOrDefault());
        }
    }
}
