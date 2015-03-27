using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CardAnimator : MonoBehaviour {

    GameObject cardBack;
    GameObject cardFront;
    Sequence mySequence;

	// Use this for initialization
    void Start()
    {
        //cardFront = transform.FindChild("Front").gameObject;
        //cardBack = transform.FindChild("Back").gameObject;
        //cardFront.SetActive(false);

        //mySequence = DOTween.Sequence();
        //// Add a 1 second move tween only on the Y axis
        //mySequence.Append(transform.DOMoveY(2, 1));
        //// Add a 1 second rotation tween, using Join so it will start when the previous one starts
        //mySequence.Join(transform.DORotate(new Vector3(0, 135, 0), 1));
        //// Add a 1 second scale Y tween, using Append so it will start after the previously added tweens end
        //mySequence.Append(transform.DOScaleY(0.2f, 1));
        //// Add an X axis relative move tween that will start from the beginning of the Sequence
        //// and last for the whole Sequence duration
        //mySequence.Insert(0, transform.DOMoveX(4, mySequence.Duration()).SetRelative());
        //// Oh, and let's also make the whole Sequence loop backward and forward 4 times
        //mySequence.SetLoops(-1, LoopType.Yoyo);
	}
	
	// Update is called once per frame
	void Update () {
        OnRotate();

	}

    void OnRotate()
    {
        //Debug.Log(transform.rotation.y);
    //    if (transform.rotation.y > 0.7f)
    //    {
    //        cardFront.SetActive(true);
    //        cardBack.SetActive(false);
    //    }
    //    else
    //    {
    //        cardFront.SetActive(false);
    //        cardBack.SetActive(true);
    //    }
    }
}
