using PTC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrailerSearchLocation.Managers;
using TrailerSearchLocation.Models;
using TrailerSearchLocation.TrailerSearchService;

namespace TrailerSearchLocation.Adapters
{
    public class VehicleAPIAdapter
    {
        XMLSerializerManager xmlManager;
        public VehicleAPIAdapter()
        {
            xmlManager = new XMLSerializerManager();
        }

        public IEnumerable<VehicleRowModel> CallServiceForVehicleList()
        {
            string usn = AppSettings.GetStringValue("username");
            string pwd = AppSettings.GetStringValue("password");
            ArrayOfString vehicleColumns = new ArrayOfString { "VehicleID" , "VehicleTypeID", "VehicleName"};
            string options = "";

            var api = new FleetManagerAPISoapClient();
            var vehicleList = api.GetVehicles_v1Async(usn, pwd, vehicleColumns, options);
            var vehicleResponseBody = vehicleList.Result.Body.GetVehicles_v1Result;
            var vehicleResult = xmlManager.VehicleDeserializer<XmlVehicleModel>(vehicleResponseBody);
            var newVehicleList = vehicleResult.VehicleResponse.row.ToList();

            return newVehicleList;
        }
    }
}