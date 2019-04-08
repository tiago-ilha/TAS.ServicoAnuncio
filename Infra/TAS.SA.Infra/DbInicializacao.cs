using System;
using System.Collections.Generic;
using TAS.SA.Dominio;
using TAS.SA.Infra.Config;

namespace TAS.SA.Infra
{
    public class DbInicializacao
    {
        public static void PopularDados(ServicoAnuncioContexto context)
        {
            var listaDeAnuncios = new List<Anuncio>
            {
                new Anuncio("Projeto A", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                new Anuncio("Projeto B", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                new Anuncio("Projeto C", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                new Anuncio("Projeto D", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                new Anuncio("Projeto E", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
                new Anuncio("Projeto F", "kljdasdkjsaoidjasiodsauidfuiasudihiusaduiasdasdihasdiuas"),
            };

            context.Anuncios.AddRange(listaDeAnuncios);
            context.SaveChanges();
        }
    }
}