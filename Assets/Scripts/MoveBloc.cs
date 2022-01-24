using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class MoveBloc : MonoBehaviour
{
    private static readonly List<Vector3> SaveBeforeMove = new List<Vector3>();
    
    private string _direction;
    private Vector3 _destination;
    private static Vector3 _lastCoCharacter;
    
    
    
    private const float DistanceDetect = 0.999f;
    private const float Speed = 8.0f;
    private const float DistanceToPush = 2.0f;
    
    
    private const bool Pushable = true;

    public GameObject[] go;
    public GameObject character;
    public GameObject parent;
    
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        character = GameObject.Find("Player");
        parent = GameObject.Find("BlocMovable");
        parent.gameObject.GetComponent<SaveNbrWinPoint>().AddAnInstance(GetInstanceID());
        //SaveCoBeforeMove();
        
        go = GameObject.FindGameObjectsWithTag("MovableBloc");
      
      
       

    }
    

   

   
    
   
    // Update is called once per frame
    void Update()
    {
        if (tag == "Moving")
        {
            if (_direction == "Right")
            {
                if (VerifNextBlocRight())
                {
                    if ((_destination.x - transform.position.x) > 0)
                    {
                        transform.position = new Vector3(transform.position.x + Speed * Time.deltaTime,
                            transform.position.y, transform.position.z);
                    }
                }
            }
            else if (_direction == "Left")
            {
                if (VerifNextBlocLeft())
                {
                    if ((_destination.x - transform.position.x) < 0)
                    {
                        transform.position = new Vector3(transform.position.x - Speed * Time.deltaTime,
                                transform.position.y, transform.position.z);
                    }
                }
            }
            else if (_direction == "Top")
            {
                if (VerifNextBlocForward())
                {
                    if ((_destination.z - transform.position.z) > 0)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y,
                            transform.position.z + Speed * Time.deltaTime);
                    }
                }
            }
            else if (_direction == "Down")
            {
                if (VerifNextBlocBack())
                {
                    if ((_destination.z - transform.position.z) < 0)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y,
                            transform.position.z - Speed * Time.deltaTime);
                    }
                }
            }
        }
        else if (tag == "EndMoving")
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y,
                Mathf.Round(transform.position.z));
            SetTag("NotMoving");
        }
    }


    

    public void SetTag(string text)
    {
        gameObject.tag = text;
    }

    public void Move(Vector3 player)
    {
        SaveCoBeforeMove();
        SetTag("Moving");
        
        
        
            if (transform.position.x - 1.4f > player.x)
            {
                //Push Right
                _destination = new Vector3(transform.position.x + DistanceToPush, transform.position.y,
                    transform.position.z);
                _direction = "Right";
                
            }
            else if (transform.position.x + 1.4f < player.x)
            {
                //PushLEft

                _destination = new Vector3(transform.position.x - DistanceToPush, transform.position.y,
                    transform.position.z);
                _direction = "Left";
                
            }
            else if (transform.position.z + 1.4f < player.z)
            {
                //Push Back
                _destination = new Vector3(transform.position.x, transform.position.y,
                    transform.position.z - DistanceToPush);
                _direction = "Down";
                
            }
            else if (transform.position.z - 1.4f > player.z)
            {
                // Push Top
                _destination = new Vector3(transform.position.x, transform.position.y,
                    transform.position.z + DistanceToPush);
                _direction = "Top";
                
            }

            StartCoroutine(TimeMoving());
            
        
    }

    IEnumerator TimeMoving()
    {
        
        yield return new WaitForSeconds(DistanceToPush / Speed);
        SetTag("EndMoving");
        detectWinPoint();

    }

  

    
    
    private void detectWinPoint()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(downRay, out hit, DistanceDetect))
        {
            if (hit.collider.tag == "WinPoint")
            {
                parent.gameObject.GetComponent<SaveNbrWinPoint>().isOnBloc(GetInstanceID());
            }
            else
            {
                parent.gameObject.GetComponent<SaveNbrWinPoint>().isNotOnBloc(GetInstanceID());
            }
        }


    }

    private bool VerifNextBlocRight()
    {
        RaycastHit hit;
      
            Ray rightRay = new Ray(transform.position, Vector3.right);
            
            if (Physics.Raycast(rightRay, out hit, 1))
            {
                return false;
            }
         
        return true;
    }
    
    
    private bool VerifNextBlocLeft()
    {
        RaycastHit hit;
      
        Ray leftRay = new Ray(transform.position, Vector3.left);
            
        if (Physics.Raycast(leftRay, out hit, 1))
        {
            return false;
        }
         
        return true;
    }
    
    
    private bool VerifNextBlocBack()
    {
        RaycastHit hit;
      
        Ray backRay = new Ray(transform.position, Vector3.back);
            
        if (Physics.Raycast(backRay, out hit, 1))
        {
            return false;
        }
         
        return true;
    }
    
    
    private bool VerifNextBlocForward()
    {
        RaycastHit hit;
      
        Ray forwardRay = new Ray(transform.position, Vector3.forward);
            
        if (Physics.Raycast(forwardRay, out hit, 1))
        {
            return false;
        }
         
        return true;
    }

    public void SaveCoBeforeMove()
    {
        SaveBeforeMove.Clear();
      
        for (var i = 0; i < go.Length; i++)
        {
            SaveBeforeMove.Add(go[i].transform.position);
        }
        
        _lastCoCharacter = new Vector3(character.transform.position.x,0.5f,character.transform.position.z);
     
    }


    public void MoveBackBlockAndCharacter()
    {
        for (var i = 0; i < go.Length; i++)
        {
            go[i].transform.position = SaveBeforeMove[i];
        }
       
        character.gameObject.GetComponent<PlayerMovement>().moveBackCharacter(_lastCoCharacter);
    }
}