using UnityEngine;
using System.Collections;

namespace Brainiacs.Generate
{

    public class PositionGenerator {

        private float mapStartX = -4.0f;
        private float mapStartY = -4.0f;
        private float mapHeight = 6.75f;
        private float mapWidth = 11.0f;

        public Vector3 GenerateRandomPosition()
        {
            Vector3 vec = new Vector3();
            System.Random rnd = new System.Random();
            float randX = (float)rnd.NextDouble() * mapWidth;
            vec.x = randX + mapStartX;
            Debug.Log("vec X " + vec.x);
            float randY = (float)rnd.NextDouble() * mapHeight;
            vec.y = randY + mapStartY;
            Debug.Log("vec Y " + vec.y);
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
