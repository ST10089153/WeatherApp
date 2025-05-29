public class AccuWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "YOUR_ACCUWEATHER_KEY";

    public AccuWeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherData> GetWeatherAsync(string cityKey)
    {
        var response = await _httpClient.GetFromJsonAsync<List<dynamic>>(
            $"http://dataservice.accuweather.com/currentconditions/v1/{cityKey}?apikey={_apiKey}");
        var weather = response.First();
        return new WeatherData
        {
            WeatherText = weather.WeatherText,
            Temperature = weather.Temperature.Metric.Value
        };
    }
}