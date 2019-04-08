using Flunt.Notifications;
using Flunt.Validations;

namespace TAS.SA.Api.ViewModels
{
    public class RegistrarAnuncioViewModel : Notifiable, IValidatable
    {
        public string NomeProjeto { get; set; }
        public string DescricaoProjeto { get; set; }

        public void Validate()
        {
            AddNotifications
            (
               new Contract()
               .IsNotNullOrWhiteSpace(NomeProjeto ,nameof(NomeProjeto), "Informe o nome do projeto.")
               .IsNotNullOrWhiteSpace(DescricaoProjeto, nameof(DescricaoProjeto), "Informe a descrição do projeto.")
            );
        }
    }
}