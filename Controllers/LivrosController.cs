﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LivrosApi.Models;

namespace LivrosApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivroContext _context;

        public LivrosController(LivroContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
        {
            return await _context.Livros.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivro(int id)
        {
            var livro = await _context.Livros.FindAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(Livro livro)
        {
            /*
            if (id != livro.Id)
            {
                return BadRequest();
            }*/

            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(livro.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

    
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro livro)
        {
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLivro", new { id = livro.Id }, livro);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Livro>> DeleteLivro(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return livro;
        }

        private bool LivroExists(int id)
        {
            return _context.Livros.Any(e => e.Id == id);
        }
    }
}
