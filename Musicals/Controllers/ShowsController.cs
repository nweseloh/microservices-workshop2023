using Microsoft.AspNetCore.Mvc;
using Musicals.Models;
using Musicals.Repositories;

namespace Musicals.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // https://localhost:1234/api/Shows
    public class ShowsController : ControllerBase
    {
        private readonly IRepository<Show> _showsRepository;

        public ShowsController(IRepository<Show> showsRepository)
        {
            _showsRepository = showsRepository;
        }

        [HttpGet(Name="GetAll")]
        public IEnumerable<Show> Get()
        {
            return _showsRepository.GetAll();
        }
    }
}