using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class MovePlayerOnObjects : MonoBehaviour
{
    public List<GameObject> playerModels;
    public GameObject bubblesPrefap;
    public Transform instantiateBubbleHolder;
    public Transform exitPoint;
    public TrackPartsController track;

    [Range(0,6)]
    public float movement_duration = 1;
    [Range(0, 6)]
    public float instantiate_duration = 1;

    [Range(2, 10)]
    public float start_delay_duration = 2;

    public Vector3 offset;

    List<Tracks> tracks=new List<Tracks>();

    int in_part_index;
    int part_index;

    float time;
    float nextActionTime;
    bool startInstantiateBubbles = false;
    bool EndTheTrack;

    bool startDrawing = false;
    IEnumerator Start()
    {
        once = true;

        int playerModelIndex = PlayerPrefs.GetInt("playerSelectedModel",0);
        for (int i=0;i< playerModels.Count;i++)
        {
            if (i == playerModelIndex)
            {
                playerModels[i].SetActive(true);
            }
            else
            {
                playerModels[i].SetActive(false);
            }
        }
        startDrawing = false;
        yield return new WaitForSeconds(start_delay_duration);

        if (PlayerPrefs.GetString("SelectCharacter") == "unicorn")
        {
            playerModels[0].GetComponent<Animator>().SetInteger("animation",5);
        }

        if (SharedData.inst)
        {
            track.currentLetter = SharedData.inst.selectedLetter;
        }
        tracks = track.GetAllAvilabeTracksForLetter();

        startDrawing = true;
    }


    public Vector3 toPosition;
    void Update()
    {
        if (startDrawing == false)
            return;

        if (EndTheTrack == false)
        {

  //          Debug.Log("tracks.count "+ tracks.Count + "// part_index" + part_index);
//            Debug.Log("parts.count " + tracks[part_index].parts.Count + "// in_part_index" + in_part_index);
            toPosition = tracks[part_index].parts[in_part_index].position;
            transform.position = Vector3.MoveTowards(transform.position,  tracks[part_index].parts[in_part_index].position, movement_duration * Time.deltaTime);
            transform.LookAt( tracks[part_index].parts[in_part_index].position);

            if (transform.position ==  tracks[part_index].parts[in_part_index].position)
            {
                startInstantiateBubbles = true;
                in_part_index++;
            }

            if (startInstantiateBubbles)
            {
                // first wat to execute block of code periodiclly
                time += Time.deltaTime;
                if (time >= instantiate_duration)
                {
                    time = time - instantiate_duration;
                    InstantiateBubbles();
                }

                // second wat to execute block of code periodiclly
                //if (Time.time > nextActionTime)
                //{
                //    InstantiateBubbles();
                //    nextActionTime += instantiate_duration;
                //}
            }

            if (in_part_index >=  tracks[part_index].parts.Count)
            {

                if ( tracks.Count >= part_index)
                {
                    startInstantiateBubbles = false;
                    in_part_index = 0;
                    part_index++;

                    if ( tracks.Count <= part_index)
                    {
                        EndTheTrack = true;
                    }

                    if (PlayerPrefs.GetString("SelectCharacter") == "unicorn")
                    {
                        DrawEndOfTheTrachLine();
                    }
                }
                else
                {
                    EndTheTrack = true;
                }
            }
        }
        else
        {
            if (PlayerPrefs.GetString("SelectCharacter") == "unicorn")
            {
                playerModels[0].GetComponent<Animator>().SetInteger("animation", 0);
            }

            if (once)
            {
                once = false;
                SoundManager.inst?.PlayAudioLetterLatest();
            }
            //    transform.position = Vector3.MoveTowards(transform.position, exitPoint.position, movement_duration * Time.deltaTime);
            //    transform.LookAt(exitPoint.position);
        }
    }
    bool once=true;

    void InstantiateBubbles()
    {
        if (PlayerPrefs.GetString("SelectCharacter") == "unicorn")
        {
            DrawLine(transform.position);
        }
        else
        {
            GameObject bubble = Instantiate(bubblesPrefap, transform.position, transform.rotation);
            bubble.transform.SetParent(instantiateBubbleHolder);

        }
    }

    LineRenderer lineRenderer;
    public Material rainbwoMaterial;
    int previousPart=-1;
    int index = 1;
    void DrawLine(Vector3 vector3)
    {
        if (previousPart != part_index)
        {
            previousPart = part_index;
            index = 1;
            GameObject line = new GameObject();
            lineRenderer = line.AddComponent<LineRenderer>();
            lineRenderer.material = rainbwoMaterial;
            lineRenderer.generateLightingData = true;
            lineRenderer.SetPosition(0, tracks[part_index].parts[0].position);

        }

        index++;
        lineRenderer.positionCount = index;
        lineRenderer.SetPosition(index - 1, transform.position);
    }

    public void DrawEndOfTheTrachLine()
    {
        lineRenderer.SetPosition(index - 1, toPosition);
    }



    //    GameObject line = new GameObject();

    //    Debug.Log("part_index " + part_index + "// in_part_index" + in_part_index);
    //    //if ((in_part_index + 1) <= tracks[part_index].parts.Count)
    //    //{
    //        LineRenderer lineRendererObject = line.AddComponent<LineRenderer>();
    //        lineRendererObject.SetPosition(0, tracks[part_index].parts[in_part_index-1].position);
    //        lineRendererObject.SetPosition(1, tracks[part_index].parts[in_part_index ].position);
    //        lineRendererObject.material = rainbwoMaterial;
    //  //  }
    //}
}
