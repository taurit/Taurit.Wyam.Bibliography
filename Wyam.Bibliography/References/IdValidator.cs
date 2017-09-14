using System;
using JetBrains.Annotations;

namespace Wyam.Bibliography.References
{
    /// <summary>
    ///     Helps ensure that user-provided ID is a valid HTML id.
    /// </summary>
    internal class IdValidator
    {
        internal bool IsValid([CanBeNull]string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return false;

            // todo: additional validation rules

            return true;
        }
    }
}