using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntegratedInventoryAndOrderManagementSystem.Services
{
   
    public interface ICrud<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T GetById(int id);
        List<T> GetAll();
    }
    
}