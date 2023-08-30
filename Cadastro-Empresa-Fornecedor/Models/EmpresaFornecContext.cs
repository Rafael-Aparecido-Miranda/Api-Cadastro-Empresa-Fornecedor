using Microsoft.EntityFrameworkCore;

namespace Cadastro_Empresa_Fornecedor.Models
{
    public class EmpresaFornecContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public EmpresaFornecContext(DbContextOptions options): base(options)
        {
        }
    }
}
