using Microsoft.EntityFrameworkCore.Migrations;
using BC = BCrypt.Net.BCrypt;
#nullable disable

namespace Consumer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Usuários / Funcionarios 
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "UserId", "Username", "Password" },
                values: new object[,]
                {
                   { 1L, Guid.NewGuid(), "user01@email.com", BC.HashPassword("123456") },
                   { 2L, Guid.NewGuid(), "user02@email.com", BC.HashPassword("123456") },
                   { 3L, Guid.NewGuid(), "user03@email.com", BC.HashPassword("123456") }
                });

            // Mesas / Comandas
            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Number", "UserId", "Status", "OpenedAt", "ClosedAt" },
                values: new object[,]
                {
                   // USER 01 (5 mesas - 3 abertas, 2 fechadas)
                   { 1L, 1, 1L, 1, new DateTime(2025, 1, 24, 9, 0, 0), null },
                   { 2L, 2, 1L, 1, new DateTime(2025, 1, 24, 10, 0, 0), null },
                   { 3L, 3, 1L, 1, new DateTime(2025, 1, 24, 11, 0, 0), null },
                   { 4L, 4, 1L, 2, new DateTime(2025, 1, 23, 10, 0, 0), new DateTime(2025, 1, 23, 12, 0, 0) },
                   { 5L, 5, 1L, 2, new DateTime(2025, 1, 23, 11, 0, 0), new DateTime(2025, 1, 23, 13, 0, 0) },

                   // USER 02 (3 mesas - 1 aberta, 2 fechadas)
                   { 6L, 6, 2L, 1, new DateTime(2025, 1, 24, 9, 30, 0), null },
                   { 7L, 7, 2L, 2, new DateTime(2025, 1, 23, 15, 0, 0), new DateTime(2025, 1, 23, 17, 0, 0) },
                   { 8L, 8, 2L, 2, new DateTime(2025, 1, 23, 16, 0, 0), new DateTime(2025, 1, 23, 18, 0, 0) },

                   // USER 03 (2 mesas - todas abertas)
                   { 9L, 9, 3L, 1, new DateTime(2025, 1, 24, 10, 0, 0), null },
                   { 10L, 10, 3L, 1, new DateTime(2025, 1, 24, 10, 30, 0), null }
                });

            // OrderDetails
            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "Id", "TableId", "ClientName", "Quantity", "TotalTime", "Total", "ServiceFee", "TotalPayment" },
                values: new object[,]
                {
                   { 1L, 1L, "Lucas Lima", 2, new DateTime(2025, 1, 24, 9, 0, 0), 52.80M, 5.28M, 58.08M },
                   { 2L, 2L, "Maria Santos", 2, new DateTime(2025, 1, 24, 10, 0, 0), 98.80M, 9.88M, 108.68M },
                   { 3L, 3L, "Pedro Oliveira", 4, new DateTime(2025, 1, 24, 11, 0, 0), 245.60M, 24.56M, 270.16M },
                   { 4L, 6L, "Ana Costa", 2, new DateTime(2025, 1, 24, 9, 30, 0), 112.90M, 11.29M, 124.19M },
                   { 5L, 9L, "Carlos Souza", 3, new DateTime(2025, 1, 24, 10, 0, 0), 178.50M, 17.85M, 196.35M },
                   { 6L, 10L, "Lucia Ferreira", 2, new DateTime(2025, 1, 24, 10, 30, 0), 134.80M, 13.48M, 148.28M }
                });

            // Itens
            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "Id", "OrderDetailId", "Name", "Quantity", "Price", "Observation" },
                values: new object[,]
                {
                   { 1L, 1L, "Pizza Costela com Bacon", 1, 45.90M, null },
                   { 2L, 1L, "Refrigerante Cola", 2, 6.90M, null },
                   { 3L, 1L, "Lasanha", 1, 42.90M, "Extra queijo" },
                   { 4L, 2L, "Pizza Calabresa", 1, 49.90M, null },
                   { 5L, 2L, "Suco Natural", 2, 8.90M, "Sem açúcar" },
                   { 6L, 3L, "Pizza Frango Catupiry", 2, 52.90M, null },
                   { 7L, 3L, "Cerveja", 4, 12.90M, "Bem gelada" },
                   { 8L, 4L, "Feijoada", 2, 45.90M, null },
                   { 9L, 4L, "Caipirinha", 2, 18.90M, "Limão" },
                   { 10L, 5L, "Pizza Portuguesa", 1, 48.90M, "Sem ovo" },
                   { 11L, 5L, "Cerveja Premium", 3, 15.90M, null },
                   { 12L, 6L, "Pizza Especial", 1, 55.90M, null },
                   { 13L, 6L, "Vinho Tinto", 1, 89.90M, null }
                });

            // Adicionais
            migrationBuilder.InsertData(
                table: "Addition",
                columns: new[] { "Id", "ItemId", "Name", "Price" },
                values: new object[,]
                {
                   { 2L, 3L, "Queijo Extra", 4.90M },
                   { 3L, 4L, "Cebola Caramelizada", 3.90M },
                   { 4L, 6L, "Catupiry Extra", 5.90M },
                   { 5L, 8L, "Torresmo Extra", 7.90M },
                   { 6L, 10L, "Azeitona Extra", 3.90M },
                   { 7L, 12L, "Mix de Queijos", 8.90M }
                });

            // Opções
            migrationBuilder.InsertData(
                table: "Option",
                columns: new[] { "Id", "ItemId", "Name" },
                values: new object[,]
                {
                   { 2L, 4L, "Borda Recheada Cheddar" },
                   { 3L, 6L, "Borda Recheada Cream Cheese" },
                   { 4L, 10L, "Borda Recheada Chocolate" },
                   { 5L, 12L, "Borda Recheada Especial" },
                   { 6L, 2L, "Gelo Extra" },
                   { 7L, 5L, "Com Gengibre" },
                   { 8L, 9L, "Dose Extra" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Option", "Id", new[] { 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L });
            migrationBuilder.DeleteData("Addition", "Id", new[] { 1L, 2L, 3L, 4L, 5L, 6L, 7L });
            migrationBuilder.DeleteData("Item", "Id", new[] { 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L, 9L, 10L, 11L, 12L, 13L });
            migrationBuilder.DeleteData("OrderDetails", "Id", new[] { 1L, 2L, 3L, 4L, 5L, 6L });
            migrationBuilder.DeleteData("Tables", "Id", new[] { 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L, 9L, 10L });
            migrationBuilder.DeleteData("Users", "Id", new[] { 1L, 2L, 3L });
        }
    }
}
