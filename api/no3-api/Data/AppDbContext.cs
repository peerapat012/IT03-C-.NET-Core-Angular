using Microsoft.EntityFrameworkCore;
using no3_api.Models;

namespace no3_api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<RequestListModel> Requests => Set<RequestListModel>();
}