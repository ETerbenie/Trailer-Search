using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using PTC;
using System.Web.Mvc;
using TrailerSearchLocation.Managers;
using TrailerSearchLocation.Models;
using TrailerSearchLocation.TrailerSearchService;

namespace TrailerSearchLocation.Controllers
{
    public class ResultsController : Controller
    {
        // GET: Results
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(TrailerModel trailer)
        {
            XMLSerializerManager xmlManager = new XMLSerializerManager();

            string usn = AppSettings.GetStringValue("username");
            string pwd = AppSettings.GetStringValue("password");
            int requestedRowNumber = 1;
            ArrayOfString columns = new ArrayOfString { "Latitude", "Longitude", "RecordTimeStamp", "VehicleTypeID", "VehicleName" };
            //DateTime startTimeStamp = new DateTime(2019, 11, 05);
            DateTime startTimeStamp = DateTime.Now.AddHours(-24);
            //DateTime startTimeStamp = DateTime.Now.AddDays(-6);
            string options = "";
            int vehicleID = trailer.VehicleId;

            // Instantiate API client
            var api = new FleetManagerAPISoapClient();
            // Get response
            var response = api.GetGpsReadingsForVehicle_v1Async(usn, pwd, requestedRowNumber, columns, startTimeStamp, options, vehicleID);
            // Get response body
            var responseBody = response.Result.Body;
            // Get GetGpsReadingsForVehicle_v1Result body 
            string result = responseBody.GetGpsReadingsForVehicle_v1Result;
            // Split string body into an array 
            // string[] rows = result.Split(new[] { " /&gt;&lt;" }, StringSplitOptions.None);

            var listOfCoords = xmlManager.TestDeserializer<XmlTrailerModel>(result);

            var trailerCheck = TrailerCheckManager.TrailerCheck(listOfCoords); // TODO: still need to add an else  for error
            if (trailerCheck.ErrorMessage == "This is not a trailer")
            {
                return View("Error");
            };

            var lastTrailer = trailerCheck.TrailerResponse.row.LastOrDefault();

            var trailerCoordinates = new ResultsModel()
            {
                Latitude = lastTrailer.Latitude,
                Longitude = lastTrailer.Longitude
            };

            ViewBag.Latitude = trailerCoordinates.Latitude;
            ViewBag.Longitude = trailerCoordinates.Longitude;

            return View();
        }

        



        // GET: Results/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Results/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Results/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Results/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Results/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Results/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Results/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
