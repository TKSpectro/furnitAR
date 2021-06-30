using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryTest : MonoBehaviour
{
    [TextArea]
    public string Notes = "This script just works when the project is being build on the quest-> https://forum.unity.com/threads/can-we-reuse-user-s-vr-boundaries.818331/#post-5423601";

    public GameObject wallMarker;
    public GameObject wallMarker2;
    bool isBoundaryFinished = false;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        // Safe the type for easier further use -> OuterBoundary are 256 point in counterclockwise order from the boundary
        // PlayArea is the best possible Reactangle from all the points
        OVRBoundary.BoundaryType type = OVRBoundary.BoundaryType.OuterBoundary;

        // If a boundary is found and it wasnt already drawn
        if (OVRManager.boundary.GetConfigured() && !isBoundaryFinished)
        {
            // Get the boundary and instantiate cubes on every position
            Vector3[] boundary = OVRManager.boundary.GetGeometry(type);

            int counter = 0;

            foreach (Vector3 pos in boundary)
            {
                if (counter % 2 == 0)
                    Instantiate(wallMarker, pos, Quaternion.identity);

                counter++;
            }

            // Get the boundary and instantiate cubes on every position
            Vector3[] boundary2 = OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea);

            int counter2 = 0;

            foreach (Vector3 pos in boundary2)
            {

                Instantiate(wallMarker2, pos, Quaternion.identity);

                counter2++;
            }

            isBoundaryFinished = true;

            isBoundaryFinished = true;
        }

    }
}
