
using IntegratedInventoryAndOrderManagementSystem.Data;
using IntegratedInventoryAndOrderManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegratedInventoryAndOrderManagementSystem.Services
{
    
    public class ServiceBase<T> : ControllerBase, ICrud<T> where T:class
    {
        readonly ApplicationDbContext context;
        protected IQueryable<T> Entities {get => context.Set<T>();}
        public ServiceBase(ApplicationDbContext _context)
        {
            context = _context;
        }
        //Add
        public bool Add(T entity)
        {   
            try
            {
                context.Set<T>().Add(entity);
                return context.SaveChanges() != 0;
            }
            catch 
            {
              return false;
            }
            
        }
       //Delete
        public bool Delete(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
                return context.SaveChanges() != 0;
            }
            catch 
            {
              return false;
            }
        }
        //GetAll
        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        //GetById
        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
       
        //Update
        public bool Update(T entity)
        {
           try
            {
                context.Set<T>().Update(entity);
                return context.SaveChanges() != 0;
            }
            catch 
            {
              return false;
            }
        }
        // GET: api/controller/5
        // [HttpGet("{id}")]
        // private bool BatchExists(int id)
        // {
        //     return context.Batches.Any(e => e.Id == id);
        // }
    }
}