# 2 Space Defence
Finish the Space defence game.

The Space Defence game is a game in which the player controls a space ship. In the game the player can shoot bullets at aliens theat are invading earth to defeat them. The player can get a temporary upgrade by shooting down a crate. Hitting the crate will turn the ship's cannon into a laser. The player shoots by clicking where they want to aim at. (Aiming to be implemented)

Use the Game found [here](https://github.com/Samonum/GameDevSamples/tree/main/3.%20Space%20Defence) as the base. You may add extra methods, but do not change the signature (method name, parameters or return type) of the methods given in the template. Wherever applicable make proper use of GameTime.

Finish the game by adding the following:

### Functionality:
Implement the following functionality:

The spaceship uses the following static methods from the `LinePieceCollider` class to aim. Implement both methods:
1. `float GetAngle(Vector2 direction)` (.5 p)
2. `Vector2 GetDirection(Vector2 point1, Vector2 point2)` (.5p)

Add player movement:
1. In space, momentum is conserved. When the player presses one of the WASD keys, accelerate the ship in the matching direction.  Make sure the player accelerates with the same speed when moving diagonally and when moving orthogonally (vertical or horizontal). (1p)
2. Rotate the space ship in the direction it last accelerated. (1p)
3. Make the screen wrap. Whenever the ship hits the edge of the screen, teleport it to the other side of the screen. (1p)

Add movement to the enemies:
1. Make the Aliens chase the player. Every time the alien dies, a new alien spawns that should move faster than the previous. (.5p)
2. When the alien comes within a certain range of the player, the player is game over. (.5p)

### Collision:
In the Collision folder are several collider classes for different shapes. Rectangles, circles and line pieces, but the intersection logic is still missing.
The Circle collider describes a circle using a location, described by 2 floats for the X and Y coordinates respectively and a radius. Everything that is less than the Radius away from the circle is considered to be within the circle.
In the `CircleCollider` class add:
1. Logic to calculate the intersection between two circles in the `Intersects(CircleCollider other)` method. (1p)
2. Logic to calculate the intersection between a circle and a rectangle in the `Intersects(RectangleCollider other)` method. (1p)

A LinePiece is described by two Vector2: Start and End. Everything that is on the line between Start and End is on the Line. 
In the `LinePieceCollider` class add:
1. Logic to calculate the intersection between a line piece and a circle in the `Intersects(CircleCollider other)` method. (Hint: start by implementing the `NearestPointOnLine` method) (1.5p)
2. Logic to calculate the intersection between a line piece and a rectangle in the `Intersects(RectangleCollider other)` method. (Hint: start by implementing the standard line formula method) (1.5p)

### Bonus:
1. Create a collider that neatly follows the space ship as it rotates. Instead of losing when the aliens come within a certain distance, the player loses when the alien hits the collider. (1p)

The grade for the assignment is equal to the total points (max 11 including bonus)
