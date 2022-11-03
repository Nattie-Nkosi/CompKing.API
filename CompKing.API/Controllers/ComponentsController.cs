using CompKing.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompKing.API.Controllers
{
    [Route("api/computers/{computerId}/component")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ComponentDto>> GetComponents(int computerId)
        {
            var computer = ComputersDataStore.Current.computers.FirstOrDefault(c => c.Id == computerId);

            // If a Computer ID does not exist return Not Found Error -> 404 not found
            if(computer == null)
            {
                return NotFound();
            }

            return Ok(computer.Components);
        }

        [HttpGet("{componentId}", Name = "GetComponent")]
        public ActionResult<ComponentDto> GetComponent(int computerId, int componentId)
        {
            var computer = ComputersDataStore.Current.computers.FirstOrDefault(c => c.Id == computerId);
            if(computer == null)
            {
                return NotFound();
            }

            var component = computer.Components.FirstOrDefault(c => c.Id == componentId);
            if(component == null)
            {
                return NotFound();
            }

            return Ok(component);
        }

        [HttpPost]
        public ActionResult<ComponentForCreationDto> CreateComponent(int computerId, ComponentForCreationDto component)
        {
            
            var computer = ComputersDataStore.Current.computers.FirstOrDefault(c => c.Id == computerId);
            if(computer == null)
            {
                return NotFound();
            }

            // Calculate an Id of a component
            var maxComponentId = ComputersDataStore.Current.computers.SelectMany(c => c.Components).Max(p => p.Id);

            var finalComponent = new ComponentDto()
            {
                Id = ++maxComponentId,
                Name = component.Name,
                Description = component.Description,
            };

            computer.Components.Add(finalComponent);

            return CreatedAtRoute("GetComponent", new
            {
                computerId = computerId,
                componentId = finalComponent.Id
            }, finalComponent);
        }
    }
}
