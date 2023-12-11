using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Infrastructure
{
    public class JsonResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
    }
}
