using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class DepartmentRepositories : GeneralRepository <Department>
    {
        MyContext myContext;

        public DepartmentRepositories(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        [HttpGet("DepartmentName")]
        public List<Department> Get(string name)
        {
            return myContext.Departments.Where(x => x.Name == name).ToList();
        }
    }
}
