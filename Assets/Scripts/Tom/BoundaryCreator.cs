using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCreator : MonoBehaviour
{
    [TextArea]
    public string Notes = "Using the Boundary from the device just work when its being build on the quest-> https://forum.unity.com/threads/can-we-reuse-user-s-vr-boundaries.818331/#post-5423601";

    [SerializeField]
    GameObject outerBoundaryWallMarker;
    [SerializeField]
    GameObject playAreaWallMarker;

    [SerializeField]
    bool drawMarkers;

    [SerializeField]
    Material matGround;
    [SerializeField]
    Material matWalls;
    [SerializeField]
    Material matCeiling;
    [SerializeField]
    Material matUV;

    [SerializeField]
    bool drawWalls;
    [SerializeField]
    bool drawRoof;

    [SerializeField]
    float wallHeight = 3.0f;

    [Tooltip("The height offset is really weird. It changes quite randomly after device restarts. Values that worked for me: '0.55','-0.55','0'.")]
    [SerializeField]
    float heightOffset = 0.0f;

    void Start()
    {
        Vector3[] debugBoundary = { new Vector3(-2.0f, -1.1f, 2.0f), new Vector3(-2.0f, -1.1f, -2.0f), new Vector3(2.0f, -1.1f, -2.0f), new Vector3(2.0f, -1.1f, 2.0f) };
        Vector3[] playAreaBoundary = GetPlayAreaBoundary();

        if (drawMarkers)
        {
            DrawBoundaryMarkers(GetPlayAreaBoundary(), GetOuterBoundary());
        }

        if (playAreaBoundary.Length > 0)
        {
            Vector3[] resetedBoundary = ResetHeight(playAreaBoundary);

            DrawGround(resetedBoundary);
            if (drawWalls)
                DrawWalls(resetedBoundary);
            if (drawRoof)
                DrawRoof(resetedBoundary);

        }
        else
        {
            Vector3[] resetedBoundary = ResetHeight(debugBoundary);

            DrawGround(resetedBoundary);
            if (drawWalls)
                DrawWalls(resetedBoundary);
            if (drawRoof)
                DrawRoof(resetedBoundary);
        }
    }

    Vector3[] ResetHeight(Vector3[] vertices)
    {
        for (int i = 0; i < vertices.Length; ++i)
        {
            vertices[i].y = 0.0f + heightOffset;
            //vertices[i].y = vertices[i].y / 2;
            //vertices[i].y = 0.0f + -(vertices[i].y / 2);
        }

        return vertices;
    }

    void DrawBoundaryMarkers(Vector3[] playAreaBoundary, Vector3[] outerBoundary)
    {
        foreach (Vector3 pos in playAreaBoundary)
        {
            Instantiate(playAreaWallMarker, pos, Quaternion.identity);
        }

        foreach (Vector3 pos in outerBoundary)
        {
            Instantiate(outerBoundaryWallMarker, pos, Quaternion.identity);
        }
    }

    // PlayArea is the best possible Reactangle from all the points
    Vector3[] GetPlayAreaBoundary()
    {
        // If a boundary is found and it wasnt already drawn
        if (OVRManager.boundary.GetConfigured())
        {
            // Get the boundary and instantiate cubes on every position
            Vector3[] boundary = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);

            return boundary;
        }
        return new Vector3[0];
    }

    // OuterBoundary are 256 point in counterclockwise order from the boundary
    Vector3[] GetOuterBoundary()
    {
        // If a boundary is found and it wasnt already drawn
        if (OVRManager.boundary.GetConfigured())
        {
            // Get the boundary and instantiate cubes on every position
            Vector3[] boundary = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary);

            return boundary;
        }

        return new Vector3[0];
    }

    void DrawGround(Vector3[] vertices)
    {
        GameObject ground = Instantiate(new GameObject(), gameObject.transform);
        ground.name = "ground";

        // Add the MeshFilter, MeshCollider and MeshRenderer as Components
        MeshFilter meshFilter = ground.AddComponent<MeshFilter>();
        MeshCollider meshCollider = ground.AddComponent<MeshCollider>();
        Renderer renderer = ground.AddComponent<MeshRenderer>();

        // Set the material of the mesh to the material given by unity
        renderer.material = matGround;

        Mesh mesh = new Mesh();
        mesh.name = "ProceduralGeneratedMesh";
        meshFilter.mesh = mesh;

        // Use the vertices from the parameter to build a quad
        mesh.vertices = vertices;

        Vector2[] uv = { new Vector2(0.0f, 0.0f), new Vector2(0.0f, 1.0f), new Vector2(1.0f, 1.0f), new Vector2(1.0f, 0.0f) };
        mesh.uv = uv;

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

    void DrawRoof(Vector3[] vertices)
    {
        // Start by adding the wallheight to the y value of the ground vertices
        for (int i = 0; i < vertices.Length; ++i)
        {
            vertices[i].y += wallHeight;
        }

        GameObject roof = Instantiate(new GameObject(), gameObject.transform);
        roof.name = "roof";

        // Add the MeshFilter, MeshCollider and MeshRenderer as Components
        MeshFilter meshFilter = roof.AddComponent<MeshFilter>();
        MeshCollider meshCollider = roof.AddComponent<MeshCollider>();
        Renderer renderer = roof.AddComponent<MeshRenderer>();

        // Set the material of the mesh to the material given by unity
        renderer.material = matCeiling;

        Mesh mesh = new Mesh();
        mesh.name = "ProceduralGeneratedMesh";
        meshFilter.mesh = mesh;

        // Use the vertices from the parameter to build a quad
        mesh.vertices = vertices;

        Vector2[] uv = { new Vector2(0.0f, 0.0f), new Vector2(0.0f, 1.0f), new Vector2(1.0f, 1.0f), new Vector2(1.0f, 0.0f) };
        mesh.uv = uv;

        int[] triangles = new int[6];
        triangles[0] = 1;
        triangles[1] = 2;
        triangles[2] = 0;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 0;

        mesh.triangles = triangles;

        // Set the MeshCollider to our generated mesh
        meshCollider.sharedMesh = mesh;
        // Need to recalculate the normals as they would be backwards
        mesh.RecalculateNormals();
    }

    void DrawWalls(Vector3[] vertices)
    {
        // We always have 4 walls so we just loop through 4 time and create them procedural
        for (int i = 0; i < 4; ++i)
        {
            Vector3 lowLeft = vertices[i % 4];
            Vector3 lowRight = vertices[(i + 1) % 4];
            Vector3 topLeft = new Vector3(lowLeft.x, lowLeft.y + wallHeight, lowLeft.z);
            Vector3 topRight = new Vector3(lowRight.x, lowRight.y + wallHeight, lowRight.z);

            Vector3[] wallVertices = { lowLeft, lowRight, topRight, topLeft };

            GameObject currentWall = Instantiate(new GameObject(), gameObject.transform);
            currentWall.name = "wall" + i;

            // Add the MeshFilter, MeshCollider and MeshRenderer as Components
            MeshFilter meshFilter = currentWall.AddComponent<MeshFilter>();
            MeshCollider meshCollider = currentWall.AddComponent<MeshCollider>();
            Renderer renderer = currentWall.AddComponent<MeshRenderer>();

            // Set the material of the mesh to the material given by unity
            renderer.material = matWalls;

            Mesh mesh = new Mesh();
            mesh.name = "ProceduralGeneratedMesh";
            meshFilter.mesh = mesh;

            // Use the vertices from the parameter to build a quad
            mesh.vertices = wallVertices;

            Vector2[] uv = { new Vector2(0.0f, 0.0f), new Vector2(0.0f, 1.0f), new Vector2(1.0f, 1.0f), new Vector2(1.0f, 0.0f) };
            mesh.uv = uv;

            int[] triangles = new int[6];

            // Basically the same as the ground but inverted each triangle so it looks into the inside
            triangles[0] = 1;
            triangles[1] = 2;
            triangles[2] = 0;
            triangles[3] = 2;
            triangles[4] = 3;
            triangles[5] = 0;

            mesh.triangles = triangles;

            // Set the MeshCollider to our generated mesh
            meshCollider.sharedMesh = mesh;
            // Need to recalculate the normals as they would be backwards
            mesh.RecalculateNormals();

        }
    }

}
