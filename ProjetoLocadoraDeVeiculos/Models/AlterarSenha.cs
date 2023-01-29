using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoLocadoraDeVeiculos.Models
{
    public class AlterarSenha
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a senha atual do usuário.")]
        [DisplayName("Senha atual")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a nova senha do usuário.")]
        [DisplayName("Nova senha")]
        [RegularExpression(@"(?=.*[0-30])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "A senha precisa atender os requisitos:<br /> " +
            "                                                                                       - No mínimo uma letra maiúscula;<br />" +
            "                                                                                       - No mínino uma letra minúscula;<br />" +
            "                                                                                       - No mínimo um número;<br />" +
            "                                                                                       - No mínimo um caractere especial (@#$%^&+=).<br />")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha do usuário.")]
        [DisplayName("Senha")]
        [Compare("NovaSenha", ErrorMessage = "A senha informada é diferente do campo anterior!")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
