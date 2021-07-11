using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMenu : MonoBehaviour
{
    public GameObject obj;
    Material material;
    public GameObject materialsCollection;
    int counter = 0;

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
        if (obj.tag == "wardrobe")
        {
            foreach (Transform spher in materialsCollection.transform)
            {
                spher.GetComponent<MeshRenderer>().material = transform.parent.GetComponent<FurnitureControl>().wardrobes[counter];
                counter++;
            }

        }
        else if (obj.tag == "table")
        {
            foreach (Transform spher in materialsCollection.transform)
            {
                spher.GetComponent<MeshRenderer>().material = transform.parent.GetComponent<FurnitureControl>().tables[counter];
                counter++;
            }

        }
        else
        {
            foreach (Transform spher in materialsCollection.transform)
            {
                spher.GetComponent<MeshRenderer>().material = transform.parent.GetComponent<FurnitureControl>().chairs[counter];
                counter++;
            }
        }
        materialsCollection.SetActive(true);
        counter = 0;
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
