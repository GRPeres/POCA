using MudBlazor;

namespace POCA.Web.Utils
{
    public class LoginService
    {
        private string UserName;
        private int Age;
        private int Progress;
        private string Email;
        private string UserLogin;
        private string Password;

        public LoginService(string usuario, int idade, int progresso, string email, string login, string senha)
        {
            UserName = usuario;
            Age = idade;
            Progress = progresso;
            Email = email;
            UserLogin = login;
            Password = senha;
        }
    }
}