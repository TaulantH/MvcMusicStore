namespace MvcMusicStore.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int AlbumID {  get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Credit_Card { get; set; }
        public int Expiry_date { get; set; }
        public int CVV { get; set; }
        public decimal Total_Amount {get; set; }
        public DateOnly Purchase_date { get; set; }

        public Album? Album { get; set; }
    }
}
