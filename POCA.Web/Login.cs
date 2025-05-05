namespace POCA.Web
{
    public class LoginService
    {
        private string ID;
        private string Usuario;
        private string Email;
        private string Senha;

        public LoginService(string id, string usuario, string email, string senha)
        {
            ID = id;
            Usuario = usuario;
            Email = email;
            Senha = senha;
        }

        public bool AutenticarUsuario(string username, string password)
        {
            if ((username == Usuario || username == Email) && password == Senha)
            {
                return true;
            }
            return false;
        }
    }
}