using BookServiceSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookServiceSample.Controllers
{
    [RoutePrefix("booksamples")]
    public class BookChaptersAttrController : ApiController
    {
        private static List<Books> books;
        private static List<BookChapter> chapters;
        static BookChaptersAttrController()
        {
            chapters = new List<BookChapter>
            {
                new BookChapter(){ Number = 1,Title=".Net体系结构",Pages=22},
                new BookChapter(){Number=2,Title="核心C#",Pages=50},
                new BookChapter(){Number=3,Title="对象和类型",Pages=110},
                new BookChapter(){Number = 4,Title="继承",Pages=200},
                new BookChapter(){ Number = 5,Title="泛型",Pages=400}
            };
            books = new List<Books> {
                new Books(1,"C#高级编程",chapters.ToArray()),
                new Books(2,"Asp.Net本质论")
            };
        }

        // GET: api/BookChaptersAttr
        [Route("books/{bookId}")]
        public IEnumerable<BookChapter> GetBookChapters(int bookId)
        {
            return books.Where(b => b.Id == bookId).SingleOrDefault().Chapters;
        }
        // GET: api/BookChaptersAttr/5
        //[Route("books/{bookId:int}/chapters/{number:int}")]
        [Route("{bookId:int}/{number:int}")]
        public BookChapter Get(int bookId,int number)
        {
            return books.Where(b => b.Id == bookId).SingleOrDefault().Chapters.Where(b => b.Number == number).SingleOrDefault();
        }

        // POST: api/BookChaptersAttr
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/BookChaptersAttr/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BookChaptersAttr/5
        public void Delete(int id)
        {
        }
    }
}
