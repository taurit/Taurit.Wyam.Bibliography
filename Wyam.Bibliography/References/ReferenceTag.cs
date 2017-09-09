using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace Wyam.Bibliography.References
{
    /// <summary>
    ///     Parses user-provided reference tag and provides obtained information.
    /// </summary>
    internal class ReferenceTag
    {
        private static readonly Regex DateMatcher = new Regex(@"(?<year>\d{4})(-\d{2}-\d{2})?");
    
        public ReferenceTag(string htmlMarkup)
        {
            RawHtml = htmlMarkup;
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlMarkup);
            ReferenceNode = doc.DocumentNode.ChildNodes[0];

            PersonNameParser personNameParser = new PersonNameParser();
            Author = personNameParser.ParseName(ReferenceNode.Attributes["author"]?.Value);
            
        }

        public string RawHtml { get; }
        private HtmlNode ReferenceNode { get; }

        public PersonName Author { get; }
        public int? Year
        {
            get
            {
                var date = ReferenceNode.Attributes["date"]?.Value;
                if (string.IsNullOrWhiteSpace(date)) return null;

                date = date.Trim();
                var dateMatch = DateMatcher.Match(date);
                if (dateMatch.Success)
                {
                    var year = Convert.ToInt32(dateMatch.Groups["year"].Value);
                    return year;
                }
                return null;
            }
        }

        public string Pages
        {
            get
            {
                var pages = ReferenceNode.Attributes["pages"]?.Value;
                if (string.IsNullOrWhiteSpace(pages)) return null;
                return pages.Trim();
            }
        }

        public string Title
        {
            get
            {
                var title = ReferenceNode.Attributes["title"]?.Value;
                if (string.IsNullOrWhiteSpace(title)) return null;
                return title.Trim();
            }
        }

    }
}