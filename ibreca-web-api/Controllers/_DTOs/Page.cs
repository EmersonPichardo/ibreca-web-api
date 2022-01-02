namespace ibreca_web_api.Controllers
{
    public class Page<T>
    {
        public T[] List { get; set; }
        public int Number { get; set; }
        public int TotalLength { get; set; }
        public bool HasMore
        {
            get
            {
                return TotalLength > Page.Length * Number;
            }
        }
    }

    public static class Page
    {
        public static readonly int Length = 6;
    }
}
