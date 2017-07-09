using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.NHibarnate.Maps
{
    public class StudentsMap:ClassMap<Students>
    {

        public StudentsMap()
        {

            Id(x => x.id);
            Map(x => x.name);
            Map(x => x.reault);
            Map(x => x.age);
            Table("Students");
        }
    }
}