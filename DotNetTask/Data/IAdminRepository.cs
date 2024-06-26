﻿using static DotNetTask.Models.InternShipProgram;

namespace DotNetTask.Data
{
    public interface IAdminRepository
    {
        /// <summary>
        /// Interface for handling administrative tasks related to internship programs.
        /// Provides methods to create, retrieve, update, and delete program tasks.
        /// </summary>
        Task<PersonalInformation> CreateProgram(PersonalInformation personalInformation);
        Task<PersonalInformation> GetTaskByIdAsync(string TaskId);
        Task<IEnumerable<PersonalInformation>> GetTask();
        Task<PersonalInformation> GetProgramByIdAsync(string taskId, string userId);
        Task<PersonalInformation> UpdateprogramTaskAsync(PersonalInformation personalInformation);
        Task DeleteProgramAsync(string taskId);
    }
}
