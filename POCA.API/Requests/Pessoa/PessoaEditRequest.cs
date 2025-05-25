using System.ComponentModel.DataAnnotations;

namespace POCA.API.Requests.Pessoa
{
    public record PessoaEditRequest(
        [Required] int IdPessoa,
        string LoginPessoa,
        string SenhaPessoa,
        bool IsProfessor,
        int? IdAluno,
        int? IdProfessor
    ) : PessoaRequest(LoginPessoa, SenhaPessoa, IsProfessor);
}
