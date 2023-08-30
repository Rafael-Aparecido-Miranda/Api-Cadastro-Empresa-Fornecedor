using Cadastro_Empresa_Fornecedor.Entity;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Cadastro_Empresa_Fornecedor.Controllers
{
    [Route("api/fornecedor")]
    public class FornecedorController : Controller
    {
        private readonly IDbConnection _dbConnection;

        public FornecedorController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost]
        public async Task<IActionResult> Create(FornecedorEntity fornecedor)
        {
            string sql = "INSERT INTO Fornecedor (CNPJ, CPF, Nome, Email, Cep, PessoaFisica, RG, DataNascimento) VALUES (@CNPJ,@CPF,@Nome, @Email, @Cep, @PessoaFisica, @RG, @DataNascimento)";
            int rowsAffected = await _dbConnection.ExecuteAsync(sql, fornecedor);
            bool cnpjCpfExist = await CheckCnpjCpfExistsAsync(fornecedor.CPF, fornecedor.CNPJ);
            if (rowsAffected > 0)
            {
                return !cnpjCpfExist ? Ok() : BadRequest("CNPJ OU CPF JÁ EXISTE");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{cnpjoucpf}")]
        public async Task<IActionResult> Read(string cnpjoucpf)
        {
            string sql = "SELECT * FROM Fornecedor WHERE CNPJ = @CNPJ OR CPF = @CPF";
            FornecedorEntity fornecedor = await _dbConnection.QueryFirstOrDefaultAsync<FornecedorEntity>(sql, new { CNPJ = cnpjoucpf, CPF = cnpjoucpf });
            if (fornecedor != null)
            {
                return Ok(fornecedor);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{cnpjoucpf}")]
        public async Task<IActionResult> Update(string cnpjoujcpf, FornecedorEntity fornecedor)
        {
            string sql = "UPDATE Fornecedor SET Nome = @Nome, Email = @Email, Cep = @Cep, PessoaFisica = @PessoaFisica, RG = @RG, DataNascimento = @DataNascimento WHERE CNPJ OR CPF = @CPF";
            fornecedor.CNPJ = cnpjoujcpf;
            fornecedor.CPF = cnpjoujcpf;
            int rowsAffected = await _dbConnection.ExecuteAsync(sql, fornecedor);
            bool cnpjCpfExist = await CheckCnpjCpfExistsAsync(fornecedor.CPF,fornecedor.CNPJ);
            if(rowsAffected > 0)
            {
                return !cnpjCpfExist ? Ok() : BadRequest("CNPJ OU CPF JÁ EXISTE");
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<bool> CheckCnpjCpfExistsAsync(string cnpj, string cpf)
        {
            bool exists = false;

            // Verificar se o CNPJ ou CPF já existe em Fornecedores
            bool existsInFornecedores = await _dbConnection.QueryFirstOrDefaultAsync<bool>(
                "SELECT 1 FROM Fornecedores WHERE CNPJ = @CNPJ OR CPF = @CPF",
                new { CNPJ = cnpj, CPF = cpf });

            // Verificar se o CNPJ já existe em Empresas
            bool existsInEmpresas = await _dbConnection.QueryFirstOrDefaultAsync<bool>(
                "SELECT 1 FROM Empresas WHERE CNPJ = @CNPJ",
                new { CNPJ = cnpj });

            // Se o CNPJ ou CPF existir em Fornecedores OU o CNPJ existir em Empresas, setar exists para true
            if (existsInFornecedores || existsInEmpresas)
            {
                exists = true;
            }

            return exists;
        }

        [HttpDelete("{cnpjoucpf}")]
        public async Task<IActionResult> Delete(string cnpjoucpf)
        {
            string sql = "DELETE FROM Fornecedor WHERE CNPJ = @CNPJ OR CPF = @CPF";
            int rowsAffected = await _dbConnection.ExecuteAsync(sql, new { CNPJ = cnpjoucpf, CPF = cnpjoucpf });
            if(rowsAffected > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
