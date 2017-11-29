using JournalDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using JournalProjectWebApp.Models;

namespace JournalProjectWebApp.Controllers
{
    public class ArticlesController : ApiController
    {
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
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
        }
        public Article GetBySerial(int id)
        {
            using (JournalEntities _entities = new JournalEntities())
            {
                return _entities.Articles.FirstOrDefault(c => c.Serial == id);
            }
        }
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

    }
}
