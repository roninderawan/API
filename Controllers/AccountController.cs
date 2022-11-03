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
        private AccountRepositories accountRepositories;

        public AccountController(AccountRepositories accountRepositories)
        {
            this.accountRepositories = accountRepositories;
        }

        [HttpGet]
        public ActionResult Login(string email, string password)
        {
            try
            {
                
                var log = accountRepositories.Login(email, password);
                return log switch
                {
                    1 => Ok(new
                    {
                        StatusCode = 200,
                        Message = "Login Berhasil"
                    }),
                };
            }

            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Login Gagal"
                });
            }

        }

        [HttpPost]
        public ActionResult Register(LoginResponse loginResponse)
        {
            try
            {
                var reg = accountRepositories.Register(loginResponse);
                return reg switch
                {
                    1 => Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil Masuk"
                    }),
                };
            }

            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Data Gagal Masuk"
                });
            }



        }

        [HttpPut]
        public ActionResult ChangePassword(string email, string password, string baru)
        {
            try
            {
                var reg = accountRepositories.ChangePassword(email, password, baru);
                return reg switch
                {
                    1 => Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil diupdate",
                        
                    }),
                };
            }

            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Data Gagal diupdate"
                });
            }

        }

        [HttpPut("ForgotPw")]
        public ActionResult ForgotPassword(string email, string baru)
        {
            try
            {
                var reg = accountRepositories.ForgotPassword(email, baru);
                return reg switch
                {
                    1 => Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil diupdate",

                    }),
                };
            }

            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Data Gagal diupdate"
                });
            }
        }
    }
}
