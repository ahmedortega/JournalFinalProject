using JournalDB;
using JournalProjectWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
public struct User
{
    public int Id { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int UserType { get; set; }
}
namespace JournalProjectWebApp.Controllers
{
    public class AdminsController : ApiController
    {
        public IEnumerable<Admin> GetAdmins()
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                return _entities.Admins.ToList();
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
        /*public void PostBUser(int id, BUser buser)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                if (id == 2)
                {
                    buser.UserType = 2;
                    _entities.BUsers.Add(buser);
                    _entities.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, buser);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + buser.Id);
                }
            }
        }*/
        //last post
        public void PostLast(int id, User emp)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                if (id == 2)
                {
                    BUser buser = new BUser();
                    buser.Id = emp.Id;
                    buser.Fname = emp.Fname;
                    buser.Lname = emp.Lname;
                    buser.Phone = emp.Password;
                    buser.Email = emp.Email;
                    buser.Username = emp.Username;
                    buser.Password = emp.Password;
                    buser.UserType = 2;
                    _entities.BUsers.Add(buser);
                }
                else if(id == 3)
                {
                    Admin admin = new Admin();
                    admin.Id = emp.Id;
                    admin.Fname = emp.Fname;
                    admin.Lname = emp.Lname;
                    admin.Phone = emp.Phone;
                    admin.Username = emp.Username;
                    admin.Password = emp.Password;
                    admin.UserType = 3;
                    _entities.Admins.Add(admin);

                }
                _entities.SaveChanges();
            }
        }

        public void Put(int usertype, int id, User emp)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                if (usertype == 1)
                {
                    var vuser = _entities.VUsers.FirstOrDefault(c => c.Id == id);
                    vuser.Fname = emp.Fname;
                    vuser.Lname = emp.Lname;
                    vuser.Email = emp.Email;
                    vuser.Username = emp.Username;
                    vuser.Password = emp.Password;
                    vuser.UserType = 2;
                }
                if (usertype == 2)
                {
                    var buser = _entities.BUsers.FirstOrDefault(c => c.Id == id);
                    buser.Fname = emp.Fname;
                    buser.Lname = emp.Lname;
                    buser.Phone = emp.Phone;
                    buser.Email = emp.Email;
                    buser.Username = emp.Username;
                    buser.Password = emp.Password;
                    buser.UserType = 2;
                }
                else if (usertype == 3)
                {
                    var admin = _entities.Admins.FirstOrDefault(c => c.Id == id);
                    admin.Fname = emp.Fname;
                    admin.Lname = emp.Lname;
                    admin.Phone = emp.Phone;
                    admin.Username = emp.Username;
                    admin.Password = emp.Password;
                    admin.UserType = 3;
                    _entities.Admins.Add(admin);

                }
                _entities.SaveChanges();
            }
        }
        public void Delete(int usertype, int id)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                if (usertype == 1)
                {
                    var vuser = _entities.VUsers.FirstOrDefault(c => c.Id == id);
                    _entities.VUsers.Remove(vuser);
                }
                else if (usertype == 2)
                {
                    var buser = _entities.BUsers.FirstOrDefault(c => c.Id == id);
                    _entities.BUsers.Remove(buser);
                }
                else if (usertype == 3)
                {
                    var admin = _entities.Admins.FirstOrDefault(c => c.Id == id);
                    _entities.Admins.Remove(admin);

                }
                _entities.SaveChanges();
            }
        }
        /*public void Post(int usertype, Admin admin, BUser buser)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                if (usertype == 3)
                {
                    admin.UserType = 3;
                    _entities.Admins.Add(admin);
                    _entities.SaveChanges();
                }

                else if (usertype == 2)
                {
                    buser.UserType = 2;
                    _entities.BUsers.Add(buser);
                    _entities.SaveChanges();
                }
            }
        }*/
        /*public HttpResponseMessage PostBUser(int userType, BUser buser)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
              if (userType == 2)
                {
                    buser.UserType = 2;
                    _entities.BUsers.Add(buser);
                    _entities.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, buser);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + buser.Id);
                    return msg;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Business User doesn't inserted");
                }
            }
        }
        public HttpResponseMessage PostAdmin(int userType, Admin admin)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                if (userType == 3)
                {
                    admin.UserType = 3;
                    _entities.Admins.Add(admin);
                    _entities.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, admin);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + admin.Id);
                    return msg;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Admin doesn't inserted");
                }
            }
        }
        */
    }
}
