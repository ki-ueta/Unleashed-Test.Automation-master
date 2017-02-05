// <copyright file="AddProductPage.cs" company="Objectivity Bespoke Software Specialists">
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

    public class AddProductPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator pageHeader = new ElementLocator(Locator.XPath, "//div[@class='pageName' and contains(., 'Add Product')]"),
                                        productCodeForm = new ElementLocator(Locator.XPath, "//input[@id='Product_ProductCode']"),
                                        productDescForm = new ElementLocator(Locator.XPath, "//textarea[@id='Product_ProductDescription']"),
                                        saveButton = new ElementLocator(Locator.XPath, "//a[@id='btnSave']"),
                                        messageBoxTitle = new ElementLocator(Locator.Id, "messageBoxTitle"),
                                        messageBoxContent = new ElementLocator(Locator.Id, "messageBoxContent");

        public AddProductPage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for page to open");
            this.Driver.IsElementPresent(this.pageHeader, BaseConfiguration.ShortTimeout);
        }

        public bool IsMessageTitlePresent
        {
            get
            {
                return this.Driver.IsElementPresent(this.messageBoxTitle, BaseConfiguration.ShortTimeout);
            }
        }

        public string MessageTitle
        {
            get
            {
                return this.Driver.GetElement(this.messageBoxTitle).Text;
            }
        }

        public string MessageContent
        {
            get
            {
                return this.Driver.GetElement(this.messageBoxContent).Text;
            }
        }

        /// <summary>
        /// Methods for entering product code
        /// </summary>
        /// <param name="productCode">specify code of the product</param>
        public void EnterProductCode(string productCode)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Product code is '{0}' ", productCode);
            this.Driver.GetElement(this.productCodeForm).SendKeys(productCode);
        }

        /// <summary>
        /// Methods for entering product description
        /// </summary>
        /// <param name="productDesc">specify describtion of the product</param>
        public void EnterProductDescription(string productDesc)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Product Description is '{0}'", productDesc);
            this.Driver.GetElement(this.productDescForm).SendKeys(productDesc);
        }

        /// <summary>
        /// Methods for clicking Save Button
        /// </summary>
        public void ClickSaveButton()
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on Save Button");

            this.Driver.GetElement(this.saveButton).JavaScriptClick();
        }
    }
}
