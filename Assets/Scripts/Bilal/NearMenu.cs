using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMenu : MonoBehaviour
{
    private GameObject furniturePiece;
    public GameObject materialsCollection;
    private int counter = 0;
    private Material material;

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
        Destroy(furniturePiece);
        gameObject.SetActive(false);
    }

    public void ShowMaterialSpheres()
    {

        materialsCollection.transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f
            , transform.position.z);

        if (furniturePiece.tag == "wardrobe")
        {
            Debug.Log("wardrobe");
            foreach (Transform spher in materialsCollection.transform)
            {
                spher.GetComponent<MeshRenderer>().material = transform.parent.GetComponent<FurnitureControl>().wardrobes[counter];
                counter++;
            }

        }
        else if (furniturePiece.tag == "table")
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

    }



    public void ChangeMaterial()
    {

        //  Debug.Log("Change Materials");
        //furniturePiece = furniturePiece.transform.GetChild(0).gameObject;
        Debug.Log("child = ", furniturePiece.transform.GetChild(0).gameObject);

        if (furniturePiece.transform.GetComponent<MeshRenderer>())
        {
            furniturePiece.transform.GetComponent<MeshRenderer>().material = material;
        }
        else
        {
            furniturePiece.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = material;

        }
    }

    public void SetMaterial(Material mat)
    {
        material = mat;
    }
    public void SetFurniture(GameObject obj)
    {
        furniturePiece = obj;
    }
}
