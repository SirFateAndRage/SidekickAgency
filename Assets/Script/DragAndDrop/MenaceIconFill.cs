using Menace;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class MenaceIconFill : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private MenaceRecyclableObject _menaceRecyclable;

        private float _menaceMultiplicator;
        private float _currentModifier;
        private CancellationTokenSource _cancellationToken;
        private MenaceOutCome _menaceOutCome;

        private int _menaceId;

        public void InitFillAmount(float menaceMultiplicator, int menaceId,MenaceOutCome menaceOutCome)
        {
            _menaceMultiplicator = menaceMultiplicator;
            _currentModifier = _menaceMultiplicator;
            _menaceOutCome = menaceOutCome;
            _menaceId = menaceId;
        }

        public void Reset()
        {
            _fillImage.fillAmount = 0;
            CancelTask();
        }

        public void SetFillSpeed(float menaceMultiplicator)
        {
            _currentModifier = menaceMultiplicator;

            CancelTask();

            _cancellationToken = new CancellationTokenSource();

            InitFill(_cancellationToken.Token);

        }

        public void CancelTask()
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

                _fillImage.fillAmount += _currentModifier * Time.deltaTime /100;

                _fillImage.color = _colorGradient.Evaluate(_fillImage.fillAmount);

                if (_fillImage.fillAmount >= 1f)
                {
                    CancelTask();
                    _menaceOutCome.MenaceLost(_menaceId);
                    _menaceRecyclable.Recycle();
                }

                if (_fillImage.fillAmount <= 0f)
                {
                    CancelTask();
                    _menaceOutCome.MenaceDefeted(_menaceId);
                    _menaceRecyclable.Recycle();
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
