using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeightLog.DAL;
using WeightLog.Models;

namespace WeightLog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMaxDAO maxDAO;
        private readonly ILogDAO logDAO;

        public UserController(IMaxDAO _maxDAO, ILogDAO _logDAO)
        {
            this.maxDAO = _maxDAO;
            this.logDAO = _logDAO;
        }

        [HttpGet("{userId}")]
        public ActionResult<List<Max>> GetUsersMaxes(int userId)
        {
            List<Max> maxes = this.maxDAO.GetMaxes(userId);

            if (maxes.Count > 0)
            {
                return maxes;
            }

            return NotFound();
        }

        [HttpGet("{userId}/Max-{lift}")]
        public ActionResult<Max> GetLiftMax(int userId, string lift)
        {
            Max max = this.maxDAO.GetLiftMax(userId, lift);

            return max;
        }

        [HttpPost("{userId}/Set")]
        public ActionResult<Set> PostNewSet(int userId, Set set)
        {
            Set loggedSet = this.logDAO.LogNewSet(set);

            if (loggedSet.Id != 0)
            {
                return Created($"User/{userId}/{loggedSet.Id}", set);
            }
            else
            {
                return this.StatusCode(304, "Not Modified");
            }
        }

        [HttpGet("{userId}/Set-{setId}")]
        public ActionResult<Set> GetLoggedSetById(int userId, int setId)
        {
            Set set = this.logDAO.LookupSetById(userId, setId);

            if (set.LiftId != 0)
            {
                return Ok(set);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
