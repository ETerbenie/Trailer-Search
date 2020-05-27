using System;
using TrailerSearchLocation.Models;

namespace TrailerSearchLocation.Managers
{
    public interface IDetectRowResultsManager
    {
        RootModel RowResults(TrailerModel trailer, RootModel listOfCorrds, DateTime startTimeStamp);
    }
}