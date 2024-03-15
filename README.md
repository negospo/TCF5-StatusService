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

### Relatório RIPD
https://drive.google.com/file/d/1pPCSkVgNZBGcHD-SGRoFrNX9ge3KG9V4/view?usp=sharing


### Como iniciar o serviço 

Antes de inicializar o serviço, deve-se ter certeza de que o [cluster kubernetes no EKS](https://github.com/mvcosta/FIAPTerraformEKS), o [banco de dados RDS deste serviço](https://github.com/mvcosta/FIAPTerraformRDSStatus) e o [serviço orquestrador](https://github.com/negospo/TCF5-PedidoService) foram corretamente inicializados

A inicialização do serviço pode ser realizada de duas formas:

#### 1. Realizar o fork do repositório

1. Faça o fork deste repositório.
2. Preencha as secrets `AWS_ACCESS_KEY_ID` e `AWS_SECRET_ACCESS_KEY` com as informações da sua conta a AWS.
3. Execute a action "Deploy to Amazon EKS".

#### 2. Realizando o clone para sua máquina
1. Faça o clone do repositório na sua máquina.
2. Instale a AWS CLI.
3. Realize a autenticação na AWS CLI.
4. Crie o repositório ECR, caso ele não exista com o seguinte comando: `aws ecr describe-repositories --repository-names fiap-status || aws ecr create-repository --repository-name fiap-status`
5. Realize o build da imagem deste serviço com o seguinte comando `docker build -t {link-do-seu-registry}/fiap-status:latest`. Substituindo {link-do-seu-registry} pelo link do seu registry ECR.
6. Faça o push da imagem deste serviço para o ECR com o seguinte comando `docker push {link-do-seu-registry}/fiap-status:latest`. Substituindo {link-do-seu-registry} pelo link do seu registry ECR.
7. Instale o kubectl com o seguinte comando:
   ```
   curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
   chmod +x kubectl
   sudo mv kubectl /usr/local/bin/
   ```
8. Set o contexto do kubectl para o cluster EKS com o seguinte comando: `aws eks --region us-east-1 update-kubeconfig --name fiap`
9. Crie os recursos do kubernetes (pods, services, secrets etc) com o seguinte comando: `kubectl apply -f kubernetes/`
