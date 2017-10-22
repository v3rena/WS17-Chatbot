using Autofac;
using Chatbot.Data.DataAccessLayer;
using Chatbot.DataAccessLayer;
using Chatbot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Chatbot.Controllers
{
    public class ValuesController : ApiController
    {

        private IBusinessLayer<Test> _bl;

        public ValuesController()
        {
            //todo use autofac injection
            _bl = new BusinessLayer<Test>(new DAL());
        }


        [Route("monika/test/{id}")]
        public string GetTestByIdForMonika(int id)
        {
            return _bl.SelectFirst(item => item.TestID == id).TestName;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
