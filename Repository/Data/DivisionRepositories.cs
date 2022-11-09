using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Repository
{
    public class DivisionRepositories : GeneralRepository<Division>
    {
        MyContext myContext;

        public DivisionRepositories(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        [HttpGet("Name")]
        public List<Division> Get(string name)
        {
            return myContext.Divisions.Where(x => x.Name == name).ToList();
        }


    }
}
