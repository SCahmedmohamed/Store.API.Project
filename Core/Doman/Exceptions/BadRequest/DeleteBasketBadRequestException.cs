using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doman.Exceptions.BadRequest
{
    public class DeleteBasketBadRequestException() : BadRequestException($"Invaild Operation While Deleting The Basket !!")
    {
    }
}
