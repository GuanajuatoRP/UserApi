using UserApi.Data;
using UserApi.Data.Enum;
using UserApi.Models.Car;

namespace UserApi.Mapper
{
    public static class CarsMapper
    {
        public static OriginalCarDTO ToModel(this OriginalCar car)
        {
            return new OriginalCarDTO
            {
                IdCar = car.IdCar,
                CarId = car.CarId,
                CarOrdinal = car.CarOrdinal,
                Maker = car.Maker.Name,
                Origin = car.Maker.Origin ?? string.Empty,
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

        public static Voitures ToVoiture(this OriginalCar car, string userId)
        {
            return new Voitures
            {
                IdUser = userId,
                IdCar = car.IdCar,
                PowerBHP = car.PowerBhp,
                PowerKW = car.PowerKw,
                TorqueLBFT = car.TorqueLbft,
                TorqueNM = car.TorqueNm,
                WeightKG = car.WeightKg,
                WeightLBS = car.WeightLbs,
                EngineDisplacement = car.EngineDisplacement,
                NbCylindre = car.NbCylindre,
                EnginePosition = (EnginePosition)Enum.Parse(typeof(EnginePosition), car.EnginePosition),
                Aspiration = (Aspiration)Enum.Parse(typeof(Aspiration), car.Aspiration),
                PrixModif = 0,
                PrixTotal = car.Price,
                Transmission = car.Transmission,
                GearBox = car.GearBox,
                Speed = car.Speed,
                Handling = car.Handling,
                Accelerate = car.Accelerate,
                Launch = car.Launch,
                Braking = car.Braking,
                Offroad = car.Offroad,
                Pi = car.Pi,
                Class = (Class)Enum.Parse(typeof(Class), car.Class),
                Imatriculation = ""
            };
        }
    }
}
