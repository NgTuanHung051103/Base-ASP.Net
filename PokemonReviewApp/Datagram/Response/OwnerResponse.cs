using PokemonReviewApp.Models;

namespace PokemonReviewApp.Datagram
{
    public class OwnerResponse : BaseMapper<OwnerResponse, Owner>
    {
        public int OwnerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gym { get; set; }
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public override void AddCustomMappings()
        {
            SetCustomMappings()
                .Map(dest => dest.Id, src => src.OwnerId);

            SetCustomMappingsInverse()
                .Map(dest => dest.OwnerId, src => src.Id)
                .AfterMapping( (src, dest) =>
                {
                    if(src.Country != null)
                    {
                        dest.CountryName = src.Country.Name;
                    }
                });
        }
    }
}
