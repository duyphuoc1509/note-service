namespace NoteService.Domain;

public sealed class Note
{
    public Guid Id { get; init; }
    public string Title { get; private set; }
    public string? Content { get; private set; }

    public Note(Guid id, string title, string? content = null)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        Id = id;
        Title = title.Trim();
        Content = content;
    }
}
