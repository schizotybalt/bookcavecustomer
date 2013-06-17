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
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Results = new List<SuperDto>();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            var author = formCollection["Author"];
            var title = formCollection["Title"];
            var skill = formCollection["Skill"];
            var content = formCollection["Content"];
            var summary = formCollection["Summary"];

            //var uri = new Uri(String.Format(@"http://apps.my.apprendacloud.com/api/services/json/r/bookcavetest(v1)/BookService/IBook/books?author={0}&skill={1}&content={2}&title={3}&summary={4}",
            var uri = new Uri(String.Format(@"http://12249d74a12e4d1d9176382e7176c88c.cloudapp.net/Book.svc/books?author={0}&skill={1}&content={2}&title={3}&summary={4}",
                //var uri = new Uri(String.Format(@"http://localhost:40000/book.svc/books?author={0}&skill={1}&content={2}&title={3}&summary={4}",
                author,
                skill,
                content,
                title,
                summary));
            using (var webClient = new WebClient())
            {
                var resultBitStream = webClient.DownloadData(uri);
                var serializedJson = new DataContractJsonSerializer(typeof(List<SuperDto>));
                IEnumerable<SuperDto> returnSet = (List<SuperDto>)serializedJson.ReadObject(new MemoryStream(resultBitStream));

                foreach (SuperDto result in returnSet)
                {
                    var resultAuthor = result.Author;

                    if (resultAuthor != null && resultAuthor.Split(',').Length>1)
                        result.Author = resultAuthor.Split(',')[1] + " " + resultAuthor.Split(',')[0];
                }

                ViewBag.Results = returnSet;
            }

            return View();
        }
    }
}
