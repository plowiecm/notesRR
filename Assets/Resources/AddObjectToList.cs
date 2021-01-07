using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddObjectToList : MonoBehaviour
{
    public GameObject itemTemplate;
    public GameObject content;

    // Start is called before the first frame update
    void OnEnable()
    {
        removeAllNotes();

        //todomp get notes from api
        List<Note> notes = getNotesFromApi();
        notes.ForEach(note =>
        {
            var copy = Instantiate(itemTemplate);
            copy.transform.SetParent(content.transform);
        });

    }

    private List<Note> getNotesFromApi()
    {
        List<Note> a = new List<Note>();

        IEnumerable<Vuforia.TrackableBehaviour> tbs = Vuforia.TrackerManager.Instance.GetStateManager().GetActiveTrackableBehaviours();
        var tb = tbs.FirstOrDefault();

        //getnotes with tb.name
        a.Add(new Note());
        a.Add(new Note());
        a.Add(new Note());
        a.Add(new Note());
        a.Add(new Note()); 
        a.Add(new Note());
        a.Add(new Note());

        return a;
    }

    void OnDisable()
    {
        removeAllNotes();
    }

    private void removeAllNotes()
    {
        int childs = content.transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(content.transform.GetChild(i).gameObject);
        }
    }
}
