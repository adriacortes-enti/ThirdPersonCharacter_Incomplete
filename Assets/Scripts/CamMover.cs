using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CamMover : MonoBehaviour
{
    float cameraPitch;
    public float rotateSpeed;
    public float lerpSpeed;
    public Transform camPos;
    public GameObject cam;
    public float distance;
    float currentDistance;
    Vector3 hitPosition;

    private void Start()
    {
        hitPosition = transform.position;
    }

    void Update()
    {
        RaycastHit hit;
        float targetDistance;

        transform.position = new Vector3(camPos.position.x, camPos.position.y, camPos.position.z);
        Debug.DrawRay(transform.position, -transform.forward * distance, Color.red);

        if (Physics.Raycast(transform.position, -transform.forward, out hit, distance))
        {
            Debug.DrawRay(transform.position, -transform.forward * hit.distance, Color.green);
            targetDistance = hit.distance;
        }
        else
        {
            targetDistance = distance;
        }

        if (targetDistance < currentDistance)
        {
            currentDistance = targetDistance;
        }
        else
        {
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, lerpSpeed * Time.deltaTime);
        }

        hitPosition = transform.position - transform.forward * currentDistance;

        transform.Rotate(0, Mouse.current.delta.ReadValue().x * rotateSpeed, 0);
        cameraPitch = Mathf.Clamp(cameraPitch - Mouse.current.delta.ReadValue().y * rotateSpeed, -75f, 75f);
        transform.localRotation = Quaternion.Euler(cameraPitch, transform.localEulerAngles.y, 0);

        cam.transform.position = new Vector3(hitPosition.x, hitPosition.y, hitPosition.z) + transform.right * 0.5f;
        cam.transform.rotation = transform.rotation;
    }
}
