namespace Wyam.Bibliography.References
{
    internal class ReferenceListTag
    {
        public ReferenceListTag(string rawHtml)
        {
            RawHtml = rawHtml;
        }

        public string ReferenceStyle { get; private set; }
        public string RawHtml { get; }
    }
}