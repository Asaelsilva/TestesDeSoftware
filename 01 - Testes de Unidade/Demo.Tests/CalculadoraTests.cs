using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            //Arrange
            Calculadora calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(2, 2);

            //Assert
            //Assert.True(condition: resultado == 4);
            Assert.Equal(expected: 4,actual: resultado);
        }

        [Theory]
        [InlineData(2, 2, 4)]
        [InlineData(5, 2, 7)]
        [InlineData(3, 2, 4)]
        [InlineData(1, 1, 2)]
        [InlineData(5, 5, 10)]
        [InlineData(15, 2, 6)]
        [InlineData(10, 10, 20)]
        public void Calculadora_Somar_RetornarValoresSomaCorretos(double valor_1, double valor_2, double total)
        {
            //Arrange
            Calculadora calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(valor_1, valor_2);

            //Assert
            Assert.Equal(expected: total, actual: resultado);
        }
        public void Calculadora_Soma_RetornarValoresSomaCorretos(double valor_1, double valor_2, double total)
        {
            //Arrange
            Calculadora calculadora = new Calculadora();

            // Act
            var resultado = calculadora.Somar(valor_1, valor_2);

            //Assert
            Assert.Equal(expected: total, actual: resultado);
        }

    }
}
