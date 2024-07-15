using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityLightningBolt : Ability
{
    private ButtonAbility _buttonAbility;
    [SerializeField] private GameObject connector;
    private void Awake()
    {
        this._buttonAbility = FindFirstObjectByType<ButtonAbility>();
        this.BntAbility = GetComponent<Button>();
        this.BntAbility.onClick.AddListener(this.ExcutedAbility);

    }
    private void Start()
    {
        this.isConsumpMana = 50;
    }

    private void Update()
    {

    }

    protected override void ExcutedAbility()
    {
        if (CheckConditionUseAbility())
        {
            this.UpdateMana();
            base.ExcutedAbility();
            var objConnector = Instantiate(this.connector);
            objConnector.SetActive(true);
            this._buttonAbility.ExitAbility();
        }
    }

}
