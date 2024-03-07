# StatusService

### Características

- .NET 6.
- EKS  (AWS).
- RDS PostgreSQL (AWS).
- RabbitMQ para Mensageria (Container)
- CI/CD com GitHub Actions.
- Integração com o SonarCloud

### SAGA (orquestrado)

Optamos pelo padrão SAGA **orquestrado** em detrimento do coreografado para gerenciar transações entre os serviços de Pedido, Pagamento e Status da nossa aplicação, devido à necessidade de uma coordenação centralizada. O padrão orquestrado, com o serviço **PedidoService** atuando como o orquestrador central, permite uma gestão mais clara e direta das transações, simplificando a lógica de negócios e facilitando o tratamento de falhas por meio de operações de compensação. Além disso, a utilização do RabbitMQ como sistema de mensageria apoia esta abordagem, oferecendo entrega confiável de mensagens e garantindo a consistência dos dados. Escolhi este padrão para beneficiar-me de sua estrutura de controle centralizado, que é mais alinhada com a complexidade e requisitos específicos da minha aplicação, oferecendo um mecanismo mais robusto e intuitivo para lidar com transações distribuídas.
