using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameLogic : MonoBehaviour {

  public float distanceVideoToCam = 10.0f;
  public GameObject player;
  public GameObject museum;
  public GameObject startPoint;
  public GameObject restartPoint;
  public GameObject welcomeScreen;
  public GameObject videoVRDrivingExperience = null;
  public GameObject videoVRExperience = null;
  public GameObject videoVREngineering = null;
  public GameObject videoVRCustomerPreview = null;
  public GameObject videoVRAirbrush = null;

  public AudioSource environmenMusic;

  private GameObject video = null;

  // Use this for initialization
  void Start () {
    
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  public void OnClickedHMD() {
    float sightlength = 10f;

    // Checks which video to play
    RaycastHit seen;
    Ray raydirection = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
    if (Physics.Raycast(raydirection, out seen, sightlength)) { 

      // VERY IMPORTANT: Checks if the user stands still to create the video canvas, cause if he is still moving the 
      // collider he has clicked is of the Waypoint. This causes to crash the app.
      if(seen.collider.name == "Waypoint") {
        Debug.Log("ERROR: The user is still moving");
        return;
      }

      // Calculate the position and the rotation of the gameobject to instatiate it in front of the camera
      Vector3 objPosition = Camera.main.transform.position + Camera.main.transform.forward * distanceVideoToCam;
      Quaternion objRotation = new Quaternion(0.0f, Camera.main.transform.rotation.y, 0.0f, Camera.main.transform.rotation.w);

      museum.SetActive(false);    // Hide the museum

      environmenMusic.Pause();    // Pauses the environment music
      
      // Checks which one of the videos it will play
      if (seen.collider.name == "HMD_1") {
        if (videoVRDrivingExperience == null)
          Debug.Log("Video canvas of VR Experience is missing");
        else 
          video = Instantiate(videoVRDrivingExperience, new Vector3(objPosition.x, 1.0f, objPosition.z), objRotation);
      } else if (seen.collider.name == "HMD_2") {
        if (videoVRExperience == null)
          Debug.Log("Video canvas of VR Experience is missing");
        else
          video = Instantiate(videoVRExperience, new Vector3(objPosition.x, 2.0f, objPosition.z), objRotation);
      } else if (seen.collider.name == "HMD_3") {
        if (videoVREngineering == null)
          Debug.Log("Video canvas of VR Experience is missing");
        else
          video = Instantiate(videoVREngineering, new Vector3(objPosition.x, 5.0f, objPosition.z), objRotation);
      } else if (seen.collider.name == "HMD_4") {
        if (videoVRCustomerPreview == null)
          Debug.Log("Video canvas of VR Experience is missing");
        else
          video = Instantiate(videoVRCustomerPreview, new Vector3(objPosition.x, 4.0f, objPosition.z), objRotation);
      } else if (seen.collider.name == "HMD_5") {
        if (videoVRAirbrush == null)
          Debug.Log("Video canvas of VR Experience is missing");
        else
          video = Instantiate(videoVRAirbrush, new Vector3(objPosition.x, 5.0f, objPosition.z), objRotation);
      }

      // This adds at last an event trigger to exit the video when the user clicks on it
      EventTrigger trigger = video.AddComponent<EventTrigger>();
      EventTrigger.Entry entry = new EventTrigger.Entry();
      entry.eventID = EventTriggerType.PointerClick;
      entry.callback.AddListener((eventData) => { OnExitVideo(); });
      trigger.triggers.Add(entry);
    }
  }

  // Exits the video that is playing
  private void OnExitVideo() {
    museum.SetActive(true);    // Unhide the museum
    if(video != null) 
      Destroy(video);
    environmenMusic.Play();   // Continues playing the environment music
  }

  // Start the sightseeing
  public void OnClickStart() {
    welcomeScreen.SetActive(false);
    iTween.MoveTo(player, startPoint.transform.position, 15f);  // Moves to the signpost
  }

  // Restart the app
  public void OnClickRestart() {
    iTween.MoveTo(player, restartPoint.transform.position, 0.01f);  // Moves to the restart point
    welcomeScreen.SetActive(true);
  }

  // Exit application
  public void OnClickExitApplication() {
    Application.Quit();
  }
}
