using System.Collections;
using UnityEngine;

namespace SelfDef.Interfaces
{
    public interface ICanChangeSettings
    {
        Vector3 StartPosition { get; set; }
        int LevelIndex { get; set; }
        bool StopAnimation { set; get; }

        IEnumerator Explode(float delay);
        void ResetPosition();
        void Lock();
        void Kill();
    }
}