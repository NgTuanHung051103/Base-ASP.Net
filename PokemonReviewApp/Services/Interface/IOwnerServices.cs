using PokemonReviewApp.Common.DataGram;

namespace PokemonReviewApp.Services
{
    public interface IOwnerServices
    {
        public ValueTask<ResponseBase> GetAllOwnersAsync();
    }
}
