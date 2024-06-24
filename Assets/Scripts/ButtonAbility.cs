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

    public void OnClickAbility()
    {
        this.bgrAbility.SetActive(true);
    }

    public void ExitAbility()
    {
        this.bgrAbility.SetActive(false);
    }

}
