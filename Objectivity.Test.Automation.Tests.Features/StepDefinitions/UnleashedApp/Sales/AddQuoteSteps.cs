// <copyright file="AddQuoteSteps.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

namespace Objectivity.Test.Automation.Tests.Features.StepDefinitions
{
    using System;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.UnleashedWebsite;
    using TechTalk.SpecFlow;

    [Binding]
    public class AddQuoteSteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        public AddQuoteSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException("scenarioContext");
            }

            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [Given(@"I create a quote of product code ""(.*)"" with (\d+) qty for customer code ""(.*)""")]
        public void WhenICreateNewAQuote(string productCode, int qty, string customerCode)
        {
            var addQuotePage = new AddQuotePage(this.driverContext);

            // selecting customer by customer code
            addQuotePage.EnterCustomerCode(customerCode);
            addQuotePage.SelectAutoComplete(customerCode);

            // selecting product by product code
            addQuotePage.EnterProductCode(productCode);
            addQuotePage.SelectAutoComplete(productCode);

            // read available quantity of the product
            var availableQty = addQuotePage.AvaialbleQuantityText;
            this.scenarioContext.Set(availableQty, "AvailableQty");

            // enter product quanity then add product details to the Quote
            addQuotePage.EnterProductQuantity(qty);
            addQuotePage.ClickAddButton();

            this.scenarioContext.Set(productCode, "ProductCode");
            this.scenarioContext.Set(qty, "SoldQty");

            // validate quote status after save
            addQuotePage.ClickSaveButton();
            var orderStatus = addQuotePage.OrderStatusText;
            Verify.That(this.driverContext, () => Assert.AreEqual(orderStatus, "DRAFT"), false, false);
        }

        [Given(@"Customer accepts the quote")]
        public void WhenCustomerAcceptsTheQuote()
        {
            var addQuotePage = new AddQuotePage(this.driverContext);

            // validate quote status after quote accepted
            addQuotePage.ClickAcceptQuoteButton();
            var orderStatus = addQuotePage.OrderStatusText;
            Verify.That(this.driverContext, () => Assert.AreEqual(orderStatus, "ACCEPTED"), false, false);

            // convert Sales Quote to Sales Order
            addQuotePage.ClickCreateOrderButton();
            new ViewSalesOrderPage(this.driverContext);
        }
    }
}
