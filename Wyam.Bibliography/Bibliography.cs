using System.Collections.Generic;
using System.IO;
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

        private string ProcessBibliographicReferences(string contentBefore)
        {
            var referenceFinder = new ReferenceFinder(contentBefore);

            // find all reference tags in page content
            var allReferences = referenceFinder.References;
            var referenceList = referenceFinder.ReferenceList;

            // sort references according to style rules
            var referenceStyle = ReferenceStyleFactory.Get(referenceList.ReferenceStyle);
            allReferences = referenceStyle.SortReferences(allReferences);

            var contentAfter = contentBefore;
            foreach (var reference in allReferences)
            {
                // replace in-text references with a hyperlink and number
                var renderedReference = referenceStyle.RenderReference(reference);
                contentAfter = contentAfter.Replace(reference.RawHtml, renderedReference);
            }

            var renderedReferenceList = referenceStyle.RenderReferenceList(referenceList);
            contentAfter = contentAfter.Replace(referenceList.RawHtml, renderedReferenceList);

            return contentAfter;
        }
    }
}