using System.ComponentModel.DataAnnotations;

namespace serverest_frontend_aspnet_mvc.Models
{
    public class Usuario
    {
        [Display(Name = "Id")]
        public string _id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string password { get; set; }

        [Display(Name = "Administrador")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string administrador { get; set; }
    }

    public class UsuarioResponse
    {
        public int Quantidade { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
