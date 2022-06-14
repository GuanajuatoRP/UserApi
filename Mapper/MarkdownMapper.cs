using UserApi.Data;
using UserApi.Models.Markdown;

namespace UserApi.Mapper
{
    public static class MarkdownMapper
    {
        public static MarkdownDTO ToModel(this Markdown md)
        {
            return new MarkdownDTO
            {
                TextId = md.TextId,
                CatName = md.CatName,
                Title = md.Title,
                RawText = md.RawText,
                FormatType = md.FormatType
            };
        }

        public static List<MarkdownsDTO> ToModelList(this List<Markdown> md)
        {
            List<string> catNames = md.Select(md => md.CatName).Distinct().ToList();

            var result = new List<MarkdownsDTO>();

            foreach (string catName in catNames)
            {
                MarkdownsDTO mdDTO = new MarkdownsDTO
                {
                    title = catName,
                    categories = new List<MarkdownsObjectDTO>(),
                };

                var mdFiltred = md.Where(m => m.CatName == catName).ToList();

                foreach (Markdown markdown in mdFiltred)
                {
                    MarkdownsObjectDTO mdObjDTO = new MarkdownsObjectDTO
                    {
                        title = markdown.Title,
                        rawText = markdown.RawText,
                    };
                    mdDTO.categories.Add(mdObjDTO);
                }

                result.Add(mdDTO);
            }

            return result;
        }
    }
}
