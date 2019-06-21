using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcessLayer.Entities;
using TestEmployeeApplication.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAcessLayer.Repository;
using DataAcessLayer.Interface;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using System.Net;

namespace Sample_Test_Application
{ 
    [TestClass]
    public class TestSimpleController
    {
        private  Mock<IEmployeeRepository<Employee>> mock;
        private EmployeesController employees;
        public TestSimpleController()
        {
            mock = new Mock<IEmployeeRepository<Employee>>();
            employees= new EmployeesController(mock.Object);
        }

        [TestMethod]
        public void GetAllEmployees_ShouldReturnAllEmployees()
        {          
            mock.Setup(p => p.GetAll()).Returns(new List<Employee>());            
            var actionResult = employees.Get();
            var contentResult = actionResult as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.IsInstanceOfType(contentResult, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetAllEmployees_ReturnsNoContent()
        {
            mock.Setup(p => p.GetAll()).Returns(It.IsAny<List<Employee>>());
            var actionResult = employees.Get();
            var contentResult = actionResult as NoContentResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);

        }
        [TestMethod]
        public void GetEmployeeById_WithValidEmpID_ShouldReturnEmployee()
        {
            mock.Setup(p => p.GetById(It.IsAny<int>())).Returns(new Employee());
            var actionResult = employees.Get(1);
            var contentResult = actionResult as OkObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);         

        }

        [TestMethod]
        public void GetEmployeeById_WithInValidEmpId_ReturnsBadRequest()
        {
            mock.Setup(p => p.GetById(It.IsAny<int>())).Returns(It.IsAny<Employee>());
            var actionResult = employees.Get(1);
            var contentResult = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);          
        }
        [TestMethod]
        public void PostEmployee_WithValidData_ReturnsCreatedResult()
        {          
            mock.Setup(p => p.Add(It.IsAny<Employee>())).Returns(true);          
            var actionResult = employees.Post(new Employee());
            var contentResult = actionResult as CreatedResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.StatusCode, Convert.ToInt32(HttpStatusCode.Created));
            Assert.IsInstanceOfType(contentResult, typeof(CreatedResult));

        }


        [TestMethod]
        public void PostEmployee_WithInValidData_ReturnsBadRequest()
        {
            mock.Setup(p => p.Add(It.IsAny<Employee>())).Returns(false);
            var actionResult = employees.Post(new Employee());
            var contentResult = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.StatusCode, Convert.ToInt32(HttpStatusCode.BadRequest));
            Assert.IsInstanceOfType(contentResult, typeof(BadRequestObjectResult));

        }
        [TestMethod]
        public void PutEmployee_WithValidData_ReturnsCreatedResult()
        {
            mock.Setup(p => p.Update(It.IsAny<int>(), It.IsAny<Employee>())).Returns(true);
            var actionResult = employees.Put(It.IsAny<int>(), new Employee());
            var contentResult = actionResult as CreatedResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.StatusCode, Convert.ToInt32(HttpStatusCode.Created));
            Assert.IsInstanceOfType(contentResult, typeof(CreatedResult));
        }
        [TestMethod]
        public void PutEmployee_WithInValidData_ReturnsBadRequest()
        {
            mock.Setup(p => p.Update(It.IsAny<int>(), It.IsAny<Employee>())).Returns(false);
            var actionResult = employees.Put(It.IsAny<int>(), new Employee());
            var contentResult = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.StatusCode, Convert.ToInt32(HttpStatusCode.BadRequest));
            Assert.IsInstanceOfType(contentResult, typeof(BadRequestObjectResult));
        }
        [TestMethod]
        public void DeleteEmployee_WithValidID_ReturnsCreateRequest()
        {
            mock.Setup(p => p.Delete(It.IsAny<int>())).Returns(true);
            var actionResult = employees.Delete(It.IsAny<int>());
            var contentResult = actionResult as CreatedResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.StatusCode, Convert.ToInt32(HttpStatusCode.Created));
            Assert.IsInstanceOfType(contentResult, typeof(CreatedResult));

        }
        [TestMethod]
        public void DeleteEmployee_WithInValidID_ReturnsBadRequest()
        {
            mock.Setup(p => p.Delete(It.IsAny<int>())).Returns(false);
            var actionResult = employees.Delete(It.IsAny<int>());
            var contentResult = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(contentResult.StatusCode, Convert.ToInt32(HttpStatusCode.BadRequest));
            Assert.IsInstanceOfType(contentResult, typeof(BadRequestObjectResult));

        }




        private List<Employee> GetTestEmployees ()
        {
            var testEmployees = new List<Employee>();
            testEmployees.Add(new Employee { Id = 1, Name = "Demo1", Designation = "HR", Department = "ITDD" });
            testEmployees.Add(new Employee { Id = 2, Name = "Demo2", Designation = "Engineer", Department = "ITDD" });
            testEmployees.Add(new Employee { Id = 3, Name = "Demo3", Designation = "Manager", Department = "ITDD" });
            testEmployees.Add(new Employee { Id = 4, Name = "Demo4", Designation = "Lead", Department = "ITDD" });
            testEmployees.Add(new Employee { Id = 5, Name = "Demo5", Designation = "Manager", Department = "ITDD" });
            testEmployees.Add(new Employee { Id = 6, Name = "Demo6", Designation = "Lead", Department = "ITDD" });


            return testEmployees;
        }
    }
}
