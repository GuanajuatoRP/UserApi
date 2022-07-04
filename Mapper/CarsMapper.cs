using UserApi.Data;
using UserApi.Models.Car;

namespace UserApi.Mapper
{
    public static class CarsMapper
    {
        public static GetOriginalCarListDTO ToModel(this Car car)
        {
            return new GetOriginalCarListDTO
            {
                IdCar = car.IdCar,
                CarId = car.CarId,
                CarOrdinal = car.CarOrdinal,
                Maker = car.Maker.Name,
                Model = car.Model,
                Year = car.Year,
                PowerBhp = car.PowerBhp,
                PowerKw = car.PowerKw,
                TorqueLbft = car.TorqueLbft,
                TorqueNm = car.TorqueNm,
                WeightLbs = car.WeightLbs,
                WeightKg = car.WeightKg,
                EngineDisplacement = car.EngineDisplacement,
                NbCylindre = car.NbCylindre,
                Aspiration = car.Aspiration,
                EngineConfiguration = car.EngineConfiguration,
                EnginePosition = car.EnginePosition,
                Transmission = car.Transmission,
                GearBox = car.GearBox,
                Speed = car.Speed,
                Handling = car.Handling,
                Accelerate = car.Accelerate,
                Launch = car.Launch,
                Braking = car.Braking,
                Offroad = car.Offroad,
                Pi = car.Pi,
                Class = car.Class,
                RequiredDlc = car.RequiredDlc,
                Aviability = car.Aviability,
                Price = car.Price,
                Type = car.Type,
                Rarity = car.Rarity,
                WikiLink = car.WikiLink,
                PictureLink = car.PictureLink

            };
        }
    }
}
