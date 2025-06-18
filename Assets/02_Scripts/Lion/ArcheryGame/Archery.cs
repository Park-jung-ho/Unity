using System;
using System.Collections;
using ArcheryGame;
using UnityEngine;
using Random = UnityEngine.Random;

public class Archery : MonoBehaviour
{
    public Transform target;
    public GameObject _UIcanvas;
    public float recyleTime;
    public float minDistance;
    public float maxDistance;
    public Animator anim;
    private Collider col;
    private bool isHit;

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        init();
        StartCoroutine(running());
    }

    void init()
    {
        isHit = false;
        float ranDistance = Random.Range(minDistance, maxDistance);
        Vector3 randDir = new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f,1f)).normalized * ranDistance;
        Vector3 pos = target.position + randDir;
        
        transform.position = pos;
        transform.LookAt(target);
        
        _UIcanvas.SetActive(false);
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
            GameManager.instance.getScore();
        }
    }

    IEnumerator running()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= recyleTime)
            {
                timer = 0f;
                init();
            }
            if (isHit)
            {
                col.enabled = false;
                isHit = false;
                anim.SetTrigger("atk");
                _UIcanvas.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                col.enabled = true;
                init();
                timer = 0f;
            }
            yield return null;
        }
    }
}
