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

                    var videoInfo = FFmpeg.GetMediaInfo(tempFilePath);

                    var result = new
                    {
                        Nome = videoFile.FileName,
                        TamanhoEmBytes = videoFile.Length,
                        TipoDeMídia = videoFile.ContentType,
                        Extensao = Path.GetExtension(videoFile.FileName),
                        DataDeUpload = DateTime.Now,
                        VideoInfo = videoInfo
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