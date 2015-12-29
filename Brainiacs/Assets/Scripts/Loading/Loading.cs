using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {

    // Use this for initialization
    private SpriteRenderer sr;

    private AudioClip beep2;
    private AudioClip beep3;
    private AudioClip beep4;
    
    private int frameCountSinceLvlLoad;

    void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_background_01");

        beep2 = Resources.Load<AudioClip>("Sounds/Loading/beep2");
        beep3 = Resources.Load<AudioClip>("Sounds/Loading/beep2");
        beep4 = Resources.Load<AudioClip>("Sounds/Loading/beep2");
        
        frameCountSinceLvlLoad = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if(frameCountSinceLvlLoad == 50)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_background_02");
            //beep
            SoundManager.instance.PlaySingle(beep2);
        }

        if (frameCountSinceLvlLoad == 100)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_background_03");
            //beep
            SoundManager.instance.PlaySingle(beep3);

        }

        if (frameCountSinceLvlLoad == 150)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_background_04");
            //beep
            SoundManager.instance.PlaySingle(beep4);

        }

        //chvilku trvá...případně posunout
        if(frameCountSinceLvlLoad == 10)//fastload
        {
            GameInfo gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
            Application.LoadLevel(gameInfo.mapName);
        }

        frameCountSinceLvlLoad++;

    }
}
