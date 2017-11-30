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
        public bool SearchVusers(Employee emp)
        {
            JournalEntities _entities = new JournalEntities();
            if(emp.UserType == 1)
            {
                var result = _entities.VUsers.FirstOrDefault(c => c.Username.ToLower() == emp.Username.ToLower());
                if(result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        [Route("searchB")]
        public bool SearchBusers(Employee emp)
        {
            JournalEntities _entities = new JournalEntities();
            if (emp.UserType == 1)
            {
                var result = _entities.BUsers.FirstOrDefault(c => c.Username.ToLower() == emp.Username.ToLower());
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        [Route("searchA")]
        public bool SearchAdmins(Employee emp)
        {
            JournalEntities _entities = new JournalEntities();
            if (emp.UserType == 1)
            {
                var result = _entities.Admins.FirstOrDefault(c => c.Username.ToLower() == emp.Username.ToLower());
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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
