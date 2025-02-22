using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleaningApp.Infrastructure.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class TaskRenameToCleaningTaskEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("15cb26d9-5f4a-4eef-9bf8-1c8bf09c7e03"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("7db467f3-275e-41ff-b01c-9ffe8eb83863"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("8fc9de16-fcee-4528-819d-1fbda6b49d62"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("bf297c42-bb01-4a06-9033-c2ac438aab79"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("c77e0e61-91e4-4beb-970f-2d471036f9c9"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("5387d01a-8bd2-45ac-8bca-f294583d6430"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("59106689-5995-4ff9-a832-e8f4d8f20b01"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("71915d86-90fa-428b-83ef-1bf6c185a829"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("978e053d-d3d0-447a-9c86-89050a908ee8"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("9b2bb5da-dfc2-4d37-9964-e4df38d0aa30"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("c19b9818-3aa8-4056-96f9-f5b3635d84f1"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("caa75b45-475a-4141-af5d-c8f9919336d8"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("df494896-13e4-427d-9b2a-59c63484b935"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("ebab6358-9109-4dbc-950e-a38ff1daeb0d"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("fad31a36-d337-4402-bcf2-6f88054cfbc9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("992d047b-d0cc-4976-8678-ebd3f4d914a0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b8dc0358-577d-4839-9c08-e652ba8d465b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e9868e20-8f1b-4c33-9437-85d2c516c1ee"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("15cb26d9-5f4a-4eef-9bf8-1c8bf09c7e03"), "Badrum nere" },
                    { new Guid("7db467f3-275e-41ff-b01c-9ffe8eb83863"), "Sovrum" },
                    { new Guid("8fc9de16-fcee-4528-819d-1fbda6b49d62"), "Kök" },
                    { new Guid("bf297c42-bb01-4a06-9033-c2ac438aab79"), "Badrum uppe" },
                    { new Guid("c77e0e61-91e4-4beb-970f-2d471036f9c9"), "Vardagsrum" }
                });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5387d01a-8bd2-45ac-8bca-f294583d6430"), "Bytt sängkläder" },
                    { new Guid("59106689-5995-4ff9-a832-e8f4d8f20b01"), "Dammsugit golv" },
                    { new Guid("71915d86-90fa-428b-83ef-1bf6c185a829"), "Tömt sopor" },
                    { new Guid("978e053d-d3d0-447a-9c86-89050a908ee8"), "Startat diskmaskin" },
                    { new Guid("9b2bb5da-dfc2-4d37-9964-e4df38d0aa30"), "Tvättat golv" },
                    { new Guid("c19b9818-3aa8-4056-96f9-f5b3635d84f1"), "Tömt tvättmaskin" },
                    { new Guid("caa75b45-475a-4141-af5d-c8f9919336d8"), "Tömt diskmaskin" },
                    { new Guid("df494896-13e4-427d-9b2a-59c63484b935"), "Rengjort badrum" },
                    { new Guid("ebab6358-9109-4dbc-950e-a38ff1daeb0d"), "Torkat av alla ytor" },
                    { new Guid("fad31a36-d337-4402-bcf2-6f88054cfbc9"), "Startat tvättmaskin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("992d047b-d0cc-4976-8678-ebd3f4d914a0"), "Planerad" },
                    { new Guid("b8dc0358-577d-4839-9c08-e652ba8d465b"), "Markus" },
                    { new Guid("e9868e20-8f1b-4c33-9437-85d2c516c1ee"), "Cecilia" }
                });
        }
    }
}
