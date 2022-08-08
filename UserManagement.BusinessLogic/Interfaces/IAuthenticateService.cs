using UserManagement.BusinessLogic.Models.Requests;
using UserManagement.BusinessLogic.Models.Responses;

namespace UserManagement.BusinessLogic.Interfaces
{
    public interface IAuthenticateService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest authReq);
    }
}
