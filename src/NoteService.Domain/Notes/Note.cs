using NoteService.Domain.Common;

namespace NoteService.Domain.Notes;

public sealed class Note : Entity
{
    private Note() { }

    public Note(string ownerId, string title, string content)
    {
        OwnerId = string.IsNullOrWhiteSpace(ownerId)
            ? throw new ArgumentException("Owner is required.", nameof(ownerId))
            : ownerId;

        Update(title, content);
    }

    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public bool IsFavorite { get; private set; }

    public void Update(string title, string content)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Note title is required.", nameof(title));

        if (title.Length > 200)
            throw new ArgumentException("Note title must be 200 characters or fewer.", nameof(title));

        Title = title.Trim();
        Content = content?.Trim() ?? string.Empty;
        Touch();
    }

    public void SetFavorite(bool isFavorite)
    {
        if (IsFavorite == isFavorite) return;

        IsFavorite = isFavorite;
        Touch();
    }
}
