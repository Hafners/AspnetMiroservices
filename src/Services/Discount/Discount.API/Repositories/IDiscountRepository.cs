using Discount.API.Entities;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);

        Task<bool> DeleteDiscount(string productName);

        Task<bool> UpdateDiscount(Coupon coupon);

        Task<bool> CreateDiscount(Coupon coupon);
    }
}
