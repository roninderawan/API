using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace API.Repository.Data
{
    public class AccountRepositories
    {
        MyContext myContext;

        public AccountRepositories(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Login (string email, string password)
        {
            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .SingleOrDefault(x => x.Employee.Email.Equals(email) && x.Password.Equals(password));
            if (data != null)
            {
                LoginResponse a = new LoginResponse()
                {
                    FullName = data.Employee.FullName,
                    Email = data.Employee.Email,
                    Role = data.Role.Id
                };
                return 1;
            }

            return 2;
        }

        public int Register(LoginResponse loginResponse)
        {
            Employee employee = new Employee()
            {
                FullName = loginResponse.FullName,
                Email = loginResponse.Email,
                BirthDate = loginResponse.BirthDate,
            };

            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(loginResponse.Email)).Id;
                User user = new User()
                {
                    Id = id,
                    Password = loginResponse.Password,
                    RoleId = 1
                };
                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                    return 1;

            }
            return 2;
        }

        public int ChangePassword(string email, string password,string baru)
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
                    return 1;
            }

            return 2;
        }

        public int ForgotPassword(string email, string baru)
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
                    return 1;
            }
            return 2;
        }
    }
}
