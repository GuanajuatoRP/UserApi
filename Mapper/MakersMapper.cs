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
            };
        }

        public static GetMakerDTO ToModel(this Maker maker)
        {
            return new GetMakerDTO
            {
                IdMaker = maker.IdMaker,
                Name = maker.Name,
                Origin = maker.Origin,
                Cars = maker.Cars.Select(c => c.ToModel()).ToList(),
            };
        }
    }
}
