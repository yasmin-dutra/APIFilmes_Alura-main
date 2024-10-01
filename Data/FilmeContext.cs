using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext 
    {                                                   // passando as opções de acesso p/ o construtor da classe que estou estendendo, no caso DbContext.
        public FilmeContext(DbContextOptions<FilmeContext> opts) : base (opts) 
        {                                   // tipo da classe está sendo referenciado
                                            // opts é o nome que eu dou p/ ele

        }


        // criando a propriedade/atributo de acesso aos filmes da base
        public DbSet<Filme> Filmes { get; set; } // vai ser um conjunto de dados do banco de um <Filme> e o nome da propriedade é Filmes

    }
}