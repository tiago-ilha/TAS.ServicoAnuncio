using System;

namespace TAS.SA.Dominio.Filtros
{
    public class DetalheAnuncio
    {
        public static Func<Anuncio, bool> Filtro(Guid idAnuncio)
        {
            return x => x.IdAnuncio == idAnuncio;
        }
    }
}