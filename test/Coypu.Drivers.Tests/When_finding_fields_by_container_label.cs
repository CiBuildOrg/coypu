﻿using System.Linq;
using Coypu.Finders;
using Shouldly;
using Xunit;

namespace Coypu.Drivers.Tests
{
    internal class When_finding_fields_by_container_label : DriverSpecs
    {
        [Fact]
        public void Finds_text_input()
        {
            Field("text input field in a label container", options: Options.Exact).Id.ShouldBe("containerLabeledTextInputFieldId");
        }

        [Fact]
        public void Finds_password()
        {
            Field("password field in a label container").Id.ShouldBe("containerLabeledPasswordFieldId");
        }

        [Fact]
        public void Finds_checkbox()
        {
            Field("checkbox field in a label container").Id.ShouldBe("containerLabeledCheckboxFieldId");
        }

        [Fact]
        public void Finds_radio()
        {
            Field("radio field in a label container").Id.ShouldBe("containerLabeledRadioFieldId");
        }

        [Fact]
        public void Finds_select()
        {
            Field("select field in a label container").Id.ShouldBe("containerLabeledSelectFieldId");
        }

        [Fact]
        public void Finds_textarea()
        {
            Field("textarea field in a label container").Id.ShouldBe("containerLabeledTextareaFieldId");
        }

        [Fact]
        public void Finds_file_field()
        {
            Field("file field in a label container", options: Options.Exact).Id.ShouldBe("containerLabeledFileFieldId");
        }

        [Fact]
        public void Finds_by_substring()
        {
            var fields = new FieldFinder(Driver, "Some container labeled radio option", Root, DefaultOptions).Find(Options.Substring);
            Assert.Equal(new[]
                {
                    "containerLabeledRadioFieldExactMatchId",
                    "containerLabeledRadioFieldPartialMatchId"
                }, fields.Select(e => e.Id).OrderBy(id => id));
        }

        [Fact]
        public void Finds_by_exact_text()
        {
            var fields = new FieldFinder(Driver, "Some container labeled radio option", Root, DefaultOptions).Find(Options.Exact);
            Assert.Equal(new[]
                {
                    "containerLabeledRadioFieldExactMatchId"
                }, fields.Select(e => e.Id).OrderBy(id => id));
        }
    }
}