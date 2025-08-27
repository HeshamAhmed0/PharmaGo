using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.MedulesDto.BasketDtos
{
    public class BasketResponseDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
