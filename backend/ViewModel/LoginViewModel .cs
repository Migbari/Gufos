using System.ComponentModel.DataAnnotations;

namespace backend.ViewModel
{
    public class LoginViewModel 
    {
          // Data Annotations
        [Required]
        public string Email { get; set; }
        // definimos o tamanho do campo
        [StringLength(255, MinimumLength = 5)]
        public string Senha { get; set; }
    }
}
