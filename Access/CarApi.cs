using UserApi.Models.Car;
using System.Text.Json;
using System.Text;

namespace UserApi.Access
{
    public class CarApi
    {
        public static async Task<OriginalCarDTO> GetCarById(Guid Id)
        {
            using HttpClient client = new HttpClient();
            var response = await client.GetAsync($"http://172.17.0.4:443/api/Cars/{Id}");

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                OriginalCarDTO? originalCar = JsonSerializer.Deserialize<OriginalCarDTO>(json);
                if (originalCar == null) throw new Exception("Aucune voiture trouvée avec cet id"); 
                return originalCar;
            }
            throw new Exception($"{response.StatusCode}, {response.ReasonPhrase}");
            
        }
        public static async Task<List<OriginalCarDTO>> GetCarsByIds(IEnumerable<Guid> ids)
        {
            var json = JsonSerializer.Serialize(ids);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpClient client = new HttpClient();
            var response = await client.PostAsync($"https://172.17.0.4:443/api/Cars/CarsIds", data);

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                List<OriginalCarDTO>? listOriginalCar = JsonSerializer.Deserialize<List<OriginalCarDTO>>(json);
                if (listOriginalCar == null) throw new Exception("Aucune voiture trouvée avec cet id"); 
                return listOriginalCar;
            }
            throw new Exception($"{response.StatusCode}, {response.ReasonPhrase}");
            
        }
    }
}
