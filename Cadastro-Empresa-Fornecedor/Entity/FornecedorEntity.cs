using System.ComponentModel.DataAnnotations;

namespace Cadastro_Empresa_Fornecedor.Entity
{
    public class FornecedorEntity
    {
        public int Id { get; set; }
        [Required]
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public string EMail { get; set; }
        public string Cep { get; set; }
        public bool PessoaFisica { get; set; }
        public string RG { get; set; }
        public DateTime DataNascimento { get; set; }

        public ICollection<EmpresaFornecedorEntity> EmpresasFornecedores { get; set; }
    }
}
