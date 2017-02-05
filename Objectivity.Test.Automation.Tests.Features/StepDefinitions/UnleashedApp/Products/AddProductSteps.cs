// <copyright file="AddProductSteps.cs" company="Objectivity Bespoke Software Specialists">
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
    public class AddProductSteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        private readonly string newProductCodePrefix = "AB",
                                newProductDescription = "AB is the first trial product",
                                expectedSuccessfulTitle = "Adding a Product",
                                expectedSuccessfulMessage = "You have updated the product successfully.";

        public AddProductSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException("scenarioContext");
            }

            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [When(@"I create a new product with mandatory details")]
        public void WhenICreateNewProduct()
        {
            var addProductPage = new AddProductPage(this.driverContext);

            // create unique product code
            var rnd = new Random().Next();
            var productCode = this.newProductCodePrefix + rnd.ToString();

            // enter product details and save
            addProductPage.EnterProductCode(productCode);
            addProductPage.EnterProductDescription(this.newProductDescription);
            addProductPage.ClickSaveButton();
        }

        [Then(@"Valid successful message is displayed")]
        public void ThenValidSuccessfulMessageIsDisplayed()
        {
            var addProductPage = new AddProductPage(this.driverContext);
            var isMessagePresent = addProductPage.IsMessageTitlePresent;
            Verify.That(this.driverContext, () => Assert.IsTrue(isMessagePresent, "Message doesnt display"), false, false);

            var messageTitleText = addProductPage.MessageTitle;
            Verify.That(this.driverContext, () => Assert.AreEqual(messageTitleText, this.expectedSuccessfulTitle), false, false);

            var messageContentText = addProductPage.MessageContent;
            Verify.That(this.driverContext, () => Assert.AreEqual(messageContentText, this.expectedSuccessfulMessage), false, false);
        }
    }
}
