namespace AiManagementApp.Models.DTOs ;

    public record AIResponse
    (
        string mensagem,
        string orientacao,
        string recursosSugeridos
        );