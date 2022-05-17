using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    
    // Update is called once per frame
    public float speed=1;
    public float mouseSpeed=1;
    void Update()
    {
        float H = Input.GetAxisRaw("Horizontal");//ˮƽ����0~1֮�䲨��
        float V = Input.GetAxisRaw("Vertical");
        float mouse = Input.GetAxisRaw("Mouse ScrollWheel");//������
        transform.Translate(new Vector3(V * speed, -mouse * mouseSpeed, -H* speed) *Time.deltaTime,Space.World);//Space.World�������������ƶ�
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(-Input.GetAxis("Mouse Y") * speed/3, 0, 0);
        }
    }
}
