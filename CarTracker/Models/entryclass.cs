using System;
using System.Collections.Generic;

namespace CarTracker.Models
{

    public enum CarService
    {
        //TODO: Add more car service
        MPG,
        Gas,
    };

    public class Service
    {
        public string date { get; }
        public string millage { get; }
        public string location { get; }
        public string description { get; }
        public string car { get; }

        public Service(string entryDate, string entryMillage, string entryLocation, string entryDescriprion, string entryCar)
        {
            date = entryDate;
            millage = entryMillage;
            location = entryLocation;
            description = entryDescriprion;
            car = entryCar;
        }

        public string GetStatement(string statement)
        {

            switch (statement)
            {
                case "date":
                    return date;
                case "millage":
                    return millage;
                case "location":
                    return location;
                case "description":
                    return description;
                case "car":
                    return car;
                default:
                    return null;
            }
        }
    }
}