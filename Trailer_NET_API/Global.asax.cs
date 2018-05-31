using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Trailer_NET_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Filters.Add(new ValidateViewModelAttribute());

            string youtubeEmbed = System.Configuration.ConfigurationManager.AppSettings["youtubeEmbed"];
            string youtubeImg = System.Configuration.ConfigurationManager.AppSettings["youtubeImg"];

            AutoMapper.Mapper.Initialize(x =>
            {
                x.CreateMap<Models.Movie, Trailer_NET_Library.Entities.Movie>()
                .ForSourceMember(w => w.Image_Url, c => c.Ignore())
                .ForMember(w => w.Youtube_Video_Id, c => c.MapFrom(y => y.Trailer_Url));

                x.CreateMap<Trailer_NET_Library.Entities.Movie, Models.Movie>()
                .ForMember(w => w.Trailer_Url, c => c.MapFrom(y => string.Format(youtubeEmbed, y.Youtube_Video_Id)))
                .ForMember(w => w.Image_Url, c => c.MapFrom(y => string.Format(youtubeImg, y.Youtube_Video_Id)));
            });
        }
    }
}
