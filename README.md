Projeto construido em C# com .NET 9
Utilizado como estudo dos conceitos de CQRS utilizando a biblioteca MediatR como gerenciadora de commands e request validation e a biblioteca Denver como gerenciadora de queries.
O projeto consistem em um a construção de uma WebApi para fazer um Crud de Membros de um sistema.
Toda a estrutura foi construida utilizando conceitos da Clean Architecture (Arquitetura Limpa).
Dentro do projeto pincipal existem outros 5 projetos que se comunicam entre si utilizando interfaces e inversão de dependencia respeitando os conceitos de SOLID.
Projeto Domain: contem as regras de negocio e core da aplicação, alem das interfaces/abstrações a serem utilizadas pelas proximas camadas assinarem o contrato.
Projeto Application: contem a implementação da camada Domain, os services e a logica.
Projeto Infrastructure: mantem a infraestrutura do projeto, contexto (DB), migrations e repositorys.
Projeto API: contem os controllers, é o ponto de entrada e saida de aplicação, onde recebe as requisições e as trata/valida antes de passar para as outras camadas, alem de devolver as respostas.
