using DotNetTask.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static DotNetTask.Models.InternShipProgram;

namespace DotNetTask.Controllers
{
    /*Developer - Eranda Yohan*/
    /*Date - 5-17-2014 */
    /*Email  - ErandaYohan1234@gmail.com */

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;

        //Constructor to inject the repository dependency
        public AdminController(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        // GET: api/admin
        // Retrieves all tasks from the repository
        [HttpGet]
        public async Task<IEnumerable<PersonalInformation>> GetTask()
        {
            var task = await _adminRepository.GetTask();
            return task;
        }

        // GET: api/admin/{taskId}
        // Retrieves a specific task by its ID
        [HttpGet("{taskId}")]
        public async Task<ActionResult<PersonalInformation>> GetTask(string taskId)
        {
            var task = await _adminRepository.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                return NotFound(); // Returns 404 if the task is not found
            }

            return task;
        }

        // POST: api/admin
        // Creates a new task with the provided information
        [HttpPost]
        public async Task<ActionResult<PersonalInformation>> CreateProgram(PersonalInformation personalInformation)
        {
            var createdTask = await _adminRepository.CreateProgram(personalInformation);
            return CreatedAtAction(nameof(GetTask), new { userId = createdTask.id }, createdTask);
        }

        // PUT: api/admin/{taskId}
        // Updates an existing task with the provided information
        [HttpPut("{taskId}")]
        public async Task<ActionResult<PersonalInformation>> UpdateProgram(string taskId, PersonalInformation personalInformation)
        {
            if (personalInformation == null || personalInformation.id != taskId)
            {
                return BadRequest("Task ID mismatch.");
            }

            var existingTask = await _adminRepository.GetProgramByIdAsync(taskId, personalInformation.id);
            if (existingTask == null)
            {
                return NotFound(); // Returns 404 if the task is not found
            }

            personalInformation.id = existingTask.id; // Preserve the original ID
            var updatedTask = await _adminRepository.UpdateprogramTaskAsync(personalInformation);
            return Ok(updatedTask); // Returns 200 OK with the updated task
        }

        // DELETE: api/admin/{id}
        // Deletes a task by its ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(string id)
        {
            var existingTask = await _adminRepository.GetTaskByIdAsync(id);
            if(existingTask == null)
            {
                return NotFound(); // Returns 404 if the task is not found
            }

            await _adminRepository.DeleteProgramAsync(id);
            return Ok(); // Returns 200 OK with the updated task
        }
    }
}
