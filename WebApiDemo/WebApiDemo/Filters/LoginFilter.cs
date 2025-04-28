using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static WebApiDemo.Controllers.BaseController;

namespace WebApiDemo.Filters
{
    public class LoginFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // 假設登入狀態儲存在 Header 中
            var token = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(token) || !IsValidToken(token))
            {
                // 如果沒有有效的 Token，返回統一格式的錯誤回應
                var apiResponse = new ApiResponse<object>
                {
                    Message = "Unauthorized access. Invalid or missing token.",
                    Code = 401,
                    Status = "Unauthorized",
                    Body = null
                };

                // 設定返回結果為統一格式的 JSON
                context.Result = new JsonResult(apiResponse)
                {
                    StatusCode = 401 // 設定狀態碼為 401
                };
                return;
            }

            base.OnActionExecuting(context);
        }

        private bool IsValidToken(string token)
        {
            // 這裡根據您的需求檢查 Token 是否有效，這裡是範例
            return token == "valid_token"; // 這是範例，您可以替換為您的 Token 驗證邏輯
        }
    }
}
