using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LevelBase : MonoBehaviour {

    protected bool levelActive;

    public abstract void StartLevel();
    public abstract void EndLevel();
}
