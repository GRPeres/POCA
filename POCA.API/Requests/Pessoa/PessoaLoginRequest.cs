using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Pessoa
{
    public record PessoaLoginRequest(
        [Required] string Login,
        [Required] string Senha
    );
}
