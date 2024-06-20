namespace APIBrechoRFCC.Application.DTO.OutputDTOs
{
    public class HomeBannerOutputDTO
    {
        public string? Title { get; set; }
        public string? Image { get; set; }
        public int? Position { get; set; }
        public List<int>? ProductIds { get; set; }
    }
}
