using Microsoft.EntityFrameworkCore;
using SVSTTest002Lib;

namespace SVSTTest02.Data
{
    public class AppDataContextModel : DbContext
    {
        public DbSet<GAS_VALUESModel>? GAS_VALUES { get; set; }

        public AppDataContextModel(DbContextOptions<AppDataContextModel> options) : base(options)
        {
        }
    }
}