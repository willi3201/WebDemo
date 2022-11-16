using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class DemoApiController : ApiController
    {
        private DemoTibox dt = new DemoTibox();
        // GET: api/demoApi
        public IEnumerable<Producto> Get()
        {
            return dt.Producto.ToList();
        }
    }
}
