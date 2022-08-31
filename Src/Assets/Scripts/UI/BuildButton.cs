using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Turret turret;
    float cost;

    public void SetTurret(Turret turretPrefab) {
        turret = turretPrefab;
        SetCost(turret.value);
    }
    public void SetCost(float cost) {
        this.cost = cost;
    }

    public void CreateNoteText() {
        string noteText = "";
        noteText += turret.turretName + "\n";
        noteText += "攻击范围：" + turret.radius.ToString() + "\n";
        noteText += "攻击力：" + turret.atk.ToString() + "\n";
        noteText += "花费：" + cost.ToString() + "\n";
        noteText += "说明：" + turret.turretNote.text + "\n";
        UIManager.instance.UpdateNote(noteText);
    }
    public void DestroyNoteText() {
        UIManager.instance.UpdateNote("");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        CreateNoteText();
    }
    public void OnPointerExit(PointerEventData eventData) {
        DestroyNoteText();
    }
}
