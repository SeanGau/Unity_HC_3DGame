using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float turn = 50f;
    public Vector2 limit = new Vector2(30, -20);

    private Quaternion rot;
    void Awake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Track()
    {
        Vector3 posA = transform.position;
        Vector3 posB = target.position;

        transform.position = Vector3.Lerp(posA, posB, Time.deltaTime * speed);

        rot.x -= Input.GetAxis("Mouse Y") * turn * Time.deltaTime;
        rot.y += Input.GetAxis("Mouse X") * turn * Time.deltaTime;

        rot.x = Mathf.Clamp(rot.x, limit.y, limit.x);
        transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z);
    }
    void LateUpdate()
    {
        Track();
    }
}
