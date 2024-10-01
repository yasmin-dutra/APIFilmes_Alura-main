using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase 
    {
        private FilmeContext _context; // controlador passa a usar o context p/ acessar o db por meio da injeção de dependências
        private IMapper _mapper;


        // gera o construtor p/ criar em memória
        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context; // passa a ser uma instância/IMPLEMENTAÇÃO do contexto que vou utilizar
            _mapper = mapper;
        }
        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)  // o filme que eu vou receber contém as infos referentes a título, gênero e duração no body da requisição
        {
            /*
            filme.Id = id++;        // adiciona id de forma incremental nos filmes A MÃO
            filme.Add(filme);
            */

            // autoMapper vai converter um filmeDto p/ filme (isso fará ser interpretado no db)
            Filme filme = _mapper.Map<Filme>(filmeDto); // "Filme será mapeado a partir de um filmeDto"

            _context.Filmes.Add(filme); // Filme foi passado como propriedade lá no FilmeContext
            _context.SaveChanges();     // adicionar um objeto mapeado no db por meio do DbContext e salvar esta operação.


            // retorna o objeto criado, bem como a sua localização
            return CreatedAtAction(nameof(RecuperarFilmePorId),
                new { id = filme.Id },  // id do filme que acabei de criar
                filme );    // último parâmetro é o objeto que foi criado
        }


        [HttpGet]
        public IEnumerable<ReadFilmeDto> RecuperarFilmes([FromQuery] int skip=0, [FromQuery] int take=10)
        {                                               // caso essa interface seja implementada em algum lugar o Enumerable já dá conta(abstração), pq faz parte da classe List
                                                        // qnt menos depender de classes concretas, melhor. Melhor depender da interface.


            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));

                            // PAGINAÇÃO: retornar trechos reduzidos da minha lista de filmes, não a sua totalidade.
                            // Skip: qnts elementos da lista quero pular
                            // Take: qnts quero pegar
        }


        [HttpGet("{id}")]
        public IActionResult RecuperarFilmePorId(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
            // "ô método, para cada elemento da minha lista de filmes eu quero que o id desse filme seja igual ao id fornecido como parâmetro"
        

            // "se o filme que for recuperado a partir desta consulta for nulo significa que esse retorno será customizado"
            if(filme == null)
                return NotFound();
            
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return Ok(filmeDto);  // retorna o corpo do filme
            
        }


        [HttpPut("{id}")]
        public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto) // o filme que eu vou receber contém as infos referentes a título, gênero e duração no body da requisição
        {
            // esse filme já está no db
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if(filme == null)
                return NotFound();

            _mapper.Map(filmeDto, filme); // "eu quero que os campos do filmeDto sejam mapeados p/ filme afim de atualizá-los"
            _context.SaveChanges(); 

            return NoContent();         // retorna status code de um recurso atualizado -> convencional      
        }


        // verbo para atualizações parciais
        [HttpPatch("{id}")]
        public IActionResult AtualizarPartesDoFilme(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if(filme == null)
                return NotFound();
            
            // converter o filme do banco p/ updateFilmeDto, a fim de validá-lo. Se aprovado, vai ser alterado e convertido p/ filme novamente.
            var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);   // filme recuperado
            patch.ApplyTo(filmeParaAtualizar, ModelState);

            if(!TryValidateModel(filmeParaAtualizar))
                return ValidationProblem(ModelState);
                
            _mapper.Map(filmeParaAtualizar, filme);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletarFilmes(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if(filme == null)
                return NotFound();
            
            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();       
        }
    }
}