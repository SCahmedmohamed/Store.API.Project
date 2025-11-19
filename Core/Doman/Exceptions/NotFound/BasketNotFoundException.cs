using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doman.Exceptions.NotFound
{
    public class BasketNotFoundException(string Id) : NotFoundException($"The Basket With Id {Id} Is Not Found !!")
    {
    }
}
