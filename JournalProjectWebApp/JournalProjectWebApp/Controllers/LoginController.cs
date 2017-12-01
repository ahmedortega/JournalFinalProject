using JournalDB;
using JournalProjectWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JournalProjectWebApp.Controllers
{
    [RoutePrefix("Login")]
    public class LoginController : ApiController
    {
        [Route("searchV")]
        public HttpResponseMessage SearchVusers(Employee emp)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            JournalEntities _entities = new JournalEntities();
            try
            {
                var result = _entities.VUsers.FirstOrDefault(c => c.Username.ToLower() == emp.Username.ToLower() && c.Password == emp.Password);
                if (result != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    return response;
                }
                else
                {

                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return response;
            }
        }
        [Route("searchB")]
        public HttpResponseMessage SearchBusers(Employee emp)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            JournalEntities _entities = new JournalEntities();
            try
            {
                var result = _entities.BUsers.FirstOrDefault(c => c.Username.ToLower() == emp.Username.ToLower() && c.Password == emp.Password);
                if (result != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    return response;
                }
                else
                {

                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return response;
            }
        }
        [Route("searchA")]
        public HttpResponseMessage SearchAdmins(Employee emp)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            JournalEntities _entities = new JournalEntities();
            try
            {
                var result = _entities.Admins.FirstOrDefault(c => c.Username.ToLower() == emp.Username.ToLower() && c.Password == emp.Password);
                if (result != null)
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    return response;
                }
                else
                {

                    response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error");
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return response;
            }
        }
        [Route("search")]
        public int SearchAllUsers(Employee emp)
        {
            JournalEntities _entities = new JournalEntities();
            int result = 0;
            switch(emp.UserType)
            {
                case 1:
                    if ( ( _entities.VUsers.FirstOrDefault(c => c.Username.ToLower() == emp.Username.ToLower() && c.Password.ToLower() == emp.Password.ToLower() )) != null)
                    {
                        result = 1;
                    }
                    else
                    {
                       result = 0;
                    }
                    break;
                case 2:
                    if ( ( _entities.BUsers.FirstOrDefault(c => c.Username.ToLower() == emp.Username.ToLower() && c.Password.ToLower() == emp.Password.ToLower() ) ) != null)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                    break;
                case 3:
                    if ((_entities.Admins.FirstOrDefault(c => c.Username.ToLower() == emp.Username.ToLower() && c.Password.ToLower() == emp.Password.ToLower() ) ) != null)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = 0;
                    }
                    break;
                
            }
            return result;
        }
    }
}
