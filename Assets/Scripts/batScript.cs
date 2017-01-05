using UnityEngine;
using System.Collections;

public class batScript : MonoBehaviour {

	public void weaponBackswing()
	{
		Animator anim = GetComponent<Animator> ();
		anim.SetBool ("weaponUsed", false);
	}
}
