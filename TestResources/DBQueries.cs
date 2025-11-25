namespace SanlamAutomation.TestResources
{
    public class DBQueries
    {
        public string GetOtpQuery(string phoneNumber) =>
            $"SELECT TOP 1 OTP FROM [dbo].[Otp] WHERE [To] = '{phoneNumber}' ORDER BY createddate DESC";

        public string BasicverificationfromDBQuery(string idNumber) =>
            $"SELECT * FROM [dbo].[ExternalCommLog] WHERE ExternalCommLogTypeId = 3 AND IdNumber = '{idNumber}'";

        public string UpdateSPLUserDecision(string userId, string decision) =>
            $"UPDATE [dbo].[UserSPLQualificationDecision] SET Decision = '{decision}' WHERE UserId = '{userId}'";

        public string UpdateSPLJsonDecisionReasons(string userId, string decision) =>
            $"UPDATE [dbo].[UserSPLQualificationDecision] SET JsonDecisionReasons = '{decision}' WHERE UserId = '{userId}'";

        public string GetCreditHistoryQuery(string idNumber) =>
            $"SELECT * FROM [dbo].[CreditHistory] WHERE IdNumber = '{idNumber}' ORDER BY createddate DESC";

        public string GetKeynameValuefromCreditHistory(string creditHistoryId, string keyName) =>
            $"SELECT * FROM [CreditHistory].[OtherDetail] WHERE CreditHistoryId = '{creditHistoryId}' AND KeyName = '{keyName}'";

        public string GetCreditHistoryIDfromDB(string idNumber) =>
            $"SELECT TOP 1 * FROM [dbo].[CreditHistory] WHERE IdNumber = '{idNumber}' ORDER BY createddate DESC";

        public string Dbo_UserTable(string idNumber) =>
            $"SELECT * FROM [dbo].[User] WHERE IdNumber = '{idNumber}'";

        public string getLesDecisionFromUserSPLQualificationDecision(string userId) =>
            $"SELECT * FROM [dbo].[UserSPLQualificationDecision] WHERE UserId = '{userId}'";

        public string DBC_Client_Conversion(string idNumber) =>
            $"SELECT TOP 1 * FROM [dbo].[DbQuoteInfo] WHERE IdNumber = '{idNumber}' ORDER BY CreatedDate DESC";

        public string UpdateCreditCoachScore_PersonalLoan(string creditHistoryId, string keyName, string keyVal) =>
            $"UPDATE [CreditHistory].[OtherDetail] SET KeyVal = '{keyVal}' WHERE CreditHistoryId = '{creditHistoryId}' AND KeyName = '{keyName}'";

        public string GetCampaignSource(string idNumber) =>
            $"SELECT TOP 1 ECL.*, ECLT.[Type] FROM [dbo].[ExternalCommLog] ECL LEFT JOIN [dbo].[ExternalCommLogType] ECLT ON ECL.ExternalCommLogTypeId = ECLT.Id WHERE ECL.IdNumber = '{idNumber}' AND ECL.ExternalCommLogTypeId = 5 AND ECL.ResponseData = '{{\"message\":\"Complete\"}}' ORDER BY ECL.ResponseTime DESC;";

        public string UpdateTotalCurrentBalance(string creditHistoryId, string totalCurrentBalance) =>
            $"UPDATE [CreditHistory].[CreditHealthInfo] SET TotalCurrentBalance = '{totalCurrentBalance}' WHERE CreditHistoryId = '{creditHistoryId}'";

        public string GetCreditHealthInfo(string creditHistoryId) =>
            $"SELECT * FROM [CreditHistory].[CreditHealthInfo] WHERE CreditHistoryId = '{creditHistoryId}'";

        public string GetCreditCoachScore(string creditHistoryId) =>
            $"SELECT * FROM [CreditHistory].[OtherDetail] WHERE CreditHistoryId = '{creditHistoryId}' AND KeyName = 'CreditCoachScorePerc'";

        public string GetRegistrationSource(string idNumber) =>
            $"SELECT * FROM [dbo].[UserSetting] WHERE IdNumber = '{idNumber}'";

        public string GetExternalLead(string userId) =>
            $"SELECT * FROM [dbo].[ExternalLead] WHERE UserId = '{userId}'";

        public string GetSPLLESInformation(string creditHistoryId) =>
            $"SELECT * FROM [CreditHistory].[SPLLESInformation] WHERE CreditHistoryId = '{creditHistoryId}'";

        public string UpdateSPLLESInformation(string creditHistoryId, string creditCoachScorePersonalLoan) =>
            $"UPDATE [CreditHistory].[SPLLESInformation] SET STTS_LTST_AL_AL_DTRV_AL = '-2', CreditCoachScore_PersonalLoan = '{creditCoachScorePersonalLoan}', AGEY_BRTH_AL_AL_ALLT_AL = '60', NUMB_LTST_AL_AL_ALLT_C9 = '0', NUMB_LTST_AL_AL_ALLT_6p = '1', AGEM_OLDT_AL_AL_ALLT_AL = '7', TTBL_OPNG_AL_AL_ALLT_AL = '6000' WHERE CreditHistoryId = '{creditHistoryId}'";

        public string FetchPhoneNumberfromDB() =>
            "SELECT TOP 10 * FROM [dbo].[User] WHERE IsActive = 1 AND PhoneNumberConfirmed = 'true'";

        public string FetchIdNumberfromDB() =>
            "SELECT DISTINCT TOP 10 u.* FROM [dbo].[User] u JOIN [dbo].[CreditHistory] ch ON u.IdNumber = ch.IdNumber WHERE u.IsActive = 'true' AND u.IdNumber LIKE '[1-9]%' AND u.GrossIncome IS NOT NULL ORDER BY u.ActivatedDate DESC";

        public string UpdateScorefromDB(string creditScore, string creditHistoryId) =>
            $"UPDATE [CreditHistory].[ScoreInformation] SET ScorePercent = '{creditScore}' WHERE CreditHistoryId = '{creditHistoryId}'";

        public string ScoreInformationTable(string creditId) =>
            $"SELECT TOP 1 * FROM [CreditHistory].[ScoreInformation] WHERE CreditHistoryId = '{creditId}' ORDER BY createddate DESC";

        public string WealthScoreTable(string userId) =>
            $"SELECT * FROM [dbo].[UserWealth] WHERE UserId = '{userId}'";

        public string DeleteUserWealthEntry(string userId) =>
            $"DELETE [dbo].[UserWealth] WHERE UserId = '{userId}'";

        public string UserBudgetTable(string userId) =>
            $"SELECT * FROM [dbo].[UserBudget] WHERE UserId = '{userId}'";

        public string FaqTable() =>
            "SELECT * FROM [dbo].[Faq] WHERE IsActive = '1'";

        public string SapiVisitLogTable(string userId) =>
            $"SELECT * FROM SapiVisitLog WHERE UserId = '{userId}'";

        public string FetchIdnumberHavingNumberOfMonthsCreditHistory(int numberOfMonths) =>
            $"SELECT IdNumber, COUNT(DISTINCT Month) AS MonthCount FROM [dbo].[CreditHistory] WHERE IdNumber LIKE '[1-9]%' GROUP BY IdNumber HAVING COUNT(DISTINCT Month) > '{numberOfMonths}'";

        public string FetchCreditScoreForPreviousThreeMonths(string userId) =>
            $"SELECT TOP 3 * FROM [CreditHistory].[ScoreInformation] WHERE UserId = '{userId}' ORDER BY Year DESC, Month DESC";

        public string JudgementsAndLegalActionTable(string creditHistoryId, string cardTitle) =>
            $"SELECT * FROM [CreditHistory].[JudgmentInformation] WHERE CreditHistoryId = '{creditHistoryId}' AND Plaintiff = '{cardTitle}'";

        public string DebtRestructureReviewTable(string creditHistoryId, string cardTitle)
        {
            var genericUtils = new GenericUtils();
            string firstName = genericUtils.SplitString(cardTitle, " ", 0);
            string lastName = genericUtils.SplitString(cardTitle, " ", 1);
            return $"SELECT * FROM [CreditHistory].[DebtRestructureReview] WHERE CreditHistoryId = '{creditHistoryId}' AND Counsellor_First_Name = '{firstName}' AND Counsellor_Last_Name = '{lastName}'";
        }

        public string AccountInformationTable(string creditHistoryId, string cardTitle, string accNumber) =>
            $"SELECT * FROM [CreditHistory].[AccountInformation] WHERE CreditHistoryId = '{creditHistoryId}' AND Name = '{cardTitle}' AND Account_No = '{accNumber}'";

        public string GetResponseUrl(string userId) =>
            $"SELECT * FROM ExternalLead WHERE UserId = '{userId}' ORDER BY CreatedDate DESC";

        public string QuestionQuery(string idNumber) =>
            $"DECLARE @json NVARCHAR(MAX) SET @json = (SELECT [SecurityQuestionJson] from [SecurityQuestionAnswer] where idNumber='{idNumber}') SELECT * FROM OPENJSON ( @json ) WITH (Question varchar(2000) '$.Question', CurrectAnswer0 varchar(20) '$.SecurityAnswerIds[0]', CurrectAnswer1 varchar(20) '$.SecurityAnswerIds[1]', CurrectAnswer2 varchar(20) '$.SecurityAnswerIds[2]', CurrectAnswer3 varchar(20) '$.SecurityAnswerIds[3]')";

        public string GetExternalCommLog(string idNumber, int externalCommLogTypeId) =>
            $"SELECT * FROM [dbo].[ExternalCommLog] WHERE ExternalCommLogTypeId = {externalCommLogTypeId} AND IdNumber = '{idNumber}' order by RequestTime desc";

        public string UpdateDBC_Client_Conversion(string userId, string dBClient, string dBC_Conversion) =>
           $"UPDATE [dbo].[DbQuoteInfo] SET DBClient = '{dBClient}', DBC_Conversion = '{dBC_Conversion}' WHERE Userid = '{userId}'";

        public static string DeleteUser(string idNumber) =>
           $"declare @idnumber nvarchar(100) = '{idNumber}' delete from[User] where idnumber = @idnumber delete from SecurityQuestionAnswer where idnumber = @idnumber delete from UserSetting where idnumber = @idnumber delete from ExternalCommLog where idnumber = @idnumber delete from CreditHistory where idnumber = @idnumber";

        public string FetchCustomEvents(string elementId, string userId, string datetime) =>
           $"customEvents | where timestamp > datetime('{datetime}') | where customDimensions contains \"{elementId}\" and customDimensions contains \"{userId}\" | top 1 by timestamp desc";
        public string FetchException(string datetime) =>
          $"exceptions | where timestamp > datetime('{datetime}') | where operation_Name == \"SendTrackingEventFunction\" | top 1 by timestamp desc";

        public static string DeleteCreditHistoryOfCurrentMonth(string idNumber) =>
          $"DELETE FROM [dbo].[CreditHistory] WHERE idnumber = '{idNumber}' and  Month = FORMAT(GETDATE(), 'MM')";

        public static string UpdateCreditHistoryIsActive(string idNumber, int isActive, string month) =>
          $"UPDATE [dbo].[CreditHistory] SET  isActive = {isActive} WHERE idnumber = '{idNumber}' and  Month = '{month}'";

        public string GetBranchInfo(string branchId) =>
          $"select * from [dbo].[Branch] where id='{branchId}'";

        public string FetchTempPassword(string idNumber) =>
          $"traces | order by timestamp desc | where customDimensions.MessageTemplate contains 'Temporary password for BranchUser' | where tostring(customDimensions.IdNumber) == '{idNumber}' | where timestamp >= ago(5m) | take 1";

        public string DeleteFromExternalCommLog(string idNumber, int ExternalCommLogTypeId) =>
          $"delete from [dbo].[ExternalCommLog] where IdNumber='{idNumber}' and ExternalCommLogTypeId={ExternalCommLogTypeId}";

        public string FetchDCQuoteInfo(string creditHistoryId) =>
          $"select * from [dbo].[dbquoteinfo] where credithistoryid='{creditHistoryId}'";

        public string UpdateUserActiveStatus(string idNumber, string isActiveStatus) =>
          $"update [dbo].[User] set IsActive='{isActiveStatus}' where idnumber='{idNumber}'";

        public string FetchLeadLog() =>
          "select * from [dbo].[LeadLog] order by createddate desc";

        public string FetchCampaignDetails(string campaignUtmId) =>
          $"select * from [dbo].[campaignlookup] where CampaignUtmId = '{campaignUtmId}' order by createddate desc";
    }
}