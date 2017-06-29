using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FulentNHirbent001.Models;
using NHibernate;
using NHibernate.Tool.hbm2ddl;


namespace FulentNHirbent001.NHibarnateHelper
{
    public class NHibernateHelper<TEntity>
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                  .ConnectionString(@"Data Source=DESKTOP-UQ8FFK2\SARMAD;Initial Catalog=googe;Integrated Security=True")
                              .ShowSql()
                )
               .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<TEntity>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(false, false))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}