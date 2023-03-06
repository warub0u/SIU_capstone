namespace CS_FavouritesAPI.Models
{
    public class Favourites
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        //this is to store actual user's origin
        public string Source { get; set; }
        //this is to store actual user's destination
        public string Destination { get; set; }
        public string SourceLang { get; set; }
        public string SourceLong { get; set; }
        public string DestinationLang { get; set; }
        public string DestinationLong { get; set; }
        public string? Mode { get; set; }
        public string MapUrl { get; set; }
        
    }
}
