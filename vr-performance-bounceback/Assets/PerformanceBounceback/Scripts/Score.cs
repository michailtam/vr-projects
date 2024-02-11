using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

  public GameManager gameManager;
  private string prevScore; // The previous saved score
  // CHANGES
  // 1. Text component reference added, to remove it from the Update method
  // 2. Score management on score board improved. The score gets changed only
  // if it differs from the previous score
  public Text text;

  void Start()
  {
    prevScore = gameManager.score.ToString();
  }

  // Update is called once per frame
  void Update () {
    // Change the score on score board only if it differs from the previous score
    if(string.Compare(prevScore, gameManager.score.ToString()) != 0) {
      text.text = "Score: " + gameManager.score.ToString();
      prevScore = gameManager.score.ToString();
    }
	}
}
