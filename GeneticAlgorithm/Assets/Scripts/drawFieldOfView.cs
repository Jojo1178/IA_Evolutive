using UnityEngine;
using System.Collections;

public class drawFieldOfView : MonoBehaviour
{
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
    int quality = 15;
    Mesh mesh;
    public Material material;
    public Material detectionMaterial;

    public Material currentMaterial;

    float angle_fov = 40;
    public float m_halfConeSize = 40f;
    float dist_min = 0.5f;
    float dist_max = 10.0f;

    public bool detectionMode = false;

    public IANavigationScript iaNavigationScript;

    public PointOfInterest pointOfInterestScript;

    MeshCollider mc;

    void Start()
    {
        //iaNavigationScript = GetComponent<IANavigationScript>();
        print(iaNavigationScript);
        mesh = new Mesh();
        mesh.vertices = new Vector3[4 * quality];
        mesh.triangles = new int[3 * 2 * quality];

        Vector3[] normals = new Vector3[4 * quality];
        Vector2[] uv = new Vector2[4 * quality];

        for (int i = 0; i < uv.Length; i++)
            uv[i] = new Vector2(0, 0);
        for (int i = 0; i < normals.Length; i++)
            normals[i] = new Vector3(0, 1, 0);

        mesh.uv = uv;
        mesh.normals = normals;
    }

    void Update()
    {
        float angle_lookat = GetEnemyAngle();

        float angle_start = angle_lookat - angle_fov;
        float angle_end = angle_lookat + angle_fov;
        float angle_delta = (angle_end - angle_start) / quality;

        float angle_curr = angle_start;
        float angle_next = angle_start + angle_delta;

        Vector3 pos_curr_min = Vector3.zero;
        Vector3 pos_curr_max = Vector3.zero;

        Vector3 pos_next_min = Vector3.zero;
        Vector3 pos_next_max = Vector3.zero;

        Vector3[] vertices = new Vector3[4 * quality];   // Could be of size [2 * quality + 2] if circle segment is continuous
        int[] triangles = new int[3 * 2 * quality];

        for (int i = 0; i < quality; i++)
        {
            Vector3 sphere_curr = new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * (angle_curr)), 0,   // Left handed CW
            Mathf.Cos(Mathf.Deg2Rad * (angle_curr)));

            Vector3 sphere_next = new Vector3(
            Mathf.Sin(Mathf.Deg2Rad * (angle_next)), 0,
            Mathf.Cos(Mathf.Deg2Rad * (angle_next)));

            pos_curr_min = transform.position + sphere_curr * dist_min;
            pos_curr_max = transform.position + sphere_curr * dist_max;

            pos_next_min = transform.position + sphere_next * dist_min;
            pos_next_max = transform.position + sphere_next * dist_max;

            int a = 4 * i;
            int b = 4 * i + 1;
            int c = 4 * i + 2;
            int d = 4 * i + 3;

            vertices[a] = pos_curr_min;
            vertices[b] = pos_curr_max;
            vertices[c] = pos_next_max;
            vertices[d] = pos_next_min;

            triangles[6 * i] = a;       // Triangle1: abc
            triangles[6 * i + 1] = b;
            triangles[6 * i + 2] = c;
            triangles[6 * i + 3] = c;   // Triangle2: cda
            triangles[6 * i + 4] = d;
            triangles[6 * i + 5] = a;

            angle_curr += angle_delta;
            angle_next += angle_delta;

        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        if (detectionMode)
            Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, detectionMaterial, 0);
        else
            Graphics.DrawMesh(mesh, Vector3.zero, Quaternion.identity, material, 0);
    }

    float GetEnemyAngle()
    {
        return 90 - Mathf.Rad2Deg * Mathf.Atan2(transform.forward.z, transform.forward.x); // Left handed CW. z = angle 0, x = angle 90
    }

    void OnTriggerEnter(Collider col)
    {
        //print("collision with " + col.gameObject.name);
        //if (col.gameObject.name != "Plane")
        if (col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            if (!pointOfInterestScript.pointOfInterest2.ContainsKey(col.gameObject.name))
            {
                Vector3 myPos = transform.position;
                Vector3 myVector = transform.forward;
                Vector3 theirPos = col.transform.position;
                Vector3 theirVector = theirPos - myPos;


                //Step 2: Is the object in front of this enemy?

                float mag = Vector3.SqrMagnitude(myVector) * Vector3.SqrMagnitude(theirVector);

                if (mag == 0f) //prevent divide by zero.  
                    return;

                float dotProd = Vector3.Dot(myVector, theirPos - myPos);
                bool isNegative = dotProd < 0f;
                dotProd = dotProd * dotProd;
                //The Square operation will eliminate negative values, but we want to retain them.
                if (isNegative)
                    dotProd *= -1;

                float sqrAngle = Mathf.Rad2Deg * Mathf.Acos(dotProd / mag);
                bool isInFront = sqrAngle < m_halfConeSize;

                //Step 3: Is there anything obscuring the object?

                Debug.DrawLine(myPos, theirPos, isInFront ? Color.green : Color.red);
                if (isInFront)
                {
                    detectionMode = true;
                    StartCoroutine(iaNavigationScript.popEventDetection(col.gameObject.transform.position));
                }
            }
        }
    } 

}