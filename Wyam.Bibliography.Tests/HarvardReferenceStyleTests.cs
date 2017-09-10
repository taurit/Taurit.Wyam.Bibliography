using System.Collections.Generic;
using Wyam.Bibliography.References;
using Wyam.Bibliography.ReferenceStyles;
using Xunit;

namespace Wyam.Bibliography.Tests
{

    /// <summary>
    ///     Examples from http://www.citethisforme.com/harvard-referencing
    ///     are accepted as correct.
    /// </summary>
    public class HarvardReferenceStyleTests
    {
        [Fact]
        public void ReferencesShouldBeOrderedByAuthorsLastName()
        {
            // Arrange
            IReferenceStyle sut = new HarvardReferenceStyle();
            var tagsToSort = new List<ReferenceTag>();
            tagsToSort.Add(new ReferenceTag("<reference author='Andrew Smith' />"));
            tagsToSort.Add(new ReferenceTag("<reference author='Charles Duhigg' />"));
            tagsToSort.Add(new ReferenceTag("<reference author='Liu Xie' />"));

            // Act
            var sorted = sut.SortReferences(tagsToSort);

            // Assert
            Assert.Equal(3, sorted.Count);
            Assert.Equal("<reference author='Charles Duhigg' />", sorted[0].RawHtml);
            Assert.Equal("<reference author='Andrew Smith' />", sorted[1].RawHtml);
            Assert.Equal("<reference author='Liu Xie' />", sorted[2].RawHtml);
        }



        [Fact]
        public void ReferenceOfBook_ByASingleAuthor_RendersCorrectly()
        {
            // Arrange
            //language=html
            var tag = new ReferenceTag(@"<reference
                author='James Patterson'
                title='Maximum ride'
                place='New York'
                publisher='Little, Brown'
                date='2005'
            />");
            var sut = new HarvardReferenceStyle();

            // Act
            var rendered = sut.RenderReferenceListItemContent(tag);

            // Assert 
            Assert.Equal("Patterson, J. (2005). <i>Maximum ride</i>. New York: Little, Brown.", rendered);
        }


        [Fact]
        public void ReferenceOfBook_ByASingleAuthor_NonFirstEdition_RendersCorrectly()
        {
            // Arrange
            //language=html
            var tag = new ReferenceTag(@"<reference
                author='James Patterson'
                title='Maximum ride'
                place='New York'
                publisher='Little, Brown'
                date='2005'
                edition='6'
            />");
            var sut = new HarvardReferenceStyle();

            // Act
            var rendered = sut.RenderReferenceListItemContent(tag);

            // Assert 
            Assert.Equal("Patterson, J. (2005). <i>Maximum ride</i>. 6th ed. New York: Little, Brown.", rendered);
        }

        [Fact]
        public void WhenWebsiteIsCited_ItRendersAsExpected()
        {
            // Arrange
            //language=html
            var tag = new ReferenceTag(@"<reference
                url='http://www.mms.com/'
                author='Mms.com'
                title=""M&M'S Official Website""
                date='2015-04-20'
            />");
            var sut = new HarvardReferenceStyle();

            // Act
            var rendered = sut.RenderReferenceListItemContent(tag);

            // Assert 
            Assert.Equal("Mms.com, (2015). <i>M&M'S Official Website</i>. [online] Available at: http://www.mms.com/ [Accessed 20 Apr. 2015].", rendered);
        }

    }
}