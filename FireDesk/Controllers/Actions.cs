﻿using FireDesk.Data;
using FireDesk.Exceptions;
using FireDesk.Models;
using FireDesk.Models.ViewModels;
using FireDesk.Services;
using HtmlTags.Reflection.Expressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FireDesk.Controllers
{
    public class Actions : Controller
    {
        private readonly TicketServices _ticketsServices;
        private readonly TecnicosServices _tecnicosServices;
        private readonly Context _context;

        public Actions(TicketServices ticketServices, TecnicosServices tecnicosServices, Context context)
        {
            _ticketsServices = ticketServices;
            _tecnicosServices = tecnicosServices;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] FiltroModel filtroModel)
        {
            try
            {
                ViewBag.Tecnicos = await _tecnicosServices.FindAllAsync();
                if (filtroModel.Termo != null)
                {
                    var t = await _ticketsServices.Filtrar(filtroModel);
                    var paginar = await _ticketsServices.Paginar(filtroModel, t);
                    var viewModelLista = new TicketsViewModel { Tickets = paginar, TotalRegistros = t.Count(), Termo = filtroModel.Termo };

                    return View(viewModelLista);
                }
                else
                {
                    var filtro = await _context.Ticket.Include(x => x.Tecnicos).OrderByDescending(x => x.TicketID)
                    .AsNoTracking().ToListAsync();
                    var paginar = await _ticketsServices.Paginar(filtroModel, filtro);
                    var viewModelLista = new TicketsViewModel { Tickets = paginar, TotalRegistros = await _ticketsServices.AllTicketsAsync() };
                    return View(viewModelLista);
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de chamada de dados!" });
            }
        }

        [HttpGet]
        [Route("ticket/get/{id:int}")]
        public async Task<IActionResult> GetId(int id)

        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(TicketsModel ticketsModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    if (errors.Count > 0)
                    {
                        // Adiciona os erros à lista
                        foreach (var error in errors)
                        {
                            // Retorna os erros em formato JSON
                            return Json(new { erro = true, Resultado = $"{error.ErrorMessage}" });
                        }
                    }
                }
            }

            try
            {
                await _ticketsServices.CreateTicketAsync(ticketsModel);
                return Json(new { erro = false });
            }
            catch (TicketException e)
            {
                return Json(new { erro = true, Resultado = e });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}