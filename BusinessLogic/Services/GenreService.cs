using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;

namespace BusinessLogic.Services
{
    public class GenreService(IUnitOfWork unitOfWork, IMapper mapper) 
        : BaseService<GenreDTO, Genre>(unitOfWork.Genres, mapper);
}
