using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CarTracker.Models;
//using Newtonsoft.Json;
using SQLite;

namespace CarTracker.Models {

    public class StoredCarsModel {
        [PrimaryKey]
        public string Plate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public Color V_Color { get; set; }
        public string Vin { get; set; }
        public string Name { get; set; }
    }
}