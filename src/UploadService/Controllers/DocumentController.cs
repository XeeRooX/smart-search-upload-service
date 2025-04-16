using Microsoft.AspNetCore.Mvc;
using UploadService.Domain.Common.Entities;
using UploadService.Domain.Services;
using UploadService.Dtos;

namespace UploadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController(IDocumentUploaderService documentUploaderService) : ControllerBase
    {
        // Временный синхронный эндпоинт
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody] DocumentUploadIn input)
        {
            var documentInfo = input.Map(new DocumentInfo());
            var listInfo = input.Map(new DocumentListInfo());

            await documentUploaderService.UploadAsync(documentInfo, listInfo);

            return Ok();
        }
    }
}
