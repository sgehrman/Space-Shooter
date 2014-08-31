using UnityEngine;
using System.Collections;

public static class Helpers
{
	public static bool FireButtonDown ()
	{
		return (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space));

	}

}

