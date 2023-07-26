using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTask.Domain.Enums;
using VentionTask.Domain.Models;

namespace VentionTask.Service.Interfaces;
public interface IUserService
{
    public Task<string> UploadCVSFile(IFormFile file);
    public Task<IEnumerable<User>> GetUsers(SortingType sortingType, int limitation);
}
