using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Models.Model.BillingGiftCode;
using System.Text;
using System.Threading.Tasks;

namespace System.EntityFramework.Data
{
    public class GiftCodeDbContext : DbContext
    {
        public GiftCodeDbContext(DbContextOptions<GiftCodeDbContext> options) : base(options)
        {
        }

        public DbSet<GiftCode> GifCode { get; set; }
        public DbSet<GiftCodeData> GifCodeData { get; set; }
    }
}
