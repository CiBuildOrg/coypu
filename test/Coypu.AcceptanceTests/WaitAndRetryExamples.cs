﻿using System;
using System.IO;
using Coypu.Drivers.Selenium;
using Xunit;

namespace Coypu.AcceptanceTests
{
    public class WaitAndRetryExamples : IClassFixture<WaitAndRetryExamplesFixture>, IDisposable
    {
        protected BrowserSession browser;

        public WaitAndRetryExamples(WaitAndRetryExamplesFixture fixture)
        {
            browser = fixture.BrowserSession;
            ReloadTestPageWithDelay();
        }

        public void Dispose()
        {
            browser.Dispose();
        }

        protected void ApplyAsyncDelay()
        {
            // Hide the HTML then bring back after a short delay to test robustness
            browser.ExecuteScript("window.holdIt = window.document.body.innerHTML;");
            browser.ExecuteScript("window.document.body.innerHTML = '';");
            browser.ExecuteScript("setTimeout(function() {document.body.innerHTML = window.holdIt},250)");
        }

        protected void ReloadTestPage()
        {
            browser.Visit(TestPageLocation("InteractionTestsPage.htm"));
        }

        protected static string TestPageLocation(string page)
        {
            return "file:///" + new FileInfo(@"html\" + page).FullName.Replace("\\", "/");
        }

        protected void ReloadTestPageWithDelay()
        {
            ReloadTestPage();
            ApplyAsyncDelay();
        }
    }

    public class WaitAndRetryExamplesFixture : IDisposable
    {
        public BrowserSession BrowserSession;

        public WaitAndRetryExamplesFixture()
        {
            var configuration = new SessionConfiguration
            {
                Timeout = TimeSpan.FromMilliseconds(2000),
                Browser = Drivers.Browser.Firefox,
                Driver = typeof(SeleniumWebDriver)
            };
            BrowserSession = new BrowserSession(configuration);
        }

        public void Dispose()
        {
            BrowserSession.Dispose();
        }
    }
}