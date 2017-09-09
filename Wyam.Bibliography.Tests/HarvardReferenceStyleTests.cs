using System.Collections.Generic;
using Wyam.Bibliography.References;
using Wyam.Bibliography.ReferenceStyles;
using Xunit;

namespace Wyam.Bibliography.Tests
{
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
    }
}