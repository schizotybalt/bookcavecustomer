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

            //var uri = new Uri(String.Format(@"http://apps.apprenda.local/api/services/json/r/bookcavetest(v1)/BookService/IBook/books?author={0}&skill={1}&content={2}&title={3}&summary={4}",
            var uri = new Uri(String.Format(@"http://localhost:40000/book.svc/books?author={0}&skill={1}&content={2}&title={3}&summary={4}",
                author,
                skill,
                content,
                title,
                summary));
            var webClient = new WebClient();
            var resultBitStream = webClient.DownloadData(uri);
            var serializedJson = new DataContractJsonSerializer(typeof(List<SuperDto>));
            ViewBag.Results = serializedJson.ReadObject(new MemoryStream(resultBitStream));

            return View();
        }
    }
}
