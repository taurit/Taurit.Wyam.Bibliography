using System.Collections.Generic;

namespace Wyam.Bibliography.References
{
    internal class PersonName
    {
        public PersonName(string userProvidedAuthor, string firstName, string lastName, IReadOnlyList<char> initials)
        {
            FirstName = firstName;
            LastName = lastName;
            Initials = initials;
            UnprocessedAuthorString = userProvidedAuthor;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public IReadOnlyList<char> Initials { get; }
        public string UnprocessedAuthorString { get; }
    }
}