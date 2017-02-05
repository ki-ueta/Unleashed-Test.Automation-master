namespace Objectivity.Test.Automation.ServiceApi
{
    using System;
    using System.Globalization;
    using Common.WebElements.Kendo;
    using NLog;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using System.Net;

    class CustomerDetails
    {
        public string Guid { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
    }

    public class CustomersAPI
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();        
        
        private APIClientExtensions request;

        private JObject customerDetailsJson;

        public CustomersAPI(DriverContext driverContext)
        {
            request = new APIClientExtensions();
            request.EndPoint = BaseConfiguration.GetApiURI + "/Customers";
        }

        public void CreateNewCustomer(Guid customerGuid , string customerCode)
        {
            var customerId = customerGuid.ToString();

            // Create customer details object
            CustomerDetails customerDetails = new CustomerDetails();
            customerDetails.Guid = customerId;
            customerDetails.CustomerCode = customerCode;
            customerDetails.CustomerName = "ABC Industry";

            // Serialise Customer Details  
            string postData = JsonConvert.SerializeObject(customerDetails);

            // set up request details
            request.Method = HTTPMethod.POST;
            request.PostData = postData;

            // send request to create new customer
            var response = request.SendRequest("/" + customerId);

            // save responded customer details
            this.customerDetailsJson = JObject.Parse(response);
        }

        public void GetCustomerDetails(Guid customerGuid)
        {
            // set up request details
            request.Method = HTTPMethod.GET;

            // send request to get customer details
            var customerId = customerGuid.ToString();
            var response = request.SendRequest("/" + customerId);

            // save responded customer details
            this.customerDetailsJson = JObject.Parse(response);
        }

        public string ExtractCustomerCode()
        {
            // extract customer code from response message
            var customerCode = (string)this.customerDetailsJson["CustomerCode"];
            return customerCode;
        }

        public string ExtractErrorField()
        {
            // extract Error field from response message
            var customerCode = (string)this.customerDetailsJson["Items"][0]["Field"];
            return customerCode;
        }

        public string ExtractErrorDescription()
        {
            // extract Error Description from response message
            var errorDescription = (string)this.customerDetailsJson["Items"][0]["Description"];
            return errorDescription;
        }

        public string ExtractErrorCode()
        {
            // extract Error Code from response message
            var errorCode = (string)this.customerDetailsJson["Items"][0]["ErrorCode"];
            return errorCode;
        }

        public HttpStatusCode GetHTTPStatusReponse()
        {
             return request.HttpStatusResponse;
        }
    }
}
