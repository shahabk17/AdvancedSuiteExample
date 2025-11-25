namespace SanlamAutomation
{
    /// <summary>
    /// Handles OTP storage operations in Azure Table Storage
    /// </summary>
    [Author("Shahab Khan")]
    public class OTPStorageAccount
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private static string _connectionString()
        {
            GenericUtils genericUtils = new();

            Logger.Debug("Getting OTP storage connection string");
            return genericUtils.Decrypt(genericUtils.GetDbConnectionString(Properties.environment, "storagebrowserotp_connectionstring"), 3);
        }

        private static readonly AzureTableStorage<OtpTable> _otpTable = new("Otp", _connectionString());

        /// <summary>
        /// Retrieves OTP data for a given phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number to lookup OTP data</param>
        /// <returns>Latest 2 OTP records ordered by timestamp</returns>
        [Author("Shahab Khan")]
        public IEnumerable<OtpTable> GetOtpDataFromPhoneNumber(string phoneNumber)
        {
            Logger.Info($"Retrieving OTP data for phone number: {phoneNumber}");
            var result = _otpTable.QueryAsync(x => x.PartitionKey == phoneNumber)
                    .GetAwaiter()
                    .GetResult()
                    .OrderByDescending(entity => entity.Timestamp)
                    .Take(2);
            Logger.Debug($"Found {result.Count()} OTP records for phone number: {phoneNumber}");
            return result;
        }

        /// <summary>
        /// Retrieves specific column data from OTP table
        /// </summary>
        /// <param name="partitionKey">Phone number as partition key</param>
        /// <param name="rowNumber">Row number to retrieve (1 or 2)</param>
        /// <param name="columnName">Column to retrieve (hitcount/attemptcount/pin)</param>
        /// <returns>Integer value from specified column</returns>
        [Author("Shahab Khan")]
        public int FetchthedatafromthetableAsyncofIntType(string partitionKey, int rowNumber, string columnName)
        {
            Logger.Info($"Fetching {columnName} for partition key: {partitionKey}, row: {rowNumber}");
            try
            {
                var sortedEntities = GetOtpDataFromPhoneNumber(partitionKey);
                int result = 0;

                switch (columnName.ToLower())
                {
                    case "hitcount":
                        result = sortedEntities.Select(entity => entity.HitCount).ToArray()[rowNumber - 1];
                        Logger.Info($"HitCount of row {rowNumber}: {result}");
                        break;
                    case "attemptcount":
                        result = sortedEntities.Select(entity => entity.AttemptCount).ToArray()[rowNumber - 1];
                        Logger.Info($"AttemptCount of row {rowNumber}: {result}");
                        break;
                    case "pin":
                        result = sortedEntities.Select(entity => entity.Pin).ToArray()[rowNumber - 1];
                        Logger.Info($"Pin of row {rowNumber}: {result}");
                        break;
                }

                Report.ChildLog.Log(Status.Info, $"{columnName} of the {rowNumber} otp row: {result}");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error fetching {columnName} data for partition key: {partitionKey}");
                throw;
            }
        }

        /// <summary>
        /// Deletes all OTP records for the specified phone number
        /// </summary>
        /// <param name="partitionKey">Phone number to delete records for</param>
        [Author("Shahab Khan")]
        public void DeleteOtpTableAsync(string partitionKey)
        {
            Logger.Info($"Deleting OTP records for partition key: {partitionKey}");
            _otpTable.DeleteByPartitionKeyAsync(partitionKey);
        }

        /// <summary>
        /// Updates the expiry time of OTP records
        /// </summary>
        /// <param name="partitionKey">Phone number to update records for</param>
        /// <param name="updateFirstOrSecondOtp">1 for second OTP, any other value for first OTP</param>
        [Author("Shahab Khan")]
        public void UpdateOtpExpiryTimeAsync(string partitionKey, int updateFirstOrSecondOtp)
        {
            Logger.Info($"Updating OTP expiry time for partition key: {partitionKey}, OTP number: {updateFirstOrSecondOtp}");
            try
            {
                int expiryTimeInMinutes = 5;
                List<OtpTable> otpList = _otpTable.QueryAsync(x => x.PartitionKey == partitionKey).GetAwaiter().GetResult();
                List<OtpTable> sortedOtpList = otpList.OrderBy(otp => otp.CreatedDate).ToList();

                if (sortedOtpList.Count >= 2)
                {
                    if (updateFirstOrSecondOtp == 1)
                    {
                        OtpTable secondOtp = sortedOtpList[1];
                        secondOtp.ExpiredTime = secondOtp.ExpiredTime.AddMinutes(-expiryTimeInMinutes - 5 + 1);
                        _otpTable.UpdateAsync(secondOtp);
                        Logger.Debug($"Updated second OTP expiry time for partition key: {partitionKey}");
                    }
                    else
                    {
                        OtpTable firstOtp = sortedOtpList[0];
                        firstOtp.ExpiredTime = firstOtp.ExpiredTime.AddMinutes(-expiryTimeInMinutes + 1);
                        _otpTable.UpdateAsync(firstOtp);
                        Logger.Debug($"Updated first OTP expiry time for partition key: {partitionKey}");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error updating OTP expiry time for partition key: {partitionKey}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves the latest OTP entry for a cellphone number within the last 5 minutes with a hit count of 1.
        /// </summary>
        /// <param name="CellphoneNumber"></param>
        /// <returns></returns>
        [Author("Piyush Sharma")]
        public OtpTable GetOTPSInfoFromOTPTable(string CellphoneNumber)
        {
            var OTPList = _otpTable.QueryAsync(x => x.Timestamp > DateTime.UtcNow.AddMinutes(-5) && x.PartitionKey == CellphoneNumber && x.HitCount == 1).GetAwaiter().GetResult();
            return OTPList.OrderByDescending(x => x.Timestamp).FirstOrDefault();
        }
    }
}