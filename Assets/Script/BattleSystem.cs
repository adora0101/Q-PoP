using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYER, ENEMY, WIN, LOSE }



public class BattleSystem : MonoBehaviour
{
    public GameObject player;

    public GameObject enemy;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;



    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetUpBattle());

    }

    IEnumerator SetUpBattle()
    {
        dialogueText.text = "A wild " + enemy.GetComponent<Unit>().unitName + " appears!";
        playerHUD.setHUD(player.GetComponent<Unit>());
        enemyHUD.setHUD(enemy.GetComponent<Unit>());

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYER;

        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose!";
    }

    public void OnAttack()
    {
        if (state != BattleState.PLAYER)
            return;

        StartCoroutine(Attack());
    }

    public void OnHeal()
    {
        if (state != BattleState.PLAYER)
            return;

        StartCoroutine(Heal());
    }

    IEnumerator Attack()
    {

        Vector3 p = player.GetComponent<Transform>().position;
        Vector3 e = enemy.GetComponent<Transform>().position;

        player.GetComponent<Transform>().position = new Vector3(e.x - 2, e.y, e.z);

        yield return new WaitForSeconds(1);

        bool isDead = enemy.GetComponent<Unit>().takeDamage(player.GetComponent<Unit>().attack);
        enemyHUD.setHUD(enemy.GetComponent<Unit>());

        dialogueText.text = enemy.GetComponent<Unit>().unitName + " recieved damage!";

        yield return new WaitForSeconds(1);

        player.GetComponent<Transform>().position = p;
        dialogueText.text = "";

        yield return new WaitForSeconds(.5f);

        if (isDead)
        {
            state = BattleState.WIN;
            dialogueText.text = "YOU WIN!!!";
            //victory
        }
        else
        {
            state = BattleState.ENEMY;
            //continue to enemy turn
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator Heal()
    {
        player.GetComponent<Unit>().health += 10;
        playerHUD.setHUD(player.GetComponent<Unit>());

        dialogueText.text = "You heal yourself.";

        yield return new WaitForSeconds(1);

        state = BattleState.ENEMY;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        Vector3 p = player.GetComponent<Transform>().position;
        Vector3 e = enemy.GetComponent<Transform>().position;

        enemy.GetComponent<Transform>().position = new Vector3(p.x + 2, p.y, p.z);

        yield return new WaitForSeconds(1);

        dialogueText.text = enemy.GetComponent<Unit>().unitName + " is attacking!!";

        yield return new WaitForSeconds(1);

        bool isDead = player.GetComponent<Unit>().takeDamage(enemy.GetComponent<Unit>().attack);
        playerHUD.setHUD(player.GetComponent<Unit>());
        dialogueText.text = player.GetComponent<Unit>().unitName + " recieved damage!!";

        yield return new WaitForSeconds(1);

        enemy.GetComponent<Transform>().position = e;
        dialogueText.text = "";

        yield return new WaitForSeconds(.5f);

        if (isDead)
        {
            state = BattleState.LOSE;
            dialogueText.text = "YOU LOSE!!";
        }
        else
        {
            state = BattleState.PLAYER;
            PlayerTurn();
        }
    }

}

