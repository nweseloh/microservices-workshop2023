using Microsoft.AspNetCore.Mvc;
using Musicals.Models;
using Musicals.Repositories;

namespace Musicals.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // https://localhost:1234/api/Shows
    public class ShowsController : ControllerBase
    {
        private readonly IShowsRepository _showsRepository;

        public ShowsController(IShowsRepository showsRepository)
        {
            _showsRepository = showsRepository;
        }

        [HttpGet]
        public List<Show> Get()
        {
            return _showsRepository.GetAll();
        }
    }
}