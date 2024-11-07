﻿using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Mapper
{
    public class ImageItemProfile:Profile
    {
        public ImageItemProfile() { 
          CreateMap<ImageItemDTO, ImageItem>();
          CreateMap<ImageItem, ImageItemDTO>();
        }
    }
}