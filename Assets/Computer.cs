using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour, IInteractable
{
    private IEnumerator coroutine;
    CharacterController CC;
    private int timesacelerating=0;

    [SerializeField]
    private Transform playerTargetPosition;

    private IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        timesacelerating--;
        Player.Instance.GetMovement().speed = 6;
        CC.height =2.5f;
        DayNigthCycle.Instance.cycleSpeed /= 1.45f;
    }

    public void Interact()
    {
        if (timesacelerating < 3)
        {
            CC = Player.Instance.GetCharacterController();
            timesacelerating++;
            CC.height = 0.7f;
            print (CC.height);
            Player.Instance.GetMovement().speed = 0;
            coroutine = wait(7f);
            StartCoroutine(coroutine);
            DayNigthCycle.Instance.cycleSpeed *= 1.45f;

            if (playerTargetPosition != null)
            {
                CC.transform.position = playerTargetPosition.position;
                CC.transform.rotation = playerTargetPosition.rotation;
            }
        }
    }

    public InteractionMode InteractionMode {
        get { return InteractionMode.INTERACT; }
    }
}
