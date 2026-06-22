using Microsoft.AspNetCore.Mvc;
using TmsApi.services;
using System.Threading.Tasks;

namespace TmsApi.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    // GET /api/students -> returns all enrollment records
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await studentService.GetAllAsync();
        return Ok(students);
    }

    // GET /api/students/{id} -> returns one student record or 404
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var record = await studentService.GetByIdAsync(id);
        return record is not null ? Ok(record) : NotFound();
    }

    // POST /api/students -> creates and returns 201 with Location header
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
    {
        var record = await studentService.CreateAsync(request.Name, request.Age, request.GPA);
        return CreatedAtAction("GetById", new { id = record.Id }, record);
    }

    // DELETE /api/students/{id} -> returns 204 or 404
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await studentService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    // GET /api/students/paged -> returns stable sorted, paginated rows
    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        // Fixed: Removed the underscore to match your primary constructor variable name
        var students = await studentService.GetPagedStudentsAsync(page, pageSize); 
        return Ok(students);
    }
}

// Data Transfer Object for Request Body
public record CreateStudentRequest(string Name, int Age, decimal GPA);