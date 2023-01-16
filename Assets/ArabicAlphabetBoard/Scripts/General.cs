using UnityEngine;
using System.Collections;

//General class
public class General : MonoBehaviour
{
		private static General instance;
    	public static int MusicFlag;
		// Use this for initialization
		void Start ()
		{
				if (instance == null) {
						instance = this;
						DontDestroyOnLoad (gameObject);
				} else {
						Destroy (gameObject);
				}
		}

}
