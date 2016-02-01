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
    void FixedUpdate()
    {
        if (update && !impact)
        {
            transform.position = new Vector2(x, ComputeDescendiing(x));

            Vector2 movmentVector = new Vector2(transform.position.x - prevPosition.x, transform.position.y - prevPosition.y);
            float angle = Vector2.Angle(movmentVector, new Vector2(0, -1));
            transform.rotation = Quaternion.Euler(0, 0, -angle);

            prevPosition = transform.position;
            x -= 0.01f;

            if (transform.position.y <= impactPosition.y)
            {
                impact = true;
                StartCoroutine(PlayCrash());
                //WhiteT.gameObject.SetActive(true);
            }
        }

        if (impact)
        {
            childRenderer.material.color = new Color(1, 1, 1, alpha);
            childRenderer.color = new Color(1, 1, 1, alpha);
            alpha -= 0.01f;
        }
    }

    public void fire(FireProps fp)
    {
        // set flags and properties
        fireProps = fp;
        update = true;
        impact = false;
        alpha = 1;
        x = 1;

        // set animator state to begin
        nullAllAnimBools();
        animator.SetBool("exitBoom", true);
        

        // set iniciate position
        SetIniciatePosition();
        setBoolAnimator("iniciate", true);


    }

    void SetIniciatePosition()
    {
        impactPosition =
            GameObject.Find("PositionGenerator")
                .transform.GetComponent<PositionGenerator>()
                .GenerateRandomPosition();
        transform.position = impactPosition + new Vector2(1, 7);
        prevPosition = new Vector2(transform.position.x, transform.position.y);
    }

    float ComputeDescendiing(float x)
    {
        float p = Mathf.Pow(x - 1.5f, 4);
        return (-p + 5.0f);
    }

    IEnumerator PlayCrash()
    {
        setBoolAnimator("boom", true);
        KillInRange();
        yield return new WaitForSeconds(0.5f);                                               // tento prikaz kurvy cely kod
        animator.SetBool("exitBoom", true);
        nullAllAnimBools();
        WhiteT.gameObject.SetActive(false);
        gameObject.SetActive(false);

        //animator.runtimeAnimatorController = null;
    }

    void KillInRange()
    {
        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("Player"))
        {
            float dst = (fooObj.transform.position - transform.position).magnitude;

            if (dst <= 4.0)
            {
                fooObj.GetComponent<PlayerBase>().ApplyDamage((int)((1 - dst / 4.0f) * 101.0f), playerBase);
            }
        }
    }

    private void setBoolAnimator(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }

    private void nullAllAnimBools()
    {
        animator.SetBool("iniciated", false);
        animator.SetBool("boom", false);
        animator.SetBool("exitBoom", false);
    }
}
