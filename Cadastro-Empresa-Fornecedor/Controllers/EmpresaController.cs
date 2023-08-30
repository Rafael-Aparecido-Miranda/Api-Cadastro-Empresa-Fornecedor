using Cadastro_Empresa_Fornecedor.Entity;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace Cadastro_Empresa_Fornecedor.Controllers
{
    [ApiController]
    [Route("api/empresa")]
    public class EmpresaController : Controller
    {
        private readonly IDbConnection _dbConnection;

        public EmpresaController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmpresaEntity empresa)
        {
            string sql = "INSERT INTO Empresa (CNPJ,NomeFantasia, CEP) VALUES (@CNPJ, @NomeFantasia, @CEP)";
            int rowsAffected = await _dbConnection.ExecuteAsync(sql, empresa);

            if (rowsAffected > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{cnpj}")]
        public async Task<IActionResult> Read(string cnpj)
        {
            string sql = "SELECT * FROM Empresa WHERE CNPJ = @CNPJ";
            EmpresaEntity empresa = await _dbConnection.QueryFirstOrDefaultAsync<EmpresaEntity>(sql, new { CNPJ = cnpj });
            if (empresa != null)
            {
                return Ok(empresa);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPut("{cnpj}")]
        public async Task<IActionResult> Update(string cnpj, [FromBody] EmpresaEntity empresa)
        {
            string sql = "UPDATE Empresa SET NomeFantasia = @NomeFantasia, CEP = @CEP WHERE CNPJ = @CNPJ";
            empresa.CNPJ = cnpj;
            int rowsAffected = await _dbConnection.ExecuteAsync(sql, empresa);
            if (rowsAffected > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{cnpj}")]
        public async Task<IActionResult> Delete(string cnpj)
        {
            string sql = "DELETE FROM Empresa WHERE CNPJ = @CNPJ";
            int rowsAffected = await _dbConnection.ExecuteAsync(sql, new { CNPJ = cnpj });
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
