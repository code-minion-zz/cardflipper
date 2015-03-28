using UnityEngine;
using System.Collections;

public class PackAnimator : MonoBehaviour
{
    private static PackAnimator instance;

    public static PackAnimator Instance
    {
        get { return instance; }
        set { instance = value; }
    }
    

    public Transform[] CardPos;
    public Transform[] Cards;
    public GameObject GreyOutPlane;
    public GameObject Particles;
    private int _currentCard = 0;

	// Use this for initialization
	void Awake () {
	    if (!instance)
	    {
	        instance = this;
	    }
        PlayNextCard();
	}

    void PlayNextCard()
    {
        if (_currentCard == Cards.Length) return;
        Cards[_currentCard].gameObject.GetComponent<Animator>().enabled = true;
        for (int i = _currentCard; i < Cards.Length-1; ++i)
        {
            StartCoroutine(ShimmyCardForward(Cards[i + 1], CardPos[i].position, 1));
        }
        ++_currentCard;
    }

    IEnumerator ShimmyCardForward(Transform card, Vector3 target, float time)
    {
        Vector3 startingPos = card.position;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            card.position = Vector3.Lerp(startingPos, target, (elapsedTime/time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public void AnimDone()
    {
        if (_currentCard == 2)
        {
            Particles.SetActive(true);
        }
        else
        {
            Particles.SetActive(false);
        }
        PlayNextCard();
    }
}
