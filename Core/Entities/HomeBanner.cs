namespace APIBrechoRFCC.Core.Entities
{
    public class HomeBanner
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Uri? Image { get; set; }
        public int? Position { get; set; }
        public List<int>? ProductIds { get; set; }

    }
}
