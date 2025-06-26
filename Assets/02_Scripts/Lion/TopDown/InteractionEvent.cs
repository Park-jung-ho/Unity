using System;
using System.Collections;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    public enum InteractionType
    {
        SIGN,
        DOOR,
        NPC,
    }
    public InteractionType type;
    public GameObject PopUp;
    public Animator animator;
    public FadeRoutine fadeRoutine;
    public GameObject MapRoot;
    public GameObject House;

    public Vector3 inPos;
    public Vector3 outPos;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Interaction(other.transform);
        }        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PopUp.SetActive(false);
        }        
    }

    void Interaction(Transform player)
    {
        switch (type)
        {
            case InteractionType.SIGN:
                PopUp.SetActive(true);
                animator.Play("SignOn");
                break;
            case InteractionType.DOOR:
                StartCoroutine(DoorRoutin(player));
                break;
            case InteractionType.NPC:
                PopUp.SetActive(true);
                animator.Play("Open");
                break;
            default:
                break;
        }
    }

    IEnumerator DoorRoutin(Transform player)
    {
        // 코루틴 끝날때까지 대기
        yield return StartCoroutine(fadeRoutine.Fade(1f,Color.black, true));
        
        if (player.transform.position.y < (inPos.y + outPos.y) / 2)
        {
            player.transform.position = inPos;
            MapRoot.SetActive(false);
            House.SetActive(true);
        }
        else
        {
            player.transform.position = outPos;
            MapRoot.SetActive(true);
            House.SetActive(false);
        }
        StartCoroutine(fadeRoutine.Fade(1f,Color.black, false));
    }
}
