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

![image](https://github.com/negospo/TCF5-PedidoService/assets/39103031/f60e8629-d332-4982-adb1-df2b3be8578f)

### OWASP Reports

#### Antes:
https://drive.google.com/file/d/1UY9LAe4Zcb1Gf6Vk8zWDBbcHzw_E261X/view?usp=sharing
#### Depois:
https://drive.google.com/file/d/1Lyvu7fvT9aGAc0ojA-69kTTPG_agpMEs/view?usp=sharing
