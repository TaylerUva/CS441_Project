using System;
using System.Collections.Generic;
using SQLite;

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
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime date { get; set; }
        //public string date { get; set; }
        public int millage { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string car { get; set; }

        public Service() { }

        public Service(DateTime entryDate, int entryMillage, string entryLocation, string entryDescriprion, string entryCar)
        {
            date = entryDate;
            millage = entryMillage;
            location = entryLocation;
            description = entryDescriprion;
            car = entryCar;
        }

        //public string GetStatement(string statement)
        //{

        //    switch (statement)
        //    {
        //        case "date":
        //            return date;
        //        case "millage":
        //            return millage;
        //        case "location":
        //            return location;
        //        case "description":
        //            return description;
        //        case "car":
        //            return car;
        //        default:
        //            return null;
        //    }
        //}
    }
}