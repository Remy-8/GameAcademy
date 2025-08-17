using System.ComponentModel.DataAnnotations;

namespace GamifiedProgrammingAcademy.API.Dtos
{
    public class CreateBadgeDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripción debe tener entre 10 y 500 caracteres")]
        public string Description { get; set; }

        [StringLength(200, ErrorMessage = "La URL del icono no puede tener más de 200 caracteres")]
        public string IconUrl { get; set; }

        [Required(ErrorMessage = "El tipo de badge es obligatorio")]
        [StringLength(20, ErrorMessage = "El tipo no puede tener más de 20 caracteres")]
        public string BadgeType { get; set; }

        [Required(ErrorMessage = "La condición es obligatoria")]
        [StringLength(200, ErrorMessage = "La condición no puede tener más de 200 caracteres")]
        public string UnlockCondition { get; set; }

        [Required(ErrorMessage = "Los puntos requeridos son obligatorios")]
        [Range(0, 10000, ErrorMessage = "Los puntos deben estar entre 0 y 10000")]
        public int RequiredPoints { get; set; }

        [StringLength(20, ErrorMessage = "La rareza no puede tener más de 20 caracteres")]
        public string Rarity { get; set; }
    }
}