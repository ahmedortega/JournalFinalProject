using JournalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JournalProjectWebApp.Controllers
{
    public class VUsersController : ApiController
    {
        public IEnumerable<VUser> GetVUsers()
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                return _entities.VUsers.ToList();
            }
        }
        public VUser GetVUserById(int id)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                var vuser = _entities.VUsers.FirstOrDefault(c => c.Id == id);
                return vuser;
            }
        }
        public HttpResponseMessage PostVUser(VUser vuser)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    vuser.UserType = 1;
                    _entities.VUsers.Add(vuser);
                    _entities.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, vuser);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + vuser.Id);
                    return msg;
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        public HttpResponseMessage DeleteVUser(int id)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var vuser = _entities.VUsers.FirstOrDefault(c => c.Id == id);
                    _entities.VUsers.Remove(vuser);
                    _entities.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, vuser);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + vuser.Id + "is Deleted");
                    return msg;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        public HttpResponseMessage PutVUser(int id, VUser vuser)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var myVuser = _entities.VUsers.FirstOrDefault(c => c.Id == id);
                    if (myVuser == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, " no Visitor User with this ID");
                    }
                    else
                    {
                        myVuser.Fname = vuser.Fname;
                        myVuser.Lname = vuser.Lname;
                        myVuser.Username = vuser.Username;
                        myVuser.Password = vuser.Password;
                        myVuser.Email = vuser.Email;
                        myVuser.UserType = 1;
                        _entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, " The Visitor User is Updated ");
                    }
                }
                catch(Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }


    }
}
