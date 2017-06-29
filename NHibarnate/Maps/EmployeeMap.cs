using FluentNHibernate.Mapping;


namespace FulentNHirbent001.Models
{
    public class EmployeeMap:ClassMap<Employee>
    {
        //this is for make mapp to table feild
       public EmployeeMap()
        {
            Id(x => x.EmployeeId);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Designation);
            Table("Employee");


        }
    }
}