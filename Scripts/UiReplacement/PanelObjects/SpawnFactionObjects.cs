using UnityEngine;
using UnityEngine.UI;

namespace SceneChecker
{
    public class SpawnFactionObjects
    {
        private readonly Image _attackersEmblemImage;
        private readonly Image _attackersEmblemImageDisabled;
        
        private readonly Image _defendersEmblemImage;
        private readonly Image _defendersEmblemImageDisabled;

        private readonly Image _headerEmlemImage;

        private bool _isValid = false;

        public SpawnFactionObjects(Image attackersEmblemImage, Image attackersEmblemImageDisabled, Image defendersEmblemImage, Image defendersEmblemImageDisabled,
            Image headerEmlemImage)
        {
            _attackersEmblemImage = attackersEmblemImage;
            _attackersEmblemImageDisabled = attackersEmblemImageDisabled;
            _defendersEmblemImage = defendersEmblemImage;
            _defendersEmblemImageDisabled = defendersEmblemImageDisabled;
            _headerEmlemImage = headerEmlemImage;

            _isValid = _attackersEmblemImage != null && _attackersEmblemImageDisabled != null &&
                       _defendersEmblemImage != null && _defendersEmblemImageDisabled != null && _headerEmlemImage != null;
        }

        public void Replace(Sprite attackerImage, Sprite attackerImageDisabled, Sprite defenderImage, Sprite defenderImageDisabled, Sprite headerEmblem)
        {
            if (!_isValid){return;}
            
            if (attackerImage != null && attackerImageDisabled != null)
            {
                _attackersEmblemImage.overrideSprite = attackerImage;
                _attackersEmblemImageDisabled.overrideSprite = attackerImageDisabled;
            }
            if (defenderImage != null && defenderImageDisabled != null)
            {
                _defendersEmblemImage.overrideSprite = defenderImage;
                _defendersEmblemImageDisabled.overrideSprite = defenderImageDisabled;
            }

            if (headerEmblem != null)
            {
                _headerEmlemImage.overrideSprite = headerEmblem;
            }
        }

        public void Destroy()
        {
            if (!_isValid){return;}

            _attackersEmblemImage.overrideSprite = null;
            _attackersEmblemImageDisabled.overrideSprite = null;
            _defendersEmblemImage.overrideSprite = null;
            _defendersEmblemImageDisabled.overrideSprite = null;
            _headerEmlemImage.overrideSprite = null;
        }
    }
}