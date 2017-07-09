using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Students
    {

        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string reault { get; set; }
        public virtual int age { get; set; }
    }
}