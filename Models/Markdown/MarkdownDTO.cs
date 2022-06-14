namespace UserApi.Models.Markdown
{
    public class MarkdownDTO
    {
        public Guid TextId { get; set; }
        public string CatName { get; set; }
        public string Title { get; set; }
        public string RawText { get; set; }
        public string FormatType { get; set; }
    }
}
