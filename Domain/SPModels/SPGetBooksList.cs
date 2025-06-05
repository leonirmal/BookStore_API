namespace BookStoreAPI.Domain.SPModels
{
    public class SPGetBooksList
    {
        public int id { get; set; }
        public string? publisher { get; set; }
        public string? title { get; set; }
        public string? authorFirstName { get; set; }
        public string? authorLastName { get; set; }
        public decimal? price { get; set; }
    }
}
