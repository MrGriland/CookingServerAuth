using CookingServerAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CookingServerAuth.Controllers
{
    public class SyncController : ApiController
    {
        SyncContext synchronizationContext = new SyncContext();
        public List<Recipe> Post([FromBody] DateContainer value)
        {
            if (DateTime.Parse(value.date) < synchronizationContext.GetVersion())
            {
                return synchronizationContext.GetAfter(DateTime.Parse(value.date));
            }
            return new List<Recipe>();
        }
    }
}
