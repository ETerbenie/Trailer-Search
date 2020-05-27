using PTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrailerSearchLocation.Managers;
using TrailerSearchLocation.TrailerSearchService;
using TrailerSearchLocation.Models;

namespace TrailerSearchLocation.Adapters
{
    public class TrailerAPIAdapter
    {
        XMLSerializerManager xmlManager;
        VehicleAPIAdapter vehicleAPIAdapter;
        public TrailerAPIAdapter()
        {
            xmlManager = new XMLSerializerManager();
            vehicleAPIAdapter = new VehicleAPIAdapter();
        }

        public RootModel CallWebService(TrailerModel trailer, string startTimeStamp)
        {
            string usn = AppSettings.GetStringValue("username");
            string pwd = AppSettings.GetStringValue("password");
            int requestedRowNumber = 200;
            ArrayOfString columns = new ArrayOfString { "Latitude", "Longitude", "RecordTimeStamp", "VehicleTypeID" , "VehicleName" };
            string options = "";
            //int vehicleID = trailer.VehicleId;
            string vehicleName = trailer.VehicleName;

            var vehicleList = vehicleAPIAdapter.CallServiceForVehicleList();
            var targetVehicle = vehicleList.Where(x => x.VehicleName == vehicleName);
            if (targetVehicle.Any())
            {
                int vehicleID = targetVehicle.FirstOrDefault().VehicleID;
                // Instantiate API client
                var api = new FleetManagerAPISoapClient();
                var empstarttime = startTimeStamp.To<DateTime>();

                // Get response
                var response = api.GetGpsReadingsForVehicle_v1Async(usn, pwd, requestedRowNumber, columns, empstarttime, options, vehicleID);

                // Get response body
                var responseBody = response.Result.Body;
                // Get GetGpsReadingsForVehicle_v1Result body 
                string result = responseBody.GetGpsReadingsForVehicle_v1Result;
                
                // Get lists of coordinates for the specificied start date 
                var listOfCoords = xmlManager.TestDeserializer<XmlTrailerModel>(result);

                return listOfCoords;
            }
            else
            {
                RootModel errorVehicle = new RootModel();
                errorVehicle.ErrorMessage = "Not a valid Trailer";
                return errorVehicle;
            }
        }
    }
}