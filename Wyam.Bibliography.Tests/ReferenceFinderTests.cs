using Wyam.Bibliography.References;
using Xunit;

namespace Wyam.Bibliography.Tests
{
    public class ReferenceFinderTests
    {
        [Fact]
        public void FindingReferenceList_IsNotCaseSensitive()
        {
            // Arrange
            var sampleContent1 = @"<reference id='ref1'/><reference-list id='rl1' />";
            var sampleContent2 = @"<Reference id='ref1'/><reference-list id='rl1' />";
            var sampleContent3 = @"<ReFeReNcE id='ref1'/><reference-list id='rl1' />";

            // Act
            var sut1 = new ReferenceFinder(sampleContent1);
            var sut2 = new ReferenceFinder(sampleContent2);
            var sut3 = new ReferenceFinder(sampleContent3);

            // Assert
            Assert.NotNull(sut1.ReferenceList);
            Assert.NotNull(sut2.ReferenceList);
            Assert.NotNull(sut3.ReferenceList);
        }

        [Fact]
        public void ReferenceTagDetection_ShouldNotBeCaseSensitive()
        {
            // Arrange
            var sampleContent = @"<Reference id='ref1'/> other content
                                   <ReFeReNcE id='ref2'/>";

            // Act
            var sut = new ReferenceFinder(sampleContent);

            // Assert
            Assert.Equal(2, sut.References.Count);
            Assert.Equal("<Reference id='ref1'/>", sut.References[0].RawHtml);
            Assert.Equal("<ReFeReNcE id='ref2'/>", sut.References[1].RawHtml);
        }

        [Fact]
        public void WhenMoreThanOneReferenceListTagIsPresent_FirstOneIsReturned()
        {
            // Arrange
            var sampleContent = @"<reference id='ref1'/> other content
                                   <reference-list id='rl1' /><reference-list id='rl2' />";

            // Act
            var sut = new ReferenceFinder(sampleContent);

            // Assert
            Assert.NotNull(sut.ReferenceList);
            Assert.Equal("<reference-list id='rl1' />", sut.ReferenceList.RawHtml);
        }

        [Fact]
        public void WhenReferenceListTagIsPresent_ItIsFound()
        {
            // Arrange
            var sampleContent = @"<reference id='ref1'/> other content
                                   <reference-list />";

            // Act
            var sut = new ReferenceFinder(sampleContent);

            // Assert
            Assert.NotNull(sut.ReferenceList);
        }

        [Fact]
        public void WhenTwoReferenceTagsArePresent_BothAreDetected()
        {
            // Arrange
            var sampleContent = @"<reference id='ref1'/> other content
                                   <reference id='ref2'/>";

            // Act
            var sut = new ReferenceFinder(sampleContent);

            // Assert
            Assert.Equal(2, sut.References.Count);
        }
    }
}