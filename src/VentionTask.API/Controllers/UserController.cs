using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VentionTask.Service.Interfaces;
using VentionTask.Service.Attributes;
using VentionTask.Domain.Enums;
using VentionTask.Domain.Models;

namespace VentionTask.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("uploadCSV")]
        public async Task<IActionResult> UploadCSV([CSVFile]IFormFile file)
        {
            try
            {
                string result = await _userService.UploadCVSFile(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during CSV file upload.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred during CSV file upload.");
            }
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(SortingType sortingType,int limitation)
        {
            try
            {
                var result = await _userService.GetUsers(sortingType, limitation);
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while trying to get the users.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while trying to get the users.");
            }
        }
    }
}
