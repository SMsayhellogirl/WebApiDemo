using static WebApiDemo.Models.LoginModels;

namespace WebApiDemo.Services
{
    public class LoginServices
    {
        public LoginResponse LoginAction(LoginResult _model)
        {
            //資料庫操作 (用EF或Dapper)

            //回覆
            return new LoginResponse() { 
             Token = "12345678",
              UserName = "張哲綸"
            };
        }
    }
}
