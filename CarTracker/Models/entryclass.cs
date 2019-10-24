namespace CarTracker.Models
{

public enum CarService {
        //TODO: Add more car service
        MPG,
        Gas,
    };

    public class Service
    {
        public string date { get; }
        public int millage { get; }
        public string location { get; }
        public string description { get; }
        public string car { get; }

        public Service(string entryDate, int entryMillage, string entryLocation, string entryDescriprion, string entryCar)
        {
            date = entryDate;
            millage = entryMillage;
            location = entryLocation;
            description = entryDescriprion;
            car = entryCar;
        }
    }
}
