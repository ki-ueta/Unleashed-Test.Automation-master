// <copyright file="MainMenuAndHomePage.cs" company="Objectivity Bespoke Software Specialists">
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

    public class MainMenuAndHomePage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator pageHeader = new ElementLocator(Locator.XPath, "//div[@id='brand'");

        private string navigationXpath = "//div[@id='main']",
                       menuXpath = "//ul[contains(@class,'side-menu level-1-side-menu')]/li/a/span[text()=";

        public MainMenuAndHomePage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for page to open");
            this.Driver.IsElementPresent(this.pageHeader, BaseConfiguration.ShortTimeout);
        }

        /// <summary>
        /// Methods for navigating to Log In page
        /// </summary>
        /// <returns>Returns Log in Page</returns>
        public MainMenuAndHomePage OpenLogOnPage()
        {
            var url = BaseConfiguration.GetUrlValue;
            this.Driver.NavigateTo(new Uri(url));
            Logger.Info(CultureInfo.CurrentCulture, "Opening page {0}", url);
            return this;
        }

        /// <summary>
        /// Methods for navigating through Main menu.
        /// </summary>
        /// <param name="navigationPath">The path to navigate through the menu by passing string with '/' as delimiter. eg, "Inventory/Products/Add Product" for navigating to Add Product page</param>
        public void ClickMainMenu(string navigationPath)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on " + navigationPath + " Menu");

            var menuPath = navigationPath.Split('/');
            for (int i = 0; i < menuPath.Length; i++)
            {
                var menuLevelXpath = this.menuXpath.Replace('1', char.Parse((i + 1).ToString(CultureInfo.CurrentCulture)));
                var menuLocator = new ElementLocator(Locator.XPath, this.navigationXpath + menuLevelXpath + "'" + menuPath[i] + "']");
                this.Driver.GetElement(menuLocator).JavaScriptClick();
            }
        }
    }
}
