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
        public int mileage { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public string car { get; set; }

        public Service() { }

        public Service(DateTime entryDate, int entrymileage, string entryLocation, string entryDescriprion, string entryCar)
        {
            date = entryDate;
            mileage = entrymileage;
            location = entryLocation;
            description = entryDescriprion;
            car = entryCar;
        }
    }
}