using System.Net.Http.Json;

namespace AiManagementApp.Tests ;

    [Collection("API Tests")]
    public class GeminiAiEndpointsTest
    {
        private readonly HttpClient _client;

        public GeminiAiEndpointsTest(CustomWebAppFactory factory)
        {
            _client = factory.CreateClient();
        }
        
        [Fact]
        public async Task GetAi_DeveRetornar200EOJsonEsperado()
        {
            var response = await _client.GetAsync("/api/v1/ai/teste");
            
            response.EnsureSuccessStatusCode();
    
            var json = await response.Content.ReadFromJsonAsync<TestResponse>();
    
            Assert.NotNull(json);
            Assert.True(json.sucessoProcessamento);
            Assert.False(string.IsNullOrWhiteSpace(json.mensagem));
            Assert.False(string.IsNullOrWhiteSpace(json.orientacao));
            Assert.False(string.IsNullOrWhiteSpace(json.recursosSugeridos));
        }
    }
    
    public class TestResponse
    {
        public string mensagem { get; set; }
        public string orientacao { get; set; }
        public string recursosSugeridos { get; set; }
        public bool sucessoProcessamento { get; set; }
    }
