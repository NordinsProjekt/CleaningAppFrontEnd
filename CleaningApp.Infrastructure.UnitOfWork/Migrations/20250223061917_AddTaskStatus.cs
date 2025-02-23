using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleaningApp.Infrastructure.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("16d603e9-023e-4ece-9641-68ed340fba68"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("28309c76-9ce7-4dd8-b1da-120c3e80be16"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("b6cc9e24-aad0-44dc-a4d0-a6c84975170e"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("d79adce3-b977-4b80-a9ed-9ef373b01a4b"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("ec2bcf26-6118-4839-8ff8-99cf35b303b9"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("065a20ba-6772-43f3-851b-848fc2ce39ee"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("619787c6-2057-429a-9429-ab91a9cb2283"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("6ffe1a46-3112-4245-959c-47cb51e50c2b"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("7147263a-f239-44eb-8a2a-235c3568dc39"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("a2faa15b-a898-4176-a62c-bc49a7042077"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("a510494b-5c12-47f6-a55e-bcd589dc8d66"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("b3292da5-ef07-4521-adc5-59f573b6c2b2"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("b7b5270c-0f8d-4ccd-ac39-017bde938cc4"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("f2a0c509-bc09-4c19-b1d8-e480163b0506"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("f7febf0e-ab94-4d71-91b3-f0ef302c49ae"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2744bda8-ca5b-4e08-abde-ae11562dded3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3431b0a7-afa6-45e2-aa8d-13064ef08f5f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("52961fac-5431-4845-9b79-0c7bf88bf506"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("04a3f41c-ef09-42ca-aa8a-33eb73d588a1"), "Sovrum" },
                    { new Guid("48cd1f9b-d5b0-463f-809f-d1bbea8ad32a"), "Vardagsrum" },
                    { new Guid("4f254314-1bc2-435d-9b29-624a89fee683"), "Kök" },
                    { new Guid("631082be-81d6-45ce-851f-a8dcf96593d9"), "Badrum uppe" },
                    { new Guid("dad70fd7-60e0-46af-acf4-d9eeeecdca67"), "Badrum nere" }
                });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("216a50f1-f5bf-4dbe-bda9-5497464a9393"), "Torkat av alla ytor" },
                    { new Guid("52a2c583-f5cd-4e08-a103-b2833059258f"), "Tömt diskmaskin" },
                    { new Guid("7c1c6388-81b3-44d8-9e8b-db4235dc9c9e"), "Tömt sopor" },
                    { new Guid("90b9d9e1-c695-4a9f-a2d8-905e301a8c57"), "Startat diskmaskin" },
                    { new Guid("987aec0a-f24c-4149-a2a4-8cecd90d3231"), "Bytt sängkläder" },
                    { new Guid("afdf8411-fa94-4b3b-b239-3e9ec397d454"), "Rengjort badrum" },
                    { new Guid("c155d51d-f6b6-40bd-bcbf-b1593a79a2e0"), "Tvättat golv" },
                    { new Guid("c2999c71-25b4-4fd5-9b47-11ad5c58d1cc"), "Dammsugit golv" },
                    { new Guid("c3f79b26-ff10-4807-8a41-81e2374e34b1"), "Tömt tvättmaskin" },
                    { new Guid("cfe17e50-caf4-4638-bcf7-3df365dd6396"), "Startat tvättmaskin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("27714a73-fe87-4515-aad1-f5f19aedaca2"), "Cecilia" },
                    { new Guid("95fca50d-c790-4f47-ac33-09b98f31c590"), "Markus" },
                    { new Guid("fb9d54be-8417-49d3-abe0-212f55fba6a5"), "Planerad" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("04a3f41c-ef09-42ca-aa8a-33eb73d588a1"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("48cd1f9b-d5b0-463f-809f-d1bbea8ad32a"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("4f254314-1bc2-435d-9b29-624a89fee683"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("631082be-81d6-45ce-851f-a8dcf96593d9"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("dad70fd7-60e0-46af-acf4-d9eeeecdca67"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("216a50f1-f5bf-4dbe-bda9-5497464a9393"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("52a2c583-f5cd-4e08-a103-b2833059258f"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("7c1c6388-81b3-44d8-9e8b-db4235dc9c9e"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("90b9d9e1-c695-4a9f-a2d8-905e301a8c57"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("987aec0a-f24c-4149-a2a4-8cecd90d3231"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("afdf8411-fa94-4b3b-b239-3e9ec397d454"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("c155d51d-f6b6-40bd-bcbf-b1593a79a2e0"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("c2999c71-25b4-4fd5-9b47-11ad5c58d1cc"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("c3f79b26-ff10-4807-8a41-81e2374e34b1"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("cfe17e50-caf4-4638-bcf7-3df365dd6396"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("27714a73-fe87-4515-aad1-f5f19aedaca2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("95fca50d-c790-4f47-ac33-09b98f31c590"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fb9d54be-8417-49d3-abe0-212f55fba6a5"));

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Tasks");

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("16d603e9-023e-4ece-9641-68ed340fba68"), "Badrum uppe" },
                    { new Guid("28309c76-9ce7-4dd8-b1da-120c3e80be16"), "Badrum nere" },
                    { new Guid("b6cc9e24-aad0-44dc-a4d0-a6c84975170e"), "Sovrum" },
                    { new Guid("d79adce3-b977-4b80-a9ed-9ef373b01a4b"), "Kök" },
                    { new Guid("ec2bcf26-6118-4839-8ff8-99cf35b303b9"), "Vardagsrum" }
                });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("065a20ba-6772-43f3-851b-848fc2ce39ee"), "Tvättat golv" },
                    { new Guid("619787c6-2057-429a-9429-ab91a9cb2283"), "Rengjort badrum" },
                    { new Guid("6ffe1a46-3112-4245-959c-47cb51e50c2b"), "Tömt tvättmaskin" },
                    { new Guid("7147263a-f239-44eb-8a2a-235c3568dc39"), "Torkat av alla ytor" },
                    { new Guid("a2faa15b-a898-4176-a62c-bc49a7042077"), "Tömt diskmaskin" },
                    { new Guid("a510494b-5c12-47f6-a55e-bcd589dc8d66"), "Tömt sopor" },
                    { new Guid("b3292da5-ef07-4521-adc5-59f573b6c2b2"), "Startat diskmaskin" },
                    { new Guid("b7b5270c-0f8d-4ccd-ac39-017bde938cc4"), "Startat tvättmaskin" },
                    { new Guid("f2a0c509-bc09-4c19-b1d8-e480163b0506"), "Bytt sängkläder" },
                    { new Guid("f7febf0e-ab94-4d71-91b3-f0ef302c49ae"), "Dammsugit golv" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2744bda8-ca5b-4e08-abde-ae11562dded3"), "Markus" },
                    { new Guid("3431b0a7-afa6-45e2-aa8d-13064ef08f5f"), "Cecilia" },
                    { new Guid("52961fac-5431-4845-9b79-0c7bf88bf506"), "Planerad" }
                });
        }
    }
}
