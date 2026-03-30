namespace ITOI_EXAM.DTOs
{
    public static class AuthDTO
    {
        public class LoginRequest
        {
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
        }

        public class LoginResponse
        {
            public string Token { get; set; } = "";
            public string Fullname { get; set; } = "";
            public string Email { get; set; } = "";
            public string Role { get; set; } = "";
        }
    }
}