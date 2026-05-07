using Microsoft.EntityFrameworkCore;
using NoteService.Domain.Notes;
using NoteService.Infrastructure.Persistence;

namespace NoteService.Infrastructure.Tests;

public sealed class NotesDbContextModelTests
{
    [Fact]
    public void Notes_model_maps_to_notes_table_with_owner_archive_index()
    {
        var options = new DbContextOptionsBuilder<NotesDbContext>()
            .UseNpgsql("Host=localhost;Database=notes_db;Username=postgres;Password=postgres")
            .Options;

        using var dbContext = new NotesDbContext(options);
        var entity = dbContext.Model.FindEntityType(typeof(Note));

        Assert.NotNull(entity);
        Assert.Equal("notes", entity!.GetTableName());
        Assert.NotNull(entity.FindProperty(nameof(Note.OwnerId)));
        Assert.NotNull(entity.FindProperty(nameof(Note.ArchivedAt)));
        Assert.Contains(entity.GetIndexes(), index =>
            index.Properties.Select(p => p.Name).SequenceEqual(new[] { nameof(Note.OwnerId), nameof(Note.ArchivedAt) }));
    }
}
