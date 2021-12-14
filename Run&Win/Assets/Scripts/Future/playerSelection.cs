using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerSelection : MonoBehaviour
{
    public GameObject[] players;
    public int selectPlayer = 0;

    public void nextPlayer()
    {
        players[selectPlayer].SetActive(false);
        selectPlayer = (selectPlayer + 1) % players.Length;
        players[selectPlayer].SetActive(true);
    }

    public void previousPlayer()
    {
        players[selectPlayer].SetActive(false);
        selectPlayer--;
        if (selectPlayer< 0 )
        {
            selectPlayer+= players.Length;
        }
        players[selectPlayer].SetActive(true);
    }

}
