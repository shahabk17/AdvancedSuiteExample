using IDM.DigiTech.Common.Automation.Entity;

namespace SanlamAutomation.TestResources
{
    public class StorageBrowserTable : BaseTableEntity
    {
        public DateTime Timestamp { get; set; }
        public string IdNumber { get; set; }
        public bool IsDataAvailable { get; set; }
        public bool IsSpoofed { get; set; }
        public string LogType { get; set; }
        public int LogTypeId { get; set; }
        public string Platform { get; set; }
        public int PlatformId { get; set; }
        public string RequestParam { get; set; }
        public string ResponseData { get; set; }
        public string campaign_source { get; set; }
        public int ResponseCode { get; set; }
        public DateTime RequestTime { get; set; }
        public Guid UserId { get; set; }
        public string Endpoint { get; set; }
    }
}

