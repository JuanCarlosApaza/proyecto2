using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Final.Migrations
{
    /// <inheritdoc />
    public partial class FixCascada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Espacio",
                columns: table => new
                {
                    idespacio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ubicacion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Espacio", x => x.idespacio);
                });

            migrationBuilder.CreateTable(
                name: "Imagen",
                columns: table => new
                {
                    idimagen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imagen = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagen", x => x.idimagen);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ruta = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    monto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    idpersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    telefono = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.idpersona);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    idevento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    estado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    precio = table.Column<int>(type: "int", nullable: false),
                    idespacio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.idevento);
                    table.ForeignKey(
                        name: "FK_Evento_Espacio_idespacio",
                        column: x => x.idespacio,
                        principalTable: "Espacio",
                        principalColumn: "idespacio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarjeta",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numeroTarjeta = table.Column<int>(type: "int", nullable: false),
                    nombreTitular = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    fechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cvv = table.Column<int>(type: "int", nullable: false),
                    idPago = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjeta", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tarjeta_Pago_idPago",
                        column: x => x.idPago,
                        principalTable: "Pago",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transferencia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreBanco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    idPago = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencia", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transferencia_Pago_idPago",
                        column: x => x.idPago,
                        principalTable: "Pago",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idusuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    contrasena = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idpersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idusuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Persona_idpersona",
                        column: x => x.idpersona,
                        principalTable: "Persona",
                        principalColumn: "idpersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolModulos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idrol = table.Column<int>(type: "int", nullable: false),
                    idmodulo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolModulos", x => x.id);
                    table.ForeignKey(
                        name: "FK_RolModulos_Modulos_idmodulo",
                        column: x => x.idmodulo,
                        principalTable: "Modulos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolModulos_Roles_idrol",
                        column: x => x.idrol,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anuncio",
                columns: table => new
                {
                    idanuncio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    imagen = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    idevento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncio", x => x.idanuncio);
                    table.ForeignKey(
                        name: "FK_Anuncio_Evento_idevento",
                        column: x => x.idevento,
                        principalTable: "Evento",
                        principalColumn: "idevento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fecha",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_final = table.Column<DateTime>(type: "datetime2", nullable: false),
                    idevento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fecha", x => x.id);
                    table.ForeignKey(
                        name: "FK_Fecha_Evento_idevento",
                        column: x => x.idevento,
                        principalTable: "Evento",
                        principalColumn: "idevento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Boleto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    idEvento = table.Column<int>(type: "int", nullable: false),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idPago = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boleto", x => x.id);
                    table.ForeignKey(
                        name: "FK_Boleto_Evento_idEvento",
                        column: x => x.idEvento,
                        principalTable: "Evento",
                        principalColumn: "idevento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boleto_Pago_idPago",
                        column: x => x.idPago,
                        principalTable: "Pago",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boleto_Usuario_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuario",
                        principalColumn: "idusuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EspacioUsuario",
                columns: table => new
                {
                    idespaciousuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idusuario = table.Column<int>(type: "int", nullable: false),
                    idespacio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspacioUsuario", x => x.idespaciousuario);
                    table.ForeignKey(
                        name: "FK_EspacioUsuario_Espacio_idespacio",
                        column: x => x.idespacio,
                        principalTable: "Espacio",
                        principalColumn: "idespacio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspacioUsuario_Usuario_idusuario",
                        column: x => x.idusuario,
                        principalTable: "Usuario",
                        principalColumn: "idusuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idusuario = table.Column<int>(type: "int", nullable: false),
                    idrol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => x.id);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Roles_idrol",
                        column: x => x.idrol,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuario_idusuario",
                        column: x => x.idusuario,
                        principalTable: "Usuario",
                        principalColumn: "idusuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lista",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<bool>(type: "bit", nullable: false),
                    idusuario = table.Column<int>(type: "int", nullable: false),
                    idboleto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lista", x => x.id);
                    table.ForeignKey(
                        name: "FK_Lista_Boleto_idboleto",
                        column: x => x.idboleto,
                        principalTable: "Boleto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lista_Usuario_idusuario",
                        column: x => x.idusuario,
                        principalTable: "Usuario",
                        principalColumn: "idusuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anuncio_idevento",
                table: "Anuncio",
                column: "idevento");

            migrationBuilder.CreateIndex(
                name: "IX_Boleto_idEvento",
                table: "Boleto",
                column: "idEvento");

            migrationBuilder.CreateIndex(
                name: "IX_Boleto_idPago",
                table: "Boleto",
                column: "idPago");

            migrationBuilder.CreateIndex(
                name: "IX_Boleto_idUsuario",
                table: "Boleto",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_EspacioUsuario_idespacio",
                table: "EspacioUsuario",
                column: "idespacio");

            migrationBuilder.CreateIndex(
                name: "IX_EspacioUsuario_idusuario",
                table: "EspacioUsuario",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_idespacio",
                table: "Evento",
                column: "idespacio");

            migrationBuilder.CreateIndex(
                name: "IX_Fecha_idevento",
                table: "Fecha",
                column: "idevento");

            migrationBuilder.CreateIndex(
                name: "IX_Lista_idboleto",
                table: "Lista",
                column: "idboleto");

            migrationBuilder.CreateIndex(
                name: "IX_Lista_idusuario",
                table: "Lista",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_RolModulos_idmodulo",
                table: "RolModulos",
                column: "idmodulo");

            migrationBuilder.CreateIndex(
                name: "IX_RolModulos_idrol",
                table: "RolModulos",
                column: "idrol");

            migrationBuilder.CreateIndex(
                name: "IX_Tarjeta_idPago",
                table: "Tarjeta",
                column: "idPago");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencia_idPago",
                table: "Transferencia",
                column: "idPago");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idpersona",
                table: "Usuario",
                column: "idpersona");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_idrol",
                table: "UsuarioRol",
                column: "idrol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_idusuario",
                table: "UsuarioRol",
                column: "idusuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anuncio");

            migrationBuilder.DropTable(
                name: "EspacioUsuario");

            migrationBuilder.DropTable(
                name: "Fecha");

            migrationBuilder.DropTable(
                name: "Imagen");

            migrationBuilder.DropTable(
                name: "Lista");

            migrationBuilder.DropTable(
                name: "RolModulos");

            migrationBuilder.DropTable(
                name: "Tarjeta");

            migrationBuilder.DropTable(
                name: "Transferencia");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "Boleto");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Espacio");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
