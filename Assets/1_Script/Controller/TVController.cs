using Class.Manager;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Class
{
    public class TVController : MonoBehaviour
    {
        [SerializeField] private Transform tvScreen;
        [SerializeField] private Transform noiseScreen;
        [SerializeField] private bool useScreen;

        [SerializeField] private Material tvNoiseMaterial;
        [SerializeField, Range(1, 20)] private float minDistance;   // minDistance에서 alpha가 0
        [SerializeField, Range(1, 20)] private float maxDistance;   // maxDistance에서 alpha가 1


        private float timeValue = 0f;

        private void Start()
        {
            OnOffTV(useScreen);
        }

        public void OnOffTV(bool onoff)
        {
            useScreen = onoff;

            if (useScreen)
            {
                tvScreen.gameObject.SetActive(true);
                noiseScreen.gameObject.SetActive(true);
            }
            else
            {
                tvScreen.gameObject.SetActive(false);
                noiseScreen.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            TVNoiseRender();
        }

        private void TVNoiseRender()
        { 
            if (!useScreen) return;

            timeValue += Time.deltaTime * 10f;
            tvNoiseMaterial.SetFloat("_TimeValue", timeValue);

            float sqrDis = Vector3.Magnitude(transform.position - GameManagerEx.Instance.Controller.transform.position);
            SetNoiseAlpha((sqrDis - minDistance) / (maxDistance - minDistance));
        }

        private void SetNoiseAlpha(float alpha)
        {
            // TODO : TV 치지직 소리 볼륨 연결
            alpha = Mathf.Clamp01(alpha);
            tvNoiseMaterial.SetFloat("_Alpha", alpha);
        }
    }
}