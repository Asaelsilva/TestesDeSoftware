using Xunit;

namespace Demo.Tests
{
    public class AssertStringsTests
    {
        [Fact]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir(nome: "Asael", sobrenome: "Silva");

            // Assert
            Assert.Equal(expected: "Asael Silva", actual: nomeCompleto);
        }



        [Fact]
        public void StringsTools_UnirNomes_DeveIgnorarCase()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Asael", "Silva");

            // Assert
            Assert.Equal(expected:"ASAEL SILVA", actual: nomeCompleto, ignoreCase: true);
        }



        [Fact]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Asael", "Silva");

            // Assert
            Assert.Contains(expectedSubstring:"sael", actualString: nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Asael", "Silva");

            // Assert
            Assert.StartsWith(expectedStartString:"Asa", actualString: nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_DeveAcabarCom()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Asael", "Silva");

            // Assert
            Assert.EndsWith(expectedEndString: "va", actualString: nomeCompleto);
        }


        [Fact]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var sut = new StringsTools();

            // Act
            var nomeCompleto = sut.Unir("Asael", "Silva");

            // Assert
            Assert.Matches(expectedRegexPattern:"[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", actualString: nomeCompleto);
        }
    }
}