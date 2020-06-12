 # Roguelike

 ---
 André Figueira 21901435
 
 João Matos 21901219

 Miguel Feliciano 21904115
 
 ---

## Commits e Trabalho  Realizado  

1. **First Commit** - João Matos  
   - Criação da solução.  

2. **PrintMenu method added** - João Matos  
   - Método que imprime o menu adicionado

3. **PrintCredits method added** - João Matos  
   - Método que imprime os créditos adicionado

4. **PrintInstructions added & Command line arguments** - João Matos  
   - Método que imprime as instruções adicionado e leitura da linha de comandos

5. **Added State (Enum), Space, Board (Class)** - João Matos  
   - Adicionada a enumeração State e as classes Space e Board

6. **Board Made** - André Figueira  
   - Criação do tabuleiro de jogo

7. **Added Working Menu** - André Figueira  
   - Melhoramento do Menu e das suas funcionalidades

8. **Obstacles and Movement** - Miguel Feliciano  
   - Criação de objetos em posições aleatórias e movimentod o jogador adicionado

9.  **Enemies added and Menu fix** - Miguel Feliciano  
    - Criação de inimigos em posições aleatórias e melhoramento do Menu

10. **HP and Levels added** - João Matos  
    - Adicionado o HP do jogador e a progressão de nível

11. **Powerups Added** - João Matos
    - Adicionados os diferentes Powerups em posições aleatórias
    
12. **Minion and PowerUp BugFix part 1** - André Figueira  
    - Bugfix de Minions e Powerups pt.1

13. **Minion and PowerUp BugFix part 2** - João Matos  
    - Bugfix de Minions e Powerups pt.2

14. **Diagonal Movement Fixed** - André Figueira  
    - Bugfix de alguns movimentos dos inimigos

15. **General Movement Bug Fixing** - Miguel Feliciano  
    - Bugfix do movimento do jogador e inimigos

16. **Game Aesthetic and Enemy Actions** - Miguel Feliciano  
    - Melhoramento visual do jogo e print das ações importantes

17. **Highscores Done** - André Figueira  
    - Adicionado o Score ao jogo e lista de Highscores

18. **Report and Comments Added** - João Matos  
    Relatório feito  


## Arquitetura da Solução

Para a arquitetura da solução, o grupo decidiu que seria mais simples e fácil de realizar este projeto, fazendo todos os
objetos sendo um `Space` (Class), sendo que este `Space` se distingue com uma enumeração com `[Flags]` `State` onde os 
valoresque podem tomar são `Empty`, `Player`, `Minion`, `Boss`, `PowerupS`, `PowerupM`, `PowerupL`, `Obstacle` ou `Exit`.

Foi então feita outra classe `Board` que cria um array `Space` que vai ser o tabuleiro de jogo com o tamanho indicado 
pelo jogador nos argumentos da linha de comandos.

Contivemos então a maioria da informação no tabuleiro de jogo (Class) `Board`, onde tudo o que ocorre no jogo, modifica
os valores do mesmo tabuleiro (toma o nome de `gameBoard`).

Fizemos o programa começar por ler o número de linhas e colunas indicado, sendo que de seguida iría ler também a tabela
de highScores, caso existente. O jogo segue assim para o Menu onde se pode escolher começar um jogo novo, ver os
High Scores, ver as instruções, ver os créditos do jogo.

Começando o jogo, e criado um loop onde o jogador se pode mover e onde os inimigos se vão movendo também respeitando as
regras do jogo. O jogo termina quando o `HP` do jogador for menor ou igual a 0. É então pedido ao jogador que insira o
seu nome, para ser colocado na tabela de High Scores, caso tenha tido uma pontuação superior às presentes na tabela.


## Diagrama UML

![UML](UML.png)