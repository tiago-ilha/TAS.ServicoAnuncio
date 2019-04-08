using System;

namespace TAS.SA.Dominio.Dtos
{
    public class ListarAnuncioDTO
    {
        public Guid IdAnuncio { get; set; }
        public string NomeProjeto { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool EstaFechado { get; set; }
    }
}