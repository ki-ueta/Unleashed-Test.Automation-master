// <copyright file="CustomersSteps.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

namespace Objectivity.Test.Automation.Tests.Features.StepDefinitions
{
    using System;
    using System.Globalization;
    using System.Net;
    using NUnit.Framework;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.ServiceApi;
    using TechTalk.SpecFlow;

    [Binding]
    public class CustomersSteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        private Guid customerGuid;
        private string expectedCustomerCode;

        public CustomersSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException("scenarioContext");
            }

            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;

            var api = new CustomersAPI(this.driverContext);
            this.scenarioContext.Set(api, "Api");
        }

        [Given(@"I post a new customer details")]
        public void WhenIPostANewCustomerDetails()
        {
            var api = this.scenarioContext.Get<CustomersAPI>("Api");
            this.customerGuid = Guid.NewGuid();

            var uniqueCustomerCode = new Random().Next(000, 999).ToString(CultureInfo.CurrentCulture).PadLeft(3, '0');
            this.expectedCustomerCode = "ABC" + uniqueCustomerCode;
            api.CreateNewCustomer(this.customerGuid, this.expectedCustomerCode);

            this.scenarioContext.Set(api, "Api");
            this.scenarioContext.Set(this.expectedCustomerCode, "OriginalCustomerCode");
            this.scenarioContext.Set(this.customerGuid, "OriginalCustomerGUID");
        }

        [When(@"I post a new customer details again with same GUID")]
        public void WhenIPostANewCustomerDetailsWithTheSameGUID()
        {
            var api = this.scenarioContext.Get<CustomersAPI>("Api");

            var uniqueCustomerCode = new Random().Next(000, 999).ToString(CultureInfo.CurrentCulture).PadLeft(3, '0');
            this.expectedCustomerCode = "ABC" + uniqueCustomerCode;
            api.CreateNewCustomer(this.customerGuid, this.expectedCustomerCode);
            this.scenarioContext.Set(api, "Api");
        }

        [When(@"I post a new customer details again with same CustomerCode")]
        public void WhenIPostANewCustomerDetailsWithTheSameCustomerCode()
        {
            var api = this.scenarioContext.Get<CustomersAPI>("Api");

            this.customerGuid = Guid.NewGuid();
            api.CreateNewCustomer(this.customerGuid, this.expectedCustomerCode);
            this.scenarioContext.Set(api, "Api");
        }

        [When(@"I get customer details")]
        [Then(@"I get customer details")]
        public void WhenIGetCustomerDetails()
        {
            var api = this.scenarioContext.Get<CustomersAPI>("Api");
            api.GetCustomerDetails(this.customerGuid);
            this.scenarioContext.Set(api, "Api");
        }

        [Then(@"Valid customer code is returned")]
        public void ThenValidCustomerCodeIsReturned()
        {
            var api = this.scenarioContext.Get<CustomersAPI>("Api");
            string customerCode = api.ExtractCustomerCode();
            Verify.That(this.driverContext, () => Assert.AreEqual(this.expectedCustomerCode, customerCode, "Message doesnt display"), false, false);
        }

        [Then(@"Bad response is returned with (.*) error message")]
        public void ThenBadResponseIsReturned(string errorType)
        {
            string expectedErrorDescription, expectedErrorField, expectedErrorCode;

            switch (errorType)
            {
                case "duplicated Guid":
                    var originalCustomerCode = this.scenarioContext.Get<string>("OriginalCustomerCode");
                    expectedErrorField = "CustomerCode";
                    expectedErrorDescription = "Customer with Guid '" + this.customerGuid + "' already exists but with a different customer code (" + originalCustomerCode + ") than the one provided. Please ensure both Guid and Customer Code are correct when updating a customer.";
                    expectedErrorCode = "0";
                    break;
                case "duplicated customer code":
                    var originalCustomerGUID = this.scenarioContext.Get<Guid>("OriginalCustomerGUID");
                    expectedErrorField = "CustomerCode";
                    expectedErrorDescription = "Customer '" + this.expectedCustomerCode + "' already exists but with a different Guid (" + originalCustomerGUID + ") than the one provided. Please ensure both Guid and Customer Code are correct when updating a customer.";
                    expectedErrorCode = "0";
                    break;

                default:
                    throw new ArgumentException("Error type is not found : " + errorType);
            }

            var api = this.scenarioContext.Get<CustomersAPI>("Api");
            var expectedHTTP = HttpStatusCode.BadRequest;
            Verify.That(this.driverContext, () => Assert.AreEqual(expectedHTTP, api.GetHTTPStatusReponse()), false, false);
            Verify.That(this.driverContext, () => Assert.AreEqual(expectedErrorField, api.ExtractErrorField()), false, false);
            Verify.That(this.driverContext, () => Assert.AreEqual(expectedErrorDescription, api.ExtractErrorDescription()), false, false);
            Verify.That(this.driverContext, () => Assert.AreEqual(expectedErrorCode, api.ExtractErrorCode()), false, false);
        }
    }
}
