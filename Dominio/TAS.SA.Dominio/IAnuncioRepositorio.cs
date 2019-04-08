using System;
using System.Collections.Generic;

namespace TAS.SA.Dominio
{
    public interface IAnuncioRepositorio
    {
        IEnumerable<Anuncio> ListarAnuncios();
        IEnumerable<Anuncio> ListarAnunciosFechados();
        Anuncio ObterPorId(Guid id);
        void Salvar(Anuncio anuncio);
        void Atualizar(Anuncio anuncio);
    }
}