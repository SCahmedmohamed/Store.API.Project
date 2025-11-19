using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doman.Exceptions.BadRequest
{
    public class RegistrationBadRequest(List<string> msg) : BadRequestException(String.Join(", ",msg))
    {
    }
}
