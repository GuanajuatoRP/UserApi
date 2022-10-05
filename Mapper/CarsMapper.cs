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
                CarOrdinal = car.CarOrdinal,
                Maker = car.Maker.Name,
                Model = car.Model,
                Year = car.Year,
                PowerHp = car.PowerHp,
                WeightKg = car.WeightKg,
                DriveTrain = car.DriveTrain,
                Class = car.Class,
                Pi = car.Pi,
                OnRoad = car.OnRoad,
                Speed = car.Speed,
                Handling = car.Handling,
                Accelerate = car.Accelerate,
                Launch = car.Launch,
                Braking = car.Braking,
                Offroad = car.Offroad,
                RequiredDlc = car.RequiredDlc,
                Aviability = car.Aviability,
                Price = car.Price,
                Type = car.Type,
                Rarity = car.Rarity,
                WikiLink = car.WikiLink,
                PictureLink = car.PictureLink

            };
        }

        public static OriginalCar ToEntity(this OriginalCarDTO car, Maker maker)
        {
            return new OriginalCar
            {
                IdCar = car.IdCar,
                CarOrdinal = car.CarOrdinal,
                Maker = maker,
                Model = car.Model,
                Year = car.Year,
                PowerHp = car.PowerHp,
                WeightKg = car.WeightKg,
                DriveTrain = car.DriveTrain,
                Class = car.Class,
                Pi = car.Pi,
                OnRoad = car.OnRoad,
                Speed = car.Speed,
                Handling = car.Handling,
                Accelerate = car.Accelerate,
                Launch = car.Launch,
                Braking = car.Braking,
                Offroad = car.Offroad,
                RequiredDlc = car.RequiredDlc,
                Aviability = car.Aviability,
                Price = car.Price,
                Type = car.Type,
                Rarity = car.Rarity,
                WikiLink = car.WikiLink,
                PictureLink = car.PictureLink
            };
        }
        
        public static OriginalCar ToEntity(this CreateOriginalCarDTO car, Maker maker)
        {
            return new OriginalCar
            {
                CarOrdinal = car.CarOrdinal,
                Maker = maker,
                Model = car.Model,
                Year = car.Year,
                PowerHp = car.PowerHp,
                WeightKg = car.WeightKg,
                DriveTrain = car.DriveTrain,
                Class = car.Class,
                Pi = car.Pi,
                OnRoad = car.OnRoad,
                Speed = car.Speed,
                Handling = car.Handling,
                Accelerate = car.Accelerate,
                Launch = car.Launch,
                Braking = car.Braking,
                Offroad = car.Offroad,
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
                PowerHp = car.PowerHp,
                WeightKG = car.WeightKg,
                DriveTrain = (DriveTrain)Enum.Parse(typeof(DriveTrain), car.DriveTrain),
                Class = (Class)Enum.Parse(typeof(Class), car.Class),
                Pi = car.Pi,
                OnRoad = car.OnRoad,
                Speed = car.Speed,
                Handling = car.Handling,
                Accelerate = car.Accelerate,
                Launch = car.Launch,
                Braking = car.Braking,
                Offroad = car.Offroad,
                PrixModif = 0,
                PrixTotal = car.Price,
                Imatriculation = ""
            };
        }
    }
}
