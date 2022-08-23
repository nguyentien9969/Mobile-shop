using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entity.ResponseModels
{
    public class MainResponse
    {
        public bool Success { get; set; } = true;
        public string ExceptionMessage { get; set; }
        public Enum Code;
       
        public void HandleException(Exception ex)
        {
            Success = false;
            ExceptionMessage = ex.Message;
        }
    }

    public class MainResponse<T> : MainResponse
    {
        public T Data { get; set; }
    }
}
