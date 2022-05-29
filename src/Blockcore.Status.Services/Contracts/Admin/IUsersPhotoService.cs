using blockcore.status.Entities.Admin;

namespace blockcore.status.Services.Contracts.Admin;

public interface IUsersPhotoService
{
    string GetUsersAvatarsFolderPath();
    void SetUserDefaultPhoto(User user);
    string GetUserDefaultPhoto(string photoFileName);
    string GetUserPhotoUrl(string photoFileName);
    string GetCurrentUserPhotoUrl();
}