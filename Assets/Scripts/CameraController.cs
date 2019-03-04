using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] float panSpeed = 30f;
    [SerializeField] float panBorderThickness = 10f;
    [SerializeField] float rotSpeed = 30f;
    [SerializeField] float scrollSpeed = 5f;

    [Header("Clamp Attributes")]

    [SerializeField] float minY = 10f;
    [SerializeField] float maxY = 80f;

    [SerializeField] float minX = -60f;
    [SerializeField] float maxX = 150f;

    [SerializeField] float minZ = -120f;
    [SerializeField] float maxZ = 5f;

    void Update()
    {
        if(GameManager.GameIsOver)
        {
            this.enabled = false;
        }


        #region Movement
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        #endregion

        #region Rotation

        /*
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(-Vector3.up * rotSpeed * Time.deltaTime, Space.World);
        }
        */

        #endregion

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}