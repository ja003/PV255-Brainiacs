using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


//--MG
public class PositionGenerator : MonoBehaviour
{
    private float mapStartX;
    private float mapStartY;
    private float mapHeight;
    private float mapWidth;
    private List<Rect> barriers;
    private List<Vector2> positions;
    private const float range = 0.8f;

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
        positions = new List<Vector2>();

        for (int i = 0; i < borders.Length; i++)
        {
            if (borders[i].GetComponent<BoxCollider2D>().offset.x > 0)
            {
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

        mapStartX = westEdge +1.0f;
        mapStartY = southEdge +1.0f;
        mapHeight = northEdge - southEdge -2.0f;
        mapWidth = eastEdge - westEdge -2.0f;

        for (int i = 0; i < barrs.Length; i++)
        {
            BoxCollider2D[] colliders = barrs[i].GetComponents<BoxCollider2D>();
            foreach (BoxCollider2D boxcol in colliders)
            {
                float width = boxcol.size.x;
                float height = boxcol.size.y;
                float x = barrs[i].transform.position.x + boxcol.offset.x - (width / 2.0f);
                float y = barrs[i].transform.position.y + boxcol.offset.y - (height / 2.0f);
                barriers.Add(new Rect(x, y, width, height));
            }
        }

        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                Vector2 tmp = new Vector2(mapStartX + j, mapStartY + i);
                bool contains = false;
                for (int k = 0; k < barriers.Count; k++)
                {
                    if (barriers[k].Contains(tmp)
                        || barriers[k].Contains(new Vector2(tmp.x + range, tmp.y + range))
                        || barriers[k].Contains(new Vector2(tmp.x + range, tmp.y - range))
                        || barriers[k].Contains(new Vector2(tmp.x - range, tmp.y + range))
                        || barriers[k].Contains(new Vector2(tmp.x - range, tmp.y - range))
                        || barriers[k].Contains(new Vector2(tmp.x + range / 2.0f, tmp.y + range))
                        || barriers[k].Contains(new Vector2(tmp.x + range / 2.0f, tmp.y - range))
                        || barriers[k].Contains(new Vector2(tmp.x - range / 2.0f, tmp.y + range))
                        || barriers[k].Contains(new Vector2(tmp.x - range / 2.0f, tmp.y - range))
                        || barriers[k].Contains(new Vector2(tmp.x + range, tmp.y + range / 2.0f))
                        || barriers[k].Contains(new Vector2(tmp.x + range, tmp.y - range / 2.0f))
                        || barriers[k].Contains(new Vector2(tmp.x - range, tmp.y + range / 2.0f))
                        || barriers[k].Contains(new Vector2(tmp.x - range, tmp.y - range / 2.0f))
                        || barriers[k].Contains(new Vector2(tmp.x + range / 2.0f, tmp.y + range / 2.0f))
                        || barriers[k].Contains(new Vector2(tmp.x + range / 2.0f, tmp.y - range / 2.0f))
                        || barriers[k].Contains(new Vector2(tmp.x - range / 2.0f, tmp.y + range / 2.0f))
                        || barriers[k].Contains(new Vector2(tmp.x - range / 2.0f, tmp.y - range / 2.0f)))
                    {
                        contains = true;
                        break;
                    }
                }
                if (!contains)
                {
                    positions.Add(tmp);
                }
            }
        }
        /*
        List<GameObject> weapons = new List<GameObject>();
        GameObject tmp2 = (GameObject)Resources.Load("Prefabs/SpawnItems/PowerUps/square");
        for (int j = 0; j < positions.Count; j++)
        {
            GameObject obj = (GameObject)Instantiate(tmp2);
            obj.transform.position = positions[j];
            obj.SetActive(true);
            weapons.Add(obj);
        }
        */
    }

    public Vector2 GenerateRandomPosition()
    {
        return positions[UnityEngine.Random.Range(0, positions.Count)];
    }
}
