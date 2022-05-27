using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync("Insert Into Coupon(ProductName, Description, Amount) values (@ProductName, @Description, @Amount)", new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                });

                if(affected == 0)
                {
                    return false;
                }

                return true;
            }
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync("delete from Coupon where ProductName = @ProductName", new
                {
                    ProductName = productName,
                });

                if (affected == 0)
                {
                    return false;
                }

                return true;
            }
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("Select * from Coupon where ProductName = @ProductName", new { ProductName = productName });
                if(coupon == null)
                {
                    return new Coupon() { ProductName= "No Discount", Amount = 0, Description = "No Discount Desc" };
                }

                return coupon;
            }
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using (var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString")))
            {
                var affected = await connection.ExecuteAsync("update Coupon set ProductName = @ProductName, Description = @Description, Amount = @Amount where Id = @Id", new
                {
                    ProductName = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                    Id = coupon.Id,
                });

                if (affected == 0)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
