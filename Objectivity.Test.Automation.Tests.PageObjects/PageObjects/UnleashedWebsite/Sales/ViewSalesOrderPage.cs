// <copyright file="ViewSalesOrderPage.cs" company="Objectivity Bespoke Software Specialists">
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

    public class ViewSalesOrderPage : ProjectPageBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Locators for elements
        /// </summary>
        private readonly ElementLocator pageHeader = new ElementLocator(Locator.XPath, "//div[@class='pageName' and contains(., 'View Sales Order')]"),
                                        saveButton = new ElementLocator(Locator.XPath, "//a[text()='Save']"),
                                        orderStatus = new ElementLocator(Locator.Id, "OrderStatusDisplay"),
                                        completeButton = new ElementLocator(Locator.Id, "btnComplete"),
                                        messageBoxTitle = new ElementLocator(Locator.Id, "messageBoxTitle"),
                                        messageBoxContent = new ElementLocator(Locator.Id, "messageBoxContent");

        public ViewSalesOrderPage(DriverContext driverContext)
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

        public void ClickSaveButton()
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on Save Button");

            this.Driver.GetElement(this.saveButton).JavaScriptClick();
        }

       public void ClickCompleteButton()
        {
            Logger.Info(CultureInfo.CurrentCulture, "Click on CompleteButton");

            this.Driver.GetElement(this.completeButton).JavaScriptClick();
            this.Driver.WaitForAjax();
        }
    }
}
