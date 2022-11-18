using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;

namespace RecipieList
{
    public class Database
    {
        SQLiteAsyncConnection database;
        public Database(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<Item>().Wait();
        }
        public Task<int> SaveItemAsync(Item item)
        {
            if (item.ItemID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(Item item)
        {
            return database.DeleteAsync(item);
        }
        public Task<List<Item>> GetItemsAsync()
        {
            return database.Table<Item>().ToListAsync();
        }
        public Task<Item> GetItemAsync(int itemId)
        {
            return database.Table<Item>().Where(i => i.ItemID == itemId).FirstOrDefaultAsync();
        }
        public Database()
        {


        }
    }
}



