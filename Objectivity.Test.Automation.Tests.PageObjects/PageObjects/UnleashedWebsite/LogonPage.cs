// <copyright file="LogonPage.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.UnleashedWebsite
{
    using System;
    using System.Globalization;
    using NLog;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Tests.PageObjects;
    using OpenQA.Selenium;

    public class LogOnPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator pageHeader = new ElementLocator(Locator.XPath, "//form[@id='inputform']"),
                                        userNameForm = new ElementLocator(Locator.XPath, "//input[@id='username']"),
                                        passwordForm = new ElementLocator(Locator.XPath, "//input[@id='password']"),
                                        loginButton = new ElementLocator(Locator.XPath, "//input[@id='btnLogOn']");

        public LogOnPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for page to open");
            this.Driver.IsElementPresent(this.pageHeader, BaseConfiguration.ShortTimeout);
        }

        /// <summary>
        /// Methods for navigating to Log In page
        /// </summary>
        /// <returns>Returns Log in Page</returns>
        public LogOnPage OpenLogOnPage()
        {
            var url = BaseConfiguration.GetUrlValue;
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        /// <summary>
        /// Methods for entering password
        /// </summary>
        public void EnterPassword()
        {
            var password = BaseConfiguration.GetPassword;
            Logger.Info(CultureInfo.CurrentCulture, "Password '{0}'", password);
            this.Driver.GetElement(this.passwordForm).SendKeys(password);
            this.Driver.WaitForAjax();
        }

        /// <summary>
        /// Methods for entering username
        /// </summary>
        public void EnterUserName()
        {
            var userName = BaseConfiguration.GetUsername;
            Logger.Info(CultureInfo.CurrentCulture, "User name '{0}'", userName);
            this.Driver.GetElement(this.userNameForm).SendKeys(userName);
        }

        /// <summary>
        /// Methods for clicking LogOn Button
        /// </summary>
        public void ClickLogOnButton()
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on Logon Button");

            this.Driver.GetElement(this.loginButton).JavaScriptClick();
        }
    }
}
