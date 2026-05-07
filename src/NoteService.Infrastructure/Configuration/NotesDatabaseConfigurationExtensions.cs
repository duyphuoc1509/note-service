using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoteService.Infrastructure.Persistence;

namespace NoteService.Infrastructure.Configuration;

public static class NotesDatabaseConfigurationExtensions
{
    private const string NotesConnectionName = "NotesConnection";
    private const string NotesDatabaseName = "notes_db";

    public static IServiceCollection AddNotesDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetNotesConnectionString();
        services.AddDbContext<NotesDbContext>(options => options.UseNpgsql(connectionString));
        return services;
    }

    public static string GetNotesConnectionString(this IConfiguration configuration)
    {
        var configuredConnectionString = configuration.GetConnectionString(NotesConnectionName);
        if (!string.IsNullOrWhiteSpace(configuredConnectionString))
            return configuredConnectionString;

        var host = Resolve(configuration, "Database:Internal:Host", "DATABASE_INTERNAL_HOST");
        var port = Resolve(configuration, "Database:Internal:Port", "DATABASE_INTERNAL_PORT");
        var username = Resolve(configuration, "Database:Internal:User", "DATABASE_INTERNAL_USER");
        var password = Resolve(configuration, "Database:Internal:Password", "DATABASE_INTERNAL_PASSWORD");

        if (string.IsNullOrWhiteSpace(host)
            || string.IsNullOrWhiteSpace(port)
            || string.IsNullOrWhiteSpace(username)
            || string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                "Notes database connection is not configured. Provide ConnectionStrings:NotesConnection " +
                "or DATABASE_INTERNAL_HOST, DATABASE_INTERNAL_PORT, DATABASE_INTERNAL_USER, and " +
                "DATABASE_INTERNAL_PASSWORD. The database name is fixed to notes_db for service isolation.");
        }

        if (!int.TryParse(port, out _))
            throw new InvalidOperationException($"DATABASE_INTERNAL_PORT value '{port}' is not a valid integer.");

        return $"Host={host};Port={port};Database={NotesDatabaseName};Username={username};Password={password}";
    }

    private static string? Resolve(IConfiguration configuration, string configKey, string envVarName)
    {
        var value = configuration[configKey];
        if (!string.IsNullOrWhiteSpace(value))
            return value;

        return Environment.GetEnvironmentVariable(envVarName);
    }
}
