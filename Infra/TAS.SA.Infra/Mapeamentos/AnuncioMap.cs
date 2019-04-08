using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TAS.SA.Dominio;

namespace TAS.SA.Infra.Mapeamentos
{
    public class AnuncioMap : IEntityTypeConfiguration<Anuncio>
    {
        public void Configure(EntityTypeBuilder<Anuncio> builder)
        {
            builder.HasKey(x => x.IdAnuncio);

            builder.Property(x => x.NomeProjeto).HasMaxLength(150).IsRequired();
            builder.Property(x => x.DescricaoProjeto).HasMaxLength(300);
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.Property(x => x.DataFechamento);
        }
    }
}