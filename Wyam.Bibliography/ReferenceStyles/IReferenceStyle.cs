using System.Collections.Generic;
using JetBrains.Annotations;
using Wyam.Bibliography.References;

namespace Wyam.Bibliography.ReferenceStyles
{
    internal interface IReferenceStyle
    {
        IReadOnlyList<ReferenceTag> SortReferences([NotNull] IReadOnlyList<ReferenceTag> allReferences);
        string RenderReference([NotNull] ReferenceTag reference);

        string RenderReferenceList([NotNull] ReferenceListTag referenceList,
            [NotNull] IReadOnlyList<ReferenceTag> sortedReferences);
    }
}