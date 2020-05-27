using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrailerSearchLocation.Models;
using TrailerSearchLocation.Adapters;
using PTC;

namespace TrailerSearchLocation.Managers
{
    public class DetectRowResultsManager 
    {
        TrailerAPIAdapter trailerAPIAdapter;
        public DetectRowResultsManager()
        {
            trailerAPIAdapter = new TrailerAPIAdapter();
        }

        public RootModel RowResults(TrailerModel trailer, RootModel listOfCorrds, string startTimeStamp)
        {
            RootModel newCoords = listOfCorrds;
            var empstarttime = startTimeStamp.To<DateTime>();

            while (newCoords.TrailerResponse.row.Count == 0 && empstarttime > DateTime.Now.AddDays(-10))
            {
                //empstarttime = empstarttime.AddDays(-1);
                empstarttime = empstarttime.AddHours(-5);
                var newStartTimeStamp = empstarttime.ToString("yyyy-MM-ddTHH:mm:ss");
                newCoords = trailerAPIAdapter.CallWebService(trailer, newStartTimeStamp);
            }

            if (newCoords.TrailerResponse.row.Count > 0)
            {
                return newCoords;
            }
            else
            {
                newCoords.ErrorMessage = "This is not Trailer";
                return newCoords;
            }
        }
    }
}