// <copyright file="MainMenuSteps.cs" company="Objectivity Bespoke Software Specialists">
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
    public class MainMenuSteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        public MainMenuSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException("scenarioContext");
            }

            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [Given(@"I navigate to Add Product page")]
        public void WhenINavigateToAddProductpage()
        {
            var mainMenuPage = new MainMenuAndHomePage(this.driverContext);

            mainMenuPage.ClickMainMenu("Inventory/Products/Add Product");

            // verify successfully navigation by checking the Add Product page
            new AddProductPage(this.driverContext);
        }

        [Given(@"I navigate to Add Quote page")]
        public void WhenINavigateToAddQuotepage()
        {
            var mainMenuPage = new MainMenuAndHomePage(this.driverContext);

            mainMenuPage.ClickMainMenu("Sales/Quotes/Add Quote");

            // verify successfully navigation by checking the Add Quote page
            new AddProductPage(this.driverContext);
        }

        [When(@"I navigate to View Products page")]
        [Then(@"I navigate to View Products page")]
        public void WhenINavigateToStockOnHandEnquirypage()
        {
            var mainMenuPage = new MainMenuAndHomePage(this.driverContext);

            mainMenuPage.ClickMainMenu("Inventory/Products/View Products");

            // verify successfully navigation by checking the View Products page
            new ViewProductsPage(this.driverContext);
        }
    }
}
