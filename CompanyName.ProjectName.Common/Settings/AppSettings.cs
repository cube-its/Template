using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Common.Settings
{
    /// <summary>
    /// Class that holds the application settings.
    /// </summary>
    public class AppSettings
    {
        public IntegrationSettings Integration { get; set; }
        public int SmsCodeExpiryInMinutes { get; set; }
    }

    public class IntegrationSettings
    {
        public CubeIntegrationSettings Cube { get; set; }
    }

    public class CubeIntegrationSettings
    {
        public string BaseApiUrl { get; set; }
    }
}
