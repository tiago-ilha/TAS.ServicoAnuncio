using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TAS.SA.Dominio;
using TAS.SA.Dominio.Filtros;
using TAS.SA.Infra.Config;

namespace TAS.SA.Infra
{
    public class AnuncioRepositorio : IAnuncioRepositorio
    {
        private ServicoAnuncioContexto _contexto;

        public AnuncioRepositorio(ServicoAnuncioContexto contexto)
        {
            _contexto = contexto;
        }

        public IEnumerable<Anuncio> ListarAnuncios()
        {
            IQueryable<Anuncio> query;

            if (ListarDeAnuncios.Filtro() != null)
                query = _contexto.Anuncios.AsNoTracking().OrderBy(x => x.DataCadastro).Where(ListarDeAnuncios.Filtro());
            else
                query = _contexto.Anuncios.AsNoTracking().OrderBy(x => x.DataCadastro);

            return query;
        }

        public IEnumerable<Anuncio> ListarAnunciosFechados()
        {
             IQueryable<Anuncio> query;

            if (ListarDeAnunciosFechados.Filtro() != null)
                query = _contexto.Anuncios.AsNoTracking().OrderBy(x => x.DataCadastro).Where(ListarDeAnunciosFechados.Filtro());
            else
                query = _contexto.Anuncios.AsNoTracking().OrderBy(x => x.DataCadastro);

            return query;
        }

        public Anuncio ObterPorId(Guid id)
        {
            return _contexto.Anuncios.SingleOrDefault(DetalheAnuncio.Filtro(id));
        }

        public void Salvar(Anuncio anuncio)
        {
            _contexto.Anuncios.Add(anuncio);
            _contexto.SaveChanges();
        }

        public void Atualizar(Anuncio anuncio)
        {
            _contexto.Entry(anuncio).State = EntityState.Modified;
            _contexto.SaveChanges();
        }

    }
}