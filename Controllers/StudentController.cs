using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/students")]

public class StudentsController(IStudentService studentService): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await studentService.GetAllAsync();
        return Ok(students);
    }

    

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
    {
        var student =  await studentService.CreateAsync(request.Id, request.Name, request.GPA);
        return CreatedAtAction(nameof(GetById), new{ id = student.Id}, student);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await studentService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}

public record CreateStudentRequest(string Id, string Name, string GPA);