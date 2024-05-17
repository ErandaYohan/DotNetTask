using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using static DotNetTask.Models.InternShipProgram;

namespace DotNetTask.Data
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CosmosClient cosmosClient;
        private readonly IConfiguration configuration;
        private readonly Container _taskContainer;

        public AdminRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            this.cosmosClient = cosmosClient;
            this.configuration = configuration;
            var databaseName = configuration["CosmosDbSettings:DatabaseName"];
            var taskContainerName = "ProgramDetails";
            _taskContainer = cosmosClient.GetContainer(databaseName, taskContainerName);
        }

        public async Task<PersonalInformation> CreateProgram(PersonalInformation personalInformation)
        {
            personalInformation.id = Guid.NewGuid().ToString();
            var response = await _taskContainer.CreateItemAsync(personalInformation);
            return response.Resource;
        }
        public async Task<PersonalInformation> GetTaskByIdAsync(string userId)
        {
            var query = _taskContainer.GetItemLinqQueryable<PersonalInformation>()
            .Where(t => t.id == userId)
            .Take(1)
            .ToQueryDefinition();

            var sqlQuery = query.QueryText; // Retrieve the SQL query

            var response = await _taskContainer.GetItemQueryIterator<PersonalInformation>(query).ReadNextAsync();
            return response.FirstOrDefault();
        }
        public async Task<IEnumerable<PersonalInformation>> GetTask()
        {
            var query = _taskContainer.GetItemQueryIterator<PersonalInformation>();
            List<PersonalInformation> results = new List<PersonalInformation>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }
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
        public async Task<PersonalInformation> UpdateprogramTaskAsync(PersonalInformation personalInformation)
        {
            var response = await _taskContainer.ReplaceItemAsync(personalInformation, personalInformation.id);
            return response.Resource;
        }
        public async Task DeleteProgramAsync(string taskId)
        {
            await _taskContainer.DeleteItemAsync<PersonalInformation>(taskId, new PartitionKey(taskId));
        }
    }
}
