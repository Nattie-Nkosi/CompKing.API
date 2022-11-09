using CompKing.API.Models;
using CompKing.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CompKing.API.Controllers
{
    [Route("api/computers/{computerId}/component")]
    [ApiController]
    public class ComponentsController : ControllerBase
    {
        private readonly ILogger<ComponentsController> _logger;
        private readonly IMailSevice _mailServices;

        public ComponentsController(ILogger<ComponentsController> logger, IMailSevice mailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailServices = mailService ?? throw new ArgumentNullException(nameof(mailService));    
        }

        [HttpGet]
        public ActionResult<IEnumerable<ComponentDto>> GetComponents(int computerId)
        {
            try
            {
                var computer = ComputersDataStore.Current.computers.FirstOrDefault(c => c.Id == computerId);

                // If a Computer ID does not exist return Not Found Error -> 404 not found
                if (computer == null)
                {
                    _logger.LogInformation($"Computer with id {computerId} was not found when accessing components");
                    return NotFound();
                }
                return Ok(computer.Components);
            } catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting components for computer of {computerId}.", ex);
                return StatusCode(500, "A problem happenend while handling your request.");
            }
            

            
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

        [HttpPut("{componentId}")]
        public ActionResult updateComponent(int computerId, int componentId, ComponentForUpdateDto component)
        {
            var computer = ComputersDataStore.Current.computers.FirstOrDefault(c => c.Id == computerId);
            if(computer == null)
            {
                return NotFound();
            }

            var componentFromStore = computer.Components.FirstOrDefault(c => c.Id == componentId);
            if(component == null)
            {
                return NotFound();
            }

            componentFromStore.Name = component.Name;

            componentFromStore.Description = component.Description;

            return NoContent();
        }

        [HttpPatch("{componentId}")]
        public ActionResult PartiallyUpdateComponent(int computerId, int componentId, JsonPatchDocument<ComponentForUpdateDto> patchDocument)
        {
            var computer = ComputersDataStore.Current.computers.FirstOrDefault(c => c.Id == computerId);

            if(computer == null)
            {
                return NotFound();
            }

            var componentFromStore = computer.Components.FirstOrDefault(c => c.Id == componentId);
            if(componentFromStore == null)
            {
                return NotFound();
            }

            var componentToPatch = new ComponentForUpdateDto()
            {
                Name = componentFromStore.Name,
                Description = componentFromStore.Description
            };

            patchDocument.ApplyTo(componentToPatch, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!TryValidateModel(componentToPatch))
            {
                return BadRequest(ModelState);
            }

            componentFromStore.Name = componentToPatch.Name;
            componentFromStore.Description = componentToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{componentId}")]
        public ActionResult DeleteComponent(int computerId, int componentId)
        {
            var computer = ComputersDataStore.Current.computers.FirstOrDefault(c => c.Id == computerId);

            if (computer == null)
            {
                return NotFound();
            }

            var componentFromStore = computer.Components.FirstOrDefault(c => c.Id == componentId);
            if (componentFromStore == null)
            {
                return NotFound();
            }

            computer.Components.Remove(componentFromStore);
            _mailServices.Send("Component deleted", $"Component {componentFromStore.Name} with id {componentFromStore.Id} was deleted.");
            return NoContent();
        }
    }
}
