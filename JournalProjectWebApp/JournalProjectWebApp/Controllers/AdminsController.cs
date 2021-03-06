﻿using JournalDB;
using JournalProjectWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Net;

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
    [RoutePrefix("Admins")]
    public class AdminsController : ApiController
    {
        [Route("get")]
        public IEnumerable<Admin> GetAdmins()
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                return _entities.Admins.ToList();
            }
        }
        [Route("Buser/{id:int:min(1)}")]
        public BUser GetBUserById(int id)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                var busers = _entities.BUsers.FirstOrDefault(c => c.Id == id);
                return busers;
            }
        }
        //         Add Business user &&  Add Admin user
        [Route("post/{usertype:int}")]
        public HttpResponseMessage PostLast(int usertype, User emp)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (JournalEntities _entities = new JournalEntities())
            {
                if (usertype == 2)
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
                    _entities.SaveChanges();
                    response = Request.CreateResponse(HttpStatusCode.OK, "The BUser is added");
                    return response;
                }
                else if (usertype == 3)
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
                    _entities.SaveChanges();
                    response = Request.CreateResponse(HttpStatusCode.OK, "The Admin is added");
                    return response;

                }
                else
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Added");
                    return response;
                }
            }
        }
        // update User
        [Route("userput/{usertype:int}/{id:int}")]
        public HttpResponseMessage Put(int usertype, int id, User emp)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            using (JournalEntities _entities = new JournalEntities())
            {
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
                    _entities.SaveChanges();
                    response = Request.CreateResponse(HttpStatusCode.OK, "The BUser is Updated");
                    return response;
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
                    _entities.SaveChanges();
                    response = Request.CreateResponse(HttpStatusCode.OK, "The Admin is Updated");
                    return response;

                }
                else
                {
                    response = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not updated");
                    return response;
                }
            }
        }
        // Delete User
        [Route("userdelete/{usertype:int}/{id:int:min(1)}")]
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
        // Get All Users
        [Route("users/get")]
        public IEnumerable<Employee> GetAllUsers()
        {
            JournalEntities _entities = new JournalEntities();
            var result = _entities.BUsers.Select(x => new Employee
            {
                Id = x.Id,
                Fname = x.Fname,
                Lname = x.Lname,
                Username = x.Username,
                Password = x.Password,
                UserType = x.UserType
            })
                  .Concat(_entities.VUsers.Select(x => new Employee
                  {
                      Id = x.Id,
                      Fname = x.Fname,
                      Lname = x.Lname,
                      Username = x.Username,
                      Password = x.Password,
                      UserType = x.UserType
                  }).Concat(_entities.Admins.Select(x => new Employee
                  {
                      Id = x.Id,
                      Fname = x.Fname,
                      Lname = x.Lname,
                      Username = x.Username,
                      Password = x.Password,
                      UserType = x.UserType
                  })));
            return result.ToList();

        }
        //Retrieve Users Depending on user Type
        [Route("users/{usertype:int}")]
        public IEnumerable<Employee> GetByUserType(int usertype)
        {
            JournalEntities _entities = new JournalEntities();
            IEnumerable<Employee> result;
            switch (usertype)
            {
                case 1:
                    result = _entities.VUsers.Select(x => new Employee
                    {
                        Id = x.Id,
                        Fname = x.Fname,
                        Lname = x.Lname,
                        Username = x.Username,
                        Password = x.Password,
                        Email = x.Email,
                        UserType = x.UserType
                    });
                    break;
                case 2:
                    result = _entities.BUsers.Select(x => new Employee
                    {
                        Id = x.Id,
                        Fname = x.Fname,
                        Lname = x.Lname,
                        Username = x.Username,
                        Password = x.Password,
                        Email = x.Email,
                        UserType = x.UserType
                    });
                    break;
                case 3:
                    result = _entities.Admins.Select(x => new Employee
                    {
                        Id = x.Id,
                        Fname = x.Fname,
                        Lname = x.Lname,
                        Username = x.Username,
                        Password = x.Password,
                        UserType = x.UserType
                    });
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
            return result.ToList();
        }
    }
}
        /*public IEnumerable<Employee> GetUsersByType(int userType)
        {
            JournalEntities _entities = new JournalEntities();
            var result = _entities.BUsers.Select(x => new Employee
            {
                Id = x.Id,
                Fname = x.Fname,
                Lname = x.Lname,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email,
                UserType = x.UserType
            })
                  .Concat(_entities.VUsers.Select(x => new Employee
                  {
                      Id = x.Id,
                      Fname = x.Fname,
                      Lname = x.Lname,
                      Username = x.Username,
                      Password = x.Password,
                      Email = x.Email,
                      UserType = x.UserType
                  }));
            if (userType == 1)
            {
                //result.Select(x => x.UserType == 1).ToList();
                foreach (Employee val in result)
                {
                   yield return result.FirstOrDefault(x => x.UserType == 1);
                }
                
                Console.WriteLine(result);
                //foreach (Employee num in result)
                //{
                //    return result.Where(x => x.UserType == 1);
                //}

            }
            if (userType == 2)
            {
                foreach (Employee val in result)
                {
                    yield return result.FirstOrDefault(x => x.UserType == 2);
                }
                //result.Where(x => x.UserType == 1).ToList();
                //return result;
                //yield return result.FirstOrDefault(x => x.UserType == 2);
            }
            
        }*/

        /*if (usertype == 1)
        {
            result = _entities.VUsers.Select(x => new Employee
            {
                Id = x.Id,
                Fname = x.Fname,
                Lname = x.Lname,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email,
                UserType = x.UserType
            });
        }
        else
        {
            result = _entities.BUsers.Select(x => new Employee
            {
                Id = x.Id,
                Fname = x.Fname,
                Lname = x.Lname,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email,
                UserType = x.UserType
            });
        }
        return result.ToList();
    }*/

