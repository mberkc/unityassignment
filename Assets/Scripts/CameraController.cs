using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 camStart;

    void Start()
    {
        camStart = this.transform.position - player.transform.position;
    }

    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z + camStart.z);
    }
}
