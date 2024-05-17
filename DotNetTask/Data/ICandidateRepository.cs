using static DotNetTask.Models.InternShipProgram;

namespace DotNetTask.Data
{
    public interface ICandidateRepository
    {
        /// <summary>
        /// Interface for handling Candidate tasks related to internship programs.
        /// Provides methods to create, retrieve program tasks.
        /// </summary>
        Task<PersonalInformation> GetTaskByIdAsync(string userId);
        Task<PersonalInformation> GetTaskByIdCandidateAsync(string userId);
        Task<PersonalInformation> CreateAsNewRecord(PersonalInformation task);
    }
}
