using System;
using System.Collections.Generic;

namespace Wyam.Bibliography.References
{
    /// <summary>
    ///     Finds reference tags in a user-provided html content.
    /// </summary>
    internal class ReferenceFinder
    {
        internal ReferenceFinder(string contentBefore)
        {
            ContentBefore = contentBefore;
        }

        internal string ContentBefore { get; }

        internal List<ReferenceTag> References => throw new NotImplementedException();

        internal ReferenceListTag ReferenceList => throw new NotImplementedException();
    }
}