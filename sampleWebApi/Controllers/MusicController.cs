using Microsoft.AspNetCore.Mvc;
using sampleWebApi.Model;
namespace sampleWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicController : ControllerBase
    {
        private readonly Music music = new Music
        {
            musics = new List<Track>
            {
                new Track
                {
                    id = 1,
                    title = "Backburner",
                    duration = "3:57",
                    releaseDate = "2020",
                    artist = "Niki"
                },
                new Track
                {
                    id=2,
                    title= "Facebook Friends",
                    duration = "3:58",
                    releaseDate = "2020",
                    artist = "Niki"
                }
            }
        };
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(music);
        }
        [HttpPost]
        public async Task<IActionResult> Post(int id)
        {
           if (id != 0)
            {
                var tracks = music.musics.FirstOrDefault(u => u.id == id);
                if (tracks != null)
                {
                    return new JsonResult(tracks);
                }
                else
                {
                    return new JsonResult(new { Message = "Not Found" });
                }

            }
            else
            {
                return new JsonResult(new { Message = "Id not Exist" });
            }
        }
    }
}
