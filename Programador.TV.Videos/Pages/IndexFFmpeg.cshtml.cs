using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xabe.FFmpeg;

namespace Programador.TV.Videos.Pages
{
    public class IndexFFmpegModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync(IFormFile videoFile)
        {
            if (videoFile != null && videoFile.Length > 0)
            {
                try
                {
                    var tempFilePath = Path.GetTempFileName();
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        await videoFile.CopyToAsync(stream);
                    }

                    // Use FFmpegCore to process the video and collect information
                    var videoInfo = FFmpeg.GetMediaInfo(tempFilePath);

                    // You can customize what information to collect and return in JSON format
                    var result = new
                    {
                        VideoInfo = videoInfo,
                        CustomInfo = "Additional information goes here"
                    };

                    return new JsonResult(result);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao processar o vídeo: {ex.Message}");
                }
            }

            return BadRequest("Nenhum arquivo de vídeo enviado.");
        }
    }
}