namespace Restaurant.web
{
    public static class StaticDetails
    {
        public static string ProductAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }


        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
