using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTask.DAL.Interfaces;
using VentionTask.DAL.Repositories;
using VentionTask.Domain.Enums;
using VentionTask.Domain.Models;
using VentionTask.Service.Interfaces;

namespace VentionTask.Service.Services;
public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly ILogger _logger;
    public UserService(IRepository<User> repository, ILogger<UserService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetUsers(SortingType sortingType, int limitation = 0)
    {
        IQueryable<User> users = sortingType == SortingType.descending ? _repository.GetAll().OrderByDescending(u => u.UserName) : _repository.GetAll().OrderBy(u => u.UserName);
        if (users != null)
        {
            users = limitation != 0 ? users.Take(limitation) : users;
            return await users.ToListAsync();
        }
        else throw new Exception("there is no user in the database.");
    }

    public async Task<string> UploadCVSFile(IFormFile file)
    {
        try
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<User>().ToList();
                foreach(var user in records)
                {
                    await InsertOrUpdateUser(user);
                }
            }
            _logger.LogInformation("CSV file processed successfully.");
            return "File detailes have been uploaded successfully!";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CSV file.");
            throw new Exception("An error occured during CSV file uploading process");
        }
    }
    private async Task InsertOrUpdateUser(User user)
    {
        var existingUser = await _repository.GetAsync(u => u.Id == user.Id);
        if (existingUser == null)
        {
            await _repository.CreateAsync(user);
        }
        else
        {
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;    
            existingUser.Age = user.Age;
            existingUser.City = user.City;
            existingUser.UserName = user.UserName;
            await _repository.UpdateAsync(existingUser.Id, existingUser);
        }
    }
}
