using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

  // CHANGES
  // 1. GameManager reference added to remove script acquirement from the Update method
  // 2. ParticleSystem reference added to remove the acquirement from the Update method
  // 3. Update and Start methods completely removed
  // 4. Debug log information removed

  public ParticleSystem pSystem;
  public GameManager scoreScript;

  void OnCollisionEnter(Collision col)
  {
    if (col.gameObject.CompareTag("Throwable"))
    {
        //Score Point
        scoreScript.score++;
        //Particle effect
        pSystem.Play();
    }
  }
}
