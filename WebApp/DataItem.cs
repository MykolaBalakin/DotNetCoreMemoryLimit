namespace WebApp
{
    public class DataItem
    {
        public int Id { get; set; }
        public Size Size { get; set; }

        public DataItem()
        {
        }
        
        public DataItem(int id, Size size)
        {
            Id = id;
            Size = size;
        }
    }
}