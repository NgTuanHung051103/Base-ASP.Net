using Microsoft.AspNetCore.Mvc;

namespace PokemonReviewApp.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IConfiguration _config;

        public BaseController()
        {
            _config = this.GetService<IConfiguration>()!;
        }
    }
}
