using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleaningApp.Infrastructure.UnitOfWork.Migrations
{
    /// <inheritdoc />
    public partial class AddingTaskTemplateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("128b480f-40fe-4785-af35-0ea3a9c69aa5"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("1b234aab-fa95-48bc-9cf0-99476989d396"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("88583256-aec1-49ef-ac92-bea090ebd566"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("aa1b40e0-21c7-4eba-ae7c-0d782e6c1829"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("f63ce709-dced-452e-857b-d8ea4804650f"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("027035be-5cc0-4dc8-b72e-e9f5802ce147"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("171f7583-1b33-4ad7-bb72-247afab191e8"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("238e2350-8d9c-41f6-9a23-a0aa0f33addf"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("5c3beef8-87b4-4dd3-9501-5830cd2e7b90"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("81a69c4f-b985-4fb2-8afb-24e84aafb3a1"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("92e9431e-38d8-4154-82c6-6ab5b1189ceb"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("cd66f083-04b2-4d2c-a89f-6ea3124590be"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("e0566d9f-3489-4af2-8af0-afaa6e2ff289"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("f578b79f-b5fc-44f7-9ca3-c6659b26f07e"));

            migrationBuilder.DeleteData(
                table: "TaskTypes",
                keyColumn: "Id",
                keyValue: new Guid("fb02076e-1d95-46bc-aab5-992e3a6d88a8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a44cdbdc-7af8-4b43-8b66-cda9e72f0c9a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("adc2d573-3bc7-452a-89a1-459d1488f72b"));

            migrationBuilder.CreateTable(
                name: "TaskTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TaskDuration = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    DefaultUserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RoomId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TaskTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Notes = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTemplates_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTemplates_TaskTypes_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "TaskTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTemplates_Users_DefaultUserId",
                        column: x => x.DefaultUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_TaskTemplates_DefaultUserId",
                table: "TaskTemplates",
                column: "DefaultUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTemplates_RoomId",
                table: "TaskTemplates",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTemplates_TaskTypeId",
                table: "TaskTemplates",
                column: "TaskTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskTemplates");

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
                    { new Guid("128b480f-40fe-4785-af35-0ea3a9c69aa5"), "Sovrum" },
                    { new Guid("1b234aab-fa95-48bc-9cf0-99476989d396"), "Badrum uppe" },
                    { new Guid("88583256-aec1-49ef-ac92-bea090ebd566"), "Vardagsrum" },
                    { new Guid("aa1b40e0-21c7-4eba-ae7c-0d782e6c1829"), "Badrum nere" },
                    { new Guid("f63ce709-dced-452e-857b-d8ea4804650f"), "Kök" }
                });

            migrationBuilder.InsertData(
                table: "TaskTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("027035be-5cc0-4dc8-b72e-e9f5802ce147"), "Tvättat golv" },
                    { new Guid("171f7583-1b33-4ad7-bb72-247afab191e8"), "Tömt diskmaskin" },
                    { new Guid("238e2350-8d9c-41f6-9a23-a0aa0f33addf"), "Bytt sängkläder" },
                    { new Guid("5c3beef8-87b4-4dd3-9501-5830cd2e7b90"), "Rengjort badrum" },
                    { new Guid("81a69c4f-b985-4fb2-8afb-24e84aafb3a1"), "Startat tvättmaskin" },
                    { new Guid("92e9431e-38d8-4154-82c6-6ab5b1189ceb"), "Torkat av alla ytor" },
                    { new Guid("cd66f083-04b2-4d2c-a89f-6ea3124590be"), "Tömt sopor" },
                    { new Guid("e0566d9f-3489-4af2-8af0-afaa6e2ff289"), "Startat diskmaskin" },
                    { new Guid("f578b79f-b5fc-44f7-9ca3-c6659b26f07e"), "Dammsugit golv" },
                    { new Guid("fb02076e-1d95-46bc-aab5-992e3a6d88a8"), "Tömt tvättmaskin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a44cdbdc-7af8-4b43-8b66-cda9e72f0c9a"), "Cecilia" },
                    { new Guid("adc2d573-3bc7-452a-89a1-459d1488f72b"), "Markus" }
                });
        }
    }
}
