using Assets.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserDisplayHandlerBase
{
    private readonly Text _textToChange;
    public UserDisplayHandlerBase(Text textToChange)
    {
        _textToChange = textToChange;
    }

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
                _index = value;
                RenderData();
            }
        }
    }

    private List<UserDto> _users;
    public List<UserDto> Users
    {
        get => _users;
        set
        {
            if (_users != value)
            {
                _users = value;
                Index = 0;
            }
        }
    }
    public void RenderData()
    {
        if (Users.Count > Index || Index < 0)
            _textToChange.text = Users[Index].Email;
        else
            _textToChange.text = string.Empty;
    }
}

public class UserDisplayHandler : MonoBehaviour
{
    public Text Email;

    private UserDisplayHandlerBase _handler;
    public UserDisplayHandlerBase Handler
    {
        get
        {
            if (_handler == null)
                _handler = new UserDisplayHandlerBase(Email);

            return _handler;
        }
    }

}
