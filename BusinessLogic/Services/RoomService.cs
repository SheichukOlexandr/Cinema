using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;

namespace BusinessLogic.Services
{
    public class RoomService(IUnitOfWork unitOfWork, IMapper mapper) 
        : BaseService<RoomDTO, Room>(unitOfWork.Rooms, mapper);
}
