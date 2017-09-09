using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Wyam.Bibliography.References
{
    /// <summary>
    ///     Finds reference tags in a user-provided html content.
    /// </summary>
    internal class ReferenceFinder
    {
        private static readonly Regex ReferenceTagRegex =
            new Regex(@"\<reference\s.*?\/\>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        private static readonly Regex ReferenceListTagRegex =
            new Regex(@"\<reference-list\s.*?\/\>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

        internal ReferenceFinder([CanBeNull] string contentBefore)
        {
            References = FindReferenceTags(contentBefore);
            ReferenceList = FindReferenceListTag(contentBefore);
        }

        [NotNull]
        internal List<ReferenceTag> References { get; }

        [CanBeNull]
        internal ReferenceListTag ReferenceList { get; }

        public bool ContentContainsAnyReferences => this.References.Count > 0;

        [Pure]
        private List<ReferenceTag> FindReferenceTags([CanBeNull] string htmlContent)
        {
            var foundTags = new List<ReferenceTag>();
            if (htmlContent == null) return foundTags;

            var match = ReferenceTagRegex.Match(htmlContent);
            while (match.Success)
            {
                var tag = new ReferenceTag(match.Groups[0].Value);
                foundTags.Add(tag);

                match = match.NextMatch();
            }
            return foundTags;
        }

        [Pure]
        private ReferenceListTag FindReferenceListTag([CanBeNull] string htmlContent)
        {
            if (htmlContent == null) return null;
            var match = ReferenceListTagRegex.Match(htmlContent);
            return match.Success ? new ReferenceListTag(match.Groups[0].Value) : null;
        }
    }
}