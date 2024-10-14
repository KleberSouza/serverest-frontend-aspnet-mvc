using System.ComponentModel.DataAnnotations;

namespace serverest_frontend_aspnet_mvc.Models
{
    public class LoginDtoRequest
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage ="Campo Obrigatório!")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class LoginDtoResponse
    {
        public string Message { get; set; }
        public string Authorization { get; set; } 
    }
}
