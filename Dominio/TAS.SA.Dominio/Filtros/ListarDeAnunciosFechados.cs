using System;
using System.Linq.Expressions;

namespace TAS.SA.Dominio.Filtros
{
    public class ListarDeAnunciosFechados
    {
        public static Expression<Func<Anuncio, bool>> Filtro()
        {
            return x => x.DataFechamento != null;
        }
    }
}