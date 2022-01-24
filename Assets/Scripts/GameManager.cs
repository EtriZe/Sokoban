using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject cube;

    private void Start()
    {
        cube = GameObject.Find("MovableBloc");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Undo()
    {
        cube.GetComponent<MoveBloc>().MoveBackBlockAndCharacter();
    }
}
