using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteRotator : MonoBehaviour {


	void FixedUpdate(){
		transform.Rotate (0, 60.0f * Time.deltaTime, 0);
	}
}
