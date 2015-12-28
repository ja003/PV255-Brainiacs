using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


//--MG
public class PositionGenerator : MonoBehaviour {

        private float mapStartX;
        private float mapStartY;
        private float mapHeight;
        private float mapWidth;
        private List<Rect> barriers;

        public void Start()
        {
        float eastEdge = 7.0f;
        float northEdge = 2.75f;
        float southEdge = -4.0f;
        float westEdge = -4.0f;

        GameObject[] borders;
        borders = GameObject.FindGameObjectsWithTag("Border");

        GameObject[] barrs;
        barrs = GameObject.FindGameObjectsWithTag("Barrier");
        int barrsLength = barrs.Length;

        //add not proof barriers
        GameObject[] barrs2;
        barrs2 = GameObject.FindGameObjectsWithTag("Barrier_notproof");

        Array.Resize<GameObject>(ref barrs, barrsLength + barrs2.Length);
        Array.Copy(barrs2, 0, barrs, barrsLength, barrs2.Length);


        barriers = new List<Rect>();

            for (int i = 0; i < borders.Length; i++)
            {
                if (borders[i].GetComponent<BoxCollider2D>().offset.x > 0){
                    eastEdge = borders[i].GetComponent<BoxCollider2D>().offset.x - (borders[i].GetComponent<BoxCollider2D>().size.x / 2.0f);
                }
                if (borders[i].GetComponent<BoxCollider2D>().offset.x < 0)
                {
                    westEdge = borders[i].GetComponent<BoxCollider2D>().offset.x + (borders[i].GetComponent<BoxCollider2D>().size.x / 2.0f);
                }
                if (borders[i].GetComponent<BoxCollider2D>().offset.y > 0)
                {
                    northEdge = borders[i].GetComponent<BoxCollider2D>().offset.y - (borders[i].GetComponent<BoxCollider2D>().size.y / 2.0f);
                }
                if (borders[i].GetComponent<BoxCollider2D>().offset.y < 0)
                {
                    southEdge = borders[i].GetComponent<BoxCollider2D>().offset.y + (borders[i].GetComponent<BoxCollider2D>().size.y / 2.0f);
                }
            }

            mapStartX = westEdge + 1.0f;
            mapStartY = southEdge + 1.0f;
            mapHeight = northEdge - southEdge - 2.0f;
            mapWidth = eastEdge - westEdge - 2.0f;

            for (int i = 0; i < barrs.Length; i++)
            {
                float width = barrs[i].GetComponent<BoxCollider2D>().size.x;
                float height = barrs[i].GetComponent<BoxCollider2D>().size.y;
                float x = barrs[i].GetComponent<BoxCollider2D>().offset.x - (width / 2.0f);
                float y = barrs[i].GetComponent<BoxCollider2D>().offset.y - (height / 2.0f);
                barriers.Add(new Rect(x, y, x + width, y + height));
            }
        }

        public Vector2 GenerateRandomPosition(float height, float width)
        {
            bool badPosition = false;
            Vector2 vec = new Vector3();
            System.Random rnd = new System.Random();
            
            float randX = (float)rnd.NextDouble() * mapWidth;
            vec.x = randX + mapStartX;
            
            float randY = (float)rnd.NextDouble() * mapHeight;
            vec.y = randY + mapStartY;

            for (int i = 0; i < barriers.Count; i++)
            {
                if (barriers[i].Contains(vec)
                    || barriers[i].Contains(new Vector2(vec.x + (width / 2.0f), vec.y + (height / 2.0f)))
                    || barriers[i].Contains(new Vector2(vec.x + (width / 2.0f), vec.y - (height / 2.0f)))
                    || barriers[i].Contains(new Vector2(vec.x - (width / 2.0f), vec.y + (height / 2.0f)))
                    || barriers[i].Contains(new Vector2(vec.x - (width / 2.0f), vec.y - (height / 2.0f))))
                {
                    badPosition = true;
                    break;
                }
                
            }

                if (badPosition)
                {
                    vec = GenerateRandomPosition(height, width);
                }
            return vec;
        }
    }
