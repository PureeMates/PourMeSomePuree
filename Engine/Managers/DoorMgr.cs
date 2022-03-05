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

        public static void Init(int numDoors, int numEnemiesPerDoor)
        {
            doorId = 0;
            counter = numDoors;
            doors = new Door[numDoors];

            numEnemies = numEnemiesPerDoor;
            enemyId = 0;
        }

        public static void PutDoorAt(Vector2 point)
        {
            if(counter > 0)
            {
                doors[doors.Length - counter] = new Door(doorId++, numEnemies, ref enemyId, point);
                counter--;
            }
        }
    }
}
