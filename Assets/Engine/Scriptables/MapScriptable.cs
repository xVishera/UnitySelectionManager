using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Engine.Scriptables
{
    [CreateAssetMenu(fileName = "New Map", menuName = "Hall of Brothers/Map")]
    public class MapScriptable : ScriptableObject
    {
        [Header("Data")]
        public string Name;
        [Tooltip("Preview to be displayed to the user")]
        public Sprite Thumbnail;
        [Tooltip("Scene to be loaded upon selection")]
        public Object Scene;
    }
}