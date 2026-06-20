using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TmsApi.Data;
using TmsApi.Entities;

namespace TmsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssessmentsController(TmsDbContext context) : ControllerBase
{
    // GET: api/assessments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessments()
    {
        return await context.Assessments.Include(a => a.Course).ToListAsync();
    }
}