using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Model;

public class TodoResponse<T> : BaseResponse
{
    public TodoResponse()
    {
        IsSuccess = true;
        DataList = new List<T>();
        WarningMessages = new List<string>();
        StatusCode = (int)HttpStatusCode.OK;
    }

    public T? Data { get; set; }
    public List<T> DataList { get; set; }
}

public class BaseResponse
{
    public BaseResponse()
    {
        StatusCode = (int)HttpStatusCode.OK;
    }

    public bool IsSuccess = true;
    public string MessageTitle { get; set; }
    public string ErrorMessage { get; set; }
    public int StatusCode { get; set; }
    public List<string> WarningMessages { get; set; }
}