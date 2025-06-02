using AutoMapper;
using KampüsSepeti.Application.DTOs;
using KampüsSepeti.Application.Features.Commands.LocationCommands;
using KampüsSepeti.Application.Features.Commands.RegisterCommands;
using KampüsSepeti.Application.Features.Results.AnnouncementResults;
using KampüsSepeti.Application.Features.Results.CategoryResults;
using KampüsSepeti.Application.Features.Results.CommentResults;
using KampüsSepeti.Application.Features.Results.FavoriteResults;
using KampüsSepeti.Application.Features.Results.LocationResults;
using KampüsSepeti.Application.Features.Results.PostResults;
using KampüsSepeti.Application.Features.Results.UniversityResults;
using KampüsSepeti.Application.Features.Results.UserResults;
using KampüsSepeti.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();

            CreateMap<Location, LocationDto>()
                .ReverseMap();
           

            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<Location, GetAllLocationQueryResult>();

            CreateMap<Location, GetLocationByIdQueryResult>();

            CreateMap<Location, UpdateLocationCommand>();

            CreateMap<Status, StatusDto>()
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<Post, GetPostByLocationIdQueryResult>()
               .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
               .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
               .ReverseMap();

            CreateMap<RegisterCommand, User>()
                .ReverseMap();

            CreateMap<GetAllUniversityQueryResult, University>()
                .ReverseMap();

            CreateMap<GetAllCategoryQueryResult, Category>()
               .ReverseMap();

            CreateMap<GetPostByIdQueryResult, Post>()
              .ReverseMap();

            CreateMap<GetAllPostByUserIdQueryResult, Post>()
             .ReverseMap();

            CreateMap<GetFavoritePostsByUserIdQueryResult, Post>()
             .ReverseMap();

            CreateMap<Announcement, GetAllAnnouncementQueryResult>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
             .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))
             .ForMember(dest => dest.commentCount, opt => opt.MapFrom(src => src.Comments.Count))
             .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.User.Image));

            CreateMap<Comment, GetCommentsByAnnouncementIdQueryResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName));


            CreateMap<Announcement, GetAnnouncementByIdQueryResult>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
             .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name))
             .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
             .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.User.Image));

            CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.User.Image));


            CreateMap<Comment, GetCommentByIdQueryResult>()
               .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.User.FullName));

            CreateMap<Comment, GetLastCommentQueryResult>()
               .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.User.FullName))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));


            CreateMap<Announcement, GetAnnouncementsByUserIdQueryResult>()
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.Name));

            CreateMap<GetAllUsersQueryResult, User>()
            .ReverseMap();

            CreateMap<Post, GetAllPostQueryResult>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));


        }
    }
}

