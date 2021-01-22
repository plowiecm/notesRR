using Assets.Common;
using Assets.Constants;
using Assets.Extensions;
using Assets.Models;
using Assets.Models.Responses;
using Assets.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GroupsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetGroups();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Text GroupName;

    public List<GroupDto> Groups = new List<GroupDto>();

    public GameObject UserDetailPrefab;

    public GameObject ParentPanel;

    private int _index = -1;
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            if (_index != value)
            {
                if (value < Groups.Count && value >= 0)
                {
                    _index = value;
                    RenderData();
                }
            }
        }
    }

    public void ChangeDisplayResult(int direction)
    {

        if (direction == (int)Direction.Next)
            Index++;
        else
            Index--;   
    }

    private void RenderData()
    {
        GroupName.text = Groups[Index].Name;
        ParentPanel.DestroyAllChildren();

        var i = 2;
        foreach (var member in Groups[Index].Members)
        {
            var detail = Instantiate(UserDetailPrefab);
            detail.transform.SetParent(ParentPanel.transform);
            detail.GetComponent<TextMeshProUGUI>().text = member.Username;

            var rect = detail.GetComponent<RectTransform>();
            rect.SetLeft(0);
            rect.SetRight(0);
            rect.SetTop(50 * 1);
            rect.SetBottom(0);

            i++;
        }
    }


    public async void GetGroups()
    {
        var response = await StateManager.HttpServiceClient.GetAsync<IEnumerable<GroupDto>>(FridgeNotesEndpoints.GetGroups);

        if (!response.Any())
            return;

        Groups = response.ToList();
        Index = 0;
    }
}
