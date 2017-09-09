using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Humanizer;
using JetBrains.Annotations;
using Wyam.Bibliography.References;

namespace Wyam.Bibliography.ReferenceStyles
{
    internal class HarvardReferenceStyle : IReferenceStyle
    {
        /// <summary>
        ///     Orders references by author's last name.
        ///     "Citations are listed in alphabetical order by the author’s last name."
        ///     (http://www.citethisforme.com/harvard-referencing)
        /// </summary>
        /// <param name="allReferences"></param>
        /// <returns></returns>
        public IReadOnlyList<ReferenceTag> SortReferences(IReadOnlyList<ReferenceTag> allReferences)
        {
            return allReferences.OrderBy(x => x.Author.LastName).ToList();
        }

        /// <summary>
        ///     Guide: http://guides.library.uwa.edu.au/c.php?g=380288&p=2637377
        /// </summary>
        /// <param name="reference"></param>
        /// <returns></returns>
        public string RenderReference(ReferenceTag reference)
        {
            string inTextReference = "[*]"; // fallback value
            if (reference.Author?.LastName != null)
            {
                if (reference.Year != null)
                {
                    inTextReference = reference.Pages != null
                        ? $"({reference.Author.LastName} {reference.Year.Value}, p. {reference.Pages})"
                        : $"({reference.Author.LastName} {reference.Year.Value})";
                }
                else
                    inTextReference = $"({reference.Author.LastName})";
            }

            string link = $"<a href='#{reference.Id}' class='resource-reference'>{inTextReference}</a>";
            return link;
        }

        public string RenderReferenceList(ReferenceListTag referenceList, IReadOnlyList<ReferenceTag> sortedReferences)
        {
            StringBuilder referenceListMarkup = new StringBuilder();

            // typically h1 is article's title in a well-structured document, so h2 was chosen as default
            string headerWrapperTag = referenceList.HeaderWrapper ?? "h2"; 

            referenceListMarkup.AppendLine($@"<{headerWrapperTag} id='reference-list'>Reference list</{headerWrapperTag}>");
            referenceListMarkup.AppendLine($@"<ol>");
            foreach (var reference in sortedReferences)
            {
                string renderedReference = RenderReference(reference);
                referenceListMarkup.AppendLine(renderedReference);
            }
            referenceListMarkup.AppendLine($@"</ol>");

            var renderedReferenceList = referenceListMarkup.ToString();
            return renderedReferenceList;
        }

        [Pure]
        private string RenderReferenceListItem(ReferenceTag reference)
        {
            string edition = RenderEdition(reference.Edition);
            string publisher = RenderPublisher(reference.Publisher, reference.Place);

            return $@"<li id='#{reference.Id}'>
                        {reference.Author.LastName}, {reference.Author.Initials[0]}. ({reference.Year}) <i>{reference.Title}</i>.{edition}{publisher}
                    </li>";
        }

        private string RenderPublisher(string referencePublisher, string referencePlace)
        {
            return $"{referencePlace}: {referencePublisher}.";
        }

        private string RenderEdition(int? edition)
        {
            if (edition == null) return String.Empty;
            string editionHumanized = edition.Value.Ordinalize(); // 1st, 2nd, 3rd etc...
            return $"{editionHumanized} edn.";

        }
    }
}