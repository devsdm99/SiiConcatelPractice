using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiiConcatelPractice.Results
{
    public class ApiResult
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
    }
}
