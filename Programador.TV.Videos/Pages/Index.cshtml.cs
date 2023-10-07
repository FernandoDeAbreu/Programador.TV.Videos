using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Programador.TV.Videos.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(IFormFile videoFile)
        {
            if (videoFile == null || videoFile.Length <= 0)
            {
                return BadRequest("Nenhum arquivo de vídeo enviado.");
            }

            var videoInfo = new
            {
                Nome = videoFile.FileName,
                TamanhoEmBytes = videoFile.Length,
                TipoDeMídia = videoFile.ContentType,
                Extensao = Path.GetExtension(videoFile.FileName),
                DataDeUpload = DateTime.Now,
            };

           
            var json = JsonConvert.SerializeObject(videoInfo, Formatting.Indented);

            return Content(json, "application/json");
        }
    }
}