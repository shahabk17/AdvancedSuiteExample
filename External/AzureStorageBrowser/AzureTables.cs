namespace IDM.Digitech.Automation.SCS.External.AzureStorageBrowser
{
    /// <summary>
    /// Handles Azure Table Storage operations
    /// </summary>
    /// <author>Shahab Khan</author>
    public class AzureTables
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly GenericUtils genericUtils = new();

        /// <summary>
        /// Gets the formatted connection string for Azure Table Storage
        /// </summary>
        private static string _connectionString()
        {
            Logger.Debug("Getting table storage connection string");
            GenericUtils genericUtils = new GenericUtils();
            return genericUtils.Decrypt(genericUtils.GetDbConnectionString(Properties.environment, "storagebrowser_connectionstring"), 3);
        }

        private static readonly AzureTableStorage<StorageBrowserTable> _exterCommLogTable = new("ExternalCommLogs", _connectionString());

        /// <summary>
        /// Retrieves external communication log entries for a specific ID number
        /// </summary>
        /// <param name="IdNumber">The ID number to query</param>
        /// <param name="currentTimeUtc">Current UTC timestamp</param>
        /// <returns>Collection of StorageBrowserTable entries</returns>
        public async Task<IEnumerable<StorageBrowserTable>> GetExternalCommLogTableEntries(string IdNumber, DateTime currentTimeUtc)
        {
            Logger.Info($"Retrieving external comm log entries for ID: {IdNumber}");
            try
            {
                var data = await _exterCommLogTable.QueryAsync(x => x.IdNumber == IdNumber && x.Timestamp >= currentTimeUtc);
                Logger.Debug($"Found {data.Count()} entries for ID: {IdNumber}");
                return data.OrderByDescending(entity => entity.Timestamp).Take(3);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error retrieving external comm log entries for ID: {IdNumber}");
                throw;
            }
        }

        /// <summary>
        /// Deletes all entries for a specific ID number
        /// </summary>
        /// <param name="IdNumber">The ID number whose entries should be deleted</param>
        public async Task DeleteByIdNumber(string IdNumber)
        {
            Logger.Info($"Deleting entries for ID: {IdNumber}");
            try
            {
                TableClient tableClient = GetTableClient();
                foreach (StorageBrowserTable item in await tableClient.QueryAsync((StorageBrowserTable x) => x.IdNumber == IdNumber).ToListAsync())
                {
                    await tableClient.DeleteEntityAsync(item.PartitionKey, item.RowKey);
                    Logger.Debug($"Deleted entry with PartitionKey: {item.PartitionKey}, RowKey: {item.RowKey}");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error deleting entries for ID: {IdNumber}");
                throw;
            }
        }

        /// <summary>
        /// Reads blob file data from a specific path
        /// </summary>
        /// <param name="fileName">Name of the file to read</param>
        /// <returns>Content of the blob file as string</returns>
        public string ReadBlobFileData(string fileName, string platformLog, string logContainer)
        {
            Logger.Info($"Reading blob file: {fileName}");
            try
            {
                GenericUtils genericUtils = new GenericUtils();
                var result = new AzureBlobStorage(genericUtils.Decrypt(genericUtils.GetDbConnectionString(Properties.environment, "storagebrowser_connectionstring"), 3))
                    .ReadBlobFile(platformLog, logContainer + fileName + ".json")
                    .GetAwaiter()
                    .GetResult();
                Logger.Debug($"Successfully read blob file: {fileName}");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error reading blob file: {fileName}");
                return null;
            }
        }

        /// <summary>
        /// This method retrieves recent referral log information from the ReferralLogTable where the timestamp is within the last two minutes, returning the most recent entry.
        /// </summary>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public StorageBrowserTable GetExternalCommLogInfo(string idNumber, int logTypeId, int platformId)
        {
            var ExternalCommLogList = _exterCommLogTable.QueryAsync(x => x.IdNumber == idNumber && x.LogTypeId == logTypeId && x.PlatformId == platformId && x.Timestamp < DateTime.UtcNow).GetAwaiter().GetResult();
            return ExternalCommLogList.OrderByDescending(x => x.Timestamp).FirstOrDefault();
        }

        /// <summary>
        /// Gets the TableClient for ExternalCommLogs
        /// </summary>
        /// <returns>TableClient instance</returns>
        private TableClient GetTableClient()
        {
            Logger.Debug("Creating TableClient for ExternalCommLogs");
            return new TableServiceClient(_connectionString()).GetTableClient("ExternalCommLogs");
        }
    }
}
