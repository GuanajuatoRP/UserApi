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
                Type = originalCar.Type,
                Price = originalCar.Price,
                Rarity = originalCar.Rarity,
                WikiLink = originalCar.WikiLink,
                PictureLink = originalCar.PictureLink,

                OriginalPowerHp = originalCar.PowerHp,
                OriginalWeightKg = originalCar.WeightKg,
                OriginalDriveTrain = originalCar.DriveTrain,
                OriginalClass = originalCar.Class,
                OriginalPi = originalCar.Pi,
                OriginalOnRoad = originalCar.OnRoad,
                OriginalSpeed = originalCar.Speed,
                OriginalHandling = originalCar.Handling,
                OriginalAccelerate = originalCar.Accelerate,
                OriginalLaunch = originalCar.Launch,
                OriginalBraking = originalCar.Braking,
                OriginalOffroad = originalCar.Offroad,

                EditPowerHp = car.PowerHp,
                EditWeightKg = car.WeightKG,
                EditDriveTrain = car.DriveTrain.ToString(),
                EditClass = car.Class.ToString(),
                EditPi = car.Pi,
                EditOnRoad = car.OnRoad,
                EditSpeed = car.Speed,
                EditHandling = car.Handling,
                EditAccelerate = car.Accelerate,
                EditLaunch = car.Launch,
                EditBraking = car.Braking,
                EditOffroad = car.Offroad,
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
