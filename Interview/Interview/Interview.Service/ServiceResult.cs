using System;
using System.Collections.Generic;
using System.Text;

namespace Interview.Service
{
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult() { }

        public T Data { get; set; }

        public ServiceResult(T data)
        {
            Data = data;
        }
    }

    public class ServiceResult
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
