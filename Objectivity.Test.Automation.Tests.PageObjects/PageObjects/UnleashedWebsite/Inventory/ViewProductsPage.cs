// <copyright file="ViewProductsPage.cs" company="Objectivity Bespoke Software Specialists">
// Copyright (c) Objectivity Bespoke Software Specialists. All rights reserved.
// </copyright>

namespace Objectivity.Test.Automation.Tests.PageObjects.PageObjects.UnleashedWebsite
{
    using System;
    using System.Globalization;
    using Common.WebElements.Kendo;
    using NLog;
    using Objectivity.Test.Automation.Common;
    using Objectivity.Test.Automation.Common.Extensions;
    using Objectivity.Test.Automation.Common.Types;
    using Objectivity.Test.Automation.Tests.PageObjects;
    using OpenQA.Selenium;

    public class ViewProductsPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator pageHeader = new ElementLocator(Locator.XPath, "//div[@class='pageName' and contains(., 'View Products')]"),
                                        productFilterForm = new ElementLocator(Locator.XPath, "//input[@id='ProductFilter']");

        private readonly string productSearchResultTableXPath = "//table[@id='ProductList_DXMainTable']//a[text()=";

        public ViewProductsPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for page to open");
            this.Driver.IsElementPresent(this.pageHeader, BaseConfiguration.ShortTimeout);
        }

        public void EnterProductCode(string productCode)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Product code is '{0}' ", productCode);
            this.Driver.GetElement(this.productFilterForm).SendKeys(productCode);
            this.Driver.Actions().SendKeys(Keys.Enter).Build().Perform();
        }

        /// <summary>
        /// Methods for selecting any link in Searh Result keyword grid
        /// </summary>
        /// <param name="keyword">specify keyword</param>
        public void SelectKeywordFromSearchResultTable(string keyword)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Keyword is clicked '{0}'", keyword);

            var keywordLocator = new ElementLocator(Locator.XPath, this.productSearchResultTableXPath + "'" + keyword + "']");
            this.Driver.GetElement(keywordLocator).JavaScriptClick();
        }
    }
}
