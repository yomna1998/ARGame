using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
	private WritingHandler writingHandler;
	public Animator winDialog;
	public GameObject menu;

	public AudioClip clickReleaseSFx;

	void Start ()
	{
		//Setting up the writingHandler reference
		GameObject letters = HierrachyManager.FindActiveGameObjectWithName ("Letters");
		if (letters != null)
			writingHandler = letters.GetComponent<WritingHandler> ();
	}
	
	//Load the next number
	public void LoadTheNextLetter ()
	{
		Debug.Log ("LoadTheNextLetter");
		if (writingHandler == null) {
			return;
		}

		//play button sound
		if (clickReleaseSFx != null) {
			Debug.Log("WritingHandler.onSatate " + WritingHandler.onSatate);
			if(WritingHandler.onSatate)
			AudioSource.PlayClipAtPoint (clickReleaseSFx, Vector3.zero);
		}

		writingHandler.LoadNextLetter ();
	}


	
	//Load the previous/number
	public void LoadThePreviousLetter ()
	{
		Debug.Log ("LoadThePreviousLetter");
		if (writingHandler == null) {
			return;
		}

		//play button sound
		if (clickReleaseSFx != null) {
            Debug.Log("WritingHandler.onSatate " + WritingHandler.onSatate);
			if (WritingHandler.onSatate)
				AudioSource.PlayClipAtPoint (clickReleaseSFx, Vector3.zero);
		}
		
		writingHandler.LoadPreviousLetter ();
		
	}
	
	//Load the current Letter
	public void LoadLetter (Object ob)
	{
		if (ob == null) {
			return;
		}
		
		WritingHandler.currentLetterIndex = int.Parse (ob.name.Split ('-') [1]);
		Application.LoadLevel ("AlphabetWriting");
	}
	
	//Erase the current Letter
	public void EraseLetter (Object ob)
	{
		if (writingHandler == null) {
			return;
		}

		//play button sound
		if (clickReleaseSFx != null) {
            if (WritingHandler.onSatate)
				AudioSource.PlayClipAtPoint (clickReleaseSFx, Vector3.zero);
		}


		writingHandler.RefreshProcess ();
		
	}
	
	//Close win dialog
	public void CloseWinDialog (Object ob)
	{
		writingHandler.letters [WritingHandler.currentLetterIndex].SetActive (true);
		menu.SetActive (true);
		GameObject [] linesRenderes = GameObject.FindGameObjectsWithTag ("LineRenderer");
		foreach (GameObject line in linesRenderes) {
			line.GetComponent<LineRenderer> ().enabled = true;
		}
		
		GameObject [] circlePoint = GameObject.FindGameObjectsWithTag ("CirclePoint");
		foreach (GameObject cp in circlePoint) {
			cp.GetComponent<MeshRenderer> ().enabled = true;
		}
		winDialog.SetBool ("isFadingIn", false);
	}
	
	//Load alphabet menu
	public void LoadAlphabetMenu (Object ob)
	{
		//play button sound
		if (clickReleaseSFx != null) {
            if (WritingHandler.onSatate)
				AudioSource.PlayClipAtPoint (clickReleaseSFx, Vector3.zero);
		}

		PlayerPrefs.SetString("BackToAllLetters", "false");
        SceneManager.LoadScene("Mainmenu");
    }

    public void LoadAllLettersPage()
    {
        PlayerPrefs.SetString("BackToAllLetters", "true");
        SceneManager.LoadScene("Mainmenu");
    }

    //ToggleMusic on/off
    public void ToggleMusic (Object ob)
	{
		WritingHandler.onSatate = !WritingHandler.onSatate;
        //Setting up the writingHandler reference
        AudioSource aud = null;
		GameObject Gen = HierrachyManager.FindActiveGameObjectWithName ("General");
		if (Gen != null) {
			if(General.MusicFlag==0){
				aud = Gen.GetComponent<AudioSource> ();
				aud.Stop ();
				General.MusicFlag=1;
			}
			else{
				aud = Gen.GetComponent<AudioSource> ();
				aud.Play ();
				General.MusicFlag=0;
			}
			
		}
	}







}