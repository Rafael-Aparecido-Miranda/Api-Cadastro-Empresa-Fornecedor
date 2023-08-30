using System.ComponentModel.DataAnnotations;

namespace Cadastro_Empresa_Fornecedor.Entity
{
    public class EmpresaEntity
    {
        public int Id { get; set; }
        [Required]
        public string CNPJ { get; set; }
        public string NomeFantasia { get; set; }
        public string CEP { get; set; }

        public ICollection<EmpresaFornecedorEntity> EmpresasFonecedores { get; set; }

    }
}
