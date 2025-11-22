using System.Text.Json.Serialization;

namespace AiManagementApp.Models.DTOs;

public record AiLogRequestJava(
   [property: JsonPropertyName("ResumoRecebido")]
    string ResumoRecebido,
    [property: JsonPropertyName("Nivel")]
    AiLog.NivelRisco Nivel,
    [property: JsonPropertyName("Prioridade")]
    bool Prioridade
    );

public record AiLogRequest(
    DateTime DHRequisicao,
    string ResumoRecebido,
    string RecomendacaoGerada,
    AiLog.NivelRisco Nivel,
    bool SucessoEnvio
    );

public record AiLogResponse(
    Guid Id,
    DateTime DHRequisicao,
    string ResumoRecebido,
    string RecomendacaoGerada,
    AiLog.NivelRisco Nivel,
    bool SucessoEnvio
    );

public record AiLogResponseHO(
    Guid Id,
    DateTime DHRequisicao,
    string ResumoRecebido,
    string RecomendacaoGerada,
    AiLog.NivelRisco Nivel,
    bool SucessoEnvio,
    List<LinkDTO> Links
    );