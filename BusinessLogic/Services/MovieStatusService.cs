using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;

namespace BusinessLogic.Services
{
    public class MovieStatusService(IUnitOfWork unitOfWork, IMapper mapper)
        : BaseService<MovieStatusDTO, MovieStatus>(unitOfWork.MovieStatues, mapper);
}
