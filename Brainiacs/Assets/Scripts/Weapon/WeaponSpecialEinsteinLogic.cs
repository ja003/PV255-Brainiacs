using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSpecialEinsteinLogic : MonoBehaviour {

    private BulletManager bulletManager;
    private PlayerBase playerBase;


    private SpriteRenderer renderer;
    public SpriteRenderer childRenderer;
    public Transform WhiteT;
    private Animator animator;

    private FireProps fireProps;
    private bool update = false;

    private Vector2 impactPosition;
    private bool impact = false;
    private Vector2 prevPosition = new Vector2();
    private float x;
    private float alpha;

    private float traveledDistance = 0;
    private float clicksShooted = 0;
    private float shootEveryDst = 0.75f;

 
    public void SetUpVariables(PlayerBase pb, BulletManager bm)
    {
        //WhiteT = transform.Find("WhiteT", false);
        //childRenderer = GetComponentInChildren<SpriteRenderer>();
        WhiteT.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Transparent");
        playerBase = pb;
        bulletManager = bm;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    
}
