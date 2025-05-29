using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using System;

public class NewsApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "9eb193be05cd46b0b97b54998488b727";

    public NewsApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

 public async Task<List<NewsArticle>> GetHeadlinesAsync()
{
    var articles = new List<NewsArticle>();

    try
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"https://newsapi.org/v2/top-headlines?country=us&apiKey={_apiKey}");

        // Add User-Agent (required by NewsAPI.org)
        request.Headers.Add("User-Agent", "WeatherNewsApp/1.0");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode(); // throws if not 2xx

        var jsonString = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Raw NewsAPI response: " + jsonString);

        using var jsonDoc = JsonDocument.Parse(jsonString);
        var root = jsonDoc.RootElement;

        if (root.TryGetProperty("articles", out JsonElement articlesElement))
        {
            foreach (var item in articlesElement.EnumerateArray())
            {
                string title = item.GetProperty("title").GetString() ?? "No Title";
                string description = item.TryGetProperty("description", out var desc) && desc.ValueKind != JsonValueKind.Null
                    ? desc.GetString() ?? "No Description"
                    : "No Description";

                articles.Add(new NewsArticle
                {
                    Title = title,
                    Description = description
                });
            }
        }
    }
 catch (HttpRequestException ex)
{
    Console.WriteLine($"[NewsApiService] HTTP Error: {ex.Message}");

    if (ex is HttpRequestException httpEx)
    {
        Console.WriteLine("Check if you're missing a User-Agent header or if your API key is invalid.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"[NewsApiService] General Error: {ex.Message}");
}

    return articles;
}


}