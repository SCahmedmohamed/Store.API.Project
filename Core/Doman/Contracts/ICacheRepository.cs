using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doman.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string Key);
        Task SetAsync(string Key , object value , TimeSpan span);
    }
}
