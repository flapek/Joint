namespace Joint.Docs.Swagger.Options
{
    public class SwaggerOptions
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string RoutePrefix { get; set; }
        public bool IncludeSecurity { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get;  set; }
    }
}