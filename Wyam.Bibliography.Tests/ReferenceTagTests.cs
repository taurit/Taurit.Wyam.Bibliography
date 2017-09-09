using Wyam.Bibliography.References;
using Xunit;

namespace Wyam.Bibliography.Tests
{
    public class ReferenceTagTests
    {
        [Fact]
        public void WhenFormatIs_FirstName_Initial_LastName_FirstNameIsRecognized()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Philip M. Borden' />");
            var tag2 = new ReferenceTag("<reference author='Carl C. Thompson' />");
            var tag3 = new ReferenceTag("<reference author='Elena T. Bell' />");

            // Assert
            Assert.NotNull(tag1.Author);
            Assert.NotNull(tag2.Author);
            Assert.NotNull(tag3.Author);
            Assert.Equal("Philip", tag1.Author.FirstName);
            Assert.Equal("Carl", tag2.Author.FirstName);
            Assert.Equal("Elena", tag3.Author.FirstName);
        }

        [Fact]
        public void WhenFormatIs_FirstName_Initial_LastName_InitialIsRecognized()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Philip M. Borden' />");
            var tag2 = new ReferenceTag("<reference author='Carl C. Thompson' />");
            var tag3 = new ReferenceTag("<reference author='Elena T. Bell' />");

            // Assert
            Assert.NotNull(tag1.Author);
            Assert.NotNull(tag2.Author);
            Assert.NotNull(tag3.Author);

            Assert.Equal(2, tag1.Author.Initials.Count);
            Assert.Equal('P', tag1.Author.Initials[0]);
            Assert.Equal('M', tag1.Author.Initials[1]);

            Assert.Equal(2, tag2.Author.Initials.Count);
            Assert.Equal('C', tag2.Author.Initials[0]);
            Assert.Equal('C', tag2.Author.Initials[1]);

