using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class MenaceIcon : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Gradient _colorGradient;

        private float _currentFillSpeed;
        private CancellationTokenSource _cancellationToken;

        public void InitFillAmount(float fillAmount)
        {
            _fillImage.fillAmount = fillAmount / 100;
        }

        public void SetFillSpeed(float menaceMultiplicator)
        {
            _currentFillSpeed = menaceMultiplicator;

            CancelTask();

            _cancellationToken = new CancellationTokenSource();

            InitFill(_cancellationToken.Token);

        }

        private void CancelTask()
        {
            if(_cancellationToken != null)
            _cancellationToken.Cancel();

            _cancellationToken = null;
        }

        private async void InitFill(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                _fillImage.fillAmount += _currentFillSpeed * Time.deltaTime /100;

                _fillImage.color = _colorGradient.Evaluate(_fillImage.fillAmount);

                if (_fillImage.fillAmount >= 1f)
                {
                    //avisar que se completó a alguin
                }

                if (_fillImage.fillAmount <= 0f)
                {
                    //avisar que se completó a alguin
                }

                await Task.Yield();
            }
        }

        private void OnDestroy()
        {
            CancelTask();
        }
    }
}
