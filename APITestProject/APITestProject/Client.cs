using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APITestProject
{

    public enum Verb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    public class Client
    {
        public string EndPoint { get; set; }
        public Verb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        public string Token { get; set; }

        public Client()
        {
            EndPoint = "";
            Method = Verb.GET;
            ContentType = "application/JSON";
            PostData = "";
        }

        public Client(string endpoint, Verb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "application/JSON";
            PostData = postData;
        }

        public string Request()
        {
            return Request("");
        }

        public string Request(string parameters)
        {
            var responseValue = string.Empty;
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);
                request.Method = Method.ToString();
                if (!string.IsNullOrEmpty(Token))
                    request.Headers.Add("X-Authorization",Token);
                request.ContentType = ContentType;


                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = String.Format("Failed: Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }

                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                {
                    using (var responseStream = ((HttpWebResponse)ex.Response).GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                }

            }
            return responseValue;            
        }
    }

    public class ResponseJson {

        public string type { get; set; }
        public string title { get; set; }
    }
}
