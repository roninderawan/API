using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class AccountRepositories
    {
        MyContext myContext;

        public AccountRepositories(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Register(string fullName, string email, DateTime birthDate, string password)
        {
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
                    return 1;

            }
            return 2;
        }
    }
}
