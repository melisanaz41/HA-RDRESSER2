using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public RecommendationController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("OpenAIAPI");
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
            // Kullanıcının girdisi ile OpenAI'ye prompt oluşturma
            var userPrompt = "Based on the user's preferences and hairstyle needs, provide hairstyle recommendations.";
            var apiResult = await CallOpenAIAPI(userPrompt);

            // Başarılı API yanıtı
            return Ok(new { Message = "Başarılı", Recommendations = apiResult });
        }
        catch (Exception ex)
        {
            // Hata detaylarını loglama
            Debug.WriteLine($"Hata: {ex.Message}");
            return StatusCode(500, new { Message = "Bir hata oluştu. Lütfen daha sonra tekrar deneyin.", Error = ex.Message });
        }
    }

    private async Task<List<string>> CallOpenAIAPI(string prompt)
    {
        var requestBody = new
        {
            model = "gpt-4",
            messages = new[]
            {
            new { role = "system", content = "You are a professional hairstylist assistant." },
            new { role = "user", content = prompt }
        },
            max_tokens = 100,
            temperature = 0.7
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            Debug.WriteLine("API isteği gönderiliyor...");
            Debug.WriteLine($"Request Body: {json}");

            var response = await _httpClient.PostAsync("chat/completions", content);

            Debug.WriteLine($"HTTP Durum Kodu: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"API Hata Detayı: {errorDetails}");
                throw new Exception($"API isteği başarısız oldu: {response.StatusCode} - {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"API Yanıtı: {responseContent}");

            var result = JsonSerializer.Deserialize<OpenAIResponse>(responseContent);
            return result?.Choices.Select(choice => choice.Message.Content).ToList() ?? new List<string>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"OpenAI API çağrısı sırasında hata oluştu: {ex.Message}");
            throw;
        }
    }


    // İstek model sınıfı
    public class HairStyleRequestModel
    {
        public string PhotoBase64 { get; set; }
    }

    // OpenAI API Yanıt Modeli
    public class OpenAIResponse
    {
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Content { get; set; }
    }
}
