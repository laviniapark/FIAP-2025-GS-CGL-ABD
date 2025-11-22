using AiManagementApp.Infrastructure;
using AiManagementApp.Infrastructure.Services;
using AiManagementApp.Models;
using AiManagementApp.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AiManagementApp.Endpoints ;

    public static class GeminiAiEndpoints
    {
        public static RouteGroupBuilder MapGeminiAiEndpoints(this RouteGroupBuilder builder, string version)
        {
            var group = builder.MapGroup("ai")
                .WithTags("Gemini AI Endpoints");
            
            // Metodo POST que recebe os dados de Java e gera a recomencadacao da IA
            // Depois registra o historico no banco, independente de sucesso ou erro
            group.MapPost("/solicitar", async (IAiService ai, AiManagementAppDb db, [FromBody] AiLogRequestJava req, ILogger<AiLog> logger) =>
            {
                logger.LogInformation("Requisição recebida! Resumo: {ResumoRecebido} | Nível: {Nivel} | Prioridade: {Prioridade}", req.ResumoRecebido, req.Nivel, req.Prioridade);

                AIResponse? respostaIA = null;
                bool sucesso = false;

                try
                {
                    respostaIA = await ai.GerarRecomendacaoAsync(req.ResumoRecebido, req.Nivel);
                    sucesso = true;
                }
                catch (Exception ex)
                {
                    logger.LogError("Erro ao gerar recomendação da IA");
                    respostaIA = new AIResponse(
                        mensagem: "Estou com dificuldades para responder agora, mas que tal respirar fundo e tomar um copo d'água? Cuidar de você continua sendo importante, mesmo nos momentos de pausa.",
                        orientacao: "Faça três respirações lentas e conscientes. Se estiver difícil enfrentar sozinho(a), conversar com alguém de confiança pode ajudar.",
                        recursosSugeridos: "Pausa breve, grounding, hidratação"
                        );
                }

                var log = new AiLog
                {
                    Id = Guid.NewGuid(),
                    DHRequisicao = DateTime.Now,
                    ResumoRecebido = req.ResumoRecebido,
                    RecomendacaoGerada = respostaIA.mensagem,
                    Nivel = req.Nivel,
                    SucessoEnvio = sucesso
                };
                
                db.AiLogs.Add(log);
                await db.SaveChangesAsync();
                
                logger.LogInformation("Log salvo com sucesso! ID: {Id}", log.Id);
                
                return Results.Ok(new
                {
                    mensagem = respostaIA.mensagem, 
                    orientacao = respostaIA.orientacao,
                    recursosSugeridos = respostaIA.recursosSugeridos, 
                    sucessoProcessamento = log.SucessoEnvio
                });
            }).WithName("PostAi")
                .WithSummary("Recebe os dados de Java e envia pra IA")
                .WithDescription("Esse método irá receber os dados de Java, e irão enviá-los para a IA gerar a resposta. " +
                                 "Independente se dê certo ou não, ele irá salvar os dados no banco, indicando em Sucesso se deu certo ou não")
                .Produces(StatusCodes.Status200OK);

            // Método GET para teste local da integracao com a IA usando dados mockados,
            // também salvando o resultado no banco para manter histórico
            group.MapGet("/teste", async (IAiService ai, AiManagementAppDb db, ILogger<AiLog> logger) =>
            {
                AIResponse? respostaIA = null;
                bool sucesso = false;

                try
                {
                    respostaIA = await ai.GerarRecomendacaoAsync(
                        "Estou meio cansada hoje",
                        AiLog.NivelRisco.Leve
                        );

                    sucesso = true;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Erro ao testar IA");

                    respostaIA = new AIResponse(
                        mensagem: "Não consegui testar agora, mas tente respirar fundo e fazer uma pausa leve.",
                        orientacao: "Três respirações lentas podem ajudar a aliviar o cansaço.",
                        recursosSugeridos: "Pausa breve, hidratação"
                        );
                }

                var log = new AiLog
                {
                    Id = Guid.NewGuid(),
                    DHRequisicao = DateTime.Now,
                    ResumoRecebido = "Teste automático da IA",
                    RecomendacaoGerada = respostaIA.mensagem,
                    Nivel = AiLog.NivelRisco.Leve,
                    SucessoEnvio = sucesso
                };

                db.AiLogs.Add(log);
                await db.SaveChangesAsync();

                logger.LogInformation("Log do teste salvo com sucesso no banco! ID = {Id}", log.Id);
                
                return Results.Ok(new
                {
                    mensagem = respostaIA.mensagem, 
                    orientacao = respostaIA.orientacao,
                    recursosSugeridos = respostaIA.recursosSugeridos, 
                    sucessoProcessamento = log.SucessoEnvio
                });
            })
                .WithName("GetAi")
                .WithSummary("Método para teste local da IA")
                .WithDescription("Utiliza dados mockados para testar a responsividade da IA e registra um log de teste no banco")
                .Produces(StatusCodes.Status200OK);



            return group;
        }
    }