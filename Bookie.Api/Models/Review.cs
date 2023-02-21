namespace Bookie.Api.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public string VoterName { get; set; }
        public int NumStars { get; set; }
        public string Comment { get; set; }
    }
}