// Retrieve all users
/*public Object CopyAllUsers()
{
    Employee emp = new Employee();
    JournalEntities _entities = new JournalEntities();
    foreach(BUser buser in _entities.BUsers)
    {
        emp.Fname = buser.Fname;
        emp.Lname = buser.Lname;
        emp.Phone = buser.Phone;
        emp.Email = buser.Email;
        emp.Username = buser.Username;
        emp.Password = buser.Password;
        emp.UserType = 2;
    }
    foreach (VUser vuser in _entities.VUsers)
    {
        emp.Fname = vuser.Fname;
        emp.Lname = vuser.Lname;
        emp.Email = vuser.Email;
        emp.Username = vuser.Username;
        emp.Password = vuser.Password;
        emp.UserType = 1;
    }
    Console.WriteLine(emp);
    return emp;
}
 * public Tuple< IEnumerable<BUser>,IEnumerable<VUser> > GetAllUsers()
{
    using (JournalEntities _entities = new JournalEntities())
    {
       // return Tuple.Create(_entities.BUsers.ToList(), _entities.VUsers.ToList());
        IEnumerable<BUser> busers = _entities.BUsers.ToList();
        IEnumerable<VUser> vusers = _entities.VUsers.ToList();
        return (busers, vusers);
        //return (_entities.BUsers.ToList(), _entities.VUsers.ToList());
        //IEnumerable<BUser> first = this.GetBUsers();
        //Assign to empty list so we can use later
        //IEnumerable<VUser> second = this.GetVUsers();
        */
//IEnumerable<object> concatedList = first.Concat(second);
//return concatedList;
//var busers = _entities.BUsers;
//var vusers = _entities.VUsers;
//var result = busers.Concat(vusers);
//return busers.AddRange(vusers);
//var result = busers.Concat(vusers);
//return busers;

/* public static Tuple<BUser, VUser> GetAllUsers()
 {
     using (JournalEntities _entities = new JournalEntities())
     { 
             var busers = _entities.BUsers.ToList();
             var vusers = _entities.VUsers.ToList();
             var result = Tuple.Create(busers, vusers);
             return result;
     }
 }*/


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
