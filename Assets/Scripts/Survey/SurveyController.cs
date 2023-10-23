using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurveyController : MonoBehaviour
{
    public Camera playerCamera;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI text;
    public TextMeshProUGUI objective;
    [TextArea] public string[] questions;
    public Animator lightAnimator;
    public GameObject puzzle;
    public GameObject questionScreen;
    public GameObject endScreen;
    public GameObject plate;
    private int progress;

    private void Start() {
        questionText.text = questions[progress];
    }

    private void Update() {
        if (progress == 11)
        {
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(cameraRay, out RaycastHit hitinfo, 100.0f))
            {
                if (hitinfo.collider.name == "TurnBack")
                {
                    Destroy(hitinfo.transform.gameObject);
                    text.text = "Just kidding. Your food will be delivered soon.";
                    progress += 1;
                    Invoke("UpdateFood", 10.0f);
                }
            }
        }

        if (progress == 12 && plate.transform.childCount == 0)
        {
            progress += 1;
            text.text = "Great job. You can go sleep now, we'll have new tasks for you tomorrow.";
            objective.text = "Objective:\nGo to bed";
        }

        if (progress == 13)
        {
            Ray cameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(cameraRay, out RaycastHit hitinfo, 10.0f) && Input.GetKeyDown(KeyCode.E))
            {
                if (hitinfo.collider.name == "bed")
                {
                    Debug.Log("sleep");
                    Destroy(plate);
                    gameObject.SetActive(false);
                    puzzle.SetActive(true);
                    //progress += 1;
                }
            }
        }
    }

    public void UpdateQuestion(bool answer)
    {
        progress += 1;

        if (progress == 3)
        {
            lightAnimator.Play("LightFlash");
        }

        if (progress == 11)
        {
            questionScreen.SetActive(false);
            endScreen.SetActive(true);
            return;
        }

        questionText.text = questions[progress];
    }

    public void UpdateFood()
    {
        plate.SetActive(!plate.activeSelf);
        objective.text = "Objective:\nEnjoy your meal";
    }
}
