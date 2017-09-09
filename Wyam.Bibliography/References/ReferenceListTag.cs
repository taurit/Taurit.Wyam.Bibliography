using HtmlAgilityPack;
using JetBrains.Annotations;

namespace Wyam.Bibliography.References
{
    internal class ReferenceListTag
    {
        public ReferenceListTag(string rawHtml)
        {
            RawHtml = rawHtml;
            var doc = new HtmlDocument();
            doc.LoadHtml(rawHtml);
            ReferenceListNode = doc.DocumentNode.ChildNodes[0];
        }

        public string Id => TrimAttributeValue(ReferenceListNode.Attributes["id"]?.Value);

        private HtmlNode ReferenceListNode { get; }

        /// <summary>
        ///     HTML tag that will wrap <see cref="HeaderText" /> in output markup
        /// </summary>
        public string HeaderWrapper => TrimAttributeValue(ReferenceListNode.Attributes["header-wrapper"]?.Value);

        /// <summary>
        ///     Non-default text displayed to the user as a section header (eg. "Reference List", "Bibliography", localized
        ///     headers).
        ///     If null or empty, default header will be used (eg. for Harvard style it's "Reference List").
        /// </summary>
        public string HeaderText => TrimAttributeValue(ReferenceListNode.Attributes["header"]?.Value);

        public string RawHtml { get; }

        private string TrimAttributeValue([CanBeNull] string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            return value.Trim();
        }
    }
}