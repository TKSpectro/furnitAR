using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCreator : MonoBehaviour
{
    [TextArea]
    public string Notes = "This script just works when the project is being build on the quest-> https://forum.unity.com/threads/can-we-reuse-user-s-vr-boundaries.818331/#post-5423601";

    [SerializeField]
    GameObject outerBoundaryWallMarker;
    [SerializeField]
    GameObject playAreaWallMarker;

    [SerializeField]
    Material matGround;

    // Start is called before the first frame update
    void Start()
    {
        Vector3[] debugBoundary = { new Vector3(-1.4f, -1.1f, -1.2f), new Vector3(0.1f, -1.1f, -2.8f), new Vector3(2f, -1.1f, -1.1f), new Vector3(0.4f, -1.1f, 0.6f) };

        Vector3[] playAreaBoundary = DrawPlayAreaBoundary();
        Vector3[] outerBoundary = DrawOuterBoundary();

        if (playAreaBoundary.Length > 0)
        {
            for (int i = 0; i < playAreaBoundary.Length; ++i)
            {
                playAreaBoundary[i].y = 0.0f;
            }

            DrawMesh(playAreaBoundary);
        }
        else
        {
            for (int i = 0; i < debugBoundary.Length; ++i)
            {
                debugBoundary[i].y = 0.0f;
            }

            DrawMesh(debugBoundary);
        }
    }

    // PlayArea is the best possible Reactangle from all the points
    Vector3[] DrawPlayAreaBoundary()
    {
        // If a boundary is found and it wasnt already drawn
        if (OVRManager.boundary.GetConfigured())
        {
            // Get the boundary and instantiate cubes on every position
            Vector3[] boundary = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);

            foreach (Vector3 pos in boundary)
            {
                Instantiate(playAreaWallMarker, pos, Quaternion.identity);
            }

            return boundary;
        }
        return new Vector3[0];
    }

    // OuterBoundary are 256 point in counterclockwise order from the boundary
    Vector3[] DrawOuterBoundary()
    {
        // If a boundary is found and it wasnt already drawn
        if (OVRManager.boundary.GetConfigured())
        {
            // Get the boundary and instantiate cubes on every position
            Vector3[] boundary = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);

            foreach (Vector3 pos in boundary)
            {
                Instantiate(outerBoundaryWallMarker, pos, Quaternion.identity);
            }

            return boundary;
        }

        return new Vector3[0];
    }

    void DrawMesh(Vector3[] vertices)
    {
        // Add the MeshFilter, MeshCollider and MeshRenderer as Components
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        Renderer renderer = gameObject.AddComponent<MeshRenderer>();

        // Set the material of the mesh to the material given by unity
        renderer.material = matGround;

        Mesh mesh = new Mesh();
        mesh.name = "ProceduralGeneratedMesh";
        meshFilter.mesh = mesh;

        // Use the vertices from the parameter to build a quad
        mesh.vertices = vertices;

        int[] triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 0;
        triangles[4] = 3;
        triangles[5] = 2;

        mesh.triangles = triangles;

        // Set the MeshCollider to our generated mesh
        meshCollider.sharedMesh = mesh;
        // Need to recalculate the normals as they would be backwards
        mesh.RecalculateNormals();
    }
}
