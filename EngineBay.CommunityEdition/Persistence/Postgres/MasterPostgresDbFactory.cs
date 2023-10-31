namespace EngineBay.CommunityEdition
{
    using EngineBay.DatabaseManagement;
    using EngineBay.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class MasterPostgresDbFactory : IDesignTimeDbContextFactory<MasterPostgresDb>
    {
        public MasterPostgresDb CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ModuleWriteDbContext>();
            optionsBuilder.UseNpgsql();

            return new MasterPostgresDb(optionsBuilder.Options);
        }
    }
}