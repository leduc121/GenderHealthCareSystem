namespace GenderHealthcare.BLL
{
    public static class AuthService
    {
        // Thay đổi kiểu dữ liệu thành string để khớp với User.Id
        public static string CurrentUserId { get; private set; }

        public static void SetCurrentUser(string userId)
        {
            CurrentUserId = userId;
        }
    }
}