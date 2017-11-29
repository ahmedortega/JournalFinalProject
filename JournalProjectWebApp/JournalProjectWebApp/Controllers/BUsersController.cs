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
    [RoutePrefix("api/busers")]
    public class BUsersController : ApiController
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
        [Route("articles/post")]
        public HttpResponseMessage Post(Article article)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    _entities.Articles.Add(article);
                    _entities.SaveChanges();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, article);
                    response.Headers.Location = new Uri(Request.RequestUri + "/" + article.Serial);
                    return response;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        [Route("articles/getbyid/{id}")]
        public Article GetBySerial(int id)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                return _entities.Articles.FirstOrDefault(c => c.Serial == id);
            }
        }
        [Route("articles/delete/{id}")]
        public HttpResponseMessage DeleteArticle(int id)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var article = _entities.Articles.FirstOrDefault(c => c.Serial == id);
                    _entities.Articles.Remove(article);
                    _entities.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.Created, article);
                    msg.Headers.Location = new Uri(Request.RequestUri + "/" + article.Serial + "is Deleted");
                    return msg;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        [Route("articles/put/{id}")]
        public HttpResponseMessage PutArticle(int id, Article article)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var myArticle = _entities.Articles.FirstOrDefault(c => c.Serial == id);
                    if (myArticle == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, " no Visitor User with this ID");
                    }
                    else
                    {
                        myArticle.Title = article.Title;
                        myArticle.Subject = article.Subject;
                        myArticle.Author = article.Author;
                        myArticle.AuthorID = article.AuthorID;
                        _entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, " The Visitor User is Updated ");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        //view user profile and can upload image after perform any operation on it 
        [Route("myProfile/put/{password}")]
        public HttpResponseMessage PutBUser(string password, BUser buser)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var myBuser = _entities.BUsers.FirstOrDefault(c => c.Password.ToLower() == password.ToLower() );
                    if (myBuser == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, " no Business User with this Password");
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
                        return Request.CreateResponse(HttpStatusCode.OK, " Ur Profile is Updated ");
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
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
        /*public HttpResponseMessage PutBUser(int id, BUser buser)
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
        }*/
    }
}
