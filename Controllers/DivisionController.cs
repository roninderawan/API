using API.Context;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private DivisionRepositories divisionRepositories;

        public DivisionController(DivisionRepositories divisionRepositories)
        {
            this.divisionRepositories = divisionRepositories;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var data = divisionRepositories.Get();
                if(data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Tidak Ada"

                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Ada",
                        Data = data
                    }); 
                }
            }
            catch(Exception ex)
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
            try
            {
                var data = divisionRepositories.GetById(Id);
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
        public ActionResult Create(Division division)
        {
            try
            {
                var result = divisionRepositories.Create(division);
                if (result == 0)
                {
                    return Ok(new 
                    { 
                        StatusCode = 200,
                        Message = "Data Gagal Disimpan" 
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil Tersimpan",
                        result = division
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
        public ActionResult Update(Division division)
        {
            try
            {
                var result = divisionRepositories.Update(division);
                if (result == 0)
                {
                    return Ok(new 
                    { 
                        StatusCode = 200,
                        Message = "Data Gagal Diupdate" 
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil Diupdate",
                        result = division
                    });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        public ActionResult Delete(int Id)
        {
            try
            {
                var result = divisionRepositories.Delete(Id);
                if (result == 0)
                {
                    return Ok(new 
                    { 
                        StatusCode = 200,
                        Message = "Data Gagal Ditemukan" 
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil Terhapus",
                        result = Id
                    });
                }
            }
            catch(Exception ex)
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
