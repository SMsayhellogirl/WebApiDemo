using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Filters;
using WebApiDemo.Services;
using static WebApiDemo.Controllers.BaseController;
using static WebApiDemo.Models.LoginModels;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DemoController : BaseController
    {
        /// <summary>
        /// 登入方法，接受使用者的帳號密碼並返回登入結果
        /// </summary>
        /// <param name="_model">登入請求的模型，包含帳號與密碼</param>
        /// <returns>登入回應，包含訊息、狀態碼與使用者資訊</returns>
        [HttpPost("login")]
        [HttpPost]
        public ApiResponse<LoginResponse> DemoLogin(LoginResult _model)
        {

            LoginServices _service = new LoginServices();

            LoginResponse loginResponse = _service.LoginAction(_model);
            // 包裝統一格式的回應
            var apiResponse = new ApiResponse<LoginResponse>
            {
                Message = "Login successful",
                Code = 200,
                Status = "OK",
                Body = loginResponse
            };

            // 返回統一格式的 JSON
            return apiResponse;
        }

        /// <summary>
        /// 錯誤範例
        /// </summary>
        /// <param name="_model">登入請求的模型，包含帳號與密碼</param>
        /// <returns>錯誤統一回覆</returns>
        [HttpPost]
        public ApiResponse<LoginResponse> DemoError(LoginResult _model)
        {
            //模擬出錯
            throw new Exception("這是模擬出來的錯誤，測試用！");

            //以下只是模擬範例不會執行
            LoginServices _service = new LoginServices();

            LoginResponse loginResponse = _service.LoginAction(_model);
            // 包裝統一格式的回應
            var apiResponse = new ApiResponse<LoginResponse>
            {
                Message = "Login successful",
                Code = 200,
                Status = "OK",
                Body = loginResponse
            };

            // 返回統一格式的 JSON
            return apiResponse;
        }

        /// <summary>
        /// 未登入call API 範例
        /// </summary>
        /// <returns>回覆無權限</returns>
        [HttpPost]
        [LoginFilter]
        public ApiResponse<LoginResponse> DemoLoginAfter()
        {
            //測試無權限,底下不會執行

            // 包裝統一格式的回應
            var apiResponse = new ApiResponse<LoginResponse>
            {
                Message = "Login successful",
                Code = 200,
                Status = "OK",
                Body = null
            };

            // 返回統一格式的 JSON
            return apiResponse;
        }

    }
}
