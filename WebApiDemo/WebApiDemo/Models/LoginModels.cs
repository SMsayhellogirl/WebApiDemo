namespace WebApiDemo.Models
{
    public class LoginModels
    {
        public class LoginResponse
        {
            /// <summary>
            /// 登入Token
            /// </summary>
            public string Token { get; set; }
            /// <summary>
            /// 使用者名稱
            /// </summary>
            public string UserName { get; set; }
        }

        public class LoginResult
        {
            /// <summary>
            /// 使用者帳號
            /// </summary>
            public string UserAccount { get; set; }
            /// <summary>
            /// 使用者密碼
            /// </summary>
            public string PassWord { get; set; }
        }
    }
}
