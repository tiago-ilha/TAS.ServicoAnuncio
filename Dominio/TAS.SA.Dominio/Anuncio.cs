using System;
using Flunt.Notifications;

namespace TAS.SA.Dominio
{
    public class Anuncio : Notifiable
    {
        public Anuncio()
        {
            IdAnuncio = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        public Anuncio(string nomeProjeto, string descricaoProjeto) : this()
        {
            NomeProjeto = nomeProjeto;
            DescricaoProjeto = descricaoProjeto;
        }

        public Guid IdAnuncio { get; private set; }
        public string NomeProjeto { get; private set; }
        public string DescricaoProjeto { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public DateTime? DataFechamento { get; private set; }

        public void AlterarNome(string nome) => NomeProjeto = nome;

        public void AlterarDescricao(string descricao) => DescricaoProjeto = descricao;

        public void Finalizar()
        {
            if (DataFechamento.HasValue)
            {
                AddNotification(nameof(DataFechamento), "Não é possível finalizar esse anúncio, pois o mesmo já se encontra finalizado");
                return;
            }

            DataFechamento = DateTime.Now;
        }
    }
}