using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public void EnableVictoryScreen()
    {
        GameManager.Instance.Pause();
        this.gameObject.SetActive(true);
    }

    public void CloseVictoryScreen()
    {
        GameManager.Instance.Unpause();
        this.gameObject.SetActive(false);
    }
}
