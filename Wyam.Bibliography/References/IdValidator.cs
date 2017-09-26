using System;
using JetBrains.Annotations;

namespace Wyam.Bibliography.References
{
    /// <summary>
    ///     Helps ensure that user-provided ID is a valid HTML id.
    /// </summary>
    internal class IdValidator
    {
        /// <summary>
        /// "The value must be unique amongst all the IDs in the element’s home subtree and must contain at least one character. The value must not contain any space characters."
        /// https://stackoverflow.com/a/15337855
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool IsValid([CanBeNull]string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return false;

            if (id.Contains(" "))
                return false;
            
            // todo: additional validation rules

            return true;
        }
    }
}