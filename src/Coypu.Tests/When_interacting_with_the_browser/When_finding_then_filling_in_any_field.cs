﻿using System.Linq;
using Coypu.Tests.TestDoubles;
using NUnit.Framework;

namespace Coypu.Tests.When_interacting_with_the_browser {
    [TestFixture]
    public class When_finding_then_filling_in_any_field : BrowserInteractionTests
    {
        [Test]
        public void It_makes_robust_call_to_find_then_clicks_element_on_underlying_driver()
        {
            var element = new StubElement();
            driver.StubId("something_to_fill_in", element, browserSession, sessionConfiguration);
            SpyTimingStrategy.AlwaysReturnFromSynchronise(element);

            var elementScope = browserSession.FindId("something_to_fill_in");

            elementScope.FillInWith("some filled in stuff");

            Assert.That(driver.SetFields.Keys, Has.No.Member(elementScope));

            RunQueryAndCheckTiming();

            Assert.That(driver.SetFields.Keys, Has.Member(elementScope));
            Assert.That(driver.SetFields[elementScope].Value, Is.EqualTo("some filled in stuff"));
        }
    }
}