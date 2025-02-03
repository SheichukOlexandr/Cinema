using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Models;
using DataAccess.Repositories.UnitOfWork;

namespace BusinessLogic.Services
{
    public class ReservationStatusService(IUnitOfWork unitOfWork, IMapper mapper) 
        : BaseService<ReservationStatusDTO, ReservationStatus>(unitOfWork.ReservationStatues, mapper);
}
