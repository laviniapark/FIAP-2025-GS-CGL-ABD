namespace AiManagementApp.Models.DTOs;

public record AiLogRequestJava(
    string ResumoRecebido,
    AiLog.NivelRisco Nivel
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