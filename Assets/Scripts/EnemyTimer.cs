using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimer : MonoBehaviour
{

    [SerializeField] int turns=3;
    public int stepCounter;
    Animator anim;
    [SerializeField]  HoldingPointAI holdingPointAI;
    [SerializeField] Color attackColor;
    [SerializeField] Color idleColor;
    [SerializeField] SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("bulletTimer",0, 0);
        anim.Update(0f);
    }

    public void Shoot() {

        spriteRenderer.color = idleColor;
        holdingPointAI.Shoot();
       
    }

    public void AdvanceFrame() {
        stepCounter++;
        if(stepCounter == turns)
            spriteRenderer.color = attackColor;
        if (stepCounter-1 >= turns)
        {

            stepCounter = 0;
            Shoot();
        }
        float frame = (float)stepCounter/turns;
        if (frame == 1)
            frame = 0.99f;
        anim.Play("bulletTimer", 0, frame);
    }
}
