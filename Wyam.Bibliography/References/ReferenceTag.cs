using System;
using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using JetBrains.Annotations;

namespace Wyam.Bibliography.References
{
    /// <summary>
    ///     Parses user-provided reference tag and provides obtained information.
    /// </summary>
    internal class ReferenceTag
    {
        private static readonly Regex YearMatcher = new Regex(@"(?<year>\d{4})(-\d{2}-\d{2})?");
        private static readonly Regex DateMatcher = new Regex(@"(?<isoDate>\d{4}-\d{2}-\d{2})");

        public ReferenceTag(string htmlMarkup)
        {
            RawHtml = htmlMarkup;
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlMarkup);
            ReferenceNode = doc.DocumentNode.ChildNodes[0];

            var personNameParser = new PersonNameParser();
            Author = personNameParser.ParseName(ReferenceNode.Attributes["author"]?.Value);
            var idGenerator = new IdGenerator(new IdValidator());
            var userProvidedId = TrimAttributeValue(ReferenceNode.Attributes["id"]?.Value);
            Id = idGenerator.GetId(userProvidedId, Author, Year);

        }

        public string RawHtml { get; }
        private HtmlNode ReferenceNode { get; }

        public PersonName Author { get; }

        public DateTime? Date
        {
            get
            {
                var date = TrimAttributeValue(ReferenceNode.Attributes["date"]?.Value);
                if (date == null) return null;

                var dateMatch = DateMatcher.Match(date);
                if (dateMatch.Success)
                {
                    try
                    {
                        DateTime datetime = DateTime.ParseExact(dateMatch.Groups["isoDate"].Value, "yyyy-MM-dd",
                            CultureInfo.InvariantCulture);
                        return datetime;
                    }
                    catch (FormatException)
                    {
                        // eg date "2000-13-01": regex matches, but month is invalid.
                        return null;
                    }
                    
                }
                return null;
            }
        }

        public int? Year
        {
            get
            {
                var date = TrimAttributeValue(ReferenceNode.Attributes["date"]?.Value);
                if (date == null) return null;

                var dateMatch = YearMatcher.Match(date);
                if (dateMatch.Success)
                {
                    var year = Convert.ToInt32(dateMatch.Groups["year"].Value);
                    return year;
                }
                return null;
            }
        }

        public string Pages => TrimAttributeValue(ReferenceNode.Attributes["pages"]?.Value);
        public string Title => TrimAttributeValue(ReferenceNode.Attributes["title"]?.Value);
        public string Id { get; }
        public string Url => TrimAttributeValue(ReferenceNode.Attributes["url"]?.Value);

        public int? Edition
        {
            get
            {
                var attributeValue = TrimAttributeValue(ReferenceNode.Attributes["edition"]?.Value);
                int edition;
                if (int.TryParse(attributeValue, out edition) && edition > 0)
                    return edition;
                return null;
            }
        }

        public string Place => TrimAttributeValue(ReferenceNode.Attributes["place"]?.Value);
        public string Publisher => TrimAttributeValue(ReferenceNode.Attributes["publisher"]?.Value);
        public string Translator => TrimAttributeValue(ReferenceNode.Attributes["translator"]?.Value);

        private string TrimAttributeValue([CanBeNull] string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return value.Trim();
        }
    }
}