﻿using JournalDB;
using JournalProjectWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JournalProjectWebApp.Controllers
{
    [RoutePrefix("busers")]
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
        public HttpResponseMessage Post(PocoArticles article)
        {
            Article myArticle = new Article();
            Author myAuthor = new Author();
            HttpResponseMessage respone = new HttpResponseMessage();
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    if (article != null)
                    {
                        myArticle.AuthorID = article.authorId;
                        myArticle.Title = article.title;
                        myArticle.Subject = article.subject;
                        myAuthor.Lname = article.authorLname;
                        myAuthor.Fname = article.authorFname;
                        myAuthor.BirthYear = article.authorBirthYear;
                        myAuthor.WorkYears = article.authorWorkYears;
                        var SID = _entities.Articles.FirstOrDefault(x=> x.AuthorID == myArticle.AuthorID);
                        if(myArticle.Title != "" && myArticle != null && SID != null)
                        {
                            _entities.Articles.Add(myArticle);
                            _entities.SaveChanges();
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, myArticle);
                            response = Request.CreateResponse(HttpStatusCode.OK, myArticle);
                        }
                        if (myAuthor.Fname != "" && myAuthor.Fname != null && myAuthor.Lname != null && myAuthor.Lname != "" && myAuthor != null)
                        {
                            _entities.Authors.Add(myAuthor);
                            _entities.SaveChanges();
                            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, myAuthor);
                            response = Request.CreateResponse(HttpStatusCode.OK, myAuthor);
                        }
                        else
                        {
                            respone = Request.CreateErrorResponse(HttpStatusCode.Forbidden, message: "The Article Values is NULL or Authors ID is wrong");
                        }
                        return respone;
                    }
                    else
                    {
                        return respone = Request.CreateErrorResponse(HttpStatusCode.NoContent, "The article value is null");
                    }
                }
                catch (Exception ex)
                {
                    return respone = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        [Route("articles/getbyid/{id:int}")]
        public HttpResponseMessage GetBySerial(int id)
        {
            HttpResponseMessage msg;
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var article = _entities.Articles.Select(c => new PocoArticles
                    {
                        serial = c.Serial,
                        title = c.Title,
                        authorId = c.AuthorID,
                        subject = c.Subject,
                        authorFname = c.Author.Fname,
                        authorLname = c.Author.Lname,
                        authorBirthYear = c.Author.BirthYear,
                        authorWorkYears = c.Author.WorkYears
                    }).ToList().FirstOrDefault(c => c.serial == id);
                    if (article != null)
                    {
                        msg = Request.CreateResponse(HttpStatusCode.Accepted, article);
                        return msg;
                    }
                    else
                    {
                        msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                        return msg;
                    }
                }
                catch(Exception ex)
                {
                    msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                    return msg;
                }
            }
        }
        [Route("articles/delete/{id:int}")]
        public HttpResponseMessage DeleteArticle(int id)
        {
            HttpResponseMessage result;
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var article = _entities.Articles.FirstOrDefault(c => c.Serial == id);
                    if (article != null)
                    {
                        _entities.Articles.Remove(article);
                        _entities.SaveChanges();
                        var msg = Request.CreateResponse(HttpStatusCode.Created, article);
                        msg.Headers.Location = new Uri(Request.RequestUri + "/" + article.Serial + "is Deleted");
                        return msg;
                    }
                    else
                    {
                        result = Request.CreateErrorResponse(HttpStatusCode.NotFound, "The Atticle With this Serial :" + id + "Not Found");
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                }
            }
        }
        [Route("articles/put/{id:int}")]
        public HttpResponseMessage PutArticle(int id, PocoArticles article)
        {
            HttpResponseMessage response;
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var myArticle = _entities.Articles.FirstOrDefault(c => c.Serial == id);
                    /*var result = _entities.Articles.Select(c => new PocoArticles
                    {
                        authorId = c.AuthorID
                    }).ToList().FirstOrDefault(x => x.authorId == article.authorId);*/

                    if (myArticle == null)
                    {
                        response = Request.CreateErrorResponse(HttpStatusCode.NotFound, "the Author Id is not exist");
                        return response;
                    }
                    else
                    {
                        var author = _entities.Authors.FirstOrDefault(c => c.Id == article.authorId);
                        myArticle.Title = article.title;
                        myArticle.Subject = article.subject;
                        if (author != null)
                        {
                            myArticle.AuthorID = article.authorId;
                        }
                        else
                        {
                            myArticle.AuthorID = myArticle.AuthorID;
                        }
                        if (author != null && (article.authorId).GetType() == typeof(int) && (article.authorBirthYear).GetType() == typeof(int) && (article.authorWorkYears).GetType() == typeof(int) && !string.IsNullOrEmpty(article.authorFname) && !string.IsNullOrEmpty(article.authorLname))
                        {
                            author.Id = article.authorId;
                            author.Fname = article.authorFname;
                            author.Lname = article.authorLname;
                            author.BirthYear = article.authorBirthYear;
                            author.WorkYears = article.authorWorkYears;
                            myArticle.AuthorID = article.authorId;
                            _entities.SaveChanges();
                        }
                        else
                        {
                            response = Request.CreateErrorResponse(HttpStatusCode.Forbidden, "The Author id is not exist");
                        }
                        _entities.SaveChanges();
                        response = Request.CreateResponse(HttpStatusCode.OK, " The Visitor User is Updated ");
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    response = Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                    return response;
                }
            }
        }
        //view user profile and can upload image after perform any operation on it 
        [Route("Profile/put/{username:alpha}/{password}")]
        public HttpResponseMessage PutBUser(string username,string password, BUser buser)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var myBuser = _entities.BUsers.FirstOrDefault(c => c.Username == username && c.Password == password );
                    if (myBuser == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, " no Business User with this Password");
                    }
                    else
                    {
                        myBuser.Fname = buser.Fname;
                        myBuser.Lname = buser.Lname;
                        myBuser.Phone = buser.Phone;
                        myBuser.Username = buser.Username;
                        myBuser.Password = buser.Password;
                        myBuser.Email = buser.Email;
                        myBuser.Image = buser.Image;
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
        [Route("get")]
        public IEnumerable<BUser> GetBUsers()
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                return _entities.BUsers.ToList();
            }
        }

        [Route("Profile/get/{username:alpha}/{password}")]
        public BUser GetBUserById(string username, string password)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                var busers = _entities.BUsers.FirstOrDefault(c => c.Username == username && c.Password == password);
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
        [Route("GetAuthors")]
        public HttpResponseMessage GetAllAuthors()
        {
            HttpResponseMessage msg;
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    //serialization problem instead of poco class
                    _entities.Configuration.ProxyCreationEnabled = false;
                    var authors = _entities.Authors.ToList();
                    if (authors != null)
                    {
                        msg = Request.CreateResponse(HttpStatusCode.Accepted, authors);
                        return msg;
                    }
                    else
                    {
                        msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                        return msg;
                    }
                }
                catch (Exception ex)
                {
                    msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                    return msg;
                }
            }
        }
        /*public HttpResponseMessage GetAllAuthors()
        {
            HttpResponseMessage msg;
            using (JournalEntities _entities = new JournalEntities())
            {
                try
                {
                    var authors = _entities.Authors.Select(c => new PocoArticles
                    {
                        authorId = c.Id,
                        authorFname = c.Fname,
                        authorLname = c.Lname,
                        authorBirthYear = c.BirthYear,
                        authorWorkYears = c.WorkYears
                    }).ToList();
                    if (authors != null)
                    {
                        msg = Request.CreateResponse(HttpStatusCode.Accepted, authors);
                        return msg;
                    }
                    else
                    {
                        msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                        return msg;
                    }
                }
                catch (Exception ex)
                {
                    msg = Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
                    return msg;
                }
            }
        }*/
        /*public IEnumerable<Author> GetAllAuthors()
        {
            try
            {
                using (JournalEntities _entities = new JournalEntities())
                {
                    return _entities.Authors.ToList();
                }
            }
            catch
            {
                return null;
            }
        }*/
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
