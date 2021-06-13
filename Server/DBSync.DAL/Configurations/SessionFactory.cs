using DBSync.DAL.Mappings;
using DBSync.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace DBSync.DAL.Configurations
{
    public class SessionFactory
    {
        public SessionFactory(string connectionString, DatabaseType databaseType)
        {
            if (_sessionFactory == null)
            {
                _sessionFactory = databaseType switch
                {
                    DatabaseType.SQLServer => BuildSessionFactory_SQLServer(connectionString),
                    DatabaseType.MySQL => BuildSessionFactory_MySQL(connectionString),
                    _ => BuildSessionFactory_SQLite(connectionString),
                };
            }
        }

        public ISession OpenSession() => _sessionFactory.OpenSession();


        private ISessionFactory BuildSessionFactory_SQLServer(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(BaseMap<BaseModel>).Assembly))
                .ExposeConfiguration(cfg =>
                {
                    new SchemaUpdate(cfg).Execute(true, true);
                    cfg.SetProperty(Environment.CommandTimeout, "2000");
                });
            return configuration.BuildSessionFactory();
        }

        private ISessionFactory BuildSessionFactory_SQLite(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(typeof(BaseMap<BaseModel>).Assembly))
                    .ExposeConfiguration(cfg => {
                        new SchemaUpdate(cfg).Execute(false, true);
                        cfg.SetProperty(Environment.CommandTimeout, "2000");
                    });
            return configuration.BuildSessionFactory();
        }

        private ISessionFactory BuildSessionFactory_MySQL(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                            .Database(MySQLConfiguration.Standard.ConnectionString(connectionString))
                            .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(BaseMap<BaseModel>).Assembly))
                             .ExposeConfiguration(cfg =>
                             {
                                 new SchemaUpdate(cfg).Execute(true, true);
                                 cfg.SetProperty(Environment.CommandTimeout, "2000");
                             });
            return configuration.BuildSessionFactory();
        }
        public DatabaseType DatabaseType { get; set; }
        private readonly ISessionFactory _sessionFactory;
    }

    public enum DatabaseType {SQLServer, MySQL, SQLite }
}
