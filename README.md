# The Squid Card Game

# Definição de contexto de jogo

O tema seguiu a definição dos personagens, que para demonstração, e considerando usar artes simples, foram utilizadas formas geométricas. Após a definição dos personagens, a motivação dos personagens foi colocada em pauta, correlacionando com os soldados de forma geométrica da hierarquia da Série *Round 6*, também conhecida como *Squid Game.* Baseando-se nos elementos mencionados foram decididos os valores de ataque e defesa de cada um dos personagens, de forma que personagens de menor escala hierárquica na série, tivessem menor poder e defesa no jogo, da mesma forma, fazendo com que nenhum personagem de menor hierarquia vença seu superior e sobreviva.

# Atributos dos personagens

Segundo o documento de requisitos, cada personagem deve ter um ataque e uma defesa, porém levanta a mecânica de comparação de ataque apenas. Para não deixar elementos da jogabilidade sem atribuições, cada personagem tem uma contagem de Ataque e de Vida, de forma que cada batalha que a carta participa, o ataque do seu adversário é diminuído de sua vida, chegando então no ponto de morrer ao sua vida ser menor ou igual a 0.

# Aos assets utilizados

As artes utilizadas para cada carta foram feitos pelo autor através de pixel art pelo site [Pixilart - Free online pixel art drawing tool](https://www.pixilart.com/draw#)

![Circle Soldier Pixel Art](https://user-images.githubusercontent.com/82400557/215758806-ff2c06a6-bf3e-4ee4-9dd1-4300ba5152eb.png)

Circle Soldier Pixel Art

Todas os assets restantes vieram de páginas de imagens grátis como ShutterStock e a loja de Assets do Untiy (Explosões e sons).

# A mecânica de compra e jogada

Como um protótipo, a mecânica de compra e criação de decks foi feita utilizando um objeto do Unity que continha um array para as cartas no deck do jogador e do adversário. As cartas não tem custo de mana pois a implementação do custo de mana diminiu a fluidez desse protótipo.

![Objeto responsável pela definição das cartas no baralho de cada jogador](https://user-images.githubusercontent.com/82400557/215758923-74b79681-c672-4e7d-b3ae-b7d607131eca.png)

Objeto responsável pela definição das cartas no baralho de cada jogador

Além disso, a mecânica PVP Local apesar de ir contra os fundamentos dos jogos de cartas, onde não se sabe a mão do adversário, pode ser transformada em uma mecânica online que aumenta a imersão e a tensão sobre cada jogada sua e do seu oponente.

A implementação da mecânica PVP Local muda de jogador a cada carta jogada, de forma que pode ser utilizada a mecânica “passar para jogar”.

# Definições técnicas

A utilização de Scriptable Objects como containeres de dados para as cartas facilita a sua criação e a forma como os dados são inseridos em cada objeto dinâmicamente através do próprio editor, de forma que mesmo sem conhecimento do código que roda por trás, um PM possa mudar os dados dos objetos e ver sua resposta dinâmicamento no jogo local.

![Instância do Scriptable Object “Card”. Responsável pela definição dos dados de cada uma das cartas.](https://user-images.githubusercontent.com/82400557/215759249-2c5de428-6279-4857-8ca2-905018c00912.png)


Instância do Scriptable Object “Card”. Responsável pela definição dos dados de cada uma das cartas.

A separação da lógica de cada parte essencial do jogo em diferentes Managers facilita a manutenção e o teste de cada uma das funcionalidades separadamente, como também delega funções específicas e expões dados e eventos relacionados apenas aos interessados pelos seus atributos.

![Untitled 3](https://user-images.githubusercontent.com/82400557/215759389-efc1ef90-98da-4b7a-886d-51155b06f398.png)


A técnica EDD (Event Driven Development) possibilita a independência dos códigos e diminui a quantidade de código refatorado em caso de mudança de regras.

# Aos elementos de jogabilidade

Todas as ações do jogo tem respostas visuais às interações ocorridas, de forma que o jogador saiba intuitivamente as ações que pode tomar com os elementos do jogo e de resposta, como arrastar a carta para jogar, e declarar o ataque de uma carta à outra.

![Indicador de ataque de intenção de combate](https://user-images.githubusercontent.com/82400557/215759470-d8c979a7-1ca2-4569-bfc2-870f83479ec1.png)


Indicador de ataque de intenção de combate

# Disposições finais

Os códigos fontes do jogo se encontram no endereço https://github.com/1Baldasso/Teste-0xgs/

O autor não tem propriedade sobre os personagens retratados nesse jogo.
