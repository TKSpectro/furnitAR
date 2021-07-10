using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsHelper : MonoBehaviour
{
    GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child = gameObject.transform.parent.gameObject.transform.parent.GetComponent<FurnitureControl>().child;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScaleSphere()
    {
        Debug.Log("test");
        transform.localScale = new Vector3(transform.localScale.x + 0.10f, transform.localScale.y + 0.10f, transform.localScale.z + 0.10f);
    }


    public void ChooseMaterial()
    {

        child.GetComponent<NearMenu>().SetMaterial(transform.GetComponent<MeshRenderer>().material);
        child.GetComponent<NearMenu>().ChangeMaterial();
        transform.parent.gameObject.SetActive(false);
        transform.localScale = new Vector3(transform.localScale.x - 0.10f, transform.localScale.y - 0.10f, transform.localScale.z - 0.10f);

    }

}
