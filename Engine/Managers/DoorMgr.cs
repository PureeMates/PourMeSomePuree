using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourMeSomePuree
{
    static class DoorMgr
    {
        private static Door[] doors;
        private static int counter;
        private static int doorId;

        private static int numEnemies;
        private static int enemyId;

        public static int TotalEnemies;

        public static void Init(int numDoors, int numEnemiesPerDoor)
        {
            doorId = 0;
            counter = numDoors;
            doors = new Door[numDoors];

            numEnemies = numEnemiesPerDoor;
            enemyId = 0;

            TotalEnemies = numDoors * numEnemiesPerDoor;
        }

        public static void CreateAndPutDoorAt(Vector2 point)
        {
            if(counter > 0)
            {
                doors[doors.Length - counter] = new Door(doorId++, numEnemies, ref enemyId, point);
                counter--;
            }
        }

        public static void Update()
        {
            if(TotalEnemies <= 0)
            {
                Init(4, ++((PlayScene)SceneMgr.CurrentScene).Level);

                DoorMgr.CreateAndPutDoorAt(new Vector2(Game.Win.Width * 0.5f, 80 - 21.5f));
                DoorMgr.CreateAndPutDoorAt(new Vector2(Game.Win.Width * 0.5f, Game.Win.Height - 80 + 21.5f));
                DoorMgr.CreateAndPutDoorAt(new Vector2(80 - 21.5f, Game.Win.Height * 0.5f));
                DoorMgr.CreateAndPutDoorAt(new Vector2(Game.Win.Width - 80 + 21.5f, Game.Win.Height * 0.5f));
            }
        }
    }
}
