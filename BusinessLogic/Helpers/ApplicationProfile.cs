using BusinessLogic.DTOs;
using DataAccess.Models;

namespace BusinessLogic.Helpers
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            // Маппинг для Genre и GenreDTO
            CreateMap<Genre, GenreDTO>().ReverseMap();

            // Маппинг для MovieStatus и MovieStatusDTO
            CreateMap<MovieStatus, MovieStatusDTO>().ReverseMap();

            // Маппинг для Movie и MovieDTO
            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<MovieDTO, Movie>();

            // Маппинг для MoviePrice и MoviePriceDTO
            CreateMap<MoviePrice, MoviePriceDTO>()
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie))
                .ForMember(dest => dest.Sessions, opt => opt.MapFrom(src => src.Sessions));
            CreateMap<MoviePriceDTO, MoviePrice>();

            // Маппинг для Seat и SeatDTO
            CreateMap<Seat, SeatDTO>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name));
            CreateMap<SeatDTO, Seat>();

            // Маппинг для Room и RoomDTO
            CreateMap<Room, RoomDTO>().ReverseMap();

            // Маппинг для Session и SessionDTO
            CreateMap<Session, SessionDTO>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MoviePrice.Price))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.MoviePrice.Movie.Title));
            CreateMap<SessionDTO, Session>();

            // Маппинг для ReservationStatus и ReservationStatusDTO
            CreateMap<ReservationStatus, ReservationStatusDTO>().ReverseMap();

            // Маппинг для UserStatus и UserStatusDTO
            CreateMap<UserStatus, UserStatusDTO>().ReverseMap();

            // Маппинг для User и UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<UserDTO, User>();

            // Маппинг для Reservation и ReservationDTO
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Session.Date))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Session.Time))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Session.MoviePrice.Movie.Title))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.Number))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Seat.Room.Name))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<ReservationDTO, Reservation>();
        }
    }
}
