using SceneChecker.HierarchyTool;
using SceneChecker.Scripts.Logging;
using UnityEngine;
using UnityEngine.UI;

namespace SceneChecker
{
    public class TopBarObjects
    {
        private static readonly ILog Logger = LogFactory.GetLogger(typeof(TopBarObjects), LogLevelsEnum.All);

        
        private readonly Image _attackingImage;
        private readonly Image _defendingImage;
        
        
        public TopBarObjects(Image attackingImage, Image defendingImage)
        {
            _attackingImage = attackingImage;
            _defendingImage = defendingImage;
        }

        public void Replace(Sprite attackerImage, Sprite defenderImage)
        {
            if (attackerImage == null)
            {
                Logger.LogError($"{nameof(attackerImage)} is null");
            }
            else
            {
                _attackingImage.overrideSprite = attackerImage;
            }
            if (defenderImage == null)
            {
                Logger.LogError($"[{nameof(defenderImage)} is null");
            }
            else
            {
                _defendingImage.overrideSprite = defenderImage;
            }
        }

        public void Destroy()
        {
            _attackingImage.overrideSprite = null;
            _defendingImage.overrideSprite = null;
        }
    }
}