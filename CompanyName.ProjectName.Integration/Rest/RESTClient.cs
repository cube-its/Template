using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CompanyName.ProjectName.Integration.Rest
{
    public class RESTClient
    {
        public string GetRequest(string url, string accessToken = "")
        {
            return HandleRequest(url, "GET", accessToken);
        }

        public string PostRequest(string url, string data = "", string accessToken = "")
        {
            return HandleRequest(url, "POST", accessToken, data);
        }

        private string HandleRequest(string url, string method, string accessToken = "", string data = "")
        {
            using (RESTWebClient wc = new RESTWebClient())
            {
                string result = string.Empty;

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";

                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + accessToken;
                    }

                    if (method == "POST")
                    {
                        result = wc.UploadString(url, data);
                    }
                    else if (method == "GET")
                    {
                        result = wc.DownloadString(url);
                    }

                    return result;
                }
                catch (WebException ex)
                {
                    string responseText;

                    var responseStream = ex.Response?.GetResponseStream();

                    if (responseStream != null)
                    {
                        using (var reader = new System.IO.StreamReader(responseStream))
                        {
                            responseText = reader.ReadToEnd();
                            throw new ArgumentException(responseText);
                        }
                    }
                    throw;
                }
            }
        }
    }
}
