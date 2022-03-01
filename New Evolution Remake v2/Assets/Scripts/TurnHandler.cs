using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnHandler : MonoBehaviour, ITurnHandler
{
    public Text messageBox;
    public Transform actionPanel;
    private Animator anim;
    public List<Skill> skills;
    public SO_DB db;
    public Transform skillCardPref;
    public Transform skillCardParent;
    public int maxCards = 4;
    public Transform currentAnimTarget;
    private bool playerTurn = false;
    private bool gameOver = false;
    public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }

    private void Awake()
    {
        anim = actionPanel.GetComponent<Animator>();
    }

    /*Start of your turn steps:*/
    /*
     * if its your turn:
     *      Your Turn Message
     *      Generate the new 4 cards to play
     *      Card display animation
     *   (after you choose the card)
     *    check and apply effect (depending on skill ids) para el futuro
     *          Play Card Animation:
     *          update the hp bar
     *      Check Battle Result
     *      Enemy Turn Message
     * if its not your turn:
     *      Enemy Turn Message
     *      Distroy Cards and hide Panel
     *    (On receive enemy play info)
     *      Play Card Animation
     *      check and apply effec
     *      update the HP Bar
     *      Check Battle Result
     */

    private void Update()
    {
        Debug.Log(playerTurn);
    }

    public void TurnStart()
    {

        PlayerTurn = true;
        /*Show Your Turn Message*/
        StartCoroutine(ShowTurnMessageAnim("Tu Turno"));
        /*Generate the new 4 cards to play*/
        StartCoroutine(GenerateCards());
    }

    public void EndTurn()
    {
        BattleManager bm = this.GetComponent<BattleManager>();
        DeleteCurrentCards();
        playerTurn = false;
        BattleResult(bm.playerStats.curHP, bm.enemyStats.curHP);

        if (gameOver)
        {
            /*Nothing Yet*/
        }
        else
        {
            ShowEnemyTurnMessage();
        }

    }

    public void ShowEnemyTurnMessage()
    {
        StartCoroutine(ShowTurnMessageAnim("Turno Enemigo"));
    }


    public void DeleteCurrentCards()
    {
        Animator scp = skillCardParent.GetComponent<Animator>();
        scp.SetBool("show", false);
        foreach (Transform card in skillCardParent)
        {
            Destroy(card.gameObject);
        }

        skillCardParent.gameObject.SetActive(false);
    }



    /*necesito:
     *si la animacion sera del player o el enemigo
     * el id de animación (dentro del skill dic.
     * 
     */

    public void ExecuteSkillAnim(Transform caster, Transform target, Skill skill, PlayerBattleStats playerStats, PlayerBattleStats enemyStats)
    {
        currentAnimTarget = target;
        StartCoroutine(CardAnim(caster, skill));


        /*BattleManager deberia hacer los calculos y guardarlo en sus campos, luego turn handler deberia ejecutar las animaciones y actualizar las barras de vida*/

    }

    IEnumerator CardAnim(Transform player, Skill skill)
    {
        IBodyDisplay bodyparts = player.GetComponent<IBodyDisplay>();

        Animator[] anim = bodyparts.GetAnimators();

        /*la habilidad deberia tener los booleanos de las animaciones que se van a ejecutar*/
        /*
         * por ejemplo:
         *  un cabezaso debe tener attack main en cabeza y en el cuerpo(lanzamiento) y attack support en el resto
         *  sera una lista de bool true, true, false, false  por cabeza cuerpo brazos piernas 
         */
        Animator chestAnim = new Animator();
        foreach (Animator an in anim)
        {
            switch (an.transform.parent.name)
            {
                case "Head":
                    an.SetBool(skill.animationType[0], true);
                    break;
                case "Sprites":/*Chest*/
                    an.SetBool(skill.animationType[1], true);
                    chestAnim = an;
                    break;
                case "RightArm":
                    an.SetBool(skill.animationType[2], true);
                    break;
                case "LeftArm":
                    an.SetBool(skill.animationType[2], true);
                    break;
                case "RightLeg":
                    an.SetBool(skill.animationType[3], true);
                    break;
                case "LeftLeg":
                    an.SetBool(skill.animationType[3], true);
                    break;
            }
        }
        yield return new WaitForFixedUpdate();

        foreach (Animator an in anim)
        {
            switch (an.transform.parent.name)
            {
                case "Head":
                    an.SetBool(skill.animationType[0], false);
                    break;
                case "Sprites":
                    an.SetBool(skill.animationType[1], false);
                    break;
                case "RightArm":
                    an.SetBool(skill.animationType[2], false);
                    break;
                case "LeftArm":
                    an.SetBool(skill.animationType[2], false);
                    break;
                case "RightLeg":
                    an.SetBool(skill.animationType[3], false);
                    break;
                case "LeftLeg":
                    an.SetBool(skill.animationType[3], false);
                    break;
            }
        }
        if (player.name == "Player")

        {
            EndTurn();
        }


    }
    public IEnumerator HitAnim()
    {
        IBodyDisplay bodyparts = currentAnimTarget.GetComponent<IBodyDisplay>();


        Animator[] anim = bodyparts.GetAnimators();

        foreach (Animator an in anim)
        {
            Debug.Log("The target" + currentAnimTarget);

            switch (an.transform.parent.name)
            {

                case "Head":
                    an.SetBool("hit", true);
                    break;
                case "Sprites":/*Chest*/
                    an.SetBool("hit", true);

                    break;
                case "RightArm":
                    an.SetBool("hit", true);
                    break;
                case "LeftArm":
                    an.SetBool("hit", true);
                    break;
                case "RightLeg":
                    an.SetBool("hit", true);
                    break;
                case "LeftLeg":
                    an.SetBool("hit", true);
                    break;
            }
            Debug.Log("The name " + an.transform.name);
            Debug.Log(an.GetBool("hit"));
        }
        yield return new WaitForSeconds(0.2f);
        foreach (Animator an in anim)
        {
            switch (an.transform.parent.name)
            {
                case "Head":
                    an.SetBool("hit", false);
                    break;
                case "Sprites":/*Chest*/
                    an.SetBool("hit", false);
                    break;
                case "RightArm":
                    an.SetBool("hit", false);
                    break;
                case "LeftArm":
                    an.SetBool("hit", false);
                    break;
                case "RightLeg":
                    an.SetBool("hit", false);
                    break;
                case "LeftLeg":
                    an.SetBool("hit", false);
                    break;
            }
        }

        yield return null;
    }


    /*the server checks who will start*/
    public void FirstTurn()
    {

        int i = UnityEngine.Random.Range(1, 3);
        if (i == 1)
        {
            PlayerTurn = true;
        }
        else
            PlayerTurn = false;

    }

    private IEnumerator GenerateCards()
    {

        skills = db.playerData.skills;
        skillCardParent.gameObject.SetActive(true);
        Animator scp = skillCardParent.GetComponent<Animator>();
        scp.SetBool("show", true);
        for (int i = 1; i <= maxCards; i++)
        {
            int totalSkills = skills.Count;
            Debug.Log(skills.Count);
            int random = Random.Range(0, totalSkills);
            Animator newcardAnim = GenerateCard(skills[random]).GetComponent<Animator>();
            yield return new WaitForSeconds(0.3f);
            newcardAnim.SetBool("show", true);

        }

        yield return null;
    }

    public Transform GenerateCard(Skill skill)
    {

        Transform newcard = Instantiate(skillCardPref, skillCardParent);
        CardSkillDisplay csd = skillCardPref.GetComponent<CardSkillDisplay>();
        csd.skill = skill;
        csd.splashArt.sprite = skill.artImage;
        csd.cardDesc.text = skill.desc;
        csd.cardName.text = skill.SkillName;
        return newcard;
    }

    public void BattleResult(int playerHP, int enemyHP)
    {
        if (playerHP <= 0 && enemyHP <= 0)
        {
            Debug.Log("Empate");
            gameOver = true;
        }
        else if (playerHP <= 0)
        {
            YouLose();
            gameOver = true;
        }
        else if (enemyHP <= 0)
        {
            YouWin();
            gameOver = true;
        }
    }



    public IEnumerator ShowTurnMessageAnim(string message)
    {
        /*Should be set to true during enemies turn but just to make sure*/
        messageBox.text = message;
        anim.SetBool("show", true);
        yield return null;
        anim.SetBool("show", false);

    }

    public void YouLose()
    {
        anim.SetBool("youLose", true);
    }

    public void YouWin()
    {
        anim.SetBool("youWin", true);
    }

    public void Turn(bool yourTurn)
    {


        skills = db.playerData.skills;
        if (yourTurn == true)
        {




            StartCoroutine(ShowTurnMessageAnim("Tu Turno"));
            /*cuando es tu turno aparecen cuatro cartas al azar*/
            /*puedes interactuar con las cartas*/
            skillCardParent.gameObject.SetActive(true);
            StartCoroutine(GenerateCards());
        }
    }
}