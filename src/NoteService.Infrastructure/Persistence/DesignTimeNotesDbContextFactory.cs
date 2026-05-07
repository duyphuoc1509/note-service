using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NoteService.Infrastructure.Configuration;

namespace NoteService.Infrastructure.Persistence;

public sealed class DesignTimeNotesDbContextFactory : IDesignTimeDbContextFactory<NotesDbContext>
{
    public NotesDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetNotesConnectionString();
        var optionsBuilder = new DbContextOptionsBuilder<NotesDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new NotesDbContext(optionsBuilder.Options);
    }
}
