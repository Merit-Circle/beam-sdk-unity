namespace Beam.Models
{
    public class BeamGame
    {
        public string PegiRating { get; set; }
        public string[] PegiContent { get; set; }
        public string Id { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public string LogoImageUrl { get; set; }
        public string BackgroundImageUrl { get; set; }
        public int[] ChainIds { get; set; }
    }
}