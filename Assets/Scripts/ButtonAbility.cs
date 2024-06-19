using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonAbility : MonoBehaviour
{
    [SerializeField] private Button bntAbility;
    [SerializeField] private GameObject bgrAbility;
    [SerializeField] private Button exitAbility;

    private void Awake()
    {
        this.bntAbility.onClick.AddListener(this.OnClickAbility);
        this.exitAbility.onClick.AddListener(this.ExitAbility);
    }

    private void OnClickAbility()
    {
        this.bgrAbility.SetActive(true);
    }

    private void ExitAbility()
    {
        this.bgrAbility.SetActive(false);
    }

}
