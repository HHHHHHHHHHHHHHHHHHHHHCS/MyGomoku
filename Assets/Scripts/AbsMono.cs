using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMono
{
    void OnAwake();
    void OnStart();
    void OnUpdate();
    void OnRelease();
}
