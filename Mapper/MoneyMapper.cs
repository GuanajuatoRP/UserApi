using UserApi.Data;
using UserApi.Models.Money;

namespace UserApi.Mapper
{
    public static class MoneyMapper
    {
        public static MoneyDTO ToMoneyDto(this ApiUser user)
        {
            return new MoneyDTO
            {
                Money = user.Argent,
            };
        }
    }
}
