using Microsoft.EntityFrameworkCore.Migrations;
using BC = BCrypt.Net.BCrypt;
namespace Consumer.Infrastructure.Data.Seeds;
public partial class SeedInitialData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Usuários (senhas hasheadas com BCrypt)
        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "UserId", "Username", "Password" },
            values: new object[,]
            {
                { 1L, Guid.NewGuid(), "user01@email.com", BC.HashPassword("123456") },
                { 2L, Guid.NewGuid(), "user02@email.com", BC.HashPassword("123456") }
            });

        // Mesas
        migrationBuilder.InsertData(
            table: "Tables",
            columns: new[] { "Id", "Number", "UserId", "Status", "OpenedAt", "ClosedAt" },
            values: new object[,]
            {
                // Mesas do Usuário 1 (3 abertas, 2 fechadas)
                { 1L, 1, 1L, 1, DateTime.Now.AddHours(-2), string.Empty },
                { 2L, 2, 1L, 1, DateTime.Now.AddHours(-1), string.Empty },
                { 3L, 3, 1L, 1, DateTime.Now.AddMinutes(-30), string.Empty },
                { 4L, 4, 1L, 2, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1).AddHours(2) },
                { 5L, 5, 1L, 2, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1).AddHours(3) },
                
                // Mesas do Usuário 2
                { 6L, 6, 2L, 1, DateTime.Now.AddHours(-3), string.Empty },
                { 7L, 7, 2L, 1, DateTime.Now.AddHours(-2), string.Empty }
            });

        // OrderDetails para mesa 1
        migrationBuilder.InsertData(
            table: "OrderDetails",
            columns: new[] { "Id", "TableId", "ClientName", "Quantity", "TotalTime", "Total", "ServiceFee", "TotalPayment" },
            values: new object[] { 1L, 1L, "José Silva", 2, DateTime.Now.AddHours(-2), 89.80M, 8.98M, 98.78M });

        // Itens do pedido
        migrationBuilder.InsertData(
            table: "Item",
            columns: new[] { "Id", "OrderDetailId", "Name", "Quantity", "Price", "Observation" },
            values: new object[,]
            {
                { 1L, 1L, "Pizza Margherita", 1, 45.90M, "Sem cebola" },
                { 2L, 1L, "Refrigerante Cola", 2, 6.90M, string.Empty }
            });

        // Adicionais
        migrationBuilder.InsertData(
            table: "Addition",
            columns: new[] { "Id", "ItemId", "Name", "Price" },
            values: new object[] { 1L, 1L, "Bacon Extra", 5.90M });

        // Opções
        migrationBuilder.InsertData(
            table: "Option",
            columns: new[] { "Id", "ItemId", "Name" },
            values: new object[] { 1L, 1L, "Borda Recheada" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData("Option", "Id", 1L);
        migrationBuilder.DeleteData("Addition", "Id", 1L);
        migrationBuilder.DeleteData("Item", "Id", new[] { 1L, 2L });
        migrationBuilder.DeleteData("OrderDetails", "Id", 1L);
        migrationBuilder.DeleteData("Tables", "Id", new[] { 1L, 2L, 3L, 4L, 5L, 6L, 7L });
        migrationBuilder.DeleteData("Users", "Id", new[] { 1L, 2L });
    }
}