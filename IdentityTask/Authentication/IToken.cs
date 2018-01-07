using IdentityTask.Models;

namespace IdentityTask.Authentication
{
    public interface IToken
    {
        string GenerateJwt(User user);
    }
}
