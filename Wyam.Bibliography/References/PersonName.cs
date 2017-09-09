using System.Collections.Generic;

namespace Wyam.Bibliography.References
{
    internal class PersonName
    {
        public PersonName(string firstName, string lastName, IReadOnlyList<char> initials)
        {
            FirstName = firstName;
            LastName = lastName;
            Initials = initials;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public IReadOnlyList<char> Initials { get; }
    }
}