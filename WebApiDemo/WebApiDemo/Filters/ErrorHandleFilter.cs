using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiDemo.Filters
{
    public class ErrorHandleFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //寫LOG

            // 設定回應格式
            var response = new
            {
                message = "API錯誤",
                code = 500,
                status = "error",
                body = context.Exception.Message
            };

            // 設定回應內容和狀態碼
            context.Result = new JsonResult(response)
            {
                StatusCode = 500
            };

            // 避免錯誤再傳遞
            context.ExceptionHandled = true;
        }
    }
}
