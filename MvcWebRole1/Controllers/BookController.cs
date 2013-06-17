using BookCave.Service.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;

namespace root.Controllers
{
    public class BookController : Controller
    {
        //
        // GET: /Books/

        public ActionResult Details(string isbn13)
        {
            var encodedIsbn13 = Server.HtmlEncode(isbn13);

            //var uri = new Uri(String.Format(@"http://apps.my.apprendacloud.com/api/services/json/r/bookcavetest(v1)/BookService/IBook/books/{0}",
            var uri = new Uri(String.Format(@"http://12249d74a12e4d1d9176382e7176c88c.cloudapp.net/Book.svc/books/{0}",
            //var uri = new Uri(String.Format(@"http://localhost:40000/book.svc/books/{0}",
                encodedIsbn13));
            using (var webClient = new WebClient())
            {
                var resultBitStream = webClient.DownloadData(uri);
                var serializedJson = new DataContractJsonSerializer(typeof(SuperDto));
                ViewBag.Book = serializedJson.ReadObject(new MemoryStream(resultBitStream));

                //if(results.Count()>0)
                //    resultBitStream=resultBitStream.First();
            }
            return View();
        }
    }
}
