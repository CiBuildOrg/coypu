﻿using Shouldly;
using Xunit;

namespace Coypu.Drivers.Tests
{
    public class When_hovering : DriverSpecs
    {
        [Fact]
        public void Mouses_over_the_underlying_element()

        {
            var element = Id("hoverOnMeTest");
            element.Text.ShouldBe("Hover on me");
            Driver.Hover(element);

            Id("hoverOnMeTest").Text.ShouldBe("Hover on me - hovered");
        }
    }
}