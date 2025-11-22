
using System.Text.Json;
using AiManagementApp.Models;
using AiManagementApp.Models.DTOs;
using Google.GenAI;

namespace AiManagementApp.Infrastructure.Services;

public class GeminiAiService : IAiService
{
    private readonly Client _client;

    // Atrela a API Key gerada ao modelo da AI, criando um novo cliente para ser utilizado
    public GeminiAiService(IConfiguration config)
    {
        var apiKey = config["GeminiAPIKey"]
            ?? throw new InvalidOperationException("GeminiAPIKey não configurada");
        
        Environment.SetEnvironmentVariable("GOOGLE_API_KEY", apiKey);

        _client = new Client();
    }
    
    // Cria o método para gerar a resposta da IA,
    // incluindo o prompt a ser solicitado e especificaçao de qual versao do Gemini vai ser utilizada
    public async Task<AIResponse> GerarRecomendacaoAsync(string resumo, AiLog.NivelRisco nivel)
    {
        var nivelDescricao = nivel switch
        {
            AiLog.NivelRisco.Leve      => "leve",
            AiLog.NivelRisco.Moderado  => "moderado",
            AiLog.NivelRisco.Grave      => "grave",
            _                          => "desconhecido"
            };

        var prompt =
            $@"Usuário relatou: ""{resumo}""
            Nível emocional: {nivelDescricao}.

            Gere uma recomendação acolhedora e prática com base no humor do usuário.

            IMPORTANTE! RETORNE APENAS UM JSON VÁLIDO COM ESSA ESTRUTURA:

            {{
              ""mensagem"": ""frase empática e acolhedora com 3 a 5 frases"",
              ""orientacao"": ""ação ou sugestão prática e simples"",
              ""recursosSugeridos"": ""opcionais, como grounding, conversar com alguém ou exercicios de relaxamento""
            }}

            ⚠️ INSTRUÇÕES OBRIGATÓRIAS:
            - Retorne APENAS um JSON válido.
            - O JSON deve começar com {{ e terminar com }}.
            - Não escreva texto antes ou depois do JSON.
            - Não use markdown (sem ```).
            - Não escreva explicações.
            - Não mencione IA ou termos médicos.
            ";

        if (nivel == AiLog.NivelRisco.Grave)
        {
            prompt += "\nTom da resposta: mais calor humano, empatia intensa, sensação de 'estou aqui contigo'.";
        } else if (nivel == AiLog.NivelRisco.Leve)
        {
            prompt += "\nTom da resposta: leve, animador, motivacional.";
        } else if (nivel == AiLog.NivelRisco.Moderado)
        {
            prompt += "\nTom da resposta: reflexivo e acolhedor.";
        }
        
        var response = await _client.Models.GenerateContentAsync(
            model: "gemini-2.5-flash",
            contents: prompt
            );

        var texto = response.Candidates.FirstOrDefault()?
            .Content.Parts.FirstOrDefault()?
            .Text ?? "";

        AIResponse? resposta;

        try
        {
            resposta = JsonSerializer.Deserialize<AIResponse>(texto);
        }
        catch
        {
            resposta = null;
        }
        
        if (resposta is null)
        {
            resposta = new AIResponse(
                mensagem: "Estou com dificuldades para responder agora, mas que tal respirar fundo e tomar um copo d'água? Cuidar de você continua sendo importante, mesmo nos momentos de pausa.",
                orientacao: "Faça três respirações lentas e conscientes. Se estiver difícil enfrentar sozinho(a), conversar com alguém de confiança pode ajudar. Em momentos muito difíceis, você também pode ligar 188 (CVV), onde alguém está disponível para escutar com respeito e sigilo.",
                recursosSugeridos: "Hidratação, pausa breve, alongamento leve"
                );
        }

        return resposta;
    }
}