using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetButton : MonoBehaviour
{
    [SerializeField] private SquaresManager squaresManager;

    public void OnClickAction()
    {
        squaresManager.TemplateChosen();
    }
}
