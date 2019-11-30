using System;
using System.Collections.Generic;
using SQLite;

namespace CarTracker.Models {

    public class Car {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string plate { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public Color v_color { get; set; }
        public string vin { get; set; }
        public string name { get; set; }
        //TODO: Add photo

        public Car() { }

        public Car(string licensePlateNumber, string vehicleMake, string vehicleModel, Color vehicleColor, string vehicleVin, string carNickname) {
            plate = licensePlateNumber;
            make = vehicleMake;
            model = vehicleModel;
            v_color = vehicleColor;
            vin = vehicleVin;
            name = carNickname;
        }

        public string GetAttribute(string attribute) {
            switch (attribute) {
            case "plate":
                return plate;
            case "make":
                return make;
            case "model":
                return model;
            case "vin":
                return vin;
            case "name":
                return name;
            default:
                return null;
            }
        }

        public enum Color {
            //TODO: Add more colors
            Black,
            Blue,
            Gray,
            Aqua,
            Fuchsia,
            Green,
            Lime,
            Maroon,
            Olive,
            Purple,
            Red,
            Silver,
            Teal,
            White,
            Yellow,
        };

        // Dictionary to get Color from color name.
        public static Dictionary<string, Color> nameToColor = new Dictionary<string, Color>()
        {
            { "Aqua", Color.Aqua },
            { "Black", Color.Black },
            { "Blue", Color.Blue },
            { "Fucshia", Color.Fuchsia },
            { "Gray", Color.Gray },
            { "Green", Color.Green },
            { "Lime", Color.Lime },
            { "Maroon", Color.Maroon },
            { "Navy", Color.Black },
            { "Olive", Color.Olive },
            { "Purple", Color.Purple },
            { "Red", Color.Red },
            { "Silver", Color.Silver },
            { "Teal", Color.Teal },
            { "White", Color.White },
            { "Yellow", Color.Yellow }
        };
    }


}
