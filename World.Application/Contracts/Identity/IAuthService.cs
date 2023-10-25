using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using World.Application.Models.Identity;

namespace World.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
