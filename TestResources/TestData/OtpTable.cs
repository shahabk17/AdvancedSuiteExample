using IDM.DigiTech.Common.Automation.Entity;

namespace JMAutomation.TestResources.OtpTable
{
    public class OtpTable : BaseTableEntity
    {
        public int AttemptCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int HitCount { get; set; }
        public bool IsVerified { get; set; }
        public String ObjectId { get; set; }
        public String OtpObjectiveId { get; set; }
        public String OtpTypeId { get; set; }
        public int Pin { get; set; }
        public String SessionCloseReason { get; set; }
        public String Provider { get; set; }
        public String VerifiedDate { get; set; }
        public Guid SessionId { get; set; }
    }
}
