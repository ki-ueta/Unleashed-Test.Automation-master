// <copyright file="LoginSteps.cs" company="Objectivity Bespoke Software Specialists">
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
    public class LoginSteps
    {
        private readonly DriverContext driverContext;
        private readonly ScenarioContext scenarioContext;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException("scenarioContext");
            }

            this.scenarioContext = scenarioContext;

            this.driverContext = this.scenarioContext["DriverContext"] as DriverContext;
        }

        [Given(@"I log on")]
        public void GivenILogOn()
        {
            // Navigate to LogOn Page
            var loginPage = new LogOnPage(this.driverContext).OpenLogOnPage();

            // Enter Username and Password from configuration then click LogOn button
            loginPage.EnterUserName();
            loginPage.EnterPassword();
            loginPage.ClickLogOnButton();

            // verify login successfully by checking the Home page
            new MainMenuAndHomePage(this.driverContext);
        }
    }
}
