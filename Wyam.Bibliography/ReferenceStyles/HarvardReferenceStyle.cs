using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Humanizer;
using JetBrains.Annotations;
using Wyam.Bibliography.References;

namespace Wyam.Bibliography.ReferenceStyles
{
    internal class HarvardReferenceStyle : IReferenceStyle
    {
        
        public HarvardReferenceStyle()
        {
          
        }

        /// <summary>
        ///     Orders references by author's last name.
        ///     "Citations are listed in alphabetical order by the author’s last name."
        ///     (http://www.citethisforme.com/harvard-referencing)
        /// </summary>
        /// <param name="allReferences"></param>
        /// <returns></returns>
        public IReadOnlyList<ReferenceTag> SortReferences([NotNull] IReadOnlyList<ReferenceTag> allReferences)
        {
            var invalidReferences = allReferences.Where(x => x.Author == null).ToList();
            if (invalidReferences.Any())
                throw new ArgumentException(
                    $"Author is required for reference when rendering in Harvard style. Found {invalidReferences.Count} invalid references, eg. {invalidReferences.First().RawHtml}");


            return allReferences.OrderBy(x => x.Author.LastName).ToList();
        }

        /// <summary>
        ///     Guide: http://guides.library.uwa.edu.au/c.php?g=380288&p=2637377
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public string RenderReference(ReferenceTag reference)
        {
            var inTextReference = "[*]"; // fallback value
            if (reference.Author != null)
            {
                var nameForInTextReference = reference.Author.LastName ?? reference.Author.UnprocessedAuthorString;
                if (nameForInTextReference != null)
                {
                    if (reference.Year != null)
                        if (reference.Pages == null)
                        {
                            inTextReference = $"({nameForInTextReference} {reference.Year.Value})";
                        }
                        else
                        {
                            // "The correct forms are p. for a single page, and pp. for a range." https://english.stackexchange.com/a/14539
                            var pagesAbbreviation = reference.Pages.Contains("-") ? "p." : "pp.";

                            inTextReference =
                                $"({nameForInTextReference} {reference.Year.Value}, {pagesAbbreviation} {reference.Pages})";
                        }
                    else
                        inTextReference = $"({nameForInTextReference})";
                }
            }
          

            var link = $"<a href='#{reference.Id}' class='resource-reference'>{inTextReference}</a>";
            return link;
        }

        public string RenderReferenceList([NotNull] ReferenceListTag referenceList,
            [NotNull] IReadOnlyList<ReferenceTag> sortedReferences)
        {
            var referenceListMarkup = new StringBuilder();

            // typically h1 is article's title in a well-structured document, so h2 was chosen as default
            var headerWrapperTag = referenceList.HeaderWrapper ?? "h2";
            // "(...) Use “Reference list” or “Literature list” as the heading." https://innsida.ntnu.no/wiki/-/wiki/English/Using+the+Harvard+reference+style
            var headerText = referenceList.HeaderText ?? "Reference List";

            referenceListMarkup.AppendLine(
                $@"<{headerWrapperTag} id='reference-list' class='reference-list'>{headerText}</{headerWrapperTag}>");
            referenceListMarkup.AppendLine($@"<ol id='reference-list-content'>");
            foreach (var reference in sortedReferences)
            {
                var renderedReference = RenderReferenceListItem(reference);
                referenceListMarkup.AppendLine(renderedReference);
            }
            referenceListMarkup.AppendLine($@"</ol>");

            var renderedReferenceList = referenceListMarkup.ToString();
            return renderedReferenceList;
        }

        [JetBrains.Annotations.Pure]
        internal string RenderReferenceListItem(ReferenceTag reference)
        {
            var content = RenderReferenceListItemContent(reference);
            return $@"<li id='{reference.Id}'>{content}</li>";
        }

        [JetBrains.Annotations.Pure]
        internal string RenderReferenceListItemContent(ReferenceTag reference)
        {
            if (reference.Url != null && reference.Date.HasValue && reference.Publisher == null
            ) // heuristics: seems like online content
                return RenderAsWebsite(reference);

            return RenderAsBook(reference); // default
        }

        [JetBrains.Annotations.Pure]
        private string RenderAsWebsite(ReferenceTag reference)
        {
            Contract.Assert(reference.Date.HasValue);
            Contract.Assert(reference.Url != null);

            var dateTime = reference.Date.Value;
            var monthAbbreviation = DateTimeFormatInfo.InvariantInfo.GetAbbreviatedMonthName(dateTime.Month);
            var referenceDateHumanized = $"{dateTime.Day} {monthAbbreviation}. {dateTime.Year}";

            var url = $"<a href='{reference.Url}' target='_blank' rel='nofollow'>{reference.Url}</a>";

            return $@"{reference.Author.UnprocessedAuthorString}, ({reference.Year}). <i>{
                    reference.Title
                }</i>. [online] Available at: {url} [Accessed {referenceDateHumanized}].";


        }

        [JetBrains.Annotations.Pure]
        private string RenderAsBook(ReferenceTag reference)
        {
            var edition = RenderEdition(reference.Edition);
            var publisher = RenderPublisher(reference.Publisher, reference.Place);

            var author = (reference.Author.Initials.Count == 0) // author name could not have been parsed using "Name FamilyName" template
                ? reference.Author.UnprocessedAuthorString
                : $@"{reference.Author.LastName}, {reference.Author.Initials[0]}";


            return $@"{author}. ({reference.Year}). <i>{
                    reference.Title
                }</i>.{edition}{publisher}";
        }


        private string RenderPublisher(string referencePublisher, string referencePlace)
        {
            if (string.IsNullOrEmpty(referencePublisher) || string.IsNullOrEmpty(referencePlace))
                return string.Empty;

            return $" {referencePlace}: {referencePublisher}.";
        }

        private string RenderEdition(int? edition)
        {
            if (edition == null) return string.Empty;
            var editionHumanized = edition.Value.Ordinalize(); // 1st, 2nd, 3rd etc...
            return $" {editionHumanized} ed.";
        }

    }
}