using NUnit.Framework;
using Moq;
using DemoApplication.MVC.Models.Repositories;
using DemoApplication.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApplication.MVC.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Test.TestController
{
    public class TestStudentsController
    {
        private Mock<IStudentRepository> moqStudents;
        private StudentsController studentsController;

        [SetUp]
        public void Setup()
        {
            moqStudents = new Mock<IStudentRepository>();
            studentsController = new StudentsController(moqStudents.Object);
        }

        #region Index

        [Test]
        public void IndexShouldReturnStudentListViewSuccess()
        {
            //Arrange
            moqStudents.Setup(repo => repo.GetAll()).Returns(MocGetAllStudents());

            // Act
            var result = studentsController.Index().Result;

            //Assert
            var viewResult = result as ViewResult;
            var model = viewResult.Model as List<Student>;
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<ViewResult>(result);
                Assert.That(3, Is.EqualTo(model.Count));
            });
        }
        [Test]
        public void IndexShouldShowEmptyTableForNoStudentsInDB()
        {
            //Arrange
            moqStudents.Setup(repo => repo.GetAll()).Returns(Task.FromResult(new List<Student>()));

            // Act
            var result = studentsController.Index().Result;

            //Assert
            var viewResult = result as ViewResult;
            var model = viewResult.Model as List<Student>;
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<ViewResult>(result);
                Assert.That(0, Is.EqualTo(model.Count));
            });
        }
        #endregion
        #region Get Student details by id

        [Test]
        public void GetStudentByIdShouldReturnSingleStudentSuccess()
        {
            //Arrange
            var NewID = new Guid();
            moqStudents.Setup(repo => repo.GetById(NewID)).Returns(MocGetStudentsById(NewID));

            // Act
            var result = studentsController.Details(NewID).Result;

            //Assert
            var viewResult = result as ViewResult;
            var model = viewResult.Model as Student;
            Assert.Multiple(() =>
            {
                Assert.IsInstanceOf<ViewResult>(result);
                Assert.That(model, Is.TypeOf<Student>());
            });
        }

        #endregion

        #region Private methods
        private Task<List<Student>> MocGetAllStudents()
        {
            var students = new List<Student>
            {
                new Student()
                {
                    Id = new Guid(),
                    FirstName = "Test first",
                    LastName = "Test Last",
                    Active = true,
                    Dob = Convert.ToDateTime("12-12-2020")
                },
                new Student()
                {
                    Id = new Guid(),
                    FirstName = "Test first 1",
                    LastName = "Test Last 1",
                    Active = true,
                    Dob = Convert.ToDateTime("12-12-2020")
                },
                new Student()
                {
                    Id = new Guid(),
                    FirstName = "Test first 2",
                    LastName = "Test Last 2",
                    Active = true,
                    Dob = Convert.ToDateTime("12-12-2020")
                }
            };

            return Task.FromResult(students);
        }

        private Task<Student> MocGetStudentsById(Guid id)
        {
            var students = MocGetAllStudents().Result;
            var selected = students[1];
            return Task.FromResult(selected);
        }
        #endregion

    }
}
