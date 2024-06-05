using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UNICAP.SiteCurso.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class INITIAL_MIGRATION : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USER_CREDENTIALS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LOGIN = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SENHA = table.Column<string>(type: "text", nullable: false),
                    SALT = table.Column<string>(type: "text", nullable: false),
                    CARGO = table.Column<int>(type: "integer", nullable: false),
                    REFRESH_TOKEN = table.Column<string>(type: "text", nullable: true),
                    REFRESH_TOKEN_EXPIRATION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FORGOT_PASSWORD_TOKEN = table.Column<string>(type: "text", nullable: true),
                    FORGOT_PASSWORD_URL = table.Column<string>(type: "text", nullable: true),
                    FORGOT_PASSWORD_EXPIRATION = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ULTIMO_ACESSO = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER_CREDENTIALS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOME = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EMAIL = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CREDENTIALS_ID = table.Column<int>(type: "integer", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TB_USER_TB_USER_CREDENTIALS_CREDENTIALS_ID",
                        column: x => x.CREDENTIALS_ID,
                        principalTable: "TB_USER_CREDENTIALS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ARTICLE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TITULO = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false),
                    DESCRICAO = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    USER_ID = table.Column<int>(type: "integer", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ARTICLE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TB_ARTICLE_TB_USER_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "TB_USER",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ARTICLE_USER_ID",
                table: "TB_ARTICLE",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USER_CREDENTIALS_ID",
                table: "TB_USER",
                column: "CREDENTIALS_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ARTICLE");

            migrationBuilder.DropTable(
                name: "TB_USER");

            migrationBuilder.DropTable(
                name: "TB_USER_CREDENTIALS");
        }
    }
}
