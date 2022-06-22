using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain
{
    public class PedidoItem
    {
        public Guid PedidoItemId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public PedidoItem(Guid id, string descricao, int quantidade, decimal valor)
        {
            if (quantidade < Pedido.MIN_UNIDADES_ITEM) throw new DomainException($"Minimo de {Pedido.MIN_UNIDADES_ITEM} unidade(s) por produro!");

            PedidoItemId = id;
            ProdutoNome = descricao;
            Quantidade = quantidade;
            ValorUnitario = valor;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }
    }
}
