using UserApi.Data;
using UserApi.Models.Garage;

namespace UserApi.Mapper
{
    public static class GarageMapper
    {
        public static CarDTO ToModel(this Voitures car, CarDTO originalCar)
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
                Type = originalCar.Type,
                Rarity = originalCar.Rarity,
                WikiLink = originalCar.WikiLink,
                PictureLink = originalCar.PictureLink,
                EngineConfiguration = originalCar.EngineConfiguration,

                OriginalPrice = originalCar.OriginalPrice,
                Original_Accelerate = originalCar.Original_Accelerate,
                Original_Aspiration = originalCar.Original_Aspiration,
                Original_Braking = originalCar.Original_Braking,
                Original_Class = originalCar.Original_Class,
                Original_EngineDisplacement = originalCar.Original_EngineDisplacement,
                Original_EnginePosition = originalCar.Original_EnginePosition,
                Original_Handling = originalCar.Original_Handling,
                Original_Launch = originalCar.Original_Launch,
                Original_NbCylindre = originalCar.Original_NbCylindre,
                Original_Offroad = originalCar.Original_Offroad,
                Original_Pi = originalCar.Original_Pi,
                Original_PowerBhp = originalCar.Original_PowerBhp,
                Original_PowerKw = originalCar.Original_PowerKw,
                Original_Speed = originalCar.Original_Speed,
                Original_TorqueLbft = originalCar.Original_TorqueLbft,
                Original_TorqueNm = originalCar.Original_TorqueNm,
                Original_WeightKg = originalCar.Original_WeightKg,
                Original_WeightLbs = originalCar.Original_WeightLbs,

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
