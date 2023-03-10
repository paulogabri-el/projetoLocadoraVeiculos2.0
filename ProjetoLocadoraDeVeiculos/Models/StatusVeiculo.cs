using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ProjetoLocadoraDeVeiculos.Models
{
    public class StatusVeiculo
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Descrição do status é obrigatório.")]
        [DisplayName("Status")]
        [RegularExpression(@"^[a-zA-Zà-úÀ-Ú /]+$", ErrorMessage = "Você inseriu caracteres inválidos para o nome.")]
        public string Nome { get; set; }

        [DisplayName("Data Cadastro")]
        [DataType(DataType.Date)]
        public DateTime? DataCadastro { get; set; }

        [DisplayName("Data Alteração")]
        [DataType(DataType.Date)]
        public DateTime? DataAlteracao { get; set; }
        public bool? Internal { get; set; }

        public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();

    }
}
