namespace FilmesAPI.Data.Dtos
{
    public class ReadFilmeDto
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int Duracao { get; set; }

        // info que pertence ao contexto do Dto, mas não ao filme
        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    }

}