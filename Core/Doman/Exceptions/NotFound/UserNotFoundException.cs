using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doman.Exceptions.NotFound
{
    public class UserNotFoundException(string email) : NotFoundException($"User With Email {email} Was Not Found !!")
    {
    }
}
