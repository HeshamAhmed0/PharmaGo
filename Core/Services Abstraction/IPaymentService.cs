using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.MedulesDto.BasketDtos;

namespace Services_Abstraction
{
    public interface IPaymentService
    {
        public Task<BasketResponseDto> CreateOrUpdatePaymentIntent(string BasketId);
    }
}
