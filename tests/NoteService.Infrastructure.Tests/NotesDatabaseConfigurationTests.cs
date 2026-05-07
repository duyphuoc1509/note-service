using Microsoft.Extensions.Configuration;
using NoteService.Infrastructure.Configuration;

namespace NoteService.Infrastructure.Tests;

public sealed class NotesDatabaseConfigurationTests
{
    [Fact]
    public void GetNotesConnectionString_uses_explicit_notes_connection_string()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:NotesConnection"] = "Host=db;Port=5432;Database=notes_db;Username=notes;Password=secret",
                ["ConnectionStrings:DefaultConnection"] = "Host=db;Port=5432;Database=monolith_db;Username=app;Password=secret"
            })
            .Build();

        var connectionString = configuration.GetNotesConnectionString();

        Assert.Contains("Database=notes_db", connectionString);
        Assert.DoesNotContain("monolith_db", connectionString, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void GetNotesConnectionString_builds_notes_db_from_database_internal_components()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["Database:Internal:Host"] = "postgres",
                ["Database:Internal:Port"] = "5432",
                ["Database:Internal:User"] = "notes_user",
                ["Database:Internal:Password"] = "notes_password"
            })
            .Build();

        var connectionString = configuration.GetNotesConnectionString();

        Assert.Contains("Host=postgres", connectionString);
        Assert.Contains("Port=5432", connectionString);
        Assert.Contains("Database=notes_db", connectionString);
        Assert.Contains("Username=notes_user", connectionString);
    }
}
