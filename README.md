# inception-project

Caso de Uso Detalhado: Depósito por Cheque via Aplicativo Bancário
Cenário:

Imagine a Sra. Silva, uma cliente assídua do Banco ABC, que precisa depositar um cheque de R$ 200,00 recebido de seu trabalho. Em vez de enfrentar filas no banco, ela decide utilizar o aplicativo bancário do Banco ABC para realizar a transação de forma rápida e segura.

Detalhes do Caso de Uso:

Atores:

Sra. Silva: Cliente do Banco ABC, usuária experiente do aplicativo bancário.
Aplicativo Banco ABC: Interface mobile desenvolvida pelo Banco ABC para oferecer serviços bancários aos seus clientes.
Sistema Bancário: Conjunto de sistemas integrados que gerenciam as informações bancárias dos clientes, transações financeiras e operações do banco.
Sistema de Mensageria: Plataforma que viabiliza o envio de notificações por SMS ou push para os clientes do banco.
Pré-condições:

A Sra. Silva possui um cheque válido e em boas condições, emitido por uma conta corrente ativa.
A conta bancária da Sra. Silva no Banco ABC também está ativa e em bom funcionamento.
O aplicativo Banco ABC está instalado e atualizado no smartphone da Sra. Silva.
A Sra. Silva possui acesso à internet em seu dispositivo móvel.
Fluxo de Atividades:

Acessando o Aplicativo: A Sra. Silva abre o aplicativo Banco ABC em seu smartphone e faz login utilizando sua senha pessoal.

Iniciando o Depósito: Na tela inicial do aplicativo, a Sra. Silva seleciona a opção "Depositar Cheque".

Informando os Dados: A Sra. Silva insere o valor do cheque (R$ 200,00) e o número da agência em que o cheque foi emitido.

Capturando a Imagem: A Sra. Silva segue as instruções na tela para tirar uma foto do cheque, garantindo que a imagem esteja nítida e capture todos os cantos e informações do documento.

Validação Automática: O aplicativo Banco ABC utiliza recursos de inteligência artificial para validar as informações do cheque, como valor, banco emissor e autenticidade.

Confirmação e Envio: Se as informações forem validadas com sucesso, a Sra. Silva confirma a transação e o aplicativo envia os dados para o sistema bancário.

Registro nas Tabelas: O sistema bancário registra a transação em duas tabelas:

Tabela de Transações: Inclui data, hora, valor do depósito, número do cheque, conta bancária creditada e outras informações relevantes.
Tabela de Cheques Depositados: Armazena o número do cheque, data do depósito e conta bancária creditada para controle interno e histórico de depósitos.
Notificação à Cliente: O sistema de mensageria envia uma notificação por SMS ou push para a Sra. Silva informando-a sobre o depósito bem-sucedido, detalhando o valor depositado e a conta bancária creditada.

Finalização da Transação: A Sra. Silva visualiza na tela do aplicativo a confirmação do depósito e o saldo atualizado em sua conta corrente.

Pós-condições:

A transação de depósito de R$ 200,00 do cheque da Sra. Silva está registrada de forma segura e completa no sistema bancário.
A Sra. Silva foi notificada por SMS ou push sobre o depósito bem-sucedido e o saldo atualizado em sua conta.
A Sra. Silva pode visualizar o histórico da transação no aplicativo Banco ABC a qualquer momento.
Benefícios:

Comodidade: A Sra. Silva pôde realizar o depósito do cheque de forma rápida, segura e sem precisar se deslocar até uma agência bancária.
Acessibilidade: O aplicativo Banco ABC está disponível 24 horas por dia, 7 dias por semana, permitindo que a Sra. Silva realize transações bancárias a qualquer momento e em qualquer lugar.
Segurança: O aplicativo Banco ABC utiliza medidas de segurança robustas para proteger as informações da Sra. Silva, como criptografia e autenticação, garantindo a confidencialidade e integridade dos dados.
Agilidade: A validação automática do cheque e o registro da transação em tempo real proporcionaram à Sra. Silva uma experiência rápida e eficiente.
Transparência: A Sra. Silva recebeu uma notificação imediata sobre o depósito e pode acompanhar o histórico da transação no aplicativo, garantindo clareza e controle sobre suas finanças.
Considerações Adicionais:

Segurança do Cheque: O Banco



share


more_vert

Continuar
volume_up

Caso de Uso Detalhado: Depósito por Cheque via Aplicativo Bancário (Continuação)
Considerações Adicionais:

Segurança do Cheque: O Banco ABC pode implementar medidas adicionais para garantir a segurança do cheque depositado, como:

Marca d'água digital: Incluir uma marca d'água digital invisível no aplicativo para autenticação do cheque durante a captura da imagem.
Geolocalização: Registrar a localização da Sra. Silva no momento da captura da imagem do cheque para auxiliar na prevenção de fraudes.
Comparação com Imagem de Arquivo: Comparar a imagem do cheque capturada com a imagem de arquivo do cheque fornecida pelo emissor, se disponível, para verificar a autenticidade.
Limite de Depósito por Cheque: O Banco ABC pode definir um limite diário ou mensal para o valor total de depósitos por cheque realizados através do aplicativo, a fim de gerenciar riscos e prevenir atividades fraudulentas.

Soluções para Falhas: O aplicativo deve apresentar mensagens claras e informativas à Sra. Silva em caso de falhas durante o processo de depósito, como problemas na captura da imagem, cheque inválido ou saldo insuficiente na conta para cobrir tarifas.

Canal de Atendimento: O Banco ABC deve disponibilizar um canal de atendimento eficiente para que a Sra. Silva possa esclarecer dúvidas, obter suporte ou registrar reclamações relacionadas ao depósito por cheque via aplicativo.

Conclusão:

O depósito por cheque via aplicativo bancário oferece uma alternativa conveniente, segura e eficiente para os clientes realizarem transações financeiras. Ao implementar recursos robustos de segurança, medidas de prevenção de fraudes e canais de atendimento eficazes, o Banco ABC pode aprimorar a experiência do cliente e fortalecer a confiança na utilização do aplicativo para diversas operações bancárias.

Observações:

Este caso de uso pode ser adaptado para incluir outros tipos de depósitos, como depósitos em dinheiro ou transferências entre contas.
A interface do aplicativo e os fluxos de atividades específicos podem variar de acordo com o design e as funcionalidades da plataforma bancária.
É importante que o Banco ABC siga as normas e regulamentações bancárias aplicáveis ao oferecer serviços de depósito por cheque via aplicativo.
Este caso de uso detalhado fornece uma visão abrangente do processo de depósito por cheque via aplicativo bancário, desde a perspectiva do cliente até os registros no sistema bancário e a comunicação por mensagem. Ao considerar as pré-condições, o fluxo de atividades, as pós-condições, os benefícios, as considerações adicionais e as observações, o Banco ABC pode aprimorar o design e a implementação desse recurso no aplicativo, oferecendo uma experiência segura, conveniente e vantajosa para seus clientes.
