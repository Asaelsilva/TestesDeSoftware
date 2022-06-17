using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class FuncionarioFactory
    {
        public static Funcionario Criar(string nome, double salario)
        {
            return new Funcionario(nome, salario);
        }
    }
}
