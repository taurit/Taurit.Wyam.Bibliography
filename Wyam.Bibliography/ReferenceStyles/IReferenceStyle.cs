using System.Collections.Generic;
using Wyam.Bibliography.References;

namespace Wyam.Bibliography.ReferenceStyles
{
    internal interface IReferenceStyle
    {
        IReadOnlyList<ReferenceTag> SortReferences(IReadOnlyList<ReferenceTag> allReferences);
        string RenderReference(ReferenceTag reference);
        string RenderReferenceList(ReferenceListTag referenceList, IReadOnlyList<ReferenceTag> sortedReferences);
    }
}