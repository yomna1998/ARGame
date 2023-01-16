   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPartsController : MonoBehaviour
{
    public Alphabets currentLetter;
    public List<Letters> letters;
     

    public List<Tracks> GetAllAvilabeTracksForLetter() 
    {
        Letters letter= letters.Find(x => x.alphabets == currentLetter);
        return letter.letterPart.tracks;
        
    }
}

[System.Serializable]
public class Tracks
{
    public List<Transform> parts;
}

[System.Serializable]
public class Letters
{
    public Alphabets alphabets;
    public LetterParts letterPart;
}

[System.Serializable]
public enum Alphabets
{
    alef, baa, Taa, Thaa, Jeem, h7aa, khaa, daal, thaal, raa, zain
        , seen, sheen, sad, dhad, tha, dha, aen, ghain, faa, gaf, kaf,
    lam, mem, noon, haa, waw, yaa
}