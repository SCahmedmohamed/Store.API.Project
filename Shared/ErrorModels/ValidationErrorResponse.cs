using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } = 400;
        public string Message { get; set; } = "Validation Failed";
        public IEnumerable<ValidationError> Errors { get; set; }
    }

    public class ValidationError
    {
        public int Field { get; set; }
        public IEnumerable<string> Error { get; set; }
    }
}
