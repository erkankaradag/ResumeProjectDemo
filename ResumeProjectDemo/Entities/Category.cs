namespace ResumeProjectDemo.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Slug { get; set; }

        public List<Portfolio> Portfolios { get; set; }
    }
}
