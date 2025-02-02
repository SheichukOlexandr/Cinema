using AutoMapper;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class MovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task CreateAsync(MovieDTO movie)
        {
            await _unitOfWork.Movies.AddAsync(_mapper.Map<Movie>(movie));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);

            if (movie == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            await _unitOfWork.Movies.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditAsync(MovieDTO movie)
        {
            await _unitOfWork.Movies.UpdateAsync(_mapper.Map<Movie>(movie));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<MovieDTO> GetAsync(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);

            if (movie == null)
                throw new Exception("" + HttpStatusCode.NotFound);

            return _mapper.Map<MovieDTO>(movie);
        }

        public async Task<IEnumerable<MovieDTO>> GetAllAsync()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync(includeProperties: [
                movie => movie.Genre, 
                movies => movies.Status
            ]);

            return _mapper.Map<IEnumerable<MovieDTO>>(movies);
        }
    }
}
