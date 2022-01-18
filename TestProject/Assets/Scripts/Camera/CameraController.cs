using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float offsetX;
    private float offsetZ;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        offsetX = this.transform.position.x;
        offsetZ = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + offsetX, transform.position.y, player.transform.position.z + offsetZ);
    }
}
