using System;
using System.Collections.Generic;
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
        public List<ReferenceTag> SortReferences(IReadOnlyList<ReferenceTag> allReferences)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Guide: http://guides.library.uwa.edu.au/c.php?g=380288&p=2637377
        /// </summary>
        /// <param name="referenceParsedReference"></param>
        /// <returns></returns>
        public string RenderReference(ReferenceTag referenceParsedReference)
        {
            throw new NotImplementedException();
        }

        public string RenderReferenceList(ReferenceListTag referenceList)
        {
            throw new NotImplementedException();
        }
    }
}