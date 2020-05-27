using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrailerSearchLocation.Models;

namespace TrailerSearchLocation.Managers
{
    public static class TrailerCheckManager
    {
        public static RootModel TrailerCheck(this RootModel trailerCoords)
        {

            var errorTrailer = "This is not a trailer";

            var invalidTrailer = new RootModel
            {
                TrailerResponse = null,
                ErrorMessage = errorTrailer
            };

            if (trailerCoords.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 1 
                || trailerCoords.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 2 
                || trailerCoords.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 4)
            {
                return trailerCoords;
            }
            else if (trailerCoords.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 0
                || trailerCoords.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 3
                || trailerCoords.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 5
                || trailerCoords.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 6
                || trailerCoords.TrailerResponse.row.FirstOrDefault().VehicleTypeID == 7)
            {
                return invalidTrailer;
            }
            else
            {
                return invalidTrailer;
            }
        }
    }
}