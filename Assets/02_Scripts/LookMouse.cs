using UnityEngine;

public class LookTarget : MonoBehaviour
{
    public TargetType _type;
    public Transform _target;
    void Update()
    {
        lookTarget();
    }

    void lookTarget()
    {
        switch (_type)
        {
            case TargetType.Player:
                break;
            case TargetType.Mouse:
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Plane plane = new Plane(Vector3.up, Vector3.up);
                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 direction = ray.GetPoint(distance) - transform.position;
                    transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                }
                break;
            case TargetType.Camera:
                break;
            default:
                break;
        }
    }
}

public enum TargetType
{
    Player,
    Mouse,
    Camera,
}
