namespace Ranges
{
    public sealed class ListItem<T>
    {
        private readonly ListItem<T> _next;

        public ListItem(ListItem<T> next)
        {
            _next = next;
        }

        public T Data { get; set; }

        public ListItem<T> Next
        {
            get
            {
                return _next;
            }
        }
    }
}
