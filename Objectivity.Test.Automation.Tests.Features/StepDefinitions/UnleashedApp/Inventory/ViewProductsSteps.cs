// <copyright file="ViewProductsSteps.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

namespace Objectivity.Test.Automation.Tests.Features.StepDefinitions
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Tests.PageObjects.PageObjects.UnleashedWebsite;
    using TechTalk.SpecFlow;

    [Binding]
    public class ViewProductsSteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        public ViewProductsSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException("scenarioContext");
            }

            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [Then(@"Stock on hand is reduced by sold quantity")]
        public void ThenStockOnHandIsReducedBySoldQuantity()
        {
            // calculate expected available on hand
            var productCode = this.scenarioContext.Get<string>("ProductCode");
            var availableQty = this.scenarioContext.Get<int>("AvailableQty");
            var soldQty = this.scenarioContext.Get<int>("SoldQty");
            var expectedAvailableOnHand = Convert.ToDouble(availableQty - soldQty);

            // Search for product by product code
            var viewProductsPage = new ViewProductsPage(this.driverContext);
            viewProductsPage.EnterProductCode(productCode);

            // Navigate to product details page
            viewProductsPage.SelectKeywordFromSearchResultTable(productCode);
            var viewProductDetailsPage = new ViewProductDetailsPage(this.driverContext, productCode);

            // Verify Available On Hand
            var actualStockOnHand = viewProductDetailsPage.AvailableOnHandText;
            Verify.That(this.driverContext, () => Assert.AreEqual(actualStockOnHand, expectedAvailableOnHand), false, false);
        }
    }
}
