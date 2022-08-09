using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UserApi.Data;
using UserApi.Models.Web;

namespace UserApi.Controllers
{
    /// <summary>
    /// Appels générique au site web
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WebController : ControllerBase
    {
        private readonly UserApiContext _context;

        public WebController(UserApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get les images pour la carousel Home Page
        /// </summary>
        /// <returns>Liste d'images</returns>
        [HttpGet]
        [Route("CarouselImages")]
        public async Task<ActionResult<List<CarouselDTO>>> GetCarouselImages()
        {
            List<CarouselDTO> srcList = new List<CarouselDTO>
            {
                new CarouselDTO{src = 
                "https://cdn.discordapp.com/attachments/729809533756506213/984520139494015016/C0411CFB-ACBC-4268-B71D-309DA251751C.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520138906804224/C58BA225-A3E9-4A4C-B350-F13E43A61795.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520138348965918/B953CF99-1006-48EA-AB70-CDAFE970EC32.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520137984069703/A211C440-2ACF-47F3-B490-2E7DDBEE68A5.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520137614975066/A6CBAF98-8568-40B9-854B-F0E75BC0DD7F.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520137237471323/87013B86-9C47-490D-B33B-19480650DDC7.png"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520136708984852/93EEE713-9624-4433-AB58-A97A5750F5D4.png"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520135912083526/53E1FF0E-6BF2-4261-8CE5-4C11471CCE61.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520135094202428/3D3A9B91-3418-4970-8CB3-DA9A7A7F683C.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520106233196544/1DE3F263-49A4-47B5-B6CD-6114FDA7A52E.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520126974013440/FFB79AC5-B670-48DC-A804-140A17ABA7DE.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520126554578944/F169BA29-8921-4B5B-B433-BB29C72C1093.png"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520125673783316/EB2C449F-CF49-434F-A195-7036068C2724.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520125124313128/DC68ED2D-A568-4EB0-A1DD-5FD71CA24781.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520124096716810/D6AB20B7-B709-42D6-B4D4-A0115525CF34.jpg"
                },new CarouselDTO{src =
                "https://cdn.discordapp.com/attachments/729809533756506213/984520122041503834/C51761B9-5597-494E-9CB3-FF8634FD72A5.jpg"
                }
            };

            return srcList;
        }

    }
}
