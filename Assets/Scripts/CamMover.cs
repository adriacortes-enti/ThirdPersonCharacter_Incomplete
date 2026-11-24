using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CamMover : MonoBehaviour
{
    float cameraPitch;
    public float rotateSpeed;
    public Transform camPos;
    public GameObject cam;
    public float distance;
    Vector3 hitPosition;

    void Update()
    {
        RaycastHit hit;

        transform.position = new Vector3(camPos.position.x, camPos.position.y, camPos.position.z);
        Debug.DrawRay(transform.position, -transform.forward * distance, Color.red);

        if (Physics.Raycast(transform.position, -transform.forward, out hit, distance))
        {
            Debug.DrawRay(transform.position, -transform.forward * hit.distance, Color.green);
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = transform.position - transform.forward * distance;
        }

        transform.Rotate(0, Mouse.current.delta.ReadValue().x * rotateSpeed, 0);
        cameraPitch = Mathf.Clamp(cameraPitch - Mouse.current.delta.ReadValue().y * rotateSpeed, -75f, 75f);
        transform.localRotation = Quaternion.Euler(cameraPitch, transform.localEulerAngles.y, 0);

        cam.transform.position = new Vector3(hitPosition.x, hitPosition.y, hitPosition.z) + transform.right * 0.5f;
        cam.transform.rotation = transform.rotation;
    }
}
