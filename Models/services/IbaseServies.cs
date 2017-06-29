using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1
{
   public interface IbaseServies<TEntity> where TEntity:class
    {
        void Insert(TEntity entity);
        void Update(TEntity entity,int id);
        void Delete(TEntity entity,int id);
        TEntity  GetById(int id);
        IEnumerable<TEntity> GetAll();

    }
}
