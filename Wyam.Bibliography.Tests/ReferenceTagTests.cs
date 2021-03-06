﻿using System;
using System.Collections;
using System.Globalization;
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
            Assert.Equal(1, tag.Edition);
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

        [Fact]
        public void WhenOnlyYearIsProvided_DateIsNull()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference date='2017' />");

            // Assert
            Assert.True(tag1.Year.HasValue);
            Assert.Equal(2017, tag1.Year.Value);
            Assert.False(tag1.Date.HasValue);
        }

        [Fact]
        public void WhenDateIsProvided_BothDateAndYearHaveValues()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference date='2017-03-05' />");

            // Assert
            Assert.True(tag1.Year.HasValue);
            Assert.True(tag1.Date.HasValue);
            Assert.Equal(2017, tag1.Year);
            Assert.Equal(new DateTime(2017, 03, 05), tag1.Date);

        }


        [Fact]
        public void WhenInvalidDateIsProvided_DateIsNull()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference date='2017-13-05' />");

            // Assert
            Assert.False(tag1.Date.HasValue);

        }

        [Fact]
        public void WhenNoReferenceIdIsProvidedByUser_ItIsAutogenerated()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference />");

            // Assert
            Assert.False(String.IsNullOrWhiteSpace(tag1.Id));
        }

        [Fact]
        public void AutogeneratedReferenceId_DoesntChangeWhenRegenereatingPage()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Donald Knuth' date='1968' />");
            var tag2 = new ReferenceTag("<reference author='Donald Knuth' date='1968' />");
            var tag3 = new ReferenceTag("<reference author='Donald Knuth' date='1968' />");

            // Assert
            Assert.True(tag1.Id == tag2.Id);
            Assert.True(tag2.Id == tag3.Id);
        }

        /// <summary>
        /// "The value must be unique amongst all the IDs in the element’s home subtree and must contain at least one character. The value must not contain any space characters."
        /// https://stackoverflow.com/a/15337855
        /// </summary>
        [Fact]
        public void AutogeneratedReferenceId_DoesntContainSpaces()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Pseudonym' date='2017' />");
            var tag2 = new ReferenceTag("<reference author='GivenName FamilyName' date='2017' />");
            var tag3 = new ReferenceTag("<reference author='Zaufana Trzecia Strona' date='2017' />");

            // Assert
            Assert.False(tag1.Id.Contains(" "));
            Assert.False(tag2.Id.Contains(" "));
            Assert.False(tag3.Id.Contains(" "));
        }

        [Fact]
        public void AutogeneratedReferenceId_DiffersForDifferentTitles()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='GivenName FamilyName' date='2017' title='abc' />");
            var tag2 = new ReferenceTag("<reference author='GivenName FamilyName' date='2017' title='def' />");

            // Assert
            Assert.NotEqual(tag1.Id, tag2.Id);
        }


        [Fact]
        public void AutogeneratedReferenceId_DoesntContainIllegalCharacters()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='GivenName FamilyName' date='2017' title=\"FAQ: what's up?\" />");
            Console.WriteLine(tag1.Id);

            // Assert
            Assert.False(tag1.Id.Contains(":"));
            Assert.False(tag1.Id.Contains("?"));
            Assert.False(tag1.Id.Contains("'"));
        }

        [Fact]
        public void WhenAuthorStringHasThreeWords_ItIsNotParsedAsName()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Zaufana Trzecia Strona' date='1968' />");

            // Assert
            Assert.Null(tag1.Author.LastName);
            Assert.Null(tag1.Author.FirstName);
            Assert.Equal("Zaufana Trzecia Strona", tag1.Author.UnprocessedAuthorString);
        }

        [Fact]
        public void WhenAuthorStringStarthsWithThe_ItIsNotParsedAsName()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='The Economist' date='2010' />");

            // Assert
            Assert.Null(tag1.Author.LastName);
            Assert.Null(tag1.Author.FirstName);
            Assert.Equal("The Economist", tag1.Author.UnprocessedAuthorString);
        }

        [Fact]
        public void WhenAuthorStringOnlyContainsOneWord_ItIsNotParsedAsName()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Johnny' date='2010' />");

            // Assert
            Assert.Null(tag1.Author.LastName);
            Assert.Null(tag1.Author.FirstName);
            Assert.Equal(0, tag1.Author.Initials.Count);
            Assert.Equal("Johnny", tag1.Author.UnprocessedAuthorString);
        }

        [Fact]
        public void WhenAuthorsNameIsAvailable_ItIsPresentInId()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Donald Knuth' date='1968' />");
            var tag2 = new ReferenceTag("<reference author='DonaldKnuth' date='1968' />");

            // Assert
            Assert.True(CultureInfo.InvariantCulture.CompareInfo.IndexOf(tag1.Id, "knuth", CompareOptions.IgnoreCase) >= 0);
            Assert.True(CultureInfo.InvariantCulture.CompareInfo.IndexOf(tag2.Id, "donaldknuth", CompareOptions.IgnoreCase) >= 0);
        }


        [Fact]
        public void WhenPublicationYearIsAvailable_ItIsPresentInId()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='Donald Knuth' date='1968' />");
            var tag2 = new ReferenceTag("<reference author='DonaldKnuth' date='1968' />");

            // Assert
            Assert.True(tag1.Id.Contains("1968"));
            Assert.True(tag2.Id.Contains("1968"));
        }

        [Fact]
        public void NationalCharactersInAutogeneratedId_AreReplacedWithASCIIEquivalents()
        {
            // Arrange/Act
            var tag1 = new ReferenceTag("<reference author='łódź jeża' date='1968' />");
            var tag2 = new ReferenceTag("<reference author='jeża łódź' date='1968' />");

            // Assert
            Assert.True(tag1.Id.Contains("jeza"));
            Assert.True(tag2.Id.Contains("lodz"));
        }
    }
}