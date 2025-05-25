using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Pessoa
{
    public record PessoaRequest(
        [Required][StringLength(50)] string LoginPessoa,
        [Required][StringLength(100)] string SenhaPessoa,
        [Required] bool IsProfessor
    );
}
