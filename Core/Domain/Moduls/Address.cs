namespace Domain.Moduls
{
    public class Address :BaseEntity<int>
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}