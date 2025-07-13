# AntecipacaoDeRecebivel
Serviço que calcula a antecipação de recebível de uma empresa

# LINK DE VIDEO DE APRESENTAÇÃO

https://drive.google.com/file/d/1hBoQ9p525UbarB1IcUNrD3dPZPfrFmSA/view?usp=sharing



# COMO RODAR A APLICAÇÃO

**Backend**
> Para rodar o BE é necessario criar as tabelas Empresa e NotaFiscal no Postgresql.
> - Voce precisa criar um BD com o nome de AntecipacaoDeRecebivel.
> - Voce pode rodar os comando descritos na proxima sessao ou rodar os script que estao na raiz do projeto

**Comandos EF Core**
> Com os dois comandos abaixo, voce vai conseguir criar o DB e as tabelas
> - Comando para criar o banco e as tabelas: dotnet ef migrations add Initial --project ..\AntecipacaoDeRecebivel.Infrastructure\ --startup-project .
> - Comando para salvar as alterações: dotnet ef database update

**Frontend**
> Eu decidi usar Next.js (mas usei componentes React puro - client side) com Tailwind.
> - Para iniciar é bem simples, só rodar o comando npm run dev


# ESTRUTURA DO PROJETO

O projeto foi feito pensando em escalabilidade, em facilidade de implementacao de unit tests e performance, por esse motivo eu escolhi fazer um backend robusto, utilizando Arquitetura Limpa, RESTful API e principios SOLID. Os cuidados se estenderam para o frontend, onde podemos ter mais performance com Next.js e usando os React Hooks de forma correta.


# MELHORIAS

> - Testes unitários
> - Autenticação/Autorização
> - Testes no frontend
> - Funcionalidades como: deletar/editar NFs, deletar/editar Empresas
> - Conteinerização
> - Padrao de Código (ex: ESlint)