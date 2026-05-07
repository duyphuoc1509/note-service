using FluentAssertions;
using NoteService.Domain;

namespace NoteService.UnitTests;

public class NoteTests
{
    [Fact]
    public void Constructor_ShouldTrimTitle_WhenTitleContainsWhitespace()
    {
        var note = new Note(Guid.NewGuid(), "  My note  ", "content");

        note.Title.Should().Be("My note");
    }

    [Fact]
    public void Constructor_ShouldThrow_WhenTitleIsEmpty()
    {
        var action = () => new Note(Guid.NewGuid(), "   ");

        action.Should().Throw<ArgumentException>()
            .WithParameterName("title");
    }
}
