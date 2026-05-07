using NoteService.Domain;
using NoteService.Shared.Errors;

namespace NoteService.UnitTests;

public sealed class NoteDomainTests
{
    [Fact]
    public void Constructor_TrimsTitleAndContent_AndSetsOwner()
    {
        var note = new Note("user-1", "  My note  ", "  Content  ");

        Assert.Equal("user-1", note.OwnerId);
        Assert.Equal("My note", note.Title);
        Assert.Equal("Content", note.Content);
        Assert.False(note.IsFavorite);
        Assert.False(note.IsArchived);
    }

    [Fact]
    public void Constructor_RequiresOwner()
    {
        var ex = Assert.Throws<ApiException>(() => new Note(" ", "Title", "Content"));

        Assert.Equal("owner_required", ex.Code);
    }

    [Fact]
    public void Update_RequiresTitle()
    {
        var note = new Note("user-1", "Title", "Content");

        var ex = Assert.Throws<ApiException>(() => note.Update(" ", "Content"));

        Assert.Equal("note_title_required", ex.Code);
    }

    [Fact]
    public void Update_RejectsTitleLongerThan200Characters()
    {
        var note = new Note("user-1", "Title", "Content");

        var ex = Assert.Throws<ApiException>(() => note.Update(new string('a', 201), "Content"));

        Assert.Equal("note_title_too_long", ex.Code);
    }

    [Fact]
    public void SetFavorite_ChangesStateAndTouchesUpdatedAt()
    {
        var note = new Note("user-1", "Title", "Content");

        note.SetFavorite(true);

        Assert.True(note.IsFavorite);
        Assert.NotNull(note.UpdatedAt);
    }

    [Fact]
    public void Archive_MarksNoteArchived()
    {
        var note = new Note("user-1", "Title", "Content");

        note.Archive();

        Assert.True(note.IsArchived);
        Assert.NotNull(note.ArchivedAt);
    }
}
