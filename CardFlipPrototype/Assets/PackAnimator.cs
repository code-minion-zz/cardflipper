using UnityEngine;
//using UnityEditor;
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
    private bool _mayAct = false;
    private bool _reverse = false;
    private bool[] _flippedArray;

	// Use this for initialization
	void Awake () {
	    if (!instance)
	    {
	        instance = this;
	    }
	    _flippedArray = new bool[5];//{false, false, false, false, false};
        PlayNextCard();
	}

    void Update()
    {
        // allow input?
        //if (_mayAct)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                PlayNextCard();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                PlayPrevCard();
            }
        }

    }

    void PlayNextCard()
    {
        _reverse = false;
        if (_currentCard == Cards.Length) return;
        Animator currentCardAnimator = Cards[_currentCard].gameObject.GetComponent<Animator>();
        currentCardAnimator.enabled = true;
        currentCardAnimator.SetBool("Reverse", false);
        currentCardAnimator.SetTrigger("Play");

        if (currentCardAnimator.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {
            for (int i = _currentCard; i < Cards.Length - 1; ++i)
            {
                StartCoroutine(ShimmyCard(Cards[i + 1], CardPos[i - _currentCard].position, 1));
            }
        } 
        //else if (currentCardAnimator.GetCurrentAnimatorStateInfo(0).IsName("FlipInPlace"))
        //{
            
        //}
        //++_currentCard;
    }

    void PlayPrevCard()
    {
        //if (_currentCard <= 0) return;

        _reverse = true;
        Animator currentCardAnimator = Cards[_currentCard].gameObject.GetComponent<Animator>();

        if (currentCardAnimator.GetCurrentAnimatorStateInfo(0).IsName("Start"))
        {
            if (_currentCard > 0)
            {
                --_currentCard;
                PlayPrevCard();
            }
        }
        else 
        {
            currentCardAnimator.SetBool("Reverse", true);
            currentCardAnimator.SetTrigger("Play");
        }
    }

    IEnumerator ShimmyCard(Transform card, Vector3 target, float time)
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

    //IEnumerator ShimmyCardBackward(Transform card, Vector3 target, float time)
    //{
    //    Vector3 startingPos = card.position;
    //    float elapsedTime = 0;

    //    while (elapsedTime < time)
    //    {
    //        card.position = Vector3.Lerp(startingPos, target, (elapsedTime/time));
    //        elapsedTime += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

    public void StartAnim(string stateName)
    {
        if (_reverse && stateName == "FlyOut")
        {
            _mayAct = true;
            --_currentCard;
            if (_currentCard < 0)
            {
                _currentCard = 0;
                Debug.Log("Current Card:" + _currentCard);
                PlayPrevCard();
            }

        }
        else if (stateName == "FlyAway" && !_reverse)
        {
            EndSparklies();
        }
    }

    public void CompleteAnim(string stateName)
    {
        if (stateName == "FlyOut")
        {
            _mayAct = true;
        }
        else if (stateName == "FlipInPlace")
        {
            Cards[_currentCard].GetComponent<Animator>().SetBool("Flipped", true);
            if (_reverse)
            {
                EndSparklies();
            }
        }
        else if (stateName == "FlyAway")// && !_reverse)
        {
            if (!_reverse)
            {
                ++_currentCard;
                if (_currentCard >= Cards.Length)
                {
                    _currentCard = Cards.Length - 1;
                }
                Debug.Log("Current Card:" + _currentCard);
            }
            else
            {
                //EndSparklies();
                if (_currentCard == 2)
                    Particles.SetActive(true);
            }
            //_mayAct = true;
        }
    }

    public void StartSparklies()
    {
        Debug.Log("startSparklies");
        if (_currentCard == 2)
            if (_reverse)
            {
                Particles.SetActive(false);
            }
            else
            {
                Particles.SetActive(true);
            }
    }
    public void EndSparklies()
    {
        Debug.Log("EndSparklies");
        if (_currentCard == 2)
            Particles.SetActive(false);
    }
}
