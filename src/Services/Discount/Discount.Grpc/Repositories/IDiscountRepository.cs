using Discount.Grpc.Entities;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);

        Task<bool> DeleteDiscount(string productName);

        Task<bool> UpdateDiscount(Coupon coupon);

        Task<bool> CreateDiscount(Coupon coupon);
    }
}
