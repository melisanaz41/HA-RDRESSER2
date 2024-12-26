using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;

[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public RecommendationController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost("GetHairStyleRecommendation")]
    public async Task<IActionResult> GetHairStyleRecommendation([FromBody] HairStyleRequestModel request)
    {
        if (string.IsNullOrEmpty(request.PhotoBase64))
        {
            return BadRequest(new { Message = "Fotoğraf verisi boş." });
        }

        try
        {
            // Fotoğrafı Base64'ten geçici bir dosyaya dönüştürme
            var photoBytes = Convert.FromBase64String(request.PhotoBase64);
            var tempPhotoPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jpg");
            await System.IO.File.WriteAllBytesAsync(tempPhotoPath, photoBytes);

            // Dış API'ye istek gönder
            var apiResult = await CallExternalAPI(photoBytes);

            // Geçici dosyayı sil
            System.IO.File.Delete(tempPhotoPath);

            return Ok(new { Message = "Başarılı", Recommendations = apiResult });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Hata oluştu.", Error = ex.Message });
        }
    }

    private async Task<List<string>> CallExternalAPI(byte[] photoBytes)
    {
        // Dış API URL'si
        var apiUrl = "https://api-inference.huggingface.co/models/google/vit-base-patch16-224"; // Değiştirin

        // İstek verilerini hazırlayın
        var content = new MultipartFormDataContent();
        var byteArrayContent = new ByteArrayContent(photoBytes);
        byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
        content.Add(byteArrayContent, "file", "photo.jpg");

        // API isteği gönder
        var response = await _httpClient.PostAsync(apiUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API isteği başarısız oldu: {response.StatusCode} - {response.ReasonPhrase}");
        }

        // Yanıtı işle
        var apiResponse = await response.Content.ReadAsStringAsync();
        // Örnek: API yanıtı JSON formatında dönerse deserialize edin
        var recommendations = System.Text.Json.JsonSerializer.Deserialize<List<string>>(apiResponse);

        return recommendations ?? new List<string>();
    }
}

public class HairStyleRequestModel
{
    public string PhotoBase64 { get; set; }
}
