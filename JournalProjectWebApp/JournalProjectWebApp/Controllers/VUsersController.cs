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
    [RoutePrefix("api/vusers")]
    public class VUsersController : ApiController
    {
        [Route("articles/get")]
        public List<PocoArticles> GetArticles()
        {
            JournalEntities _entities = new JournalEntities();
            return _entities.Articles.Select(c => new PocoArticles
            {
                serial = c.Serial,
                title = c.Title,
                authorId = c.AuthorID,
                subject = c.Subject,
                authorFname = c.Author.Fname,
                authorLname = c.Author.Lname,
                authorBirthYear = c.Author.BirthYear,
                authorWorkYears = c.Author.WorkYears
            }).ToList();
        }
        [Route("articles/{name:alpha}")]
        public PocoArticles GetaticleByname(string name)
        {
            JournalEntities _entities = new JournalEntities();
            List<PocoArticles> result;
            result = _entities.Articles.Select(c => new PocoArticles
            {
                serial = c.Serial,
                title = c.Title,
                authorId = c.AuthorID,
                subject = c.Subject,
                authorFname = c.Author.Fname,
                authorLname = c.Author.Lname,
                authorBirthYear = c.Author.BirthYear,
                authorWorkYears = c.Author.WorkYears
            }).ToList();
            return result.FirstOrDefault(c => c.title.ToLower() == name.ToLower());
        }
        [Route("articles/author/{name:alpha}")]
        public List <PocoArticles> GetaticleAuthorname(string name)
        {
            JournalEntities _entities = new JournalEntities();
            List<PocoArticles> result;
            var rr = _entities.Articles.Where(c => c.Author.Fname.ToLower() == name);
            result = _entities.Articles.Select(c => new PocoArticles
            {
                serial = c.Serial,
                title = c.Title,
                authorId = c.AuthorID,
                subject = c.Subject,
                authorFname = c.Author.Fname,
                authorLname = c.Author.Lname,
                authorBirthYear = c.Author.BirthYear,
                authorWorkYears = c.Author.WorkYears
            }).ToList();
            return result.Where(c => c.authorFname.ToLower() == name.ToLower()).ToList();
        }
    }
}
        /*[Route("articles/{name:alpha}")]
        public Article GetArticleByName(string name)
        {
            JournalEntities _entities = new JournalEntities();
            return _entities.Articles.FirstOrDefault(c => c.Title.ToLower() == name);
        }
        public IEnumerable<VUser> GetVUsers()
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                return _entities.VUsers.ToList();
            }
        }*/
        /*public VUser GetVUserById(int id)
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
        */
