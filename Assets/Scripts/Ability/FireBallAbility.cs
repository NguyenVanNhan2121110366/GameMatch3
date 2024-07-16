using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallAbility : Ability
{
    [SerializeField] private GameObject objFireball;
    [SerializeField] private Transform posSpawnFireBallOfEnemy;
    [SerializeField] private Transform posSpawnFireBallOfPlayer;
    //[SerializeField] private Button abilityFireball;
    public ButtonAbility bntAbility;
    private void Awake()
    {
        this.bntAbility = FindFirstObjectByType<ButtonAbility>();
        this.BntAbility = GetComponent<Button>();

        this.BntAbility.onClick.AddListener(this.ExcutedAbility);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.isConsumpMana = 50;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void ExcutedAbility()
    {
        if (this.CheckConditionUseAbility())
        {
            this.UpdateMana();
            base.ExcutedAbility();
            this.SpawnFireballAttackEnemy();
            Invoke(nameof(this.SpawnFireballAttackDot), 0.6f);

            this.bntAbility.ExitAbility();

        }

    }


    private void SpawnFireballAttackEnemy()
    {
        if (TurnController.Instance.currentTurn == GameTurn.Player)
        {
            var fireball = Instantiate(this.objFireball);
            fireball.transform.position = this.posSpawnFireBallOfPlayer.position;
            fireball.SetActive(true);
            fireball.GetComponent<FireBall>().IsAttackEnemy = true;
            fireball.GetComponent<FireBall>().IsAttackDot = false;
        }
    }

    private void SpawnFireballAttackDot()
    {
        if (TurnController.Instance.currentTurn == GameTurn.Player)
        {
            var objFireball = Instantiate(this.objFireball);
            objFireball.transform.position = this.posSpawnFireBallOfPlayer.position;
            objFireball.SetActive(true);
            objFireball.GetComponent<FireBall>().IsAttackDot = true;
        }
    }
}
