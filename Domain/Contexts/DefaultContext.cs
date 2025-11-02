using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models;

namespace Domain.Contexts
{
    public class DefaultContext : RFCoreDbContext
    {
        public DefaultContext() : base("DefaultConnection", contextType: ContextType.PGSQL) { }
        public DefaultContext(SAASType sAASType = SAASType.NoSaas) : base("DefaultConnection", sAASType, ContextType.PGSQL) { }

        public DbSet<DataVerificationLog> DataVerificationLog { get; set; }
        public DbSet<UserGeo> UserGeo { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Upazila> Upazila { get; set; }
        public DbSet<Unions> Unions { get; set; }
        public DbSet<Village> Village { get; set; }
    }
}
