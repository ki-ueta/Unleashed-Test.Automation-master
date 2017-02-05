// <copyright file="ViewSalesOrderSteps.cs" company="Objectivity Bespoke Software Specialists">
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
    public class ViewSalesOrderSteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        private readonly string expectedSuccessfulSavedTitle = "Save Invoice",
                                expectedSuccessfulSavedMessage = "You have successfully Completed Sales Order";

        public ViewSalesOrderSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException("scenarioContext");
            }

            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [When(@"I complete a Sales Order from the quote")]
        public void WhenICompleteASalesOrderFromTheQuote()
        {
            // validate Sales Order status after converting from Sales quote
            var viewSalesOrderPage = new ViewSalesOrderPage(this.driverContext);
            var orderStatus = viewSalesOrderPage.OrderStatusText;
            Verify.That(this.driverContext, () => Assert.AreEqual(orderStatus, "PARKED"), false, false);

            // validate messsage when successfully complete Sales Order
            viewSalesOrderPage.ClickCompleteButton();
            var isMessagePresent = viewSalesOrderPage.IsMessageTitlePresent;
            Verify.That(this.driverContext, () => Assert.IsTrue(isMessagePresent, "Message doesnt display"), false, false);

            var messageTitleText = viewSalesOrderPage.MessageTitle;
            Verify.That(this.driverContext, () => Assert.AreEqual(messageTitleText, this.expectedSuccessfulSavedTitle), false, false);

            var messageContentText = viewSalesOrderPage.MessageContent;
            Verify.That(this.driverContext, () => Assert.IsTrue(messageContentText.Contains(this.expectedSuccessfulSavedMessage)), false, false);
        }
    }
}
