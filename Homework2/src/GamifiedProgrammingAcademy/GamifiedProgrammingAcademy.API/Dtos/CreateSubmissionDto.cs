using System.ComponentModel.DataAnnotations;

namespace GamifiedProgrammingAcademy.API.Dtos
{
    public class CreateSubmissionDto
    {
        [Required(ErrorMessage = "El ID del usuario es obligatorio")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "El ID del desafío es obligatorio")]
        public int ChallengeId { get; set; }

        [Required(ErrorMessage = "El código es obligatorio")]
        [StringLength(5000, MinimumLength = 10, ErrorMessage = "El código debe tener entre 10 y 5000 caracteres")]
        public string Code { get; set; }

        [Required(ErrorMessage = "El lenguaje es obligatorio")]
        [StringLength(20, ErrorMessage = "El lenguaje no puede tener más de 20 caracteres")]
        public string Language { get; set; }
    }
}