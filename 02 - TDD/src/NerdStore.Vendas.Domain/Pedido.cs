using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
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
        public static int MAX_UNIDADES_ITEM => 15;
        public static int MIN_UNIDADES_ITEM => 1;

        public Guid ClienteId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal Desconto { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        public bool VoucherUtilizado { get; private set; }
        public Voucher Voucher { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public ValidationResult AplicarVoucher(Voucher voucher)
        {
            var result = voucher.ValidarSeAplicavel();
            if (!result.IsValid) return result;

            Voucher = voucher;
            VoucherUtilizado = true;

            CalcularValorTotalDesconto();

            return result;
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado) return;

            decimal desconto = 0;
            var valor = ValorTotal;

            if (Voucher.TipoDescontoVoucher == TipoDescontoVoucher.Valor)
            {
                if (Voucher.ValorDesconto.HasValue)
                {
                    desconto = Voucher.ValorDesconto.Value;
                    valor -= desconto;
                }
            }
            else
            {
                if (Voucher.PercentualDesconto.HasValue)
                {
                    desconto = (ValorTotal * Voucher.PercentualDesconto.Value) / 100;
                    valor -= desconto;
                }
            }

            ValorTotal = valor < 0 ? 0 : valor;
            Desconto = desconto;
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            ValidarQuantidadeItemPermitida(pedidoItem);

            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(x => x.PedidoItemId == pedidoItem.PedidoItemId);

                itemExistente.AdicionarUnidades(pedidoItem.Quantidade);
                pedidoItem = itemExistente;
                _pedidoItems.Remove(itemExistente);
            }
            _pedidoItems.Add(pedidoItem);
            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);
            ValidarQuantidadeItemPermitida(pedidoItem);

            var itemExistente = _pedidoItems.FirstOrDefault(x => x.PedidoItemId == pedidoItem.PedidoItemId);

            _pedidoItems.Remove(itemExistente);
            _pedidoItems.Add(pedidoItem);
            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            ValidarPedidoItemInexistente(pedidoItem);

            var itemExistente = _pedidoItems.FirstOrDefault(x => x.PedidoItemId == pedidoItem.PedidoItemId);

            _pedidoItems.Remove(itemExistente);
            CalcularValorPedido();

        }
        public void ValidarPedidoItemInexistente(PedidoItem pedidoItem)
        {
            if (!PedidoItemExistente(pedidoItem)) throw new DomainException($"O item não existe no pedido");
        }

        private void ValidarQuantidadeItemPermitida(PedidoItem pedidoItem)
        {
            var quantidadeItens = pedidoItem.Quantidade;
            if (PedidoItemExistente(pedidoItem))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(x => x.PedidoItemId == pedidoItem.PedidoItemId);
                quantidadeItens += itemExistente.Quantidade;
            }

            if (quantidadeItens > MAX_UNIDADES_ITEM) throw new DomainException($"Maximo de {MAX_UNIDADES_ITEM} unidades por produro!");
        }

        private bool PedidoItemExistente(PedidoItem pedidoItem)
        {
            return _pedidoItems.Any(x => x.PedidoItemId == pedidoItem.PedidoItemId);
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(i => i.CalcularValor());
            CalcularValorTotalDesconto();
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
