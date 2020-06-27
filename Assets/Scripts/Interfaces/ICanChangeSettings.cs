using System.Collections;
using UnityEngine;

namespace SelfDef.Interfaces
{
    public interface ICanChangeSettings
    {
        Vector3 StartPosition { get; set; }
        bool StopAnimation { set; get; }
        
        string TipText { set; get; }

        IEnumerator Explode(float delay);
        void ResetPosition();
        void Lock();
        void Kill();
        Sprite GetIcon();
    }
}