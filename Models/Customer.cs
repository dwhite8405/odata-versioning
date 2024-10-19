namespace Models
{
    public class Customer 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }

        public string Address { get ; set ; } = "1 High St";

    }
}