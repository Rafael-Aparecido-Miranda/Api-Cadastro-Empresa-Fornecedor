using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadastro_Empresa_Fornecedor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(maxLength: 14,nullable: false),
                    NomeFantasia = table.Column<string>(maxLength: 50,nullable: false),
                    CEP = table.Column<string>(maxLength: 8, nullable: false)
                },
            constraints: table =>
            {
                table.PrimaryKey("PK_Empresas", x => x.Id);
                table.UniqueConstraint("AK_Empresas_CNPJ", x => x.CNPJ);
            });
            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(maxLength:14, nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    CEP = table.Column<string>(maxLength: 8, nullable: true),
                    PessoaFisica = table.Column<bool>(nullable: false),
                    RG = table.Column<string>(maxLength:10, nullable: true),
                    DataNascimento = table.Column<string>(maxLength: 8, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });
            migrationBuilder.CreateTable(name: "EmpresaFornecedor",
                columns: table => new
                {
                    EmpresasId = table.Column<int>(nullable: false),
                    FornecedoresId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaFornecedor", x => new { x.EmpresasId, x.FornecedoresId });
                    table.ForeignKey(
                        name: "FK_EmpresasFornecedor_Empresas_EmpresasId",
                        column: x=> x.EmpresasId,
                        principalTable: "Empresas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpresaFornecedor_Fornecedores_FornecedoresId",
                        column: x => x.FornecedoresId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //Validação Fornecedor pessoa fisica até 120 anos
            migrationBuilder.Sql(@"
                ALTER TABLE dbo.Fornecedores
                ADD CONSTRAINT CK_Fornecedores_DataNascimentoValidacao
                CHECK (
                    (PessoaFisica = 0) OR
                    (PessoaFisica = 1 AND DataNascimento <= GETDATE() AND DataNascimento >= DATEADD(year, -120, GETDATE()))
                );
            ");
            //validação pessoa física menor que 18 anos 
            migrationBuilder.Sql(@"
                ALTER TABLE dbo.Fornecedores
                ADD CONSTRAINT CK_Fornecedores_MenorIdadeValidacao
                CHECK (
                    (PessoaFisica = 0) OR
                    (PessoaFisica = 1 AND DataNascimento <= DATEADD(year, -18, GETDATE()))
                );
            ");
            //validação de email seguindo padrões de caracter depois do @
            migrationBuilder.Sql(@"
                ALTER TABLE dbo.Fornecedores
                ADD CONSTRAINT CK_Fornecedores_EmailValidacao
                CHECK (
                    (Email IS NULL) OR
                    (Email LIKE '%_@__%.__%')
                );
            ");
            //validação do cpf e cnpj não pode ter o campo nulo
            migrationBuilder.Sql(@"
                ALTER TABLE dbo.Fornecedores
                ADD CONSTRAINT CK_Fornecedores_CPFCNPJValidacao
                CHECK (
                    (PessoaFisica = 0) OR
                    (PessoaFisica = 1 AND (CPF IS NOT NULL OR CNPJ IS NOT NULL))
                );
            ");
            //Validação de 14 digitos cnpj fornecedor
            migrationBuilder.Sql(@"
                ALTER TABLE dbo.Fornecedores
                ADD CONSTRAINT CK_Fornecedores_CNPJSizeValidacao
                CHECK (
                    (CNPJ IS NULL) OR
                    (PessoaFisica = 0 AND LEN(CNPJ) = 14)
                );
            ");
            //validação 11 digitos cpf Fornecedor Pessoa fisica
            migrationBuilder.Sql(@"
                ALTER TABLE dbo.Fornecedores
                ADD CONSTRAINT CK_Fornecedores_CPFSizeValidacao
                CHECK (
                    (CPF IS NULL) OR
                    (PessoaFisica = 1 AND LEN(CPF) = 11)
                );
            ");
            migrationBuilder.Sql(@"
                ALTER TABLE dbo.Empresas
                ADD CONSTRAINT CK_Empresas_CNPJSizeValidacao
                CHECK (
                    LEN(CNPJ) = 14
                );
            ");
            migrationBuilder.CreateIndex(
                name: "IX_Empresas_CNPJ",
                table: "Empresas",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_CPFCNPJ",
                table: "Fornecedores",
                columns: new[] { "CNPJ", "CPF" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpresaFornecedor");
            migrationBuilder.DropTable(
                name: "Empresas");
            migrationBuilder.DropTable(
                name: "Fornecedores");
        }
    }
}
