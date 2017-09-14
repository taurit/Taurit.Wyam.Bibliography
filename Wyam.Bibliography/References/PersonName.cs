using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Wyam.Bibliography.References
{
    internal class PersonName
    {
        public PersonName(string userProvidedAuthor, string firstName, string lastName, IReadOnlyList<char> initials)
        {
            FirstName = ConvertToNullIfEmpty(firstName);
            LastName = ConvertToNullIfEmpty(lastName);
            Initials = initials;
            UnprocessedAuthorString = ConvertToNullIfEmpty(userProvidedAuthor);
        }

        private string ConvertToNullIfEmpty(string s)
        {
            if (String.IsNullOrWhiteSpace(s)) return null;
            return s;
        }

        [CanBeNull]
        public string FirstName { get; }
        [CanBeNull]
        public string LastName { get; }
        [NotNull]
        public IReadOnlyList<char> Initials { get; }
        [CanBeNull]
        public string UnprocessedAuthorString { get; }
    }
}