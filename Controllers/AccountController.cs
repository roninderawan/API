using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Repository;
using API.Repositories;
using API.Models;
using API.Handler;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private JWTConfig jwtConfig;
        private readonly AccountRepositories accountRepositories;

        public AccountController(JWTConfig jwtConfig, AccountRepositories accountRepositories)
        {
            this.accountRepositories = accountRepositories;
            this.jwtConfig = jwtConfig;
        }

        [Authorize]
        [HttpGet]
        public ActionResult Get()
        {
            var data = accountRepositories.Get();
            try
            {
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Not Found "
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Found",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("{Id}")]
        public ActionResult GetById(int Id)
        {
            var data = accountRepositories.GetById(Id);
            try
            {
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Not Found"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Found",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            var data = accountRepositories.Create(user);
            try
            {
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Enter Failed"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Success",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        public ActionResult Update(User user)
        {
            var data = accountRepositories.Update(user);
            try
            {
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Not Updated"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Updated",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var data = accountRepositories.Delete(id);
            try
            {
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Gagal Dihapus"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil Dihapus",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login(string email, string password)
        {
            var data = accountRepositories.Login(email, password);
            try
            {
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Login gagal",
                    });
                }
                else
                {
                    //JWTConfig jwt = new JWTConfig();
                    string token = jwtConfig.Token(email, data.Role);
                    return Ok(new
                    {
                        Message = "Login Berhasil",
                        Data = data,
                        token
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPost("Register")]
        public ActionResult Register(string fullname, string email, DateTime birthdate, string password)
        {
            var data = accountRepositories.Register(fullname, email, birthdate, password);

            try
            {
                if (data == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Email sudah ada !"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Berhasil",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(string pw, string password, string email)
        {

            var data = accountRepositories.ChangePassword(pw, password, email);
            try
            {
                if (data == 0)
                {
                    return Ok(new { Message = "error" });
                }
                else
                {
                    return Ok(new
                    {
                        Message = "Sukses",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }


        [HttpPut("ForgotPassword")]
        public ActionResult ForgotPassword(string fullName, string email, string birthDate, string pwbaru)
        {
            var data = accountRepositories.ForgotPassword(fullName, email, birthDate, pwbaru);
            try
            {
                if (data == 0)
                {
                    return Ok(new 
                    { 
                        Message = "Gagal" 
                    });
                }
                else
                {
                    return Ok(new
                    {
                        Message = "Sukses",
                        Data = data
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

    }
}
