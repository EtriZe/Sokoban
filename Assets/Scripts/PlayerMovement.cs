using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using Object = System.Object;

public class PlayerMovement : MonoBehaviour
{
  // Variables

  [SerializeField] private float walkSpeed = 0;
  public GameObject cube;
  private Vector3 moveDirection;



  //References
  private CharacterController controller;
  private GameObject player;
  private Animator anim;
  private bool pushable;
  private void Start()
  {
    player = GameObject.Find("Player");
    controller = GetComponent<CharacterController>();
    anim = GetComponentInChildren<Animator>();
    pushable = true;

  }

  public void moveBackCharacter(Vector3 oldCo)
  {
    controller.enabled = false;
    transform.position = oldCo;
    controller.enabled = true;

  }
  
  private void Update()
  {
   
    Move();
  }

  private void LateUpdate()
  {
    
    
  }

  private void Move()
  {
    
    float moveZ = Input.GetAxis("Vertical");
    
    float moveX = Input.GetAxis("Horizontal");
    
    moveDirection = new Vector3(moveX, 0, moveZ);
    moveDirection *= walkSpeed;

    if (moveDirection == Vector3.zero)
    {
      
      Idle();
    }
    else
    {
      anim.SetFloat("Speed",1);
      transform.forward = moveDirection;

    }
    
  
    controller.Move(moveDirection* Time.deltaTime);
    
  }

  private void Idle()
  {
    anim.SetFloat("Speed",0);
  }

  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
   
     
      if (hit.gameObject.name.Contains("MovableBloc") && pushable == true)
      {
        
        
        hit.gameObject.GetComponent<MoveBloc>().Move(transform.position);
        StartCoroutine(CoolDown());
      }
    

    

  }
  
  IEnumerator CoolDown()
  {
    pushable = false;
    // Temps avant de pouvoir repousser le bloc
    yield return new WaitForSeconds(1);
    pushable = true;
  }
 
}
