using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Models.DTO.GetDtos;
using Backend.Models.DTO.UpdateDtos;
using carwash.Models.Domain;
using carwash.Models.DTO;

namespace carwash.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AddUserDto,User>().ReverseMap();
            CreateMap<UpdateUserDto,User>().ReverseMap();
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<WasherDto,User>().ReverseMap();

            CreateMap<AddCarDto,Car>().ReverseMap();
            CreateMap<CarDto,Car>().ReverseMap();

            CreateMap<AddPackageDto,Package>().ReverseMap();
            CreateMap<PackageDto,Package>().ReverseMap();

            CreateMap<AddAddressDto , Address>().ReverseMap();
            CreateMap<AddressDto , Address>().ReverseMap();
            CreateMap<UpdateAddressDto , Address>().ReverseMap();

            CreateMap<AddPromoCodeDto , PromoCode>().ReverseMap();
            CreateMap<PromoCodeDto , PromoCode>().ReverseMap();

            CreateMap<AddWashRequestDto , WashRequest>().ReverseMap();
            CreateMap<WashRequestDto , WashRequest>().ReverseMap();

            CreateMap<AddRatingReviewDto , RatingReview>().ReverseMap();
            CreateMap<RatingReviewDto , RatingReview>().ReverseMap();

            CreateMap<AddPaymentDto , Payment>().ReverseMap();
            CreateMap<PaymentDto , Payment>().ReverseMap();
        }
    }
}