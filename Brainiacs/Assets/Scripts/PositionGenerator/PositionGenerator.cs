using UnityEngine;
using System.Collections;


//--MG
namespace Brainiacs.Generate
{
    public class PositionGenerator {

        private static float mapStartX = -4.0f;
        private static float mapStartY = -4.0f;
        private static float mapHeight = 6.75f;
        private static float mapWidth = 11.0f;

        public static Vector3 GenerateRandomPosition()
        {
            Vector3 vec = new Vector3();
            System.Random rnd = new System.Random();
            
            float randX = (float)rnd.NextDouble() * mapWidth;
            vec.x = randX + mapStartX;
            
            float randY = (float)rnd.NextDouble() * mapHeight;
            vec.y = randY + mapStartY;
            
            vec.z = 0;
            
            //check ci tam uz nieco neni
            var hitColliders = Physics.OverlapSphere(vec, 1);
            if (hitColliders.Length > 1.0)
            {
                vec = GenerateRandomPosition();
            }
            return vec;
        }
    }
}
