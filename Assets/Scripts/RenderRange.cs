using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class RenderRange : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    //public float xradius = 5;
    //public float yradius = 5;

    public float radius = 10;
    LineRenderer line;

    private void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
    }

    public void SetPoints(float r)
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * r;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * r;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }
}