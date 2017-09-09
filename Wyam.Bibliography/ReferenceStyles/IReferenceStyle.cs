using System.Collections.Generic;
using Wyam.Bibliography.References;

namespace Wyam.Bibliography.ReferenceStyles
{
    internal interface IReferenceStyle
    {
        List<ReferenceTag> SortReferences(IReadOnlyList<ReferenceTag> allReferences);
        string RenderReference(ReferenceTag referenceParsedReference);
        string RenderReferenceList(ReferenceListTag referenceList);
    }
}