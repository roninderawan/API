using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class DepartmentRepositories : IRepository<Department, int>
    {
        MyContext myContext;

        public DepartmentRepositories(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable<Department> Get()
        {
            var data = myContext.Departments.ToList();
            return data;
        }

        public Department GetById(int Id)
        {
            var data = myContext.Departments.Find(Id);
            return data;
        }

        public int Create(Department Entity)
        {
            myContext.Departments.Add(Entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Update(Department Entity)
        {
            myContext.Entry(Entity).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }

        public int Delete(int Id)
        {
            var data = myContext.Departments.Find(Id);
            if (data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }
    }
}
