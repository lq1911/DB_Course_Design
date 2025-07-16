using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    // API 控制器类，用于处理 HTTP 请求
    [ApiController]
    [Route("api/[controller]")]
    public class ExampleController : ControllerBase
    {
        // 注入服务层接口，例如 IExampleService
        // private readonly IExampleService _exampleService;

        // public ExampleController(IExampleService exampleService)
        // {
        //     _exampleService = exampleService;
        // }

        // 示例：获取所有数据
        // [HttpGet]
        // public async Task<IActionResult> GetAll()
        // {
        //     var result = await _exampleService.GetAllAsync();
        //     return Ok(result);
        // }

        // 示例：添加新数据
        // [HttpPost]
        // public async Task<IActionResult> Create(ExampleDto dto)
        // {
        //     await _exampleService.CreateAsync(dto);
        //     return CreatedAtAction(nameof(GetAll), new { id = dto.Id }, dto);
        // }

        // 可根据实际情况添加 Put、Delete、GetById 等方法
    }
}
