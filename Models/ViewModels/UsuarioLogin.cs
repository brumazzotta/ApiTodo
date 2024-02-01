using System.ComponentModel.DataAnnotations;

namespace MeuTodo.mModels.ViewModels
{
    public class UsuarioLogIn
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}