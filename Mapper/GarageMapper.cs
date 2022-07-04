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
                Transmission = originalCar.Transmission,
                GearBox = originalCar.GearBox,
                Type = originalCar.Type,
                Rarity = originalCar.Rarity,
                WikiLink = originalCar.WikiLink,
                PictureLink = originalCar.PictureLink,
                EngineConfiguration = originalCar.EngineConfiguration,

                OriginalPrice = originalCar.Price,
                Original_Accelerate = originalCar.Accelerate,
                Original_Aspiration = originalCar.Aspiration,
                Original_Braking = originalCar.Braking,
                Original_Class = originalCar.Class,
                Original_EngineDisplacement = originalCar.EngineDisplacement,
                Original_EnginePosition = originalCar.EnginePosition,
                Original_Handling = originalCar.Handling,
                Original_Launch = originalCar.Launch,
                Original_NbCylindre = originalCar.NbCylindre,
                Original_Offroad = originalCar.Offroad,
                Original_Pi = originalCar.Pi,
                Original_PowerBhp = originalCar.PowerBhp,
                Original_PowerKw = originalCar.PowerKw,
                Original_Speed = originalCar.Speed,
                Original_TorqueLbft = originalCar.TorqueLbft,
                Original_TorqueNm = originalCar.TorqueNm,
                Original_WeightKg = originalCar.WeightKg,
                Original_WeightLbs = originalCar.WeightLbs,

                PowerBhp = car.Power_BHP,
                PowerKw = car.Power_KW,
                TorqueLbft = car.Torque_LBFT,
                TorqueNm = car.Torque_NM,
                WeightLbs = car.Weight_LBS,
                WeightKg = car.Weight_KG,
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
