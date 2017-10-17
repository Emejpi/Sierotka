using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsSmoothTransition : MonoBehaviour {

    public Animation anim;
    public string startAnim;
    public string endAnim;

    void Start()
    {

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
            GetComponent<Animator>().CrossFade(startAnim, 5);
    }

}
