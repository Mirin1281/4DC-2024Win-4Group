using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Credit : MonoBehaviour
{
	public void SwitchScene()
	{
		SceneManager.LoadScene("Credit", LoadSceneMode.Single);
	}
}
