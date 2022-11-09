using CompKing.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompKing.API.Controllers
{
    [ApiController]
    [Route("/api/computers")]
    public class ComputersController : ControllerBase
    {
        private readonly ComputersDataStore _computersDataStore;
        public ComputersController(ComputersDataStore computersDataStore)
        {
            _computersDataStore = computersDataStore ?? throw new ArgumentNullException(nameof(computersDataStore));
        }
        [HttpGet]
        public ActionResult<IEnumerable<ComputerDto>> GetComputers()
        {
            return Ok(_computersDataStore.computers);
        }

        [HttpGet("{id}")]
        public ActionResult<ComputerDto> GetComputer(int id)
        {
            var getComputer = _computersDataStore.computers.FirstOrDefault(c => c.Id == id);

            if (getComputer == null)
            {
                return NotFound();
            };

            return Ok(getComputer);
        }
    }
}
