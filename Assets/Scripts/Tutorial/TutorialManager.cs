using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TutorialEggTimer());
    }

    private IEnumerator TutorialEggTimer()
    {
        yield return new WaitForSeconds(1f);

        // Block 1 dialogue
        yield return StartCoroutine(DialogueSystem.instance.DialogueEggTimer(new DialogueBlock("1")));

        yield return new WaitForSeconds(1f);
        MySceneManager.instance.ChangeSceneTo(SceneState.Mine);

        yield return new WaitForSeconds(1f);

        // Block 2 dialogue
        yield return StartCoroutine(DialogueSystem.instance.DialogueEggTimer(new DialogueBlock("2")));

        // Wait until player mines Salt + Sulfur
        yield return new WaitUntil(() => PlayerHasSaltAndSulfur());

        // Block 3 dialogue
        yield return StartCoroutine(DialogueSystem.instance.DialogueEggTimer(new DialogueBlock("3")));
        MySceneManager.instance.ChangeSceneTo(SceneState.Lab);

        // Block 4 dialogue
        yield return StartCoroutine(DialogueSystem.instance.DialogueEggTimer(new DialogueBlock("4")));

        // Wait until player creates Titanium in cauldron
        yield return new WaitUntil(() => Inventory.instance.HasItem("gold"));

        yield return new WaitForSeconds(1.5f);
        MySceneManager.instance.ChangeSceneTo(SceneState.Observatory);
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(DialogueSystem.instance.DialogueEggTimer(new DialogueBlock("5")));

        ActualSceneManager.instance.LoadScene3();
    }

    private bool PlayerHasSaltAndSulfur()
    {
        return Inventory.instance.HasItem("salt") && Inventory.instance.HasItem("sulfur");
    }



}
