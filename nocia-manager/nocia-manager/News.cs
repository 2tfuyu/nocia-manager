namespace nocia_manager
{
    internal class News
    {
        public string title { get; set; }
        public string author { get; set; }
        public string date { get; set; }
        public string body { get; set; }

        public override string ToString()
        {
            return $"{title}";
        }
    }
}