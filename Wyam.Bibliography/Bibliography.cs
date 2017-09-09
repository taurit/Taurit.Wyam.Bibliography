using System;
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
            // does content require processing references?
            var referenceFinder = new ReferenceFinder(contentBefore);
            var referenceList = referenceFinder.ReferenceList;
            if (!referenceFinder.ContentContainsAnyReferences || referenceList == null)
                return contentBefore;

            // sort references according to style rules
            var referenceStyle = ReferenceStyleFactory.Get("Harvard"); // currently the only one implemented
            var sortedReferences = referenceStyle.SortReferences(referenceFinder.References);

            // replace in-text references with a hyperlink and in-text description
            var contentAfter = contentBefore;
            foreach (var reference in sortedReferences)
            {
                
                var renderedReference = referenceStyle.RenderReference(reference);
                contentAfter = contentAfter.Replace(reference.RawHtml, renderedReference);
            }

            // generate reference list
            var renderedReferenceList = referenceStyle.RenderReferenceList(referenceList, sortedReferences);
            contentAfter = contentAfter.Replace(referenceList.RawHtml, renderedReferenceList);

            return contentAfter;

        }
    }
}