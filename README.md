# Cadastro-Empresa-Fornecedor

 Criação de uma API web ASP.NET Core para cadastro de empresas e fornecedores.
 Utilização do Entity Framework Core para mapear as entidades do sistema para o banco de dados.
 Uso do Dapper para realizar consultas SQL personalizadas.
 Implementação de operações CRUD (Create, Read, Update, Delete) para as entidades Empresa e Fornecedor.
 Utilização de operações assíncronas para melhor performance e escalabilidade.
 Configuração do Swagger para documentar a API.
 Implementação de validações de dados, como validação de CNPJ e CPF.
 Uso de injeção de dependência para gerenciar as dependências da aplicação.
 Configuração das conexões com o banco de dados.

#**Peculiaridades**

Validações de CNPJ e CPF: O sistema possui validações específicas para CNPJ e CPF, de acordo com o tipo de Pessoa (Física ou Jurídica) ao cadastrar um fornecedor.

Utilização do Dapper: Além do Entity Framework Core, o sistema utiliza o Dapper para realizar consultas SQL personalizadas, oferecendo flexibilidade na recuperação de dados do banco de dados.

Operações Assíncronas: Todas as operações do sistema, como criação, leitura, atualização e exclusão, são implementadas de forma assíncrona para melhorar a performance e escalabilidade da API.

Documentação da API com Swagger: A API é documentada usando o Swagger, o que permite visualizar e testar as rotas da API por meio de uma interface interativa.

Injeção de Dependência: O sistema utiliza o padrão de injeção de dependência do ASP.NET Core para gerenciar as dependências das classes, promovendo um melhor design e facilitando a manutenção do código.

Descrição do Problema com a Injeção de Dependência:

O erro está relacionado à injeção de dependência ocorre porque o ASP.NET Core está tendo dificuldades em encontrar ou criar uma instância correta de IDbConnection quando tenta criar o EmpresaController.
Eu não consegui resolver essa questão

O erro em questão, "Unable to resolve service for type 'System.Data.IDbConnection' while attempting to activate 'Cadastro_Empresa_Fornecedor.Controllers.EmpresaController'", basicamente significa que o ASP.NET Core não consegue resolver qual implementação de IDbConnection deve ser usada quando o controlador é instanciado.
