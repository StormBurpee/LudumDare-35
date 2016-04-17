using UnityEngine;
using System.Collections;

public abstract class LevelBase : MonoBehaviour {

    protected bool levelActive;

    public abstract void StartLevel();
    public abstract void EndLevel();
}
