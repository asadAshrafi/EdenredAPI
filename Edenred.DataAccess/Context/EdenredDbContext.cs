using Edenred.Model;
using Microsoft.EntityFrameworkCore;

namespace Edenred.DataAccess.Context
{
    public class EdenredDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }
        public EdenredDbContext(DbContextOptions<EdenredDbContext> options) : base(options)
        {

        }
    }
}