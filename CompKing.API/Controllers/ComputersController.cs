using CompKing.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompKing.API.Controllers
{
    [ApiController]
    [Route("/api/computers")]
    public class ComputersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ComputerDto>> GetComputers()
        {
            return Ok(ComputersDataStore.Current.computers);
        }

        [HttpGet("{id}")]
        public ActionResult<ComputerDto> GetComputer(int id)
        {
            var getComputer = ComputersDataStore.Current.computers.FirstOrDefault(c => c.Id == id);

            if (getComputer == null)
            {
                return NotFound();
            };

            return Ok(getComputer);
        }
    }
}
