using System;
using HoldfastSharedMethods;
using UnityEngine;

namespace SceneChecker.Core
{
    public class ReplaceElementManager
    {
        
        
        private readonly CustomFactionSettingsManager _customFactionSettingsManager;

        public ScoreBoardObjects ScoreBoardObjects { get; set; } = null;
        public TopBarObjects TopBarObjects { get; set; } = null;
        public SpawnFactionObjects SpawnFactionObjects { get; set; } = null;
        public PlayerInfoObjects PlayerInfoObjects { get; set; } = null;

        private MapVotingObjects _mapVotingObjects;
        private RoundEndObjects _roundEndObjects;
        
        public FactionCountry AttackingFaction { get; }
        public FactionCountry DefendingFaction { get; }

        private FactionUIOverride _defenderSettings = null;
        private FactionUIOverride _attackerSettings = null;


        public ReplaceElementManager(FactionCountry attackingFaction, FactionCountry defendingFaction, CustomFactionSettingsManager settingsManager)
        {
            _customFactionSettingsManager = settingsManager;
            
            AttackingFaction = attackingFaction;
            DefendingFaction = defendingFaction;

            _attackerSettings = settingsManager.GetSettingsOrNull(AttackingFaction);
            _defenderSettings = settingsManager.GetSettingsOrNull(DefendingFaction);
        }
        
        public void ReplaceFactionPanel()
        {
            Sprite attackerFactionSprite = _attackerSettings != null ? _attackerSettings.factionSelectionSprite : null;
            Sprite attackerFactionDisabledSprite = _attackerSettings != null ? _attackerSettings.factionSelectionDisabledSprite : null;
            Sprite defenderFactionSprite = _defenderSettings != null ? _defenderSettings.factionSelectionSprite : null;
            Sprite defenderFactionDisabledSprite = _defenderSettings != null ? _defenderSettings.factionSelectionDisabledSprite : null;

            Sprite headerEmblem = _attackerSettings != null ? _attackerSettings.selectClassHeaderEmblem : null;
            SpawnFactionObjects?.Replace(attackerFactionSprite, attackerFactionDisabledSprite, defenderFactionSprite, defenderFactionDisabledSprite,
                headerEmblem);
        }
        public void ReplaceTopBar()
        {
            Sprite attackerTopSprite = _attackerSettings != null ? _attackerSettings.topBarSprite : null;
            Sprite defenderTopSprite = _defenderSettings != null ? _defenderSettings.topBarSprite : null;
            TopBarObjects?.Replace(attackerTopSprite, defenderTopSprite);
        }

        public void ReplaceScoreBoard()
        {
            Sprite attackerScoreboardSprite = _attackerSettings != null ? _attackerSettings.scoreboardSprite : null;
            Sprite defenderScoreboardSprite = _defenderSettings != null ? _defenderSettings.scoreboardSprite : null;

            string attackerName = _attackerSettings != null ? _attackerSettings.customNameToUse : string.Empty;
            string defenderName = _defenderSettings != null ? _defenderSettings.customNameToUse : string.Empty;
            ScoreBoardObjects?.Replace(attackerScoreboardSprite, defenderScoreboardSprite, attackerName, defenderName);
        }
        
        public void ReplacePlayerInfoPanel(FactionCountry playersFaction)
        {
            if (_customFactionSettingsManager.FactionToSettings.TryGetValue(playersFaction,
                    out FactionUIOverride playerSettings))
            {
                Sprite sprite = playerSettings.playerInfoIconSprite;
                PlayerInfoObjects.Replace(sprite);
            }
            
        }

        public void OnRoundEndFactionWinner(FactionCountry winningFaction)
        {
            string roundEndReason = string.Empty;
            if (winningFaction == AttackingFaction)
            {
                FactionUIOverride loserFaction = _customFactionSettingsManager.GetSettingsOrNull(DefendingFaction);
                roundEndReason = $"The {loserFaction.customNameAdjective} troops in the battlefield have been eliminated.";
            }
            else if (winningFaction == DefendingFaction)
            {
                FactionUIOverride loserFaction = _customFactionSettingsManager.GetSettingsOrNull(AttackingFaction);
                roundEndReason = $"The {loserFaction.customNameAdjective} troops in the battlefield have been eliminated.";
            }
            _roundEndObjects = CreatePanelReplacementFactory.CreateRoundEndPanelSettings();
            FactionUIOverride settings = _customFactionSettingsManager.GetSettingsOrNull(winningFaction);
            _roundEndObjects.ReplaceRoundEndPopup(settings.factionSelectionSprite, settings.customNameAdjective, roundEndReason);

            _roundEndObjects.ReplaceRoundEndPanel(_attackerSettings.squareRoundEndBoardSprite, _attackerSettings.scoreboardSprite, _attackerSettings.customNameToUse,
                _defenderSettings.squareRoundEndBoardSprite, _defenderSettings.scoreboardSprite, _defenderSettings.customNameToUse);
            

        }

        public void MapVoting()
        {
            _mapVotingObjects = CreatePanelReplacementFactory.CreateMapVotingPanelSettings();
            _mapVotingObjects.Replace(_customFactionSettingsManager);
        }

        public void OnDestroy()
        {
            _mapVotingObjects?.Destroy();
            _roundEndObjects?.Destroy();
            PlayerInfoObjects?.Destroy();
            SpawnFactionObjects?.Destroy();
            TopBarObjects?.Destroy();
            ScoreBoardObjects?.Destroy();
        }
    }
}