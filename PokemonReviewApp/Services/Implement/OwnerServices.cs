using PokemonReviewApp.Common.DataGram;
using PokemonReviewApp.Datagram;
using PokemonReviewApp.Interfaces;

namespace PokemonReviewApp.Services
{
    public class OwnerServices : IOwnerServices
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnerServices(IOwnerRepository ownerRepo) 
        {
            _ownerRepository = ownerRepo;
        }
        public async ValueTask<ResponseBase> GetAllOwnersAsync()
        {
            var listOwners = await _ownerRepository.GetOwnersAsync();
            return new ResponseBase().Success().SetDataWith("owner", listOwners.Select(x => OwnerResponse.FromEntity(x)).ToList());
        }
    }
}