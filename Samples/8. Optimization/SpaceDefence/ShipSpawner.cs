using System;
using Microsoft.Xna.Framework;

namespace SpaceDefence
{
    public static class ShipSpawner
    {
        // The amount of ships that will be spawned. Starts at 4x5 = 20 per faction.
        // Increase this as you go, make sure your FPS is less than ~10 before optimizing.
        // I will keep increasing the number of columns until your game breaks during evaluation.
        public static int ShipRows = 4;
        public static int ShipColumns = 5;



        public static int XSpacing = 70;
        public static int YSpacing = 200;

        public static void Spawn(GameManager manager)
        {
            Random r = new Random(7);
            // Place the player at the center of the screen
            for (int i = 0; i < ShipRows; i++)
            {
                for (int j = 0; j < ShipColumns; j++)
                {
                    Point team1Pos = new Point(r.Next(20) + 200 + j * XSpacing * ShipRows + i * XSpacing, r.Next(20) + 200 + i * YSpacing);
                    Point team2Pos = new Point(r.Next(20) + 200 + j * XSpacing * ShipRows + i * XSpacing, 2000 + r.Next(20) + 200 + i * YSpacing);
                    Ship player = new Ship(team1Pos, CollisionType.Team1, Color.Red);
                    Ship player2 = new Ship(team2Pos, CollisionType.Team2, Color.Blue);
                    manager.AddGameObject(player);
                    manager.AddGameObject(player2);
                }
            }
        }
    }
}
