using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform roomEnterPoint;
    private Animator blackOverlayAnimator;
    private GameObject player;

    void Start()
    {
        blackOverlayAnimator = GameObject.Find("Black Overlay").GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            player = trig.gameObject;
            StartCoroutine(EnterNewRoom());
        }
    }

    IEnumerator EnterNewRoom()
    {
        blackOverlayAnimator.SetTrigger("Black Out");

        yield return new WaitForSeconds(0.5f);

        blackOverlayAnimator.SetTrigger("Black In");
        player.transform.position = roomEnterPoint.position;
    }
}
