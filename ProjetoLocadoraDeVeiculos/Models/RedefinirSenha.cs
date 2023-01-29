using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoLocadoraDeVeiculos.Models
{
    public class RedefinirSenha
    {
        [Required(ErrorMessage = "Digite o e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o CPF")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Por favor, use somente números.")]
        public string Cpf { get; set; }

    }
}
