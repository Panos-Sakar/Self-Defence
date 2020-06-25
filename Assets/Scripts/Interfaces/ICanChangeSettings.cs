using System.Collections;
using UnityEngine;

namespace SelfDef.Interfaces
{
    public interface ICanChangeSettings
    {
        Vector3 StartPosition { get; set; }
        int LevelIndex { get; set; }

        bool StopAnimation { set; get; }

        void ResetPosition();
        IEnumerator Explode(Vector3 newPosition , bool destroyAfterExplode);
        void Kill();

        void Lock();
    }
}