using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeleteFurniture()
    {
        GameObject obj = gameObject.transform.parent.gameObject;
        Destroy(obj);
    }
}
