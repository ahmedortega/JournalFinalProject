//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JournalDB
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
    using System.Web;
    
    public partial class Article
    {
        public int Serial { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public int AuthorID { get; set; }
    
        public virtual Author Author { get; set; }
    }
}