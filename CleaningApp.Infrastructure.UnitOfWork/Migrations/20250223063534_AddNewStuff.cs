using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleaningApp.Infrastructure.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class AddNewStuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "DayOfWeek",
                table: "TaskTemplates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DayOfMonth",
                table: "TaskTemplates",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7a11bb98-3e4e-42c7-bccc-3dd3d564f995"), "Kök" },
                    { new Guid("83003226-c550-42ba-b2f1-a1ff2fd6dee2"), "Vardagsrum" },
                    { new Guid("c5c2f4da-017a-4f95-b707-c40884d48823"), "Badrum uppe" },
                    { new Guid("f3c3d903-b31a-480d-be93-c051a7ccc051"), "Badrum nere" },
                    { new Guid("f52a0448-2d6e-4258-b6b9-e55b9cfccc94"), "Sovrum" }
                });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("09ab5645-27ef-43f7-80b3-b9197b612dd9"), "Startat diskmaskin" },
                    { new Guid("2524e676-bca2-4d2c-abec-8df7668182f0"), "Tvättat golv" },
                    { new Guid("3164d4ea-646d-4b7d-883b-bc0b57b77c93"), "Dammsugit golv" },
                    { new Guid("5051dfb0-8447-4654-b931-a2cf9516998d"), "Startat tvättmaskin" },
                    { new Guid("6aa00f5a-3756-4fbd-80ae-70b0e4684dcf"), "Tömt diskmaskin" },
                    { new Guid("7113fe97-5d7d-436d-9086-5cd89db366c8"), "Rengjort badrum" },
                    { new Guid("792e66ad-478c-48f6-bb2e-086849cbf0f8"), "Bytt sängkläder" },
                    { new Guid("ace1e44f-6f5d-40d5-9178-8e9b71ed60ea"), "Tömt sopor" },
                    { new Guid("b576d6fb-e1db-42b8-8703-afc12fc428ad"), "Torkat av alla ytor" },
                    { new Guid("e627ce1d-8dab-4581-b2df-5f305cf581cf"), "Tömt tvättmaskin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("52785979-7ea9-49c8-a4a4-037fc01ec008"), "Markus" },
                    { new Guid("8c04a24e-f0f3-47be-949e-b2631657cddc"), "Planerad" },
                    { new Guid("b8150fea-4773-45b6-87a9-9b335c818081"), "Cecilia" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("7a11bb98-3e4e-42c7-bccc-3dd3d564f995"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("83003226-c550-42ba-b2f1-a1ff2fd6dee2"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("c5c2f4da-017a-4f95-b707-c40884d48823"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("f3c3d903-b31a-480d-be93-c051a7ccc051"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("f52a0448-2d6e-4258-b6b9-e55b9cfccc94"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("09ab5645-27ef-43f7-80b3-b9197b612dd9"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("2524e676-bca2-4d2c-abec-8df7668182f0"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("3164d4ea-646d-4b7d-883b-bc0b57b77c93"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("5051dfb0-8447-4654-b931-a2cf9516998d"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("6aa00f5a-3756-4fbd-80ae-70b0e4684dcf"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("7113fe97-5d7d-436d-9086-5cd89db366c8"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("792e66ad-478c-48f6-bb2e-086849cbf0f8"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("ace1e44f-6f5d-40d5-9178-8e9b71ed60ea"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("b576d6fb-e1db-42b8-8703-afc12fc428ad"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("e627ce1d-8dab-4581-b2df-5f305cf581cf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("52785979-7ea9-49c8-a4a4-037fc01ec008"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c04a24e-f0f3-47be-949e-b2631657cddc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b8150fea-4773-45b6-87a9-9b335c818081"));

            migrationBuilder.DropColumn(
                name: "DayOfMonth",
                table: "TaskTemplates");

            migrationBuilder.AlterColumn<int>(
                name: "DayOfWeek",
                table: "TaskTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
