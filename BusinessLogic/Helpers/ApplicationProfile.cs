using BusinessLogic.DTOs;
using DataAccess.Models;

namespace BusinessLogic.Helpers
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            // Мапінг для Genre и GenreDTO
            CreateMap<Genre, GenreDTO>().ReverseMap();

            // Мапінг для MovieStatus и MovieStatusDTO
            CreateMap<MovieStatus, MovieStatusDTO>().ReverseMap();

            // Мапінг для Movie и MovieDTO
            CreateMap<Movie, MovieDTO>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<MovieDTO, Movie>();

            CreateMap<MoviePrice, MoviePriceDTO>()
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(dest => dest.MoviePriceName, opt => opt.MapFrom(src => src.Movie.Title + " – " + src.Price));
            CreateMap<MoviePriceDTO, MoviePrice>(); // Reverse

            CreateMap<Seat, SeatDTO>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.SeatName, opt => opt.MapFrom(src => src.Room.Name + " – " + src.Number));
            CreateMap<SeatDTO, Seat>(); // Reverse

            // Мапінг для Room и RoomDTO
            CreateMap<Room, RoomDTO>().ReverseMap();

            // Мапінг для Session и SessionDTO
            CreateMap<Session, SessionDTO>()
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.MoviePrice.Price))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MoviePrice.Movie.Id));
            CreateMap<SessionDTO, Session>(); // Reverse

            // Мапінг для ReservationStatus и ReservationStatusDTO
            CreateMap<ReservationStatus, ReservationStatusDTO>().ReverseMap();

            // Мапінг для UserStatus и UserStatusDTO
            CreateMap<UserStatus, UserStatusDTO>().ReverseMap();

            // Мапінг для User и UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<UserDTO, User>();

            // Мапінг для Reservation и ReservationDTO
            CreateMap<Reservation, ReservationDTO>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Session, opt => opt.MapFrom(src => src.Session))
                .ForMember(dest => dest.SeatNumber, opt => opt.MapFrom(src => src.Seat.Number))
                .ForMember(dest => dest.SeatExtraPrice, opt => opt.MapFrom(src => src.Seat.ExtraPrice))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<ReservationDTO, Reservation>();
        }
    }
}
