using System.Threading.Tasks;
using Refit;

namespace wikr.FluentSlack
{
    [Headers("Content-Type: application/json;charset=utf-8")]
    public interface IChatApi
    {
        [Post("/chat.postMessage")]
        Task<SlackResponse> PostMessage(Payload message);
    }

    [Headers("Content-Type: application/x-www-form-urlencoded")]
    public interface IUsersApi
    {
        [Get("/users.lookupByEmail")]
        Task<SlackResponse> LookupByEmailAsync(string email);
    }
}