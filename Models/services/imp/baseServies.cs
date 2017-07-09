using FulentNHirbent001.NHibarnateHelper;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.services;

namespace WebApplication1.services.imp
{
    public class baseServies<TEntity> : IbaseServies<TEntity> where TEntity : class
    {
        public void Delete(TEntity entity, int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (ISession session = NHibernateHelper<TEntity>.OpenSession())
            {
                var ResultList= session.Query<TEntity>().ToList();
                return ResultList;
            }

            
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {

            using (ISession session = NHibernateHelper<TEntity>.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

        public IList<TEntity> PushStoredProcedure(string procedureName, IList<SqlParameter> parameters)
        {
            {
                IList<TEntity> result;

                if (parameters != null)
                {
                    string spp = "";
                    foreach (var p in parameters)
                        spp += (spp.Length > 0 ? "," : "") + " :" + p.ParameterName;
                    procedureName += spp;
                }

                using (ISession session = NHibernateHelper<TEntity>.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {



                        var query = session
                        .CreateSQLQuery(procedureName);

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                switch (parameter.DbType.ToString())
                                {
                                    case "Int":
                                    case "Int32":
                                        query.SetParameter(parameter.ParameterName, parameter.Value, NHibernateUtil.Int32);
                                        break;
                                    case "Int64":
                                        query.SetParameter(parameter.ParameterName, parameter.Value, NHibernateUtil.Int64);
                                        break;
                                    case "Guid":
                                        query.SetParameter(parameter.ParameterName, parameter.Value, NHibernateUtil.Guid);
                                        break;
                                    case "bool":
                                    case "Boolean":
                                        query.SetParameter(parameter.ParameterName, parameter.Value, NHibernateUtil.Boolean);
                                        break;
                                    case "DateTime":
                                        query.SetParameter(parameter.ParameterName, parameter.Value, NHibernateUtil.DateTime);
                                        break;
                                    case "String":
                                        query.SetParameter(parameter.ParameterName, parameter.Value, NHibernateUtil.String);
                                        break;
                                    case "Decimal":
                                        query.SetParameter(parameter.ParameterName, parameter.Value, NHibernateUtil.Decimal);
                                        break;
                                    default:
                                        query.SetParameter(parameter.ParameterName, parameter.Value);
                                        break;
                                }
                            }
                        }
                        query.SetResultTransformer(new AliasToBeanResultTransformer(typeof(TEntity)))
                       .List<TEntity>().ToList();

                        result = query.List<TEntity>();

                        return result;  // result.AsQueryable();

                    }
                }

            }
        }
            public void Update(TEntity entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
