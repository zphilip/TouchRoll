using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour
{
	void Start ()
	{
		iTween.RotateTo(gameObject, iTween.Hash("x", 28.5, "time", 5, "easeType", "easeInOutQuad", "loopType", "pingPong"));
	}
}
