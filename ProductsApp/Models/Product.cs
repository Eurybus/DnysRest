namespace ProductsApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}