using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leopard.Models
{
    public class Result
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public Result()
        {
            Status = false;
            Message = String.Empty;
        }
    }
}