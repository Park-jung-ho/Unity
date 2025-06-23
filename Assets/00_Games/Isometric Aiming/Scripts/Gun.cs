using System.Collections;
using UnityEngine;
using IsometricAiming;

namespace IsometricAiming
{
    public class Gun : MonoBehaviour, IDropItem
    {
        public Transform shotPos;
        public GameObject bullet;
        public float speed;
        public float duration;
        public void Grab(Transform grapPos)
        {
            transform.SetParent(grapPos);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            Debug.Log("총을 주웠다.");
        }

        public void Use()
        {
            Debug.Log("총을 발사한다.");
            GameObject bulletClone = Instantiate(bullet, shotPos.position, shotPos.rotation);
            StartCoroutine(nameof(bulletCoroutine), bulletClone);
        }

        IEnumerator bulletCoroutine(GameObject bulletClone)
        {
            bulletClone.SetActive(true);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.up);
            Vector3 direction = Vector3.zero;
            if (plane.Raycast(ray, out float distance))
            {
                direction = ray.GetPoint(distance) - transform.position;
            }
            direction.Normalize();
            bulletClone.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
            float coolTime = duration;
            while (coolTime > 0f)
            {
                coolTime -= Time.deltaTime;
                yield return null;
            }
            Destroy(bulletClone);
        }

        public void Drop()
        {
            transform.SetParent(null);
            float rx = Random.Range(-10.0f, 10.0f);
            float rz = Random.Range(-10.0f, 10.0f);
            transform.position = new Vector3(rx, 1, rz);
            Debug.Log("총을 버렸다.");
        }
    }
}