using Wyam.Bibliography.References;
using Xunit;

namespace Wyam.Bibliography.Tests
{
    public class ReferenceListTagTests
    {
        [Fact]
        public void AttributeValuesAreParsedCorrectly_IfTagIsMultiline()
        {
            // Arrange / Act
            var tag = new ReferenceListTag(@"<reference-list id='123'
                                                             header='Bibliography'
                                                             header-wrapper='h2' />");


            // Assert
            Assert.Equal("Bibliography", tag.HeaderText);
            Assert.Equal("h2", tag.HeaderWrapper);
            Assert.Equal("123", tag.Id);
        }

        [Fact]
        public void WhenReferenceListTagIsEmpty_AtrtibutesAreNull()
        {
            // Arrange / Act
            var tag = new ReferenceListTag("<reference-list />");

            // Assert
            Assert.Equal("<reference-list />", tag.RawHtml);
            Assert.Null(tag.HeaderText);
            Assert.Null(tag.HeaderWrapper);
            Assert.Null(tag.Id);
        }
    }
}