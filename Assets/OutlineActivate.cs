using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class OutlineActivate : MonoBehaviour
{
    public Material blackOutline;
    public Material whiteOutline;
    public MeshRenderer meshRenderer;
    private Material defaultMaterial;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material ;
        ActivateOutline();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){ActivateOutline();}
         if(!isActive){DesactivateOutline();}
    }
    public void ActivateOutline()
    {
        Material[] materials = new Material[3];
        // Assign your new materials
        materials[0] = whiteOutline;  // First material (bubble)
        materials[1] = blackOutline; // Second material (outline)
        materials[2] = defaultMaterial;
        meshRenderer.materials = materials;
   
    }
    public void DesactivateOutline()
    {
        
            Material[] materials = new Material[1];
             materials[0] = defaultMaterial;
            meshRenderer.materials= materials;
    }


}
