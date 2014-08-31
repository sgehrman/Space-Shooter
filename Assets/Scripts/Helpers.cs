using UnityEngine;
using System.Collections;

public static class Helpers
{
	public static bool FireButtonDown ()
	{
		return (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space));
	}

	public static bool SmartBombButtonDown ()
	{
		return (Input.GetButtonDown("Fire2"));
	}

	public static bool SheildBombButtonDown ()
	{
		return (Input.GetButtonDown("Fire3"));
	}

}

