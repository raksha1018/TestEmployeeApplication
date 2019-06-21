using DataAcessLayer.Entities;
using DataAcessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAcessLayer.Repository
{
    public class EmployeeRepository : IEmployeeRepository<Employee>
    {
        private string connectionString;
        public EmployeeRepository()
        {

            //connectionString = @"Data Source=WKC001155737\SQLSERVER;Database=TestDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            connectionString = ConfigurationManager.ConnectionStrings["Authorization_ConnectionString"].ConnectionString; 
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public bool Add(Employee employee)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "Insert into Employee(Name, Designation, Department) values(@Name, @Designation, @Department)";
                dbConnection.Open();
                var result = dbConnection.Execute(query, employee);
                return result > 0;
                

            }
        }

        public bool Delete(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"Delete from Employee where Id=@id";
                dbConnection.Open();
                var result = dbConnection.Execute(query, new { Id = id });
                return result > 0;
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var employeeList = dbConnection.Query<Employee>("SELECT * FROM Employee").ToList();
                return employeeList;
            }
        }

        public Employee GetById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"Select * from Employee where Id= @id";
                dbConnection.Open();
                Employee employee = dbConnection.Query<Employee>(query, new { Id = id }).FirstOrDefault();
                return employee;
            }
        }

        public bool Update(int Id, Employee employee)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = @"Update Employee set Name=@Name, Designation=@Designation, Department=@Department where Id=@id";
                dbConnection.Open();
                var result=dbConnection.Execute(query, new {id=Id, Name= employee.Name, Designation= employee.Designation, Department = employee.Department});
                return result > 0;
            }
        }
    }
}
