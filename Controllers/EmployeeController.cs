using EmployeeDepartmentManagement.Data;
using EmployeeDepartmentManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeDepartmentManagement.ViewModels;

namespace EmployeeDepartmentManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAll()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();

            ViewBag.Departments = await _context.Departments
                .ToListAsync();

            return View(employees);
        }


        public async Task<IActionResult> Details(int id)
        {
            Employee? employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _context.Departments
                .ToListAsync();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee(
    EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Salary = model.Salary,
                    DepartmentId = model.DepartmentId
                };

                _context.Employees.Add(employee);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAll));
            }

            ViewBag.Departments = await _context.Departments
                .ToListAsync();

            return View("Create", model);
        }



        // REMOTE VALIDATION

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsEmailAvailable(
            string email,
            int id)
        {
            bool emailExists = await _context.Employees
                .AnyAsync(e =>
                    e.Email == email &&
                    e.Id != id);

            if (emailExists)
            {
                return Json(false);
            }

            return Json(true);
        }



        public async Task<IActionResult> Edit(int id)
        {
            Employee? employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            EmployeeEditViewModel model = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId
            };

            ViewBag.Departments = await _context.Departments
                .ToListAsync();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditEmployee(
    EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee? employee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Id == model.Id);

                if (employee == null)
                {
                    return NotFound();
                }

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DepartmentId = model.DepartmentId;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(GetAll));
            }

            ViewBag.Departments = await _context.Departments
                .ToListAsync();

            return View("Edit", model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            Employee? employee = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            Employee? employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetAll));
        }
    }
}