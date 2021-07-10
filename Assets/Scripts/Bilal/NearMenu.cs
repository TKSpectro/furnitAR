using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMenu : MonoBehaviour
{
    public GameObject obj;
    Material material;
    public GameObject materialsCollection;


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
        Destroy(obj);
        gameObject.SetActive(false);
    }

    public void ShowMaterialSpheres()
    {
        materialsCollection.transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f
            , transform.position.z);
        materialsCollection.SetActive(true);
        //StartCoroutine(SetMaterialSpheresToActive());

        //gameObject.SetActive(false);
    }

    IEnumerator SetMaterialSpheresToActive()
    {
        yield return new WaitForSecondsRealtime(2);
        materialsCollection.SetActive(true);
    }


    public void ChangeMaterial()
    {

        //  Debug.Log("Change Materials");
        obj = obj.transform.GetChild(0).gameObject;

        obj.GetComponent<MeshRenderer>().material = material;
    }

    public void SetMaterial(Material mat)
    {
        material = mat;
    }
}
