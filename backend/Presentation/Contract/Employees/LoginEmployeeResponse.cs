namespace Presentation.Contract.Employees
{
    public class LoginEmployeeResponse
    {
        public LoginEmployeeResponse()
        {
        }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAdmin { get; set; }

        public LoginEmployeeResponse(string token, string refreshToken, bool isAdmin)
        {
            Token = token;
            RefreshToken = refreshToken;
            IsAdmin = isAdmin;
        }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
    }
}
