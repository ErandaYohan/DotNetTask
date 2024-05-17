using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using static DotNetTask.Models.InternShipProgram;

namespace DotNetTask.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CosmosClient cosmosClient; // Cosmos client instance
        private readonly IConfiguration configuration; // Configuration instance
        private readonly Container _taskContainer; // Cosmos container instance

        // Constructor to initialize CosmosClient and IConfiguration
        public AdminRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            this.cosmosClient = cosmosClient;
            this.configuration = configuration;
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var taskContainerName = "ProgramDetails";
            _taskContainer = cosmosClient.GetContainer(databaseName, taskContainerName);
        }

        // Method to create a new program
        public async Task<PersonalInformation> CreateProgram(PersonalInformation personalInformation)
        {
            personalInformation.id = Guid.NewGuid().ToString(); // Generate a new GUID for the program
            var response = await _taskContainer.CreateItemAsync(personalInformation);
            return response.Resource; // Return created program
        }

        // Method to retrieve a program by ID
        public async Task<PersonalInformation> GetTaskByIdAsync(string userId)
        {
            // Define LINQ query to retrieve program by ID
            var query = _taskContainer.GetItemLinqQueryable<PersonalInformation>()
            .Where(t => t.id == userId)
            .Take(1)
            .ToQueryDefinition();

            var sqlQuery = query.QueryText; // Retrieve the SQL query

            var response = await _taskContainer.GetItemQueryIterator<PersonalInformation>(query).ReadNextAsync();
            return response.FirstOrDefault(); // Return first result
        }

        // Method to retrieve all programs
        public async Task<IEnumerable<PersonalInformation>> GetTask()
        {
            var query = _taskContainer.GetItemQueryIterator<PersonalInformation>(); // Get iterator for all items
            List<PersonalInformation> results = new List<PersonalInformation>(); // List to store results
            while (query.HasMoreResults) // Iterate through all results
            {
                var response = await query.ReadNextAsync(); // Read next batch of results
                results.AddRange(response.ToList()); // Add results to list
            }
            return results; // Return all results
        }

        // Method to retrieve a program by ID and user ID
        public async Task<PersonalInformation> GetProgramByIdAsync(string taskId, string userId)
        {
            var query = _taskContainer.GetItemLinqQueryable<PersonalInformation>()
            .Where(t => t.id == userId)
            .Take(1)
            .ToQueryDefinition();

            var sqlQuery = query.QueryText; // Retrieve the SQL query

            var response = await _taskContainer.GetItemQueryIterator<PersonalInformation>(query).ReadNextAsync();
            return response.FirstOrDefault();
        }

        // Method to update a program task
        public async Task<PersonalInformation> UpdateprogramTaskAsync(PersonalInformation personalInformation)
        {
            var response = await _taskContainer.ReplaceItemAsync(personalInformation, personalInformation.id); // Replace item in CosmosDB
            return response.Resource; // Return updated program
        }

        // Method to delete a program by ID

        public async Task DeleteProgramAsync(string taskId)
        {
            await _taskContainer.DeleteItemAsync<PersonalInformation>(taskId, new PartitionKey(taskId)); // Delete item from CosmosDB
        }
    }
}
