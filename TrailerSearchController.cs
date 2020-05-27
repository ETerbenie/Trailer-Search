using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrailerSearchLocation.Adapters;
using TrailerSearchLocation.Models;
using TrailerSearchLocation.TrailerSearchService;

namespace TrailerSearchLocation.Controllers
{
    public class TrailerSearchController : Controller
    {
        // GET: TrailerSearch
        public ActionResult Index()
        {
            return View();
        }
    }
}