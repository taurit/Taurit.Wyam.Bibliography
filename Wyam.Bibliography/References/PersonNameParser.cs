using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace Wyam.Bibliography.References
{
    internal class PersonNameParser
    {
        private static readonly Regex NameMatcher = new Regex(@"(?<firstName>\w*)\s*((?<initial>\w\.)\s*)?(?<lastName>\w*)");

        public PersonName ParseName(string author)
        {
            if (string.IsNullOrWhiteSpace(author)) return null;
            author = author.Trim();
            var match = NameMatcher.Match(author);
            if (!match.Success) return null;

            string firstName = match.Groups["firstName"]?.Value;
            string lastName = match.Groups["lastName"]?.Value;
            string initial = match.Groups["initial"]?.Value;

            List<char> initials = new List<char>(2);
            Contract.Assert(firstName != null);
            initials.Add(firstName[0]);
            if (!String.IsNullOrEmpty(initial))
            {
                Contract.Assert(initial.Length == 2);
                Contract.Assert(initial[1] == '.');

                initials.Add(initial[0]);
            }
                
            return new PersonName(firstName, lastName, initials);
        }
    }
}