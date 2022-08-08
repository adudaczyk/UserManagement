using Microsoft.Extensions.Options;
using System.Linq;
using UserManagement.API.Jwt;
using UserManagement.BusinessLogic.Interfaces;
using UserManagement.BusinessLogic.Models.Requests;
using UserManagement.BusinessLogic.Models.Responses;
using UserManagement.Repository.Interfaces;
using UserManagement.Utils.Helpers;
using UserManagement.Utils.Settings;

namespace UserManagement.BusinessLogic.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public AuthenticateService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest authReq)
        {
            var user = _unitOfWork.UserRepository.Find(x => x.Username == authReq.Username).SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            if (Hasher.Verify(authReq.Password, user.PasswordHash))
            {
                var token = JwtHelper.GenerateToken(_appSettings.JwtSecret, user.Id);

                return new AuthenticateResponse(user, token);
            }

            return null;
        }
    }
}
