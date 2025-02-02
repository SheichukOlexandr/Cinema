using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Models;
using BusinessLogic.Services;
using DataAccess.Repositories.UnitOfWork;

namespace MVC_Cinema_app.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly MovieService _movieService;

        public MoviesController(IUnitOfWork unitOfWork, MovieService movieService)
        {
            _unitOfWork = unitOfWork;
            _movieService = movieService;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _movieService.GetAllAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetAsync((int) id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GenreId"] = new SelectList(await _unitOfWork.Genres.GetAllAsync(), "Id", "Name");
            ViewData["StatusId"] = new SelectList(await _unitOfWork.MovieStatues.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Director,Duration,Cast,GenreId,ReleaseDate,Description,MinAge,Rating,StatusId,PosterURL,TrailerURL")] MovieDTO movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateAsync(movie);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(await _unitOfWork.Genres.GetAllAsync(), "Id", "Name", movie.GenreId);
            ViewData["StatusId"] = new SelectList(await _unitOfWork.MovieStatues.GetAllAsync(), "Id", "Name", movie.StatusId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetAsync((int) id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(await _unitOfWork.Genres.GetAllAsync(), "Id", "Name", movie.GenreId);
            ViewData["StatusId"] = new SelectList(await _unitOfWork.MovieStatues.GetAllAsync(), "Id", "Name", movie.StatusId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Director,Duration,Cast,GenreId,ReleaseDate,Description,MinAge,Rating,StatusId,PosterURL,TrailerURL")] MovieDTO movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _movieService.EditAsync(movie);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(await _unitOfWork.Genres.GetAllAsync(), "Id", "Name", movie.GenreId);
            ViewData["StatusId"] = new SelectList(await _unitOfWork.MovieStatues.GetAllAsync(), "Id", "Name", movie.StatusId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetAsync((int)id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _movieService.GetAsync(id);
            if (movie != null)
            {
                await _movieService.DeleteAsync(id);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MovieExists(int id)
        {
            return await _movieService.GetAsync(id) != null;
        }
    }
}
