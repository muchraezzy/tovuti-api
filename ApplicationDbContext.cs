using System;
using System.Data.Entity;
using Npgsql;
using System.Configuration;
using tovuti_api.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace tovuti_api
{
    public partial class ApplicationDbContext : DbContext
    {
        private readonly string schema;
        public ApplicationDbContext(string schema) : base("DefaultConnection")
        {
            this.schema = schema;
            //System.Data.Entity.Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext("public");
        }
        public static NpgsqlConnection GetConnection()
        {


            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            NpgsqlConnection DBconn = null;

            try
            {

                DBconn = new NpgsqlConnection(connectionString);
                DBconn.Open();
            }
            catch (Exception ex)
            {
                //ex should be written into a error log
                ex.Data.Clear();
                // dispose  to avoid connections leak
                if (DBconn != null)
                {
                    DBconn.Dispose();
                }
            }
            return DBconn;
        }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<category> Categories { get; set; }
        public virtual DbSet<products> Products { get; set; }
        public virtual DbSet<attributes> Attributes { get; set; }
        // public virtual DbSet<Customers> Customers { get; set; }
        // public virtual DbSet<OTPEntries> OTPEntries { get; set; }
        // public virtual DbSet<ResetPasswordRequests> ResetPasswordRequests { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("public");
            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Tokens)
            //    .WithRequired(e => e.User)
            //    .WillCascadeOnDelete(false);

          //  modelBuilder.Entity<User>().ToTable("users");
            //  base.OnModelCreating(modelBuilder);
        }
    }
}
