using System.Collections.Generic;
using UnityEngine;

public class ID : MonoBehaviour
{
    private string _name = "";
    private int? _listIndex = null;

    public void Init(string baseName)
    {
        _name = baseName;
        DisplayName();
    }

    public void SetListIndex(int? listIndex)
    {
        _listIndex = listIndex;
        DisplayName();
    }

    private void DisplayName()
    {
        name = _name;
        if (_listIndex.HasValue) {
            name += " " + _listIndex;
        }
    }
}

