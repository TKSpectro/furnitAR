using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCreator : MonoBehaviour
{
    /*
     * This script needs to check if a boundary is given from the device because not every oculus has one (see note below)
     * Also there are three functions which could be cleaned up a bit Draw... . In these you have small parts which could be moved to
     * their own function but because its just 3 LOC and it also just happens three times we leave it as is.
    */

    [TextArea]
    public string Notes = "Using the Boundary from the device just work when its being build on the quest-> https://forum.unity.com/threads/can-we-reuse-user-s-vr-boundaries.818331/#post-5423601";

    // GameObject-Prefab used to mark found boundaries
    [SerializeField]
    GameObject outerBoundaryWallMarker;
    [SerializeField]
    GameObject playAreaWallMarker;
    // Toggle drawing of the Markers
    [SerializeField]
    bool drawMarkers;

    // Different materials for the different parts of the room
    [SerializeField]
    Material matGround;
    [SerializeField]
    Material matWalls;
    [SerializeField]
    Material matCeiling;
    [SerializeField]
    Material matUV;

    // Toggle drawing of walls and roof
    [SerializeField]
    bool drawWalls;
    [SerializeField]
    bool drawRoof;

    [SerializeField]
    float wallHeight = 3.0f;

    // This offset can be set if the boundary is not correctly from the decive.
    // Normally it should not be needed if you redraw you boundary before starting the app
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

        // The Length is greater if we can get a boundary from the connected device / the device on which the app is run
        if (playAreaBoundary.Length > 0)
        {
            // If the player has set up a boundary we need to remove the pre-placed furniture, so they dont clip through the walls
            GameObject placedItemsParent = GameObject.Find("Furnitures");
            // Run through the childs and remove them
            foreach (Transform child in placedItemsParent.transform)
            {
                // Remove everything, except these two, because they are needed for the furniture menus
                if (child.name == "NearMenu3x1" || child.name == "Materials")
                {
                    continue;
                }

                Destroy(child.gameObject);
            }

            // Use the resetHeight function to calculate the given offset into the boundary
            Vector3[] resetedBoundary = ResetHeight(playAreaBoundary);

            DrawGround(resetedBoundary);
            if (drawWalls)
                DrawWalls(resetedBoundary);
            if (drawRoof)
                DrawRoof(resetedBoundary);

        }
        else
        {
            // Use the resetHeight function to calculate the given offset into the boundary
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
        }

        return vertices;
    }

    // This function is used mostly for debugging and not in the actual app
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

    // OuterBoundary are 256 points in counterclockwise order from the boundary
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
        ground.name = "Ground";

        // Add the MeshFilter, MeshCollider and MeshRenderer as Components
        MeshFilter meshFilter = ground.AddComponent<MeshFilter>();
        MeshCollider meshCollider = ground.AddComponent<MeshCollider>();
        Renderer renderer = ground.AddComponent<MeshRenderer>();

        // Add for collisions with furniture
        BoxCollider bc = ground.AddComponent<BoxCollider>();
        bc.center = new Vector3(0.0f, -0.1f, 0.0f);
        bc.size = new Vector3(4.0f, 0.2f, 4.0f);

        Rigidbody rb = ground.AddComponent<Rigidbody>();
        rb.isKinematic = true;

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
        roof.name = "Roof";

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
            currentWall.name = "Wall" + i;

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
