namespace UserApi.Models.Markdown
{
    public class MarkdownsObjectDTO
    {
        public string title { get; set; }
        public string rawText { get; set; }
    }
    
    public class MarkdownsDTO
    {
        public string title { get; set; }
        public List<MarkdownsObjectDTO> categories { get; set; }
    }
}
