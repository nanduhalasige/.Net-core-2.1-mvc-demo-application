using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoApplication.MVC.Models;
using DemoApplication.MVC.Models.Repositories;
using DemoApplication.MVC.Helper;

namespace DemoApplication.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;

        public StudentsController(IStudentRepository _studentRepository)
        {
            studentRepository = _studentRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await studentRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Dob")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid();
                student.Active = true;
                await studentRepository.Add(student);
                return RedirectToAction(nameof(Index)).WithSuccess("Successfull...!", "Student added successfully");
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await studentRepository.GetById(id);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,Dob,Active")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await studentRepository.Update(student);
                    return RedirectToAction(nameof(Index)).WithSuccess("Successfull...", "Student details updated successfully");
                }
                catch
                {
                    throw;
                }
            }
            return View(student);
        }

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var IsDeleted = await studentRepository.HardDelete(id);
            if (IsDeleted)
            {
                return RedirectToAction(nameof(Index)).WithSuccess("Successfull", "Student deleted successfully");
            }
            else
            {
                return RedirectToAction(nameof(Index)).WithDanger("Oops!!!", "Failed to delete selected student");
            }
        }

        private bool StudentExists(Guid id)
        {
            return studentRepository.Exists(id);
        }
    }
}
