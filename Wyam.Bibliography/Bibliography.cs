using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using Wyam.Bibliography.Localization;
using Wyam.Bibliography.References;
using Wyam.Bibliography.ReferenceStyles;
using Wyam.Common.Documents;
using Wyam.Common.Execution;
using Wyam.Common.Modules;

namespace Wyam.Bibliography
{
    /// <summary>
    ///     Bibliography module for Wyam (https://wyam.io/).
    ///     See README.md for a high-level overview.
    /// </summary>
    public class Bibliography : IModule
    {
        public IEnumerable<IDocument> Execute(IReadOnlyList<IDocument> inputs, IExecutionContext context)
        {
            var documents = new List<IDocument>();

            foreach (var input in inputs)
            {
                var contentBefore = new StreamReader(input.GetStream()).ReadToEnd();
                var contentAfter = ProcessBibliographicReferences(contentBefore);
                var modifiedContentAsStream = context.GetContentStream(contentAfter);

                var doc = context.GetDocument(input, modifiedContentAsStream, new Dictionary<string, object>());
                documents.Add(doc);
            }

            return documents;
        }

        internal string ProcessBibliographicReferences(string contentBefore)
        {
            // does content require processing references?
            var referenceFinder = new ReferenceFinder(contentBefore);
            var referenceList = referenceFinder.ReferenceList;

            // edge cases
            // no references:
            if (referenceFinder.ContentContainsAnyReferences == false && referenceList == null)
                return contentBefore;
            // no reference list
            if (referenceFinder.ContentContainsAnyReferences && referenceList == null)
                return RemoveAllSubstrings(contentBefore, referenceFinder.References.Select(x => x.RawHtml));
            // no references
            Contract.Assert(referenceList != null);
            if (referenceFinder.ContentContainsAnyReferences == false)
                return contentBefore.Replace(referenceList.RawHtml, string.Empty);

            // sort references according to style rules
            // Allows properly "humanize" strings like 2 -> "2nd" on non-english OS versions:
            using (new SpecificCulture("en-US"))
            {
                var referenceStyle = ReferenceStyleFactory.Get("Harvard"); // currently the only one implemented
                var sortedReferences = referenceStyle.SortReferences(referenceFinder.References);
                var contentAfter = ReplaceInTextReferences(contentBefore, sortedReferences, referenceStyle);
                contentAfter = RenderReferenceList(referenceStyle, referenceList, sortedReferences, contentAfter);
                return contentAfter;
            }
        }

        private static string RenderReferenceList(IReferenceStyle referenceStyle, ReferenceListTag referenceList,
            IReadOnlyList<ReferenceTag> sortedReferences, string contentAfter)
        {
            var renderedReferenceList = referenceStyle.RenderReferenceList(referenceList, sortedReferences);
            contentAfter = contentAfter.Replace(referenceList.RawHtml, renderedReferenceList);
            return contentAfter;
        }

        /// <summary>
        ///     Replace in-text references with a hyperlink and in-text description
        /// </summary>
        /// <param name="contentBefore"></param>
        /// <param name="sortedReferences"></param>
        /// <param name="referenceStyle"></param>
        /// <returns></returns>
        private static string ReplaceInTextReferences(string contentBefore,
            IReadOnlyList<ReferenceTag> sortedReferences,
            IReferenceStyle referenceStyle)
        {
            var contentAfter = contentBefore;
            foreach (var reference in sortedReferences)
            {
                var renderedReference = referenceStyle.RenderReference(reference);
                contentAfter = contentAfter.Replace(reference.RawHtml, renderedReference);
            }
            return contentAfter;
        }

        private string RemoveAllSubstrings(string content, IEnumerable<string> substrings)
        {
            foreach (var substring in substrings)
                content = content.Replace(substring, string.Empty);
            return content;
        }
    }
}