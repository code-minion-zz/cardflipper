using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CardAnimator : MonoBehaviour
{

	
	// Update is called once per frame
	void Update () {

	}


    void CompleteAnim(string name)
    {
        Debug.Log("CompleteAnim:"+name);

        PackAnimator.Instance.CompleteAnim(name);
        
    }

    void StartAnim(string name)
    {
        Debug.Log("StartAnim:"+name);

        PackAnimator.Instance.StartAnim(name);
    }

    void StartSparklies()
    {
        PackAnimator.Instance.StartSparklies();
    }

    void EndSparklies()
    {
        PackAnimator.Instance.EndSparklies();
    }
}
