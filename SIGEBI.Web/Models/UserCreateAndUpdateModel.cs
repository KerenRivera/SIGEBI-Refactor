using System.ComponentModel.DataAnnotations;

namespace SIGEBI.Web.Models
{
    public class UserCreateAndUpdateModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Name { get; set; }
    }
}
