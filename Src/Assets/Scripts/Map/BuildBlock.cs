using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBlock : MonoBehaviour
{
    public GameObject buildEffect;
    public Material normalMaterial, selectedMaterial;
    MeshRenderer meshRenderer;
    Turret turret;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetStatus(bool selected) {
        if (selected) meshRenderer.material = selectedMaterial;
        else meshRenderer.material = normalMaterial;
    }

    public Turret GetTurret() {
        return turret;
    }

    public bool CreateTurret(Turret turretPrefab, Transform turrets = null) {
        if (turret != null) return false;
        turret = Instantiate(turretPrefab, new Vector3(transform.position.x, transform.position.y + transform.localScale.y/2, transform.position.z),
            transform.rotation);
        if (turrets != null) turret.transform.parent = turrets;
        GameObject effect = Instantiate(buildEffect, turret.transform.position, buildEffect.transform.rotation);
        Destroy(effect, 1f);
        return true;
    }
    public bool DestroyTurret() {
        if (turret == null) return false;
        Destroy(turret.gameObject);
        turret = null;
        return true;
    }
}
