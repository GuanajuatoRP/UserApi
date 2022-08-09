using UserApi.Data;
using UserApi.Models.Makers;

namespace UserApi.Mapper
{
    public static class MakersMapper
    {
        public static GetMakerListDTO ToModelList(this Maker maker)
        {
            return new GetMakerListDTO
            {
                IdMaker = maker.IdMaker,
                Name = maker.Name,
                Origin = maker.Origin,
                Founded = maker.Founded,
                Description = maker.Description,
                Related = maker.Related,
                WikiLink = maker.WikiLink
            };
        }

        public static GetMakerDTO ToModel(this Maker maker)
        {
            return new GetMakerDTO
            {
                IdMaker = maker.IdMaker,
                Name = maker.Name,
                Origin = maker.Origin,
                Founded = maker.Founded,
                Description = maker.Description,
                Related = maker.Related,
                WikiLink = maker.WikiLink,
                Cars = maker.Cars.Select(c => c.ToModel()).ToList(),
            };
        }
    }
}
