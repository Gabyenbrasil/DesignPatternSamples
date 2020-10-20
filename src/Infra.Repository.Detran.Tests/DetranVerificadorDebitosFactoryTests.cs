using DesignPatternSamples.Application.Repository;
using DesignPatternSamples.Infra.Repository.Detran;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace DesignPatternsSamples.Infra.Repository.Detran.Tests
{
    public class DetranVerificadorDebitosFactoryTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly IDetranVerificadorDebitosFactory _Factory;

        public DetranVerificadorDebitosFactoryTests(DependencyInjectionFixture dependencyInjectionFixture)
        {
            var serviceProvider = dependencyInjectionFixture.ServiceProvider;
            _Factory = serviceProvider.GetService<IDetranVerificadorDebitosFactory>();
        }

        [Theory(DisplayName = "Dado um UF que est� devidamente registrado no Factory devemos receber a sua implementa��o correspondente")]
        [InlineData("PE", typeof(DetranPEVerificadorDebitosRepository))]
        [InlineData("SP", typeof(DetranSPVerificadorDebitosRepository))]
        public void InstanciarServicoPorUFRegistrado(string uf, Type implementacao)
        {
            var resultado = _Factory.Create(uf);

            Assert.NotNull(resultado);
            Assert.IsType(implementacao, resultado);
        }

        [Fact(DisplayName = "Dado um UF que n�o est� registrado no Factory devemos receber NULL")]
        public void InstanciarServicoPorUFNaoRegistrado()
        {
            IDetranVerificadorDebitosRepository implementacao = _Factory.Create("CE");

            Assert.Null(implementacao);
        }
    }
}