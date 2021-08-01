using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsHelper : MonoBehaviour
{
    GameObject nearMenu;
    // Start is called before the first frame update
    void Start()
    {
        nearMenu = gameObject.transform.parent.gameObject.transform.parent.GetComponent<FurnitureControl>().child;

    }

    public void ScaleSphere()
    {

        transform.localScale = new Vector3(transform.localScale.x + 0.10f, transform.localScale.y + 0.10f, transform.localScale.z + 0.10f);
        StartCoroutine(HideSpheres());
    }

    IEnumerator HideSpheres()
    {

        yield return new WaitForSecondsRealtime(5);
        transform.localScale = new Vector3(transform.localScale.x - 0.10f, transform.localScale.y - 0.10f, transform.localScale.z - 0.10f);

        transform.parent.gameObject.SetActive(false);
    }

    public void ChooseMaterial()
    {

        nearMenu.GetComponent<NearMenu>().SetMaterial(transform.GetComponent<MeshRenderer>().material);
        nearMenu.GetComponent<NearMenu>().ChangeMaterial();
        transform.parent.gameObject.SetActive(false);
        transform.localScale = new Vector3(transform.localScale.x - 0.10f, transform.localScale.y - 0.10f, transform.localScale.z - 0.10f);

    }

}
