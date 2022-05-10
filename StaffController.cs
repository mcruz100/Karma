// Author:
// Purpose:
// Date: 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Matthew Cruz
// Purpose: Controlling Hospital Staff Movement and Behavior

public enum StaffState
{
    Roaming, //Walks around
    Idle, //Walks to a specified coordinate 
    Aggro,
};
public class StaffController : MonoBehaviour
{
    //This is the controller class for all Hospital Staff for the Hospital Level
    public GameObject player;
    public GameObject HospitalWayPoint;
    public StaffState currentState;
    private Vector3 targetPos;
    private Vector3 pos;
    private bool isMoving = false;
    private Animator staffAnimator;
    private bool isStanding = false;
    private float speed = 5f;
    private MenuManager menuManager;
    //Parallel arrays for Locations to follow within the Hospital
    public int pathNumber;
    private float[] xLocations;
    private float[] zLocations;
    public GameObject DemoCompleteCanvas;
    public GameObject MissionFailedCanvas;
    public GameObject MainMenuCanvas;
    public GameObject SettingsCanvas;
    public GameObject EscCanvas;
    private int idx = -1;
    private bool goToNextPoint = false;
    // Start is called before the first frame update
    void Start()
    {
        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        staffAnimator = this.gameObject.GetComponent<Animator>();
        if(pathNumber == 0)
        {
            xLocations = new float[] { 49, 49, 61, 71, 71, 71, 48, 48, 48, 16, 30, 30 };
            zLocations = new float[] { -48, -43.5f, -43.5f, -33, -13, -23, -23, -6, -23, -23, -23, -48 };
        }
        else if(pathNumber == 1)
        {
            xLocations = new float[] {69 , 88, 82, 82, 69,  69,  91,  69, 69, 47};
            zLocations = new float[] {-78,-78,-64,-78,-78,-102,-102,-102,-78,-78};
        }
        else if(pathNumber == 2)
        {
            xLocations = new float[] {  45, 45, 41, 10,  10,7.5f, 11, 37, 37, 45, 45};
            zLocations = new float[] { -93,-83,-79,-79,-100, -51,-62,-62,-79,-79,-83};
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (StaffState.Roaming):
                Roaming();
                break;
            case (StaffState.Idle):
                Idle();
                break;
            case (StaffState.Aggro):
                Aggro();
                break;
        }
        //Inspired from https://forum.unity.com/threads/enemy-ai-raycast.930795/
        if (Vector3.Distance(this.transform.position, player.transform.position) < 10f && this.gameObject.tag == "Staff")
        {
            Vector3 directionToPlayer = (player.transform.position - this.transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(this.transform.forward, directionToPlayer);
            if (angleBetweenGuardAndPlayer < 90f)
            {
                SwitchToAggro();
            }
        }
            
    }

    private void FindNewPosition()
    {
        if (idx == -1)
        {
            /*
             * Initialization 
             */
            idx = (int)Mathf.Floor(Random.Range(0, xLocations.Length - 1));
            pos = new Vector3();
            targetPos = new Vector3();
            targetPos.x = xLocations[idx];
            targetPos.y = transform.position.y;
            targetPos.z = zLocations[idx];
            transform.LookAt(targetPos);
            pos.x = xLocations[idx];
            pos.y = transform.position.y;
            pos.z = zLocations[idx];
            transform.position = pos;
        }
        else if (idx == xLocations.Length)
        {
            pos = transform.position;
            idx = 0;
            targetPos.x = xLocations[idx];
            targetPos.z = zLocations[idx];
            transform.LookAt(targetPos);
        }
        else
        {
            pos = transform.position;
            targetPos.x = xLocations[idx];
            targetPos.z = zLocations[idx];
            transform.LookAt(targetPos);
            idx++;
        }
        setWayPointCords(targetPos);
    }

    public void setWayPointCords(Vector3 pos)
    {
        HospitalWayPoint.transform.position = pos;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("Ending Level");
            Debug.Log(MissionFailedCanvas);
            MissionFailedCanvas.SetActive(true);
            MainMenuCanvas.SetActive(false);
            EscCanvas.SetActive(false);
            SettingsCanvas.SetActive(false);
            DemoCompleteCanvas.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void Aggro()
    {
        staffAnimator.SetBool("isMoving", true);
        transform.LookAt(player.transform.position);
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 5f * Time.deltaTime);
    }

    IEnumerator IdileAnimation()
    {/*
        if (isStanding)
            yield break;*/
        isStanding = true;
        staffAnimator.SetBool("isMoving", false);
        yield return new WaitForSeconds(3);
        staffAnimator.SetBool("isMoving", true);
        isStanding = false;
        goToNextPoint = true;
    }
    public void SwitchToAggro()
    {
        currentState = StaffState.Aggro;
    }
    void Roaming()
    {
        /*Debug.Log("Distance: " + Vector3.Distance(HospitalWayPoint.transform.position, this.transform.position));*/
        if (idx == -1)
        {
            FindNewPosition();
            goToNextPoint = true;
            staffAnimator.SetBool("isMoving", true);
        }
        else if (Vector3.Distance(HospitalWayPoint.transform.position, this.transform.position) < 1.5f)
        {
            goToNextPoint = false;
            FindNewPosition();
            StartCoroutine(IdileAnimation());
        }
        else if (goToNextPoint)
        {
            //Move to the next point
            Vector3 a = this.transform.position;
            Vector3 b = HospitalWayPoint.transform.position;
            this.transform.position = Vector3.MoveTowards(this.transform.position, HospitalWayPoint.transform.position, 5f * Time.deltaTime);
        }
    }
    void Idle()
    {
        staffAnimator.SetBool("isMoving", false);
        //Go to a certain coordinates or location
        //Can also provide a list of locations to head to
    }
}