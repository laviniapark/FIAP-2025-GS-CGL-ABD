using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AiManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class InitalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AiLogs",
                columns: table => new
                {
                    log_id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    data_hora_requisicao = table.Column<DateTime>(type: "DATE", nullable: false),
                    resumo_recebido = table.Column<string>(type: "VARCHAR2(4000)", nullable: false),
                    recomendacao_gerada = table.Column<string>(type: "VARCHAR2(4000)", nullable: false),
                    nivel = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    sucesso_envio = table.Column<string>(type: "CHAR(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiLogs", x => x.log_id);
                });

            migrationBuilder.InsertData(
                table: "AiLogs",
                columns: new[] { "log_id", "data_hora_requisicao", "nivel", "recomendacao_gerada", "resumo_recebido", "sucesso_envio" },
                values: new object[,]
                {
                    { new Guid("2c5e62e7-0265-4a12-a184-55c5c8eb44c7"), new DateTime(2025, 11, 10, 11, 46, 0, 0, DateTimeKind.Unspecified), 0, "É normal ter dias em que a motivação demora a aparecer. Que tal começar com uma tarefa bem simples? Cumprir um pequeno passo pode ajudar a criar um ritmo melhor para o restante do dia.", "Usuário relatou sensação de desmotivação pela manhã.", "Y" },
                    { new Guid("3e1acf1d-7827-4c4c-b7c7-bc1bb909a2e9"), new DateTime(2025, 11, 9, 11, 23, 0, 0, DateTimeKind.Unspecified), 1, "É totalmente compreensível se sentir irritado em dias mais tensos. Se puder, tire um momento para respirar profundamente e se afastar um pouco da situação. Às vezes, alguns segundos de calma já ajudam a clarear a cabeça.", "Usuário relatou irritabilidade com colegas.", "Y" },
                    { new Guid("a3bdc1f2-8c32-4a1a-9e71-8f2f3d16a101"), new DateTime(2025, 11, 7, 10, 15, 0, 0, DateTimeKind.Unspecified), 1, "Percebo que você está carregando muita coisa ao mesmo tempo. Experimente fazer uma pausa curta, respirar fundo por alguns instantes e organizar suas prioridades com calma. Pequenos intervalos podem aliviar bastante a pressão.", "Usuário relatou excesso de carga emocional no trabalho.", "Y" },
                    { new Guid("d2c17420-9f84-4ee4-8c20-102875f79d0b"), new DateTime(2025, 11, 11, 12, 15, 0, 0, DateTimeKind.Unspecified), 1, "Reuniões intensas podem realmente deixar o corpo e a mente sobrecarregados. Tente alongar os ombros e o pescoço por alguns segundos e respirar lentamente. Isso pode ajudar a liberar parte dessa tensão.", "Usuário relatou tensão acumulada após uma reunião difícil.", "Y" },
                    { new Guid("e4a7f0fa-3e44-4fd1-a92d-cdc7284a88bb"), new DateTime(2025, 11, 12, 12, 30, 0, 0, DateTimeKind.Unspecified), 0, "Tudo bem ter uma manhã mais devagar. Você pode tentar dividir suas próximas tarefas em passos menores — isso ajuda a reconstruir o ritmo sem se sentir pressionado. Vá no seu tempo.", "Usuário relatou desmotivação leve após manhã improdutiva.", "Y" },
                    { new Guid("f41e7692-57d7-4384-9bc8-1165c6d3f0e2"), new DateTime(2025, 11, 8, 10, 32, 0, 0, DateTimeKind.Unspecified), 0, "Seu corpo pode estar pedindo um descanso mais estruturado. Tente evitar telas alguns minutos antes de dormir e procure desacelerar o ritmo gradualmente. Pequenos ajustes na rotina noturna podem ajudar bastante.", "Usuário descreveu dificuldade para dormir.", "Y" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiLogs");
        }
    }
}
