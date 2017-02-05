// <copyright file="AddQuotePage.cs" company="Objectivity Bespoke Software Specialists">
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

    public class AddQuotePage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator pageHeader = new ElementLocator(Locator.XPath, "//div[@class='pageName' and contains(., 'Add Quote')]"),
                                        customerCodeForm = new ElementLocator(Locator.XPath, "//input[@id='SelectedCustomerCode']"),
                                        productAddForm = new ElementLocator(Locator.XPath, "//input[@id='ProductAddLine']"),
                                        qtyAddForm = new ElementLocator(Locator.XPath, "//input[@id='QtyAddLine']"),
                                        addButton = new ElementLocator(Locator.Id, "btnAddOrderLine"),
                                        saveButton = new ElementLocator(Locator.XPath, "//a[text()='Save']"),
                                        orderStatus = new ElementLocator(Locator.Id, "OrderStatusDisplay"),
                                        acceptQuoteButton = new ElementLocator(Locator.Id, "btnAcceptQuote"),
                                        createOrderButton = new ElementLocator(Locator.Id, "btnCreateOrder");

        private readonly string autoCompleteXPath = "//ul[contains(@class,'ui-autocomplete')]/li[contains(@class,'ui-menu-item') and contains(text(),",
                                availableQtyId = "AvailableAddLine";

        public AddQuotePage(DriverContext driverContext)
            : base(driverContext)
        {
            Logger.Info("Waiting for page to open");
            this.Driver.IsElementPresent(this.pageHeader, BaseConfiguration.ShortTimeout);
        }

        public string OrderStatusText
        {
            get
            {
                return this.Driver.GetElement(this.orderStatus).Text;
            }
        }

        public int AvaialbleQuantityText
        {
            get
            {
                var availableQty = this.Driver.FindElement(By.Id(this.availableQtyId)).GetAttribute("value");
                return int.Parse(availableQty, CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Methods for entering customer code
        /// </summary>
        /// <param name="customerCode">specify code of the customer</param>
        public void EnterCustomerCode(string customerCode)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Customer code is '{0}' ", customerCode);
            this.Driver.GetElement(this.customerCodeForm).SendKeys(customerCode);
            this.Driver.WaitForAjax();
        }

        /// <summary>
        /// Methods for selecting AutoComplete dropdown box
        /// </summary>
        /// <param name="keyWord">specify code that will be selected in autocomplete dropdown box</param>
        public void SelectAutoComplete(string keyWord)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Keyword to be selected in autocomplete '{0}'", keyWord);

            var autocompleteLocator = new ElementLocator(Locator.XPath, this.autoCompleteXPath + "'" + keyWord + "')]");
            this.Driver.GetElement(autocompleteLocator).JavaScriptClick();
            this.Driver.WaitForAjax();
        }

        /// <summary>
        /// Methods for entering product code
        /// </summary>
        /// <param name="productCode">specify code of the product</param>
        public void EnterProductCode(string productCode)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Product Code is '{0}' ", productCode);
            this.Driver.GetElement(this.productAddForm).SendKeys(productCode);
            this.Driver.WaitForAjax();
        }

        /// <summary>
        /// Methods for entering product quantity
        /// </summary>
        /// <param name="qty">specify quantity of the product</param>
        public void EnterProductQuantity(int qty)
        {
            Logger.Info(CultureInfo.CurrentCulture, "Product quantity is '{0}' ", qty);
            this.Driver.GetElement(this.qtyAddForm).SendKeys(qty.ToString(CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Methods for clicking Add Button
        /// </summary>
        public void ClickAddButton()
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on Add Button");

            this.Driver.GetElement(this.addButton).JavaScriptClick();
            this.Driver.WaitForAjax();
        }

        public void ClickSaveButton()
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on Save Button");

            this.Driver.GetElement(this.saveButton).JavaScriptClick();
            this.Driver.WaitForAjax();
        }

        public void ClickAcceptQuoteButton()
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on Accept Quote Button");

            this.Driver.GetElement(this.acceptQuoteButton).JavaScriptClick();
            this.Driver.WaitForAjax();
        }

        public void ClickCreateOrderButton()
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on Create Order Button");

            this.Driver.GetElement(this.createOrderButton).JavaScriptClick();
            this.Driver.WaitForAjax();
        }
    }
}
