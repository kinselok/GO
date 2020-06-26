using System;

using UnityEngine;

public interface IInputHandler
{
    event Action OnUp;
    event Action OnDown;
    event Action OnLeft;
    event Action OnRight;
    event Action OnPress;
    event Action OnRelease;
    event Action<Vector3> OnPointerMove;
}
