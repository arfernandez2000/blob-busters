using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : Character
{
    public float onHitFlashTime = 0.2f;
    Color origionalColor;
    public SkinnedMeshRenderer SkinnedMeshRenderer;
    void Start()
    {
        GameObject meshRenderer = transform.Find("MeshRenderer").gameObject;
        SkinnedMeshRenderer = meshRenderer.gameObject.GetComponent<SkinnedMeshRenderer>();
        origionalColor = SkinnedMeshRenderer.material.color;
    }

    void FlashRed() {
        SkinnedMeshRenderer.material.color = Color.red;
        Invoke("ResetColor", onHitFlashTime);
    }

    void ResetColor() {
        SkinnedMeshRenderer.material.color = origionalColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "SimpleSpell") {
            FlashRed();
            TakeDamage(col.gameObject.GetComponent<ISpell>().Damage);
        }
    }
}
