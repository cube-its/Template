using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CompanyName.ProjectName.Integration.Rest
{
    class RESTWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).KeepAlive = false;
            }
            request.Timeout = 1 * 60 * 1000;
            return request;
        }
    }
}
