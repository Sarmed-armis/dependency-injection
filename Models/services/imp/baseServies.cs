﻿using FulentNHirbent001.NHibarnateHelper;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
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
                var employees = session.Query<TEntity>().ToList();
                return employees;
            }

            
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}