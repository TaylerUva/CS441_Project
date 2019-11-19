using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace CarTracker.Models {
    public class CarDatabase {
        readonly SQLiteAsyncConnection carDatabase;

        public CarDatabase(string dbPath)
        {
            carDatabase = new SQLiteAsyncConnection(dbPath);
            carDatabase.CreateTableAsync<StoredCarsModel>().Wait();
        }

        public Task<List<StoredCarsModel>> GetCarAsync()
        {
            return carDatabase.Table<StoredCarsModel>().ToListAsync();
        }

        public Task<int> SaveCarAsync(StoredCarsModel car)
        {
            return carDatabase.InsertAsync(car);
        }
    }
}