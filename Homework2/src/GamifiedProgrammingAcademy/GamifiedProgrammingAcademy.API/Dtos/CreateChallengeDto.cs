using System.ComponentModel.DataAnnotations;

namespace GamifiedProgrammingAcademy.API.Dtos
{
    public class CreateChallengeDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "El título debe tener entre 5 y 100 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripción debe tener entre 10 y 500 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Los puntos son obligatorios")]
        [Range(1, 1000, ErrorMessage = "Los puntos deben estar entre 1 y 1000")]
        public int Points { get; set; }
    }
}