﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    [Authorize]
    public class CarrinhoController : MainController
    {
        private readonly IComprasBffService _comprasBffService;
        private readonly ICatalogoService _catalogoService;

        public CarrinhoController(IComprasBffService comprasBffService)
        {
            _comprasBffService = comprasBffService;
        }

        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            return View(await _comprasBffService.ObterCarrinho());
        }

        [HttpPost]
        [Route("carrinho/adicionar-item")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoViewModel itemCarrinho)
        {
            var resposta = await _comprasBffService.AdicionarItemCarrinho(itemCarrinho);

            if (ResponsePossuiErros(resposta)) return View("Index", await _comprasBffService.ObterCarrinho());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/atualizar-item")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, int quantidade)
        {
            var item = new ItemCarrinhoViewModel { ProdutoId = produtoId, Quantidade = quantidade };
            var resposta = await _comprasBffService.AtualizarItemCarrinho(produtoId, item);

            if (ResponsePossuiErros(resposta)) return View("Index", await _comprasBffService.ObterCarrinho());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/remover-item")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var produto = await _catalogoService.ObterPorId(produtoId);

            if (produto == null)
            {
                AdicionarErroValidacao("Produto inexistente!");
                return View("Index", await _comprasBffService.ObterCarrinho());
            }

            var resposta = await _comprasBffService.RemoverItemCarrinho(produtoId);

            if (ResponsePossuiErros(resposta)) return View("Index", await _comprasBffService.ObterCarrinho());

            return RedirectToAction("Index");
        }
    }
}