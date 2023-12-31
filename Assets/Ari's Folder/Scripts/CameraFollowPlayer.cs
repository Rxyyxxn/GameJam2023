using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public float followSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;
    // Update is called once per frame
    void Update()
    {
        target = FindObjectOfType<PlayerMove>().transform;
        Vector3 newPos = new Vector3(target.position.x, target.position.y - yOffset, -5f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
