using Microsoft.EntityFrameworkCore.Migrations;
using BC = BCrypt.Net.BCrypt;

#nullable disable
namespace Consumer.Infrastructure.Migrations
{
    public partial class DatabaseSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserId", "Username" },
                values: new object[,]
                {
                    { 1L, BC.HashPassword("user123"), Guid.NewGuid(), "user01" },
                    { 2L, BC.HashPassword("user456"), Guid.NewGuid(), "user02" },
                    { 3L, BC.HashPassword("user789"), Guid.NewGuid(), "user03" }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "ClientName", "ClosedAt", "Number", "OpenedAt", "Quantity", "Status", "UserId" },
                values: new object[,]
                {
                    // Mesa em atendimento normal com pedidos completos
                    { 1L, "Eduardo Santos", null, 10, DateTime.Now.AddHours(-1), 4, 3, 1L},
                    
                    // Mesa aguardando pagamento (conta solicitada)
                    {2L, "Família Costa", null, 15, DateTime.Now.AddHours(-2), 5, 4, 2L},
                    
                    // Mesa recém aberta sem garçom e sem pedidos
                    { 3L, null, null, 8, DateTime.Now.AddMinutes(-5), 0, 1, null},
                    
                    // Mesa fechada (já paga)
                    { 4L, "João Silva", DateTime.Now.AddMinutes(-30), 5, DateTime.Now.AddHours(-3), 2, 0, 1L},
                    
                    // Mesa reservada para evento
                    { 5L, "Aniversário Laura", null, 20, DateTime.Now.AddHours(2), 15, 2, null},
                    
                    // Mesa em atendimento com pedidos parciais
                    {6L, "Grupo Amigos", null, 12, DateTime.Now.AddMinutes(-45), 6, 3, 3L},
                    
                    // Mesa em limpeza
                    {7L, null, null, 7, DateTime.Now, 0, 5, null},
                    
                    // Mesa fechada do dia anterior
                    {8L, "Pedro Almeida", DateTime.Now.AddDays(-1), 9, DateTime.Now.AddDays(-1).AddHours(-2), 3, 0, 2L},
                    
                    // Mesa em atendimento (happy hour)
                    {9L, "Turma Escritório", null, 14, DateTime.Now.AddMinutes(-90), 8, 3, 1L},
                    
                    // Mesa aguardando garçom após limpeza
                    {10L, null, null, 18, DateTime.Now.AddMinutes(-2), 0, 1, null},

                    // Mesa com conta dividida aguardando pagamento
                    {11L, "Grupo Faculdade", null, 25, DateTime.Now.AddHours(-1), 4, 4, 3L},

                    // Validação de Erros (Validator)
                    {12L, "Test Table", DateTime.Now, 99, DateTime.Now, 0, 1, 2L}
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name", "Observation", "Price", "Quantity", "TableId" },
                values: new object[,]
                {
                    // Mesa 1 - Completa
                    { 1L, "Filé à Parmegiana", "Sem cebola", 89.90m, 2, 1L },
                    { 2L, "Cerveja Long Neck", null, 18.90m, 4, 1L },
                    { 3L, "Porção Batatas", "Extra crocante", 35.90m, 2, 1L },
                    
                    // Mesa 2 - Aguardando pagamento
                    { 4L, "Pizzas Variadas", "Metade calabresa/margherita", 85.90m, 2, 2L },
                    { 5L, "Refrigerante 2L", null, 15.90m, 2, 2L },
                    { 6L, "Salada Mista", "Molho à parte", 42.90m, 1, 2L },
                    
                    // Mesa 6 - Pedidos parciais
                    { 7L, "Pastéis Sortidos", "2 queijo, 2 carne", 45.90m, 1, 6L },
                    { 8L, "Chopp 500ml", null, 15.90m, 6, 6L },
                    
                    // Mesa 9 - Happy hour
                    { 9L, "Tábua de Frios", null, 98.90m, 2, 9L },
                    { 10L, "Caipirinha", "Limão", 24.90m, 8, 9L },
                    
                    // Mesa 11 - Conta dividida
                    { 11L, "Hambúrguer Artesanal", "Ponto bem passado", 48.90m, 4, 11L },
                    { 12L, "Milk Shake", "2 chocolate, 2 morango", 22.90m, 4, 11L },

                    //Erro Validação
                    { 13L, "Item Inválido", "Coca-Cola", 0m, 0, 12L }
                });

            migrationBuilder.InsertData(
                table: "Additions",
                columns: new[] { "Id", "ItemId", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, 1L, "Queijo Extra", 8.90m },
                    { 2L, 4L, "Borda Recheada", 7.90m },
                    { 3L, 7L, "Catupiry", 6.90m },
                    { 4L, 11L, "Bacon Extra", 8.90m },
                    { 5L, 12L, "Calda Extra", 3.90m },
                    { 6L, 1L, "Batata frita", 12.90m }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "ItemId", "Name" },
                values: new object[,]
                {
                    { 1L, 1L, "Bem Passado" },
                    { 2L, 2L, "Extra Gelada" },
                    { 3L, 4L, "Massa Fina" },
                    { 4L, 8L, "Sem Colarinho" },
                    { 5L, 10L, "Pouco Açúcar" },
                    { 6L, 4L, "Sem milho" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Options", keyColumn: "Id", keyValues: new object[] { 1L, 2L, 3L, 4L, 5L, 6L });
            migrationBuilder.DeleteData(table: "Additions", keyColumn: "Id", keyValues: new object[] { 1L, 2L, 3L, 4L, 5L, 6L });
            migrationBuilder.DeleteData(table: "Items", keyColumn: "Id", keyValues: new object[] { 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L, 9L, 10L, 11L, 12L, 13L });
            migrationBuilder.DeleteData(table: "Tables", keyColumn: "Id", keyValues: new object[] { 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L, 9L, 10L, 11L, 12L });
            migrationBuilder.DeleteData(table: "Users", keyColumn: "Id", keyValues: new object[] { 1L, 2L, 3L });
        }
    }
}