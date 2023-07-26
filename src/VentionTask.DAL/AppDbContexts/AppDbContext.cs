using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VentionTask.Domain.Models;

namespace VentionTask.DAL.AppDbContexts
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; } = default!;
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
    }
}
