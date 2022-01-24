using System.Collections.Generic;
using UnityEngine;


public class SaveNbrWinPoint : MonoBehaviour
{
    private int _counter = 0;
    private bool _nbrWinPoint = false;
    private int _index = 0;
   
   
    private readonly List<int> _firstInstance = new List<int>();
    private readonly List<List<int>> _instanceManagement = new List<List<int>>();

    // Start is called before the first frame update
    private void Start()
    {
        var go = GameObject.FindGameObjectsWithTag("MovableBloc");
      
        for (var i = 0; i < go.Length; i++)
        {
            _counter++;
            List<int> isOnWin = new List<int>();
            isOnWin.Add(_firstInstance[i]);
            isOnWin.Add(0);
            _instanceManagement.Add(isOnWin);
        }
    }

   
    public void isOnBloc(int instance)
    {
        _index = -1;
        for (int i = 0; i < _counter; i++)
        {
            if (_instanceManagement[i][0] == instance)
            {
                _index = i;


                break;
            }
        }

        if (_index != -1)
        {
            _instanceManagement[_index][1] = 1;
            
        }

        CountNbrOfWin();
    }


    public void AddAnInstance(int instance)
    {
        _firstInstance.Add(instance);
    }


    private void CountNbrOfWin()
    {
        for (int i = 0; i < _counter; i++)
        {
            if (_instanceManagement[i][1] == 0)
            {
                _nbrWinPoint = false;
                break;
            }
            else
            {
                _nbrWinPoint = true;
            }
            
        }

        if (_nbrWinPoint == true)
        {
            gameWon();
        }
    }

    private void gameWon()
    {
        Debug.Log("GAGNE");
    }

    public void isNotOnBloc(int instance)
    {
        _index = -1;
        for (int i = 0; i < _counter; i++)
        {
            if (_instanceManagement[i][0] == instance)
            {
                _index = i;
                break;
            }
        }
        
        if (_index != -1)
        {
            _instanceManagement[_index][1] = 0;
        }
        CountNbrOfWin();
    }
}