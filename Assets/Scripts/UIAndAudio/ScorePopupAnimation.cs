using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePopupAnimation : MonoBehaviour
    
{
    public Animator animator;
   // private Image scorePopup;
    // Start is called before the first frame update
    void Start()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
       // scorePopup = animator.GetComponent<Image>();
        
    }
   

    
}
