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
        noteText += "������Χ��" + turret.radius.ToString() + "\n";
        noteText += "��������" + turret.atk.ToString() + "\n";
        noteText += "���ѣ�" + cost.ToString() + "\n";
        noteText += "˵����" + turret.turretNote.text + "\n";
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
