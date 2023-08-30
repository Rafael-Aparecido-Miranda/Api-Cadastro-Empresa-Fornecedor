namespace Cadastro_Empresa_Fornecedor.Entity
{
    public class EmpresaFornecedorEntity
    {
        public int EmpresaId { get; set; }
        public EmpresaEntity Empresa { get; set; }

        public int FornecedorId { get; set; }
        public FornecedorEntity Fornecedor { get; set; }
    }
}
