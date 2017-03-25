using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apiest.Features.RequestBin.Infrastructure.Data.Models;
using AutoMapper;

namespace Apiest.Features.RequestBin.Infrastructure.Data.Mapping
{
    public class RequestBinMapping
    {
        public RequestBinMapping()
        {
            Mapper.Initialize(p =>
            {
               var map1 = p.CreateMap<Data.Models.WebRequestGroup, Core.Models.WebRequestGroup>();
                map1.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WebRequestGroupId));
                map1.ForMember(dest => dest.GroupUniqueId, opt => opt.MapFrom(src => src.GroupUniqueId));
                map1.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

                var map11 = p.CreateMap<Core.Models.WebRequestGroup, Data.Models.WebRequestGroup>();
                map11.ForMember(dest => dest.WebRequestGroupId, opt => opt.MapFrom(src => src.Id));
                map11.ForMember(dest => dest.GroupUniqueId, opt => opt.MapFrom(src => src.GroupUniqueId));
                map11.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

                var map2 = p.CreateMap<Data.Models.WebRequest, Core.Models.WebRequest>();
                map2.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.WebRequestId));
                map2.ForMember(dest => dest.WebRequestGroupId, opt => opt.MapFrom(src => src.WebRequestGroupId));
                map2.ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body));
                map2.ForMember(dest => dest.Header, opt => opt.MapFrom(src => src.Header));
                map2.ForMember(dest => dest.QueryString, opt => opt.MapFrom(src => src.QueryString));
                map2.ReverseMap();
                
                var map3 = p.CreateMap<Data.Models.WebResponse, WebResponse>();
                map3.ForMember(dest => dest.WebResponseId, opt => opt.MapFrom(src => src.WebResponseId));
                map3.ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body));
                map3.ReverseMap();
            });
            
           
        }
    }
}
