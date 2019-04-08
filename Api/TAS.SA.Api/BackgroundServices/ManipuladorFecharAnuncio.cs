using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TAS.SA.Dominio;
using TAS.SA.Dominio.Eventos;

namespace TAS.SA.Api.BackgroundServices
{
    // public class ManipuladorFecharAnuncio : BackgroundService
    // {
    //     private readonly IConfiguration _configuration;
    //     private IBus _bus;
    //     private IAnuncioRepositorio _repositorio;

    //     public ManipuladorFecharAnuncio()
    //     {
    //         // _repositorio = repositorio;
    //     }

        // protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        // {
            // _bus = RabbitHutch.CreateBus(_configuration.GetSection("RabbitSettings").GetSection("Connection").Value);
            // _bus.Subscribe<EventoFechamentoAnuncio>("TAS.SA", ProcessarFechamentoAnuncio);

            // while (!stoppingToken.IsCancellationRequested)
            // {
            //     await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            // }

            // _bus.Dispose();
        // }

        // private void ProcessarFechamentoAnuncio(EventoFechamentoAnuncio obj)
        // {
        //     var anuncio = _repositorio.ObterPorId(obj.IdAnuncio);
        //     anuncio.Finalizar();

        //     _repositorio.Atualizar(anuncio);
        // }
    // }
}