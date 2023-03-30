using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerMecanico.Migrations
{
    public partial class pruebaalv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VehiculoMecanico_VehiculoId",
                table: "VehiculoMecanico",
                column: "VehiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehiculoMecanico_Vehiculo_VehiculoId",
                table: "VehiculoMecanico",
                column: "VehiculoId",
                principalTable: "Vehiculo",
                principalColumn: "VehiculoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehiculoMecanico_Vehiculo_VehiculoId",
                table: "VehiculoMecanico");

            migrationBuilder.DropIndex(
                name: "IX_VehiculoMecanico_VehiculoId",
                table: "VehiculoMecanico");
        }
    }
}
