namespace IDM.Digitech.Automation.SCS.External.AzureStorageBrowser
{
    /// <summary>
    /// Handles Azure Storage Container operations
    /// </summary>
    public class AzureContainers
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly GenericUtils genericUtils = new();

        /// <summary>
        /// Gets the formatted connection string
        /// </summary>
        private static string _connectionString(string connectionString)
        {
            GenericUtils genericUtils = new();

            Logger.Debug($"Formatting connection string");
            return genericUtils.Decrypt(genericUtils.GetDbConnectionString(Properties.environment, connectionString), 3);
        }

        /// <summary>
        /// Creates an instance of AzureBlobStorage with formatted connection string
        /// </summary>
        AzureBlobStorage blobJson(string connectionString)
        {
            Logger.Debug($"Creating AzureBlobStorage instance");
            return new AzureBlobStorage(_connectionString(connectionString));
        }

        /// <summary>
        /// Retrieves spoofed questions from Azure Blob Storage as JSON array
        /// </summary>
        /// <param name="blobFolderName">Name of the blob folder</param>
        /// <returns>JArray containing spoofed questions</returns>
        [Author("Piyush Sharma")]
        public JArray SpoofedQuestions_JsonArray(string blobFolderName)
        {
            Logger.Info($"Retrieving spoofed questions from folder: {blobFolderName}");

            try
            {
                var blobClient = new BlobClient(
                    _connectionString("sapistoragebrowser_connectionstring"),
                    "user-container",
                    $"{blobFolderName}/SQ-SCS.json");

                Logger.Debug("Downloading blob content");
                BlobDownloadInfo download = blobClient.DownloadAsync().GetAwaiter().GetResult();

                string jsonString;
                using (var reader = new StreamReader(download.Content, Encoding.UTF8))
                {
                    jsonString = reader.ReadToEndAsync().GetAwaiter().GetResult();
                }

                Logger.Debug("Parsing JSON content");
                return JArray.Parse(jsonString);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error retrieving spoofed questions from folder: {blobFolderName}");
                throw;
            }
        }

        /// <summary>
        /// Updates the status code, content, and error message in a JSON blob located in the "spoofed-container" by creating a dictionary of entries to update.
        /// </summary>
        /// <param name="statuscode"></param>
        /// <param name="content"></param>
        /// <param name="filename"></param>
        [Author("Piyush Sharma")]
        public void UpdateSpoofData(int? statuscode, string content, string filename)
        {
            var entriesToUpdate = new Dictionary<string, object>
            {
                { "StatusCode", statuscode },
                { "Content", content },
                { "ErrorMessage", "null" }
            };
            var blob = blobJson("blobstorage_connectionstring");
            blob.UpdateJsonBlob("spoofed-container", filename, entriesToUpdate);
        }

        /// <summary>
        /// Modifies a JSON file in a specified Azure Blob Storage container by downloading its content, updating the value of a specific API name, and then uploading the modified JSON back to the blob.
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="JsonFileName"></param>
        /// <param name="APIName"></param>
        /// <param name="value"></param>
        [Author("Piyush Sharma")]
        public void Update_IsSpoofJson(string containerName, string JsonFileName, string APIName, bool value)
        {
            string connectionString = genericUtils.Decrypt(genericUtils.GetDbConnectionString(Properties.environment, "blobstorage_connectionstring"), 3);
            string FileName = JsonFileName + ".json";

            // Initialize Blob Client

            var blob = blobJson("blobstorage_connectionstring");
            var blobClient = blob.InitilizeBlobServiceClient(connectionString, containerName, FileName);

            // Download blob content as a string

            BlobDownloadInfo download = blobClient.DownloadAsync().GetAwaiter().GetResult();

            string jsonString;

            using (var reader = new StreamReader(download.Content, Encoding.UTF8))
            {
                jsonString = reader.ReadToEndAsync().GetAwaiter().GetResult();
            }

            // Parse JSON

            JArray jsonArray = JArray.Parse(jsonString);

            // Find the LMSLog5 entry and set its value to false

            foreach (JObject item in jsonArray)
            {
                if (item["Name"].ToString() == APIName)
                {
                    item["Value"] = value;
                    break;
                }
            }

            // Convert the modified JArray back to JSON string

            string modifiedJsonString = jsonArray.ToString();

            // Upload the modified JSON back to the blob

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(modifiedJsonString)))
            {
                blobClient.UploadAsync(ms, overwrite: true).GetAwaiter().GetResult();
            }
        }

        public IEnumerable<T> ReadInputData<T>(string environment, string className)
        {
            try
            {
                Console.WriteLine($"[DEBUG] Trying to read data for: {className}, env: {environment}");

                // Blob/file path construction log
                var blobPath = $"{environment}/{className}.json";
                string json = LoadEmbeddedResource("Database.json");
                var dataPath = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json)[environment]["blobstorage_connectionstring"]; 
                Console.WriteLine($"[DEBUG] Blob/File path: {blobPath}");
                Console.WriteLine($"[DEBUG] Data path: {dataPath}");

                // Load logic here...
                var data = new AzureBlobStorage(dataPath).ReadBlobJsonArray<T>("automation-container", "InputData/" + className + ".json").GetAwaiter().GetResult();
                Console.WriteLine($"[DEBUG] Loaded {data.Count()} records");

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to load input data: {ex}");
                throw;
            }
        }

        public string GetDataPath(string folderName)
        {
            return Path.Combine(Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent.Parent.FullName, folderName);
        }

        public static string LoadEmbeddedResource(string resourceFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // Find full resource name (namespace + folder + filename)
            string resourceFullName = assembly.GetManifestResourceNames()
                .FirstOrDefault(r => r.EndsWith(resourceFileName, StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(resourceFullName))
                throw new FileNotFoundException($"Embedded resource '{resourceFileName}' not found.");

            using (var stream = assembly.GetManifestResourceStream(resourceFullName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
