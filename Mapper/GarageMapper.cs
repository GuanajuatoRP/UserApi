using UserApi.Data;
using UserApi.Models.Car;
using UserApi.Models.Garage;

namespace UserApi.Mapper
{
    public static class GarageMapper
    {
        public static CarDTO ToModel(this Voitures car, OriginalCarDTO originalCar)
        {
            return new CarDTO
            {
                KeyCar = car.KeyCar,
                IdUser = car.IdUser,
                Username = car.User.UserName,
                IdCar = car.IdCar,
                CarId = originalCar.CarId,
                CarOrdinal = originalCar.CarOrdinal,
                Maker = originalCar.Maker,
                Model = originalCar.Model,
                Year = originalCar.Year,
                Transmission = car.Transmission,
                GearBox = car.GearBox,
                Type = originalCar.Type,
                Rarity = originalCar.Rarity,
                WikiLink = originalCar.WikiLink,
                PictureLink = originalCar.PictureLink,
                EngineConfiguration = originalCar.EngineConfiguration,

                OriginalPrice = originalCar.Price,
                OriginalAccelerate = originalCar.Accelerate,
                OriginalAspiration = originalCar.Aspiration,
                OriginalBraking = originalCar.Braking,
                OriginalClass = originalCar.Class,
                OriginalEngineDisplacement = originalCar.EngineDisplacement,
                OriginalEnginePosition = originalCar.EnginePosition,
                OriginalHandling = originalCar.Handling,
                OriginalLaunch = originalCar.Launch,
                OriginalNbCylindre = originalCar.NbCylindre,
                OriginalOffroad = originalCar.Offroad,
                OriginalPi = originalCar.Pi,
                OriginalPowerBhp = originalCar.PowerBhp,
                OriginalPowerKw = originalCar.PowerKw,
                OriginalSpeed = originalCar.Speed,
                OriginalTorqueLbft = originalCar.TorqueLbft,
                OriginalTorqueNm = originalCar.TorqueNm,
                OriginalWeightKg = originalCar.WeightKg,
                OriginalWeightLbs = originalCar.WeightLbs,

                PowerBhp = car.PowerBHP,
                PowerKw = car.PowerKW,
                TorqueLbft = car.TorqueLBFT,
                TorqueNm = car.TorqueNM,
                WeightLbs = car.WeightLBS,
                WeightKg = car.WeightKG,
                EngineDisplacement = car.EngineDisplacement,
                NbCylindre = car.NbCylindre,
                Aspiration = car.Aspiration.ToString(),
                EnginePosition = car.EnginePosition.ToString(),
                Speed = car.Speed,
                Handling = car.Handling,
                Accelerate = car.Accelerate,
                Launch = car.Launch,
                Braking = car.Braking,
                Offroad = car.Offroad,
                Pi = car.Pi,
                Class = car.Class.ToString(),
                Imatriculation = car.Imatriculation,
                EditPrice = car.PrixModif,
                TotalPrice = car.PrixTotal,
            };
        }

        
        public static Guid ToCarKey(this Voitures car)
        {
            return car.KeyCar;
        }
    }
}
