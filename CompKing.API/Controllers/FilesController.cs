using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CompKing.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtentionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtentionContentTypeProvider)
        {
            _fileExtentionContentTypeProvider = fileExtentionContentTypeProvider ?? throw new System.ArgumentNullException(nameof(fileExtentionContentTypeProvider));
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(int fileId)
        {
            var pathToFile = "How-to-build-a-pc.pdf"; 

            // Check whether the file exist
            if(!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            if(!_fileExtentionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var fileBytes = System.IO.File.ReadAllBytes(pathToFile);

            return File(fileBytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}
