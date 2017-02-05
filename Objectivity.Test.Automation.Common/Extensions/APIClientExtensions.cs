namespace Objectivity.Test.Automation.Common.Extensions
{
    using System;
    using System.IO;
    using NLog;
    using System.Net;
    using System.Net.Cache;
    using System.Security.Cryptography;
    using System.Text;
    using System.Globalization;

    /// <summary>
    /// Type of HTTP request method
    /// </summary>
    public enum HTTPMethod
    {
        /// <summary>HTTP Get</summary>
        GET,

        /// <summary>HTTP POST</summary>
        POST,

        /// <summary>HTTP PUT</summary>
        PUT,

        /// <summary>HTTP DELETE</summary>
        DELETE
    }

    /// <summary>
    /// Service for REST api
    /// </summary>
    public class APIClientExtensions
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private HttpStatusCode httpStatusResponse;
        /// <summary>
        /// Initializes a new instance of the <see cref="APIClientExtensions"/> class.
        /// </summary>
        public APIClientExtensions()
        {
            this.EndPoint = string.Empty;
            this.Method = HTTPMethod.GET;
            this.ContentType = "application/JSON";
            this.Accept = "application/json";
            this.PostData = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="APIClientExtensions"/> class.
        /// </summary>
        /// <param name="endpoint">endpoint string</param>
        /// <param name="method">method with HTTPMethod enum</param>
        /// <param name="postData">post data string</param>
        public APIClientExtensions(string endpoint, HTTPMethod method, string postData)
        {
            this.EndPoint = endpoint;
            this.Method = method;
            this.ContentType = "text/json";
            this.PostData = postData;
        }

        /// <summary>
        /// Gets HTTP Status from the response
        /// </summary>
        public HttpStatusCode HttpStatusResponse
        {
            get
            {
                return httpStatusResponse;
            }
        }

        /// <summary>
        /// Gets or sets EndPoint
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        /// Gets or sets Method with HTTPMethod enum
        /// </summary>
        public HTTPMethod Method { get; set; }

        /// <summary>
        /// Gets or sets ContentType
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets Accept
        /// </summary>
        public string Accept { get; set; }

        /// <summary>
        /// Gets or sets PostData
        /// </summary>
        public string PostData { get; set; }

        /// <summary>
        /// Sending a request
        /// </summary>
        /// <returns>string value</returns>
        public string Request()
        {
            return this.SendRequest(string.Empty);
        }

        /// <summary>
        /// Sending a request
        /// </summary>
        /// <param name="query">query of the request</param>
        /// <returns>string value</returns>
        public string SendRequest(string query)
        {
            // set up protocol
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            // initialize request
            var uri = this.EndPoint + query;
            var request = (HttpWebRequest)WebRequest.Create(uri);

            // set request details
            request.Method = this.Method.ToString();
            request.ContentType = this.ContentType;
            request.Accept = this.Accept;
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
            request.KeepAlive = true;
            request.ServicePoint.ConnectionLimit = 1;

            // Set Header
            var apiSignature = GetSignature(query, BaseConfiguration.GetApiKey);
            request.Headers.Add("api-auth-id:" + BaseConfiguration.GetApiId);
            request.Headers.Add("api-auth-signature:" + apiSignature);

            // Set ContentLength and body
            request = SetupContentTypeAndRequestBody(request);


            var responseValue = string.Empty;
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    this.httpStatusResponse = response.StatusCode;

                    if (httpStatusResponse != HttpStatusCode.OK)
                    {
                        var message = string.Format("Received HTTP Status code: {0}", httpStatusResponse);
                        Logger.Warn(CultureInfo.CurrentCulture, message);
                    }

                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                        }
                    }

                    return responseValue;
                }
            }
            catch (WebException e)
            {
                using (WebResponse response = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;

                    this.httpStatusResponse = httpResponse.StatusCode;

                    var message = string.Format("Received HTTP Status code: {0}", httpStatusResponse);
                    Logger.Warn(CultureInfo.CurrentCulture, message);

                    using (var responseStream = httpResponse.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                        }
                    }

                    return responseValue;
                }
            }
        }

        private HttpWebRequest SetupContentTypeAndRequestBody(HttpWebRequest request)
        {
           if (request.Method.Equals("POST"))
            {
                request.ContentLength = PostData.Length;
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(PostData);
                Stream newStream = request.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();
            }

            return request;
        }

        private string GetSignature(string parameter, string apikey)
        {
            if (parameter.StartsWith("?"))
            {
                parameter= parameter.Remove(0, 1);
            }
            else
            {
                parameter = string.Empty;
            }

            var encoding = new System.Text.ASCIIEncoding();
            byte[] key = encoding.GetBytes(apikey);
            var myhmacsha256 = new HMACSHA256(key);
            byte[] hashValue = myhmacsha256.ComputeHash(encoding.GetBytes(parameter));
            string hmac64 = Convert.ToBase64String(hashValue);
            myhmacsha256.Clear();
            return hmac64;
        }

    }
}
