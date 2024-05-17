using DotNetTask.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static DotNetTask.Models.InternShipProgram;

namespace DotNetTask.Controllers
{
    /*Developer - Eranda Yohan*/
    /*Date - 5-17-2014 */
    /*Email  - ErandaYohan1234@gmail.com */

    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository _candidateRepository;

        //Constructor to inject the repository dependency
        public CandidateController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        // GET: api/admin/{taskId}
        // Retrieves a specific task by its ID
        [HttpGet("{taskId}")]
        public async Task<ActionResult<PersonalInformation>> GetTask(string taskId)
        {
            var task = await _candidateRepository.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                return NotFound(); // Returns 404 if the task is not found
            }

            return task;
        }

        // GET: api/admin/{taskId}
        // Retrieves a specific task by its ID in Candidate Table
        [HttpGet("{candidateId}/task")]
        public async Task<ActionResult<PersonalInformation>> GetCandidateTask(string candidateId)
        {
            var task = await _candidateRepository.GetTaskByIdCandidateAsync(candidateId);
            if (task == null)
            {
                return NotFound(); // Returns 404 if the task is not found
            }

            return task;
        }

        // PUT: api/admin/{taskId}
        // Retrieves a specific task by its ID And Create New Record.
        [HttpPut("{taskId}")]
        public async Task<ActionResult<PersonalInformation>> CreateCandidateInformation(string taskId, PersonalInformation personalInformation)
        {
            if (personalInformation == null || personalInformation.id != taskId)
            {
                return BadRequest("Task ID mismatch.");
            }

            var existingTask = await _candidateRepository.GetTaskByIdAsync(personalInformation.id);
            if (existingTask == null)
            {
                return NotFound(); // Returns 404 if the task is not found
            }

            personalInformation.id = existingTask.id; // Preserve the original ID
            var createdTask = await _candidateRepository.CreateAsNewRecord(personalInformation);
            return CreatedAtAction(nameof(GetCandidateTask), new { userId = createdTask.id }, createdTask);
        }
    }
}
