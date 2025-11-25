namespace SanlamAutomation
{
    public class Properties
    {
        public static string environment;
        public static string folderName;

        ///* Environment */

        ////public static string environment = "preprod";
        //public static string environment = "preprod";

        ///* Report Folder path*/

        //// Reports required in local machine 
        //// D:\a\1\s\
        //public static string folderName = "C:\\Users\\shaha\\Report";

        //common password
        public static string password = "Abcd1234#";

        //Browser headless mode
        public static bool isHeadless = false;

        //download folder path
        public static string downloadFolder = "c:\\users\\VssAdministrator\\Downloads";

        static Properties()
        {
            var utils = new GenericUtils();

            string testSettingsPath = utils.GetDataPath("TestResources") + "\\TestData\\testsettings.json";
            JObject settings = utils.GetJson(testSettingsPath);
            environment = settings["Environment"]?.ToString() ?? "";
            folderName = settings["FolderName"]?.ToString() ?? "";
        }
    }
}