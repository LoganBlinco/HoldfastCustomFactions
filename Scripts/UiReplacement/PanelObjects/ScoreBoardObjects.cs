using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneChecker
{
    public class ScoreBoardObjects
    {
        private readonly Image _attackingImage;
        private readonly Image _defendingImage;

        private readonly TextMeshProUGUI _attackingText;
        private readonly TextMeshProUGUI _defendingText;

        private bool _isValid;

        public ScoreBoardObjects(Image attackingImage, Image defendingImage, TextMeshProUGUI attackingText, TextMeshProUGUI defendingText)
        {
            _attackingImage = attackingImage;
            _defendingImage = defendingImage;
            _attackingText = attackingText;
            _defendingText = defendingText;
            _isValid = _attackingImage != null && _defendingImage != null;
        }

        public void Replace(Sprite attackerImage, Sprite defenderImage, string attackerName, string defenderName)
        {
            if (attackerImage != null)
            {
                _attackingImage.overrideSprite = attackerImage;
            }

            if (defenderImage != null)
            {
                _defendingImage.overrideSprite = defenderImage;
            }

            if (attackerName != string.Empty)
            {
                _attackingText.text = attackerName;
            }
            if (defenderName != string.Empty)
            {
                _defendingText.text = defenderName;
            }
        }

        public void Destroy()
        {
            if (!_isValid){return;}
            _attackingImage.overrideSprite = null;
            _defendingImage.overrideSprite = null;
        }
    }
}