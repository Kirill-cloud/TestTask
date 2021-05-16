using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Project3.Model;

namespace Project3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly Model.UsersContext context;

        public UsersController(Model.UsersContext context) 
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new List<User>();
        }

        [HttpPut]
        public ActionResult Save([FromBody] User[] usersToSave)
        {
            try
            {
                DBMethods.SaveGroup(usersToSave.ToList<User>(),context);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public PostResponce Calculate([FromBody] User[] usersToCalculate)
        {
            return new PostResponce() { 
                UsersLifeTime = CalculateUsersLifeTimes(usersToCalculate), 
                RR7days = CalculateAndFormatRetantion7Days(usersToCalculate) 
            };
        }

        private List<int> CalculateUsersLifeTimes(User[] usersToCalculate)
        {
            List<int> daysBetween = new();

            foreach (var item in usersToCalculate)
            {
                daysBetween.Add(GetDaysBetweenStringDates(item.RegistrationDate, item.LastActivityDate));
            }

            return daysBetween;
        }
        int GetDaysBetweenStringDates(string smallerDate, string biggerDate)
        {
            var sDate = DateTime.ParseExact(smallerDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var bDate = DateTime.ParseExact(biggerDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

            return (bDate - sDate).Days;
        }

        string CalculateAndFormatRetantion7Days(User[] users)
        {
            DateTime sevenDay = DateTime.Now.AddDays(-7);

            IEnumerable<User> oldUsers = users.Where<User>(x => DateTime.ParseExact(x.RegistrationDate, "dd.MM.yyyy", CultureInfo.InvariantCulture) <= sevenDay);

            double firstParam = oldUsers.Count<User>(x => DateTime.ParseExact(x.LastActivityDate, "dd.MM.yyyy", CultureInfo.InvariantCulture) >= sevenDay);

            double secondParam = oldUsers.Count();

            string result = "Нет старых пользователей";
            if (secondParam != 0)
            {
                result = String.Format("{0:P}", firstParam / secondParam);
            }

            return result;
        }

        public class PostResponce
        {
            public List<int> UsersLifeTime { get; set; }
            public string RR7days { get; set; }
        }
    }
}
