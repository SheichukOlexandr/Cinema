using BusinessLogic.DTOs;
using DataAccess.Models;

namespace BusinessLogic.Helpers
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile() {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<MovieStatus, MovieStatusDTO>().ReverseMap();

            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<MovieDTO, Movie>(); // Reverse

            CreateMap<MoviePrice, MoviePriceDTO>().ReverseMap();
            CreateMap<Seat, SeatDTO>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name));
            CreateMap<SeatDTO, Seat>(); // Reverse

            CreateMap<Room, RoomDTO>().ReverseMap();

            CreateMap<Session, SessionDTO>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MoviePrice.Price))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.MoviePrice.Movie.Title));
            CreateMap<SessionDTO, Session>(); // Reverse

            CreateMap<ReservationStatus, ReservationStatusDTO>().ReverseMap();
            CreateMap<UserStatus, UserStatusDTO>().ReverseMap();
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<UserDTO, User>(); // Reverse

            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Session.Date))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Session.Time))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Session.MoviePrice.Movie.Title))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.Number))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Seat.Room))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<ReservationDTO, Reservation>(); // Reverse
        }
    }
}
