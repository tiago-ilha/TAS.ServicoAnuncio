using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using TAS.SA.Api.ViewModels;
using TAS.SA.Dominio;
using TAS.SA.Dominio.Dtos;

namespace TAS.SA.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioRepositorio _repositorio;
        private readonly IMapper _mapper;

        public AnuncioController(IAnuncioRepositorio repositorio, IMapper mapper) {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        // GET: api/Anuncio
        [HttpGet]
        public IActionResult ListarAnuncios() {
            var anuncios = _repositorio.ListarAnuncios();

            return RetornoListagem(_mapper.Map<IEnumerable<ListarAnuncioDTO>>(anuncios));
        }

        [HttpGet("fechados")]
        public IActionResult ListarAnunciosAtrasados() {
            var anuncios = _repositorio.ListarAnunciosFechados();

            return RetornoListagem(_mapper.Map<IEnumerable<ListarDeAnunciosFechadosDTO>>(anuncios));
        }

        // GET: api/Anuncio/5
        [HttpGet("{idAnuncio}")]
        public IActionResult DetalhesAnuncio(Guid idAnuncio)
        {
            if (idAnuncio == Guid.Empty)
                return BadRequest();

            var anuncio = RecuperarAnuncioPorId(idAnuncio);

            if (anuncio == null)
                return NotFound(RetornoAcao(false, "Nenhum registro foi encontrado.", null));

            return Ok(_mapper.Map<DetalheAnuncioDTO>(anuncio));
        }

        // POST: api/Anuncio
        [HttpPost]
        public IActionResult Cadastrar([FromBody] RegistrarAnuncioViewModel viewModel)
        {
            if (viewModel.Invalid)
                return BadRequest(RetornoAcao(viewModel.Valid, "Não foi possível completar operação.", viewModel.Notifications));

            var anuncio = new Anuncio(viewModel.NomeProjeto, viewModel.DescricaoProjeto);

            _repositorio.Salvar(anuncio);

            var url = Url.Action("DetalhesAnuncio", new { idAnuncio = anuncio.IdAnuncio });

            return Created(url, RetornoAcao(viewModel.Valid, "Operação foi realizada com sucesso."));
        }

        // PUT: api/Anuncio/5
        [HttpPut("{idAnuncio}")]
        public IActionResult Alterar(Guid idAnuncio, [FromBody] RegistrarAnuncioViewModel viewModel)
        {
            if (viewModel.Invalid)
                return BadRequest(RetornoAcao(viewModel.Valid, "Não foi possível completar operação.", viewModel.Notifications));

            var anuncio = _repositorio.ObterPorId(idAnuncio);

            if (anuncio == null)
                return NotFound(RetornoAcao(false, "Nenhum registro foi encontrado."));

            anuncio.AlterarNome(viewModel.NomeProjeto);
            anuncio.AlterarDescricao(viewModel.DescricaoProjeto);

            _repositorio.Atualizar(anuncio);

            return Ok(RetornoAcao(viewModel.Valid, "Operação foi realizada com sucesso."));
        }

        [HttpPut("{idAnuncio}/fechar")]
        public IActionResult FecharAnuncio(Guid idAnuncio)
        {
            if (idAnuncio == Guid.Empty)
                return BadRequest();

            var anuncio = RecuperarAnuncioPorId(idAnuncio);

            if (anuncio == null)
                return NotFound(RetornoAcao(false, "Nenhum registro foi encontrado."));

            anuncio.Finalizar();

            if (anuncio.Invalid)
                return BadRequest(RetornoAcao(anuncio.Valid, "Não foi possível completar operação.", anuncio.Notifications));

            _repositorio.Atualizar(anuncio);

            return Ok(RetornoAcao(anuncio.Valid, "Operação foi realizada com sucesso."));
        }

        #region Métodos compartilhados

        private IActionResult RetornoListagem(IEnumerable<dynamic> anuncios)
        {
            if (PossuiAnuncios(anuncios))
                return NoContent();

            return Ok(anuncios);
        }

        private bool PossuiAnuncios(IEnumerable<dynamic> anuncios) => anuncios == null || anuncios.Count() == 0;

        private Anuncio RecuperarAnuncioPorId(Guid idAnuncio)
        {
            return _repositorio.ObterPorId(idAnuncio);
        }

        private object RetornoAcao(bool estaValido, string mensagem, IReadOnlyCollection<Notification> erros = null)
        {
            return new
            {
                EstaValid = estaValido,
                Mensagem = mensagem,
                Erros = erros != null && erros.Count > 0 ? erros : new List<Notification>()
            };
        }

        #endregion
    }
}