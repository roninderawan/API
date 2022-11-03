using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private MyContext myContext;

        public AccountController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        [HttpGet]
        public ActionResult Login(string email, string password)
        {

            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .SingleOrDefault(x => x.Employee.Email.Equals(email) && x.Password.Equals(password));
            if (data != null)
            {
                LoginResponse loginResponse = new LoginResponse()
                {
                    FullName = data.Employee.FullName,
                    Email = data.Employee.Email,
                    Role = data.Role.Name
                };
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Login Berhasil",
                    result = loginResponse
                }); ;
            }

            return NotFound(new
            {
                StatusCode = 404,
                Message = "Login Gagal"
            });
        }

        [HttpPost]
        public ActionResult Register(string fullName, string email, DateTime birthDate, string password)
        {

            //try
            //{
            //    var reg = accountRepositories.Register(abc);
            //    return reg switch
            //    {
            //        1 => Ok(new
            //        {
            //            StatusCode = 200,
            //            Message = "Data Gagal Masuk",
            //            return = abc
            //        }),
            //    };

            //}
            //catch(Exception ex)
            //{
            //    return BadRequest(new
            //    {
            //        StatusCode = 400,
            //        Message = "Data Gagal Masuk"
            //    });
            //}

            Employee employee = new Employee()
            {
                FullName = fullName,
                Email = email,
                BirthDate = birthDate
            };
            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                User user = new User()
                {
                    Id = id,
                    Password = password,
                    RoleId = 1
                };
                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil Tersimpan",
                        result = employee
                    }); ;

            }
            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Data Gagal Masuk"
            });

        }

        [HttpPut]
        public ActionResult ChangePassword(string email, string password, string baru)
        {

            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .AsNoTracking()
                .SingleOrDefault(x => x.Employee.Email.Equals(email) && x.Password.Equals(password));
            myContext.SaveChanges();
            if (data != null)
            {
                User user = new User()
                {
                    Id = data.Id,
                    Password = baru,
                    RoleId = data.RoleId,
                };
                myContext.Entry(user).State = EntityState.Modified;
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil Diupdate",
                        result = user
                    }); ;
            }

            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Data Gagal Diupdate"
            });
        }

        [HttpPut("ForgotPw")]
        public ActionResult ForgotPassword(string email, string baru)
        {
            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .AsNoTracking()
                .SingleOrDefault(x => x.Employee.Email.Equals(email));
            myContext.SaveChanges();
            if (data != null)
            {
                User user = new User()
                {
                    Id = data.Id,
                    Password = baru,
                    RoleId = data.RoleId,
                };
                myContext.Entry(user).State = EntityState.Modified;
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil Diupdate",
                        result = user
                    }); ;
            }
            return BadRequest(new
            {
                StatusCode = 400,
                Message = "Data Gagal Diupdate"
            });
        }
    }
}
