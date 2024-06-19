namespace APIBrechoRFCC.Application.DTO.InputDTOs
{
    public class HomeBannerInputDTO
    {
        public string? Title { get; set; }
        public Uri? Image { get; set; }
        public int? Position { get; set; }
        public List<int>? ProductIds { get; set; }
    }
}
