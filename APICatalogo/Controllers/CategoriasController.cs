using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Filter;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWorkk _unitOfWork;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ICategoriaRepository repository, ILogger<CategoriasController> logger, IUnitOfWorkk unitOfWork)
        {

            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _unitOfWork.CategoriaRepository.GetAll();

            if (categorias is null) return NotFound("Não existem categorias....");

            //var categoriasDto = new List<CategoriaDTO>();
            //foreach (var item in categoriasDto)
            //{
            //    var categoriaDto = new CategoriaDTO
            //    {
            //        CategoriaId = item.CategoriaId,
            //        Nome = item.Nome,
            //        ImagemUrl = item.ImagemUrl,
            //    };

            //    categoriasDto.Add(categoriaDto);
            //}


            var categoriasDto = categorias.ToCategoriaDTOList();

            return Ok(categoriasDto);

        }

        [HttpGet("id:int", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {

            var categoria = _unitOfWork.CategoriaRepository.Get(x => x.CategoriaId == id);

            if (categoria == null)
            {
                _logger.LogWarning($"Categoria com id= {id} não encontrada....");
                return NotFound("Categoria não encontrada");
            }

            //var categiriaDto = new CategoriaDTO()
            //{
            //    CategoriaId = categoria.CategoriaId,
            //    Nome = categoria.Nome,
            //    ImagemUrl = categoria.ImagemUrl
            //};

            var categoriaDto = categoria.ToCategoriaDTO();

            return Ok(categoriaDto);

        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {

            if (categoriaDto is null)
            {
                _logger.LogWarning($"Dados inválidos..");
                return BadRequest("Dados inválidos");
            };

            //var categoria = new Categoria()
            //{
            //    CategoriaId = categoriaDto.CategoriaId,
            //    Nome = categoriaDto.Nome,
            //    ImagemUrl= categoriaDto.ImagemUrl
            //};

            var categoria = categoriaDto.ToCategoria();

            var createCategoria = _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.Commit();

            //var novaCategoriaDto = new CategoriaDTO()
            //{
            //    CategoriaId = createCategoria.CategoriaId,
            //    Nome = createCategoria.Nome,
            //    ImagemUrl = createCategoria.ImagemUrl
            //};

            var novaCategoriaDto = createCategoria.ToCategoriaDTO();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);

        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {

            if (id != categoriaDto.CategoriaId)
            {
                _logger.LogWarning($"Dados inválidos...");
                return BadRequest("Dados inválidos");
            };

            //var categoria = new Categoria()
            //{
            //    CategoriaId = categoriaDto.CategoriaId,
            //    Nome = categoriaDto.Nome,
            //    ImagemUrl = categoriaDto.ImagemUrl
            //};

            var categoria = categoriaDto.ToCategoria();

            var categoriaAtualizada = _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();

            //var categoriaAtualizadaDto = new CategoriaDTO()
            //{
            //    CategoriaId = categoriaAtualizada.CategoriaId,
            //    Nome = categoriaAtualizada.Nome,
            //    ImagemUrl = categoriaAtualizada.ImagemUrl
            //};

            var categoriaAtualizadaDto = categoriaAtualizada.ToCategoriaDTO();

            return Ok(categoriaAtualizadaDto);


        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {

            var categoria = _unitOfWork.CategoriaRepository.Get(x=> x.CategoriaId == id);

            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound("Categoria não encontrado");
            }

            var deleteCategoria = _unitOfWork.CategoriaRepository.Delete(categoria);

            _unitOfWork.Commit();

            //var categoriaExcluidaDto = new CategoriaDTO()
            //{
            //    CategoriaId = deleteCategoria.CategoriaId,
            //    Nome = deleteCategoria.Nome,
            //    ImagemUrl = deleteCategoria.ImagemUrl
            //};

            var categoriaExcluidaDto = deleteCategoria.ToCategoriaDTO();

            return Ok(categoriaExcluidaDto);
        }
    }
}
