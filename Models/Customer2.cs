namespace Models
{
    public class Customer2 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }

        public string MailingAddress { get ; set ; } = "1 High St";
        public string BillingAddress { get ; set ; } = "P.O. Box 1";
    }
}