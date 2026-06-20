using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TmsApi.Data;
using TmsApi.Entities;

namespace TmsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CertificatesController(TmsDbContext context) : ControllerBase
{
    // GET: api/certificates
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Certificate>>> GetCertificates()
    {
        return await context.Certificates
            .Include(c => c.Student)
            .Include(c => c.Course)
            .ToListAsync();
    }
}