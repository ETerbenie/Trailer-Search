using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrailerSearchLocation.Adapters;
using TrailerSearchLocation.Managers;
using TrailerSearchLocation.Models;

namespace TrailerSearchLocation.Controllers
{
    public class TrailerCoordinatesController : Controller
    {
        //private IDetectRowResultsManager _manager;

        //public TrailerCoordinatesController(IDetectRowResultsManager manager)
        //{
        //    _manager = manager;
        //}

        // GET: TrailerCoordinates
        public ActionResult Index(TrailerModel trailer)
        {
            TrailerAPIAdapter trailerAPIAdapter = new TrailerAPIAdapter();
            DetectRowResultsManager detectRowResults = new DetectRowResultsManager();
            //DateTime startTimeStamp = new DateTime();
            var startTimeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
            //startTimeStamp = DateTime.FromBinary();

            // Get trailer coordinates
            var trailerCoords = trailerAPIAdapter.CallWebService(trailer, startTimeStamp);
            if (trailerCoords.ErrorMessage == "Not a valid Trailer")
                return View("InvalidVehicle");
            

            //Check to make sure there are coordinate results
            var trailerCoordsCheck = detectRowResults.RowResults(trailer, trailerCoords, startTimeStamp);
            if (trailerCoordsCheck.ErrorMessage == "This is not Trailer")
                return View("ExceededDateRange");

            // Check to see if it's a trailer 
            if (trailerCoordsCheck.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 1 
                || trailerCoordsCheck.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 2 
                || trailerCoordsCheck.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 4)
            {
                var lastTrailerCoords = trailerCoordsCheck.TrailerResponse.row.LastOrDefault();

                var trailerCoordinates = new ResultsModel()
                {
                    Title = lastTrailerCoords.VehicleName,
                    Latitude = lastTrailerCoords.Latitude,
                    Longitude = lastTrailerCoords.Longitude
                };

                ViewBag.Title = trailerCoordinates.Title;
                ViewBag.Latitude = trailerCoordinates.Latitude;
                ViewBag.Longitude = trailerCoordinates.Longitude;

                return View();
            }
            else if (trailerCoordsCheck.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 0 
                    || trailerCoordsCheck.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 3 
                    || trailerCoordsCheck.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 5
                    || trailerCoordsCheck.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 6
                    || trailerCoordsCheck.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 7)
            {
                return View("Error");
            }

            return View();
        }
    }
}