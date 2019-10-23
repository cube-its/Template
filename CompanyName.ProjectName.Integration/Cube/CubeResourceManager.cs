using CompanyName.ProjectName.Common.Settings;
using CompanyName.ProjectName.Integration.Rest;
using CompanyName.ProjectName.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace CompanyName.ProjectName.Integration
{
    public class CubeResourceManager : ICubeResourceManager
    {
        private readonly string _baseApiUrl;
        private readonly AppSettings _appSettings;

        public CubeResourceManager(IOptions<AppSettings> appSettingsAccessor)
        {
            _appSettings = appSettingsAccessor.Value;
            _baseApiUrl = _appSettings.Integration.Cube.BaseApiUrl;
        }

        public bool PhoneExists(string phone)
        {
            try
            {
                string apiPath = $"visitors/availability/phone/{phone}";
                string result = new RESTClient().GetRequest(_baseApiUrl + apiPath);
                return Convert.ToBoolean(JsonConvert.DeserializeObject<PhoneAvailability>(result).Data);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string SendSmsVerificationCode(string phone)
        {
            string apiPath = $"utils/phone/sms/{phone}/4";
            string result = new RESTClient().PostRequest(_baseApiUrl + apiPath);
            return JsonConvert.DeserializeObject<SmsVerificationCode>(result).Code;
        }
    }
}
