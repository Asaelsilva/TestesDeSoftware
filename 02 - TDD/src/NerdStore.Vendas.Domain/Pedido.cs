using NerdStore.Vendas.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain
{
    public class Pedido
    {
        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }
        
        public Guid ClienteId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            if (_pedidoItems.Any(x => x.PedidoItemId == pedidoItem.PedidoItemId))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(x => x.PedidoItemId == pedidoItem.PedidoItemId);

                itemExistente.AdicionarUnidades(pedidoItem.Quantidade);
                pedidoItem = itemExistente;
                _pedidoItems.Remove(itemExistente);
            }
            _pedidoItems.Add(pedidoItem);
            CalcularValorPedido();
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());
        }

        public void TornarRascunho()
        {
            PedidoStatus = PedidoStatus.Rascunho;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteId)
            {
                var pedido = new Pedido
                {
                    ClienteId = clienteId,
                };

                pedido.TornarRascunho();
                return pedido;
            }
        }
    }
}
