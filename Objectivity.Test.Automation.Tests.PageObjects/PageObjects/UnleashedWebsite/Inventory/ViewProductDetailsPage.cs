// <copyright file="ViewProductDetailsPage.cs" company="Objectivity Bespoke Software Specialists">
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

    public class ViewProductDetailsPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator availableOnHandLabel = new ElementLocator(Locator.Id, "AvailableQty");

        private readonly string pageHeaderXpath = "//div[@class='pageName' and contains(., ";

        public ViewProductDetailsPage(DriverContext driverContext, string productCode)
            : base(driverContext)
        {
            Logger.Info("Waiting for page to open");

            var pageHeader = new ElementLocator(Locator.XPath, this.pageHeaderXpath + "'" + productCode + "')]");
            this.Driver.IsElementPresent(pageHeader, BaseConfiguration.ShortTimeout);
        }

        public double AvailableOnHandText
        {
            get
            {
                var availableOnHandString = this.Driver.GetElement(this.availableOnHandLabel).Text;
                return double.Parse(availableOnHandString.Replace(".0000", string.Empty), CultureInfo.CurrentCulture);
            }
        }
    }
}