            Assert.Equal(2, tag3.Author.Initials.Count);
            Assert.Equal('E', tag3.Author.Initials[0]);
            Assert.Equal('T', tag3.Author.Initials[1]);
        }

        [Fact]
        public void WhenFormatIs_FirstName_Initial_LastName_LastNameIsRecognized()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Philip M. Borden' />");
            var tag2 = new ReferenceTag("<reference author='Carl C. Thompson' />");
            var tag3 = new ReferenceTag("<reference author='Elena T. Bell' />");

            // Assert
            Assert.NotNull(tag1.Author);
            Assert.NotNull(tag2.Author);
            Assert.NotNull(tag3.Author);

            Assert.Equal("Borden", tag1.Author.LastName);
            Assert.Equal("Thompson", tag2.Author.LastName);
            Assert.Equal("Bell", tag3.Author.LastName);
        }

        [Fact]
        public void WhenFormatIs_FirstName_LastName_FirstNameIsRecognized()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Andrew Smith' />");
            var tag2 = new ReferenceTag("<reference author='Bernardine Washburn' />");
            var tag3 = new ReferenceTag("<reference author='Liu Xie' />");

            // Assert
            Assert.NotNull(tag1.Author);
            Assert.NotNull(tag2.Author);
            Assert.NotNull(tag3.Author);

            Assert.Equal("Andrew", tag1.Author.FirstName);
            Assert.Equal("Bernardine", tag2.Author.FirstName);
            Assert.Equal("Liu", tag3.Author.FirstName);
        }

        [Fact]
        public void WhenFormatIs_FirstName_LastName_LastNameIsProperlyRecognized()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Andrew Smith' />");
            var tag2 = new ReferenceTag("<reference author='Bernardine Washburn' />");
            var tag3 = new ReferenceTag("<reference author='Liu Xie' />");

            // Assert
            Assert.NotNull(tag1.Author);
            Assert.NotNull(tag2.Author);
            Assert.NotNull(tag3.Author);

            Assert.Equal("Smith", tag1.Author.LastName);
            Assert.Equal("Washburn", tag2.Author.LastName);
            Assert.Equal("Xie", tag3.Author.LastName);
        }

        [Fact]
        public void WhenInvalidDateIsPassed_YearIsNull()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference date='invalidDate' />");

            // Assert
            Assert.Equal(null, tag1.Year);
        }

        [Fact]
        public void WhenIsoDateIsPassedAsDate_YearIsProperlyParsed()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference date='2017-01-01' />");
            var tag2 = new ReferenceTag("<reference date='1800-02-02' />");
            var tag3 = new ReferenceTag("<reference date='2100-06-02' />");

            // Assert
            Assert.Equal(2017, tag1.Year);
            Assert.Equal(1800, tag2.Year);
            Assert.Equal(2100, tag3.Year);
        }

        [Fact]
        public void WhenNoDateIsPassed_YearIsNull()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference />");

            // Assert
            Assert.Equal(null, tag1.Year);
        }

        [Fact]
        public void WhenNoPagesArePassed_PropertyIsNull()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference />");
            var tag2 = new ReferenceTag("<reference pages='' />");
            var tag3 = new ReferenceTag("<reference pages='  ' />");

            // Assert
            Assert.Equal(null, tag1.Pages);
            Assert.Equal(null, tag2.Pages);
            Assert.Equal(null, tag3.Pages);
        }

        [Fact]
        public void WhenPagesArePassed_LeadingAndTraillingWhitespaceIsTrimmed()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference pages='\t123-456 ' />");

            // Assert
            Assert.Equal("123-456", tag1.Pages);
        }

        [Fact]
        public void WhenParsingNames_LeadingAndTrailingWhitespacesAreIgnored()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Andrew Smith ' />");
            var tag2 = new ReferenceTag("<reference author='\nBernardine Washburn' />");
            var tag3 = new ReferenceTag("<reference author='\tLiu Xie\t' />");

            // Assert
            Assert.NotNull(tag1.Author);
            Assert.NotNull(tag2.Author);
            Assert.NotNull(tag3.Author);

            Assert.Equal("Smith", tag1.Author.LastName);
            Assert.Equal("Washburn", tag2.Author.LastName);
            Assert.Equal("Xie", tag3.Author.LastName);
        }

        [Fact]
        public void WhenTitleIsPassed_LeadingAndTraillingWhitespaceIsTrimmed()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference title=' Animal Farm\t\n' />");

            // Assert
            Assert.Equal("Animal Farm", tag1.Title);
        }

        [Fact]
        public void WhenYearIsPassedAsDate_ItIsProperlyParsed()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference date='2017' />");
            var tag2 = new ReferenceTag("<reference date='1800' />");
            var tag3 = new ReferenceTag("<reference date='2100' />");

            // Assert
            Assert.Equal(2017, tag1.Year);
            Assert.Equal(1800, tag2.Year);
            Assert.Equal(2100, tag3.Year);
        }

        [Fact]
        public void MultilineTag_IsRecognized()
        {
            // Arrange/Act
            var tag = new ReferenceTag(@"<reference
            id = 'power_of_habit'
            title = 'The Power of Habit'
            author = 'Charles Duhigg'
            url = 'http://charlesduhigg.com/the-power-of-habit/'
            date = '2012'
            edition = '1'
            place = 'Warszawa'
            publisher = 'Dom Wydawniczy PWN'
            pages = '123'
            translator = 'Małgorzata Guzowska'
            />");

            // Assert
            Assert.Equal(2012, tag.Year);
        }

        [Fact]
        public void AllFieldsFromATag_AreRecognized()
        {
            // Arrange/Act
            var tag = new ReferenceTag(@"<reference
            id = 'power_of_habit'
            title = 'The Power of Habit'
            author = 'Charles Duhigg'
            url = 'http://charlesduhigg.com/the-power-of-habit/'
            date = '2012'
            edition = '1'
            place = 'Warszawa'
            publisher = 'Dom Wydawniczy PWN'
            pages = '123'
            translator = 'Małgorzata Guzowska'
            />");

            // Assert
            Assert.Equal("power_of_habit", tag.Id);
            Assert.Equal("The Power of Habit", tag.Title);
            Assert.Equal("Charles", tag.Author.FirstName);
            Assert.Equal("Duhigg", tag.Author.LastName);
            Assert.Equal("http://charlesduhigg.com/the-power-of-habit/", tag.Url);
            Assert.Equal("1", tag.Edition);
            Assert.Equal("Warszawa", tag.Place);
            Assert.Equal("Dom Wydawniczy PWN", tag.Publisher);
            Assert.Equal("123", tag.Pages);
            Assert.Equal("Małgorzata Guzowska", tag.Translator);

        }


        [Fact]
        public void AttributeNames_AraNotCaseSensitive()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference title='Animal Farm' />");
            var tag2 = new ReferenceTag("<reference Title='Animal Farm' />");
            var tag3 = new ReferenceTag("<reference TITLE='Animal Farm' />");
            var tag4 = new ReferenceTag("<reference TiTlE='Animal Farm' />");

            // Assert
            Assert.Equal("Animal Farm", tag1.Title);
            Assert.Equal("Animal Farm", tag2.Title);
            Assert.Equal("Animal Farm", tag3.Title);
            Assert.Equal("Animal Farm", tag4.Title);
        }

        [Fact]
        public void TagName_IsNotCaseSensitive()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference title='Animal Farm' />");
            var tag2 = new ReferenceTag("<Reference title='Animal Farm' />");
            var tag3 = new ReferenceTag("<REFERENCE title='Animal Farm' />");
            var tag4 = new ReferenceTag("<ReFeReNcE title='Animal Farm' />");

            // Assert
            Assert.Equal("Animal Farm", tag1.Title);
            Assert.Equal("Animal Farm", tag2.Title);
            Assert.Equal("Animal Farm", tag3.Title);
            Assert.Equal("Animal Farm", tag4.Title);
        }
    }
}