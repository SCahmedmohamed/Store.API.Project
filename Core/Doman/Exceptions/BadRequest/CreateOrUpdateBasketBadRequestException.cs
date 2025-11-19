using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doman.Exceptions.BadRequest
{
    public class CreateOrUpdateBasketBadRequestException() : BadRequestException($"Invaild Operation !!")
    {
    }
}
