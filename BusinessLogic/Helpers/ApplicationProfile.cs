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

            CreateMap<MoviePrice, MoviePriceDTO>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.MoviePriceName, opt => opt.MapFrom(src => src.Movie.Title + " – " + src.Price));
            CreateMap<MoviePriceDTO, MoviePrice>(); // Reverse

            CreateMap<Seat, SeatDTO>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.SeatName, opt => opt.MapFrom(src => src.Room.Name + " – " + src.Number));
            CreateMap<SeatDTO, Seat>(); // Reverse

            CreateMap<Room, RoomDTO>().ReverseMap();

            CreateMap<Session, SessionDTO>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MoviePrice.Price))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.MoviePrice.Movie));
            CreateMap<SessionDTO, Session>(); // Reverse

            CreateMap<ReservationStatus, ReservationStatusDTO>().ReverseMap();
            CreateMap<UserStatus, UserStatusDTO>().ReverseMap();
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<UserDTO, User>(); // Reverse

            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Session, opt => opt.MapFrom(src => src.Session))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.Number))
                .ForMember(dest => dest.SeatExtraPrice, opt => opt.MapFrom(src => src.Seat.ExtraPrice))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<ReservationDTO, Reservation>(); // Reverse
        }
    }
}
