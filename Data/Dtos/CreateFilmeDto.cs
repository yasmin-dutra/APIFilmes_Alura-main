using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class CreateFilmeDto 
    {
        // criando propriedades/atributos que servem como modelo p/ o meu db

        [Required(ErrorMessage = "O título do filme é obrigatório.")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "O gênero do filme é obrigatório.")]
        [StringLength(50, ErrorMessage = "O tamanho do gênero não pode exceder 50 caracteres.")] // essa prop n aloca memória no db
        public string Genero { get; set; }


        [Required(ErrorMessage = "A duração do filme é obrigatório.")]
        [Range(70, 600, ErrorMessage = "O intervalo de duração deve ter entre 70 e 600 minutos.")]
        public int Duracao { get; set; }

    }
}
