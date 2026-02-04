using no3_api.Models;

namespace no3_api.Data;

using Microsoft.EntityFrameworkCore;

public static class DbUtility
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }

    public static void AddRequestDb(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Request");
        builder.Services.AddSqlite<AppDbContext>(
            connectionString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<RequestListModel>().Any())
                {
                    context.Set<RequestListModel>().AddRange(
                        new RequestListModel
                        {
                            Id = 1,
                            Title = "Request for Annual Leave",
                            ResponseReason = "",
                            Status = RequestStatus.Pending
                        },
                        new RequestListModel
                        {
                            Id = 2,
                            Title = "Request for Equipment Upgrade",
                            ResponseReason = "",
                            Status = RequestStatus.Pending
                        },
                        new RequestListModel
                        {
                            Id = 3,
                            Title = "Request for Remote Work",
                            ResponseReason = "",
                            Status = RequestStatus.Pending
                        },
                        new RequestListModel
                        {
                            Id = 4,
                            Title = "Request for Overtime Approval",
                            ResponseReason = "Approved due to project deadline",
                            Status = RequestStatus.Approved
                        },
                        new RequestListModel
                        {
                            Id = 5,
                            Title = "Request for Budget Increase",
                            ResponseReason = "Rejected due to budget constraints",
                            Status = RequestStatus.Rejected
                        }
                    );
                    context.SaveChanges();
                }
            }));
    }
}