using System.ComponentModel.DataAnnotations;

namespace serverest_frontend_aspnet_mvc.Models
{
    public class Produto
    {
        [Display(Name = "Id")]
        public string _id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string nome { get; set; }
       
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string descricao { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int preco { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int quantidade { get; set; }
    }

    public class ProdutoCreate
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string descricao { get; set; }

        [Display(Name = "Preço")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int preco { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int quantidade { get; set; }
    }

    public class ProdutoResponse
    {
        public int Quantidade { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
