using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMSystem.Data;
using SMSystem.Models;

namespace SMSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }
        
        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SrNo,RollNumber,FirstName,MiddleName,LastName,Class,Mobile,Email,Address,Photograph")] Student student, IFormFile photograph)
        {
            // Log the form input
            Console.WriteLine("Form submitted.");
            Console.WriteLine($"Student: {student.FirstName} {student.LastName}");

            // Generate RollNumber automatically if it's not provided
            if (string.IsNullOrEmpty(student.RollNumber))
            {
                var lastStudent = await _context.Students
                    .OrderByDescending(s => s.SrNo)
                    .FirstOrDefaultAsync();

                if (lastStudent != null && !string.IsNullOrEmpty(lastStudent.RollNumber))
                {
                    // Extract the numeric part of the last roll number (RR00001 -> 00001)
                    var lastRollNumber = lastStudent.RollNumber;
                    var rollNumberPart = lastRollNumber.Substring(2); // Remove "RR"
                    if (int.TryParse(rollNumberPart, out int lastNumericPart))
                    {
                        // Increment the last numeric part by 1
                        int nextRollNumber = lastNumericPart + 1;
                        // Format the next roll number with leading zeros
                        student.RollNumber = $"RR{nextRollNumber:D5}"; // Ensure 5 digits
                    }
                }
                else
                {
                    // If no students exist, start with RR00001
                    student.RollNumber = "RR00001";
                }
            }

            if (photograph != null && photograph.Length > 0)
            {
                Console.WriteLine("File received.");
                Console.WriteLine($"File: {photograph.FileName}, Size: {photograph.Length}");

                // Ensure the upload directory exists
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Console.WriteLine($"Upload Directory: {uploadDirectory}");

                if (!Directory.Exists(uploadDirectory))
                {
                    Console.WriteLine("Creating upload directory...");
                    Directory.CreateDirectory(uploadDirectory);
                }

                // Generate a unique filename to avoid overwriting
                var fileExtension = Path.GetExtension(photograph.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(uploadDirectory, uniqueFileName);

                Console.WriteLine($"Generated file path: {filePath}");

                // Save the file
                try
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await photograph.CopyToAsync(fileStream);
                        Console.WriteLine("File successfully saved.");
                    }

                    // Save the relative file path in the student model
                    student.Photograph = "/uploads/" + uniqueFileName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"File saving failed: {ex.Message}");
                    ModelState.AddModelError("Photograph", "Error uploading file.");
                    return View(student);
                }
            }
            else
            {
                Console.WriteLine("No file uploaded, using default image.");
                student.Photograph = "/uploads/default.jpg";  // You can modify this to suit your needs
            }

            // Save the student data to the database
            try
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                Console.WriteLine("Student saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving student: {ex.Message}");
                ModelState.AddModelError("", "There was an issue saving the student data.");
                return View(student);
            }

            // Redirect after saving
            return RedirectToAction(nameof(Index));
        }



        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("SrNo,RollNumber,FirstName,MiddleName,LastName,Class,Mobile,Email,Address,Photograph")] Student student, IFormFile photograph)
{
    if (id != student.SrNo)
    {
        return NotFound();
    }

    Console.WriteLine($"Editing student: {student.FirstName} {student.LastName}");
    Console.WriteLine($"Photograph: {photograph?.FileName ?? "No file uploaded"}");

    // Clear ModelState errors for Photograph field
    ModelState.Remove("Photograph"); // Remove Photograph field from ModelState validation

    // Log the model state validation errors, if any
    if (!ModelState.IsValid)
    {
        Console.WriteLine("ModelState is invalid.");
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Error: {error.ErrorMessage}");
        }
        return View(student); // Return the same view with error messages
    }

    try
    {
        // If no new file is uploaded, retain the existing photograph
        if (photograph != null && photograph.Length > 0)
        {
            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            // Ensure the upload directory exists
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var fileExtension = Path.GetExtension(photograph.FileName);
            var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(uploadDirectory, uniqueFileName);

            // Save the file to server
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photograph.CopyToAsync(fileStream);
            }

            // If there's an existing photo, delete it from the server
            if (!string.IsNullOrEmpty(student.Photograph) && student.Photograph != "/uploads/default.jpg")
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", student.Photograph.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            student.Photograph = "/uploads/" + uniqueFileName;
        }
        else
        {
            // If no new image is uploaded, retain the current image path
            var existingStudent = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.SrNo == id);
            student.Photograph = existingStudent?.Photograph ?? "/uploads/default.jpg"; // Default image if none provided
        }

        // Update the student record in the database
        _context.Update(student);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index)); // Redirect after successful save
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception: " + ex.Message);
        return View(student); // Return to the same view if there's an error
    }
}





        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.SrNo == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.SrNo == id);
        }
    }
}
