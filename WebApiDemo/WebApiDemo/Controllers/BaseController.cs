using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    public class BaseController : ControllerBase
    {
        public class ApiResponse<T>
        {
            /// <summary>
            /// 回覆訊息
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// HTTP CODE
            /// </summary>
            public int Code { get; set; }
            /// <summary>
            /// 狀態
            /// </summary>
            public string Status { get; set; }
            /// <summary>
            /// 回傳內容
            /// </summary>
            public T Body { get; set; }
        }
    }
}
