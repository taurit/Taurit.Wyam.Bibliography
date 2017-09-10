using Xunit;

namespace Wyam.Bibliography.Tests
{
    public class BibliographyTests
    {
        [Fact]
        public void WhenNoReferencesAndNoReferenceListArePresent_ContentDoesNotChange()
        {
            // Arrange
            var sampleContent1 = "";
            var sampleContent2 = "test";
            var sampleContent3 = "TEST";
            var sampleContent4 = "<b>test</b>";
            var sampleContent5 = "\t\t<B>test</B>\nline2";
            var sut = new Bibliography();

            // Act
            var outputContent1 = sut.ProcessBibliographicReferences(sampleContent1);
            var outputContent2 = sut.ProcessBibliographicReferences(sampleContent2);
            var outputContent3 = sut.ProcessBibliographicReferences(sampleContent3);
            var outputContent4 = sut.ProcessBibliographicReferences(sampleContent4);
            var outputContent5 = sut.ProcessBibliographicReferences(sampleContent5);

            // Assert
            Assert.Equal(sampleContent1, outputContent1);
            Assert.Equal(sampleContent2, outputContent2);
            Assert.Equal(sampleContent3, outputContent3);
            Assert.Equal(sampleContent4, outputContent4);
            Assert.Equal(sampleContent5, outputContent5);
        }

        [Fact]
        public void WhenNoReferencesArePresentInContent_ReferenceListIsNotRendered()
        {
            // Arrange
            var sampleContent = "reference list exists but no references<reference-list />";
            var expectedContent = "reference list exists but no references";
            var sut = new Bibliography();

            // Act
            var outputContent = sut.ProcessBibliographicReferences(sampleContent);

            // Assert
            Assert.Equal(expectedContent, outputContent);
        }

        [Fact]
        public void WhenReferenceListIsNotPresentInContent_ReferencesAreNotRendered()
        {
            // Arrange
            var sampleContent = "lorem ipsum <reference /> reference exists but no reference list";
            var expectedContent = "lorem ipsum  reference exists but no reference list";
            var sut = new Bibliography();

            // Act
            var outputContent = sut.ProcessBibliographicReferences(sampleContent);

            // Assert
            Assert.Equal(expectedContent, outputContent);
        }
    }
}