using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiVela.Models
{
    public class CustomApiResponse<T>
    {

        public T Object { get; set; }

        public ErrorViewModel Error { get; set; }
         
    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Mensaje { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
