namespace Wyam.Bibliography.ReferenceStyles
{
    internal static class ReferenceStyleFactory
    {
        public static IReferenceStyle Get(string referenceStyleName)
        {
            switch (referenceStyleName)
            {
                case "Harvard":
                    return new HarvardReferenceStyle();
                default:
                    return new HarvardReferenceStyle(); // default to Harvard
            }
        }
    }
}