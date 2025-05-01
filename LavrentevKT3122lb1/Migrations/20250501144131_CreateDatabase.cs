using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LavrentevKT3122lb1.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_academic_degree",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Идентификатор ученой степени")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Название ученой степени"),
                    f_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Флаг удаления"),
                    d_created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Дата создания"),
                    d_updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Дата обновления")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_academic_degree_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Идентификатор дисциплины")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Название дисциплины"),
                    c_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Код дисциплины"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cd_position",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Идентификатор должности")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Название должности"),
                    f_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Флаг удаления записи"),
                    d_created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Дата создания записи"),
                    d_updated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Дата обновления записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_position_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cd_department",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Идентификатор кафедры")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Название кафедры"),
                    d_foundation = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата основания кафедры"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    f_head_id = table.Column<long>(type: "bigint", nullable: true, comment: "Идентификатор заведующего кафедрой")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_department_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cd_teacher",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Идентификатор преподавателя")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Имя преподавателя"),
                    c_lastname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Фамилия преподавателя"),
                    c_middlename = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Отчество преподавателя"),
                    d_birthdate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата рождения"),
                    d_hiredate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата приема на работу"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    f_department_id = table.Column<long>(type: "bigint", nullable: false, comment: "Ссылка на кафедру"),
                    f_position_id = table.Column<long>(type: "bigint", nullable: false, comment: "Ссылка на должность"),
                    f_degree_id = table.Column<long>(type: "bigint", nullable: true, comment: "Ссылка на ученую степень")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_teacher_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_cd_teacher_f_degree_id",
                        column: x => x.f_degree_id,
                        principalTable: "cd_academic_degree",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_cd_teacher_f_department_id",
                        column: x => x.f_department_id,
                        principalTable: "cd_department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cd_teacher_f_position_id",
                        column: x => x.f_position_id,
                        principalTable: "cd_position",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cd_teacher_workload",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Идентификатор нагрузки")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    n_hours = table.Column<int>(type: "integer", nullable: false, comment: "Количество часов"),
                    n_semester = table.Column<int>(type: "integer", nullable: false, comment: "Семестр (1 или 2)"),
                    n_year = table.Column<int>(type: "integer", nullable: false, comment: "Год"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    f_teacher_id = table.Column<long>(type: "bigint", nullable: false, comment: "Ссылка на преподавателя"),
                    f_discipline_id = table.Column<long>(type: "bigint", nullable: false, comment: "Ссылка на дисциплину")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_teacher_workload_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_cd_teacher_workload_f_discipline_id",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cd_teacher_workload_f_teacher_id",
                        column: x => x.f_teacher_id,
                        principalTable: "cd_teacher",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_department_f_head_id",
                table: "cd_department",
                column: "f_head_id",
                unique: true,
                filter: "[f_head_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "idx_cd_position_name",
                table: "cd_position",
                column: "c_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_cd_teacher_f_degree_id",
                table: "cd_teacher",
                column: "f_degree_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_teacher_f_department_id",
                table: "cd_teacher",
                column: "f_department_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_teacher_f_position_id",
                table: "cd_teacher",
                column: "f_position_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_teacher_workload_teacher_discipline_unique",
                table: "cd_teacher_workload",
                columns: new[] { "f_teacher_id", "f_discipline_id", "n_semester", "n_year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cd_teacher_workload_f_discipline_id",
                table: "cd_teacher_workload",
                column: "f_discipline_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cd_department_f_head_id",
                table: "cd_department",
                column: "f_head_id",
                principalTable: "cd_teacher",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cd_department_f_head_id",
                table: "cd_department");

            migrationBuilder.DropTable(
                name: "cd_teacher_workload");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropTable(
                name: "cd_teacher");

            migrationBuilder.DropTable(
                name: "cd_academic_degree");

            migrationBuilder.DropTable(
                name: "cd_department");

            migrationBuilder.DropTable(
                name: "cd_position");
        }
    }
}
