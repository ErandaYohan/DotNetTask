using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using static DotNetTask.Models.InternShipProgram;

namespace DotNetTask.Data
{
    // Repository class for candidate operations
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CosmosClient cosmosClient; // Cosmos client instance
        private readonly IConfiguration configuration; // Configuration instance
        private readonly Container _taskContainer; // Cosmos container instance for existing records
        private readonly Container _taskPersonContainer; // Cosmos container instance for new records

        // Constructor to initialize CosmosClient and IConfiguration
        public CandidateRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            this.cosmosClient = cosmosClient;
            this.configuration = configuration;
            var databaseName = configuration["CosmosDbSettings:DatabaseName"]; 
            var taskContainerName = "ProgramDetails";
            var taskCandidateContainerName = "PersonalProgram";
            _taskContainer = cosmosClient.GetContainer(databaseName, taskContainerName); // Get container reference for existing records
            _taskPersonContainer = cosmosClient.GetContainer(databaseName, taskCandidateContainerName); // Get container reference for new records
        }

        // Method to retrieve a task by ID
        public async Task<PersonalInformation> GetTaskByIdAsync(string userId)
        {
            // Define LINQ query to retrieve task by ID
            var query = _taskContainer.GetItemLinqQueryable<PersonalInformation>()
                .Where(t => t.id == userId)
                .Take(1)
                .ToQueryDefinition();

            var sqlQuery = query.QueryText; // Retrieve the SQL query

            var response = await _taskContainer.GetItemQueryIterator<PersonalInformation>(query).ReadNextAsync(); // Execute query
            return response.FirstOrDefault(); // Return first result
        }

        // Method to retrieve a task by ID in Candidate Table
        public async Task<PersonalInformation> GetTaskByIdCandidateAsync(string userId)
        {
            // Define LINQ query to retrieve task by ID
            var query = _taskContainer.GetItemLinqQueryable<PersonalInformation>()
                .Where(t => t.id == userId)
                .Take(1)
                .ToQueryDefinition();

            var sqlQuery = query.QueryText; // Retrieve the SQL query

            var response = await _taskPersonContainer.GetItemQueryIterator<PersonalInformation>(query).ReadNextAsync(); // Execute query
            return response.FirstOrDefault(); // Return first result
        }

        // Method to create a new record as a new item
        public async Task<PersonalInformation> CreateAsNewRecord(PersonalInformation personalInformation)
        {
            personalInformation.id = Guid.NewGuid().ToString(); // Generate a new GUID for the record
            var response = await _taskPersonContainer.CreateItemAsync(personalInformation); // Create record item in CosmosDB
            return response.Resource; // Return created record
        }
    }
}