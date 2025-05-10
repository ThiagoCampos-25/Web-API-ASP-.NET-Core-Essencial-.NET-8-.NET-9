using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using APICatalogo.Validations;
namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto : IValidatableObject
{

    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "o nome deve ter no máximo {1} e no mínimo {2} caracteres",
        MinimumLength = 5)]
    //[PrimeiraLetraMaiusculaAttibute]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300, ErrorMessage ="A descrição deve ter no máximo {1} caracteres")]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName ="decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(300)]
    public string?  ImagemUrl { get; set; }

    public  float Estoque { get; set; }

    public DateTime DataCadastro { get; set; }

    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(this.Nome))
        {
            var primeiraLetra = Convert.ToString(this.Nome[0]);

            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                yield return new
                    ValidationResult("A primeira letra do produto deve ser maiúscula",
                    new[]
                    {nameof(this.Nome)});
            }
        }

        if (this.Estoque <= 0)
        {
            yield return new
                    ValidationResult("O estoque deve ser maior que zero",
                    new[]
                    {nameof(this.Estoque)});
        }
    }
}
