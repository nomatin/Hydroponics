using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hydroponics.sqlClasses
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection _database;

        public DataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ListPots>().Wait();
        }

        public Task<List<ListPots>> GetPotAsync()
        {
            return _database.Table<ListPots>().ToListAsync();
        }

        public Task<int> SavePotAsync(ListPots pot)
        {
            return _database.InsertAsync(pot);
        }
        public async Task<int> DeleteItemAsync(ListPots pot)
        {
            return await _database.DeleteAsync(pot);
        }
        public async Task<int> UpdateItemAsync(ListPots pot)
        {
            return await _database.UpdateAsync(pot);
        }
    }
}
