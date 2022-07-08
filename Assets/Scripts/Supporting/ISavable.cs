using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavable
{
    public void SaveData(SaveData saveData);

    public void LoadData(SaveData saveData);
}
