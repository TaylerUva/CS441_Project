using system;
namespace CarTracker.Models
{

public enum Car {
        //TODO: Add more car service
        MPG,
        Gas,
        ###,
    };

  public class Entry
    {
      public string date { get; }
      public int millage { get; }
      public string location { get; }
      public string description { get; }
      public Car carService { get; }
        
    public Entry (string entryDate, int entryMillage, string entryLocation, string entryDescriprion, Car entryCarService)

      date = entryDate;
      millage = entryMillage;
      location = entryLocation;
      description = entryDescriprion;
      carService = entryCarService;
    }
}
