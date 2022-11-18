using SQLite;
namespace RecipieList
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string aother { get; set; }
        public string ingredients { get; set; }
        public string Steps { get; set; }

    }
}