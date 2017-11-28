using JournalProjectWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JournalProjectWebApp.Controllers
{
    public class EmployeesController : ApiController
    {
        static List<Employee> _emps = new List<Employee>
        {
            new Employee { Id = 1, Name = "Ahmed", Age = 23 },
            new Employee { Id = 2, Name = "Mohamed", Age = 54},
            new Employee { Id = 3, Name = "Ali", Age = 21 },
            new Employee { Id = 4, Name = "Osama", Age = 12 },
        };
        public List<Employee> GetAllEmplyees()
        {
            return _emps;
        }
        public Employee GetEmployeeById(int id)
        {
            return _emps.Find(c => c.Id == id);
        }
        public Employee Post(Employee emp)
        {
            _emps.Add(emp);
            return emp;
        }
        public void PutEmployeeChangeById(int id, Employee emp)
        {
            var myEmployee = _emps.Find(c => c.Id == id);
            myEmployee.Name = emp.Name;
            myEmployee.Age = emp.Age;
        }
        public List<Employee> DeletEmployee(int id)
        {
            _emps.RemoveAll(c => c.Id == id);
            return _emps;
        }
        public Employee GetByLastName(string name)
        {
            var myEmployee = _emps.Find(c => c.Name.ToLower() == name.ToLower());
            return myEmployee;
        }
}
}
