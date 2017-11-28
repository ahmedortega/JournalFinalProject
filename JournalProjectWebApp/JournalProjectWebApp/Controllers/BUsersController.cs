using JournalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JournalProjectWebApp.Controllers
{
    public class BUsersController : ApiController
    {
        public IEnumerable<BUser> GetBUsers()
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                return _entities.BUsers.ToList();
            }
        }
        public BUser GetBUserById(int id)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                var busers = _entities.BUsers.FirstOrDefault(c => c.Id == id);
                return busers;
            }
        }
        public HttpResponseMessage PostBUser(BUser buser)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    buser.UserType = 2;
                    _entities.BUsers.Add(buser);
                    _entities.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, buser);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + buser.Id);
                    return msg;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        public HttpResponseMessage DeleteBUser(int id)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var buser = _entities.BUsers.FirstOrDefault(c => c.Id == id);
                    _entities.BUsers.Remove(buser);
                    _entities.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, buser);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + buser.Id + "is Deleted");
                    return msg;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        public HttpResponseMessage PutBUser(int id, BUser buser)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var myBuser = _entities.BUsers.FirstOrDefault(c => c.Id == id);
                    if (myBuser == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, " no Business User with this ID");
                    }
                    else
                    {
                        myBuser.Fname = buser.Fname;
                        myBuser.Lname = buser.Lname;
                        myBuser.Username = buser.Username;
                        myBuser.Password = buser.Password;
                        myBuser.Email = buser.Email;
                        myBuser.UserType = 2;
                        _entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, " The Business User is Updated ");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
    }
}
