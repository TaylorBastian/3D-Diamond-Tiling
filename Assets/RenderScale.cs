using UnityEngine;
using UnityEngine.VR;
    public class RenderScale : MonoBehaviour {
        [SerializeField] private float m_RenderScale = 1f;              //The render scale. Higher numbers = better quality, but trades performance

        void Start() {
            VRSettings.renderScale = m_RenderScale;
        }
    }

