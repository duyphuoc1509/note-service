namespace NoteService.Shared.Auth;

public interface ICurrentUser
{
    string UserId { get; }
    bool IsAuthenticated { get; }
}
