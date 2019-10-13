using System;
namespace CarTracker.Models {
    public enum Color {
        //TODO: Add more colors
        Black,
        Blue,
        Gray,
    };


    public class Car {
        public string plate { get; }
        public string make { get; }
        public string model { get; }
        public Color v_color { get; }
        public string vin { get; }
        public string name { get; set; }
        //TODO: Add photo
        // Test

        public Car(string licensePlateNumber, string vehicleMake, string vehicleModel, Color vehicleColor, string vehicleVin, string carNickname) {
            plate = licensePlateNumber;
            make = vehicleMake;
            model = vehicleModel;
            v_color = vehicleColor;
            vin = vehicleVin;
            name = carNickname;
        }

        public void ChangeNickName(string newNickname) {
            name = newNickname;
        }

        public string GetAttribute(string attribute)
        {
            switch (attribute)
            {
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
    }
}
