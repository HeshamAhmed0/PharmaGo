using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.MedulesDto.AuthModels;

namespace Services_Abstraction
{
    public interface IAuthServices
    {
        public Task<LoginResultDto> LoginAsync(LoginDto loginDto);
        public Task<RegisterResultDto> RegisterAsync(RegisterDto registerDto);
        public Task<bool> FindByEmailAsync(string email);

    }
}
