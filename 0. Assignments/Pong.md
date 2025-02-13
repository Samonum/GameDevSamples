# Pong
Create an implementation of the classic game of Pong for 4 players, with a simple AI.

![4 player pong](https://m.gjcdn.net/game-screenshot/600/443803-ll-36qrtgrj-v4.webp)

### Basic Functionality of Pong:
1. Each player controlls one paddle, the paddles are placed on the top, bottom, left and right of the screen. (1p)
2. The players can move the paddle along their side of the field. (1p)
3. There is a ball that starts by moving in a random direction and bounces off the paddles. Whenever it bounces the ball speeds up. (1p)
5. If the ball misses the paddle that player gets -1 point. The scores for all players are displayed. Afterwards the ball is reset. (1p)
4. The direction in which the ball moves after bouncing is a combination of the angle at which the ball comes in, and the point of contact between the ball and the paddle. If the ball bounces closer to the edge of the paddle, it makes a sharper bounce. (1p)

### AI:
1. When the game starts, all paddles are controlled by an AI, until a players presses one of the controlls matching that paddle. (1p)
2. The AI always moves the paddle in the direction of the ball. (1p)

Grade: Sum(points)/7 * 10
