using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting; // 需要注入 IWebHostEnvironment

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FilesController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("upload/avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "未选择任何文件。" });
            }

            // 1. 定义存储路径 (例如：wwwroot/uploads/avatars)
            // wwwroot 是 ASP.NET Core 中用于存放静态文件的标准文件夹
            var uploadsFolderPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "avatars");
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            // 2. 生成一个唯一的文件名，避免重名覆盖
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolderPath, uniqueFileName);

            // 3. 将文件保存到服务器磁盘
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 4. 构建并返回可供前端访问的 URL
            // 例如: http://localhost:5001/uploads/avatars/xxxx_my-avatar.jpg
            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/avatars/{uniqueFileName}";

            return Ok(new { url = fileUrl });
        }
    }
}