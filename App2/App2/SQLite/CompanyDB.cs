using System.Collections.Generic;
using System.Threading.Tasks;
using App2.Model;
using SQLite;

namespace App2.SQLite
{
   public class CompanyDB
    {
        readonly SQLiteAsyncConnection _database;

        public CompanyDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<CompanyTbl>().Wait();
        }

        public Task<List<CompanyTbl>> GetItemsAsync()
        {
            return _database.Table<CompanyTbl>().ToListAsync();
            //return database.Table<MovementListTbl>().ToListAsync();
        }

        //public Task<List<CompanyTbl>> GetItemsNotDoneAsync()
        //{
        //    return database.QueryAsync<CompanyTbl>("SELECT * FROM [CompanyTbl] WHERE [Done] = 0");
        //}

        public Task<CompanyTbl> GetItemAsync(int id)
        {
            return _database.Table<CompanyTbl>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> UpdateItemAsync(CompanyTbl item)
        {
          return _database.UpdateAsync(item);
        }

        public Task<int> SaveItemAsync(CompanyTbl item)
        {
                return _database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(CompanyTbl item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
