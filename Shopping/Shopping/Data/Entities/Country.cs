using System.ComponentModel.DataAnnotations;

namespace Shopping.Data.Entities
{
    public class Country
    {
        public int Id { get; set; }
        
        [Display(Name = "País")]
        [MaxLength(50, ErrorMessage = "El campo {0} no debe tenes maximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        // 1 Pais tiene muchos estados
        public ICollection<State> States { get; set; }

        // Propiedad de lectura, 
        // Que va a devolver la cantidad de Provincia que tiene un pais.
        [Display(Name = "Departamentos/Estados")]
        public int StatesNumber => States == null ? 0 : States.Count;

        // Propiedad de lectura, 
        [Display(Name = "Ciudades")]
        public int CitiesNumber => States == null ? 0 : States.Sum(s => s.CitiesNumber);

    }
}
