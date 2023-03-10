using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HoldfastSharedMethods;
using SceneChecker.SoundReplacement;
using SceneChecker.SoundReplacement.FactionVoiceLineSounds;
using SceneChecker.SoundReplacement.PrimarySounds;
using UnityEngine;

namespace SceneChecker.Core
{
    public class CustomFactionReplacementOverload : MonoBehaviour
    {

        [Header("Custom Faction Setup")]
        public List<CustomFactionSettings> customFactions;
        [Header("Global sounds")]
        public List<GlobalSoundOverloadSetting> globalSoundsToReplace;
        

        
        private CustomFactionSettingsManager _customFactionSettingsManager;
        private ReplaceElementManager _replaceElementManager;
        private SoundReplacementManager _soundReplacementManager;
        private FactionCountry _attackingFaction = FactionCountry.None;
        private FactionCountry _defendingFaction = FactionCountry.None;

        private void Awake()
        {
            _customFactionSettingsManager = new CustomFactionSettingsManager(customFactions);
            _soundReplacementManager = new SoundReplacementManager(customFactions, globalSoundsToReplace);
        }
        private void OnDestroy()
        {
            _soundReplacementManager?.RestoreSounds();
            _replaceElementManager?.OnDestroy();
        }
        
        public async void RoundDetailsAsync(FactionCountry attackingFaction,
            FactionCountry defendingFaction)
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(150); //seems needed otherwise the things wont get made

            _attackingFaction = attackingFaction;
            _defendingFaction = defendingFaction;

            _replaceElementManager =
                new ReplaceElementManager(_attackingFaction, _defendingFaction, _customFactionSettingsManager);
            
            await Task.Delay(ts);
            
            _replaceElementManager.ScoreBoardObjects = CreatePanelReplacementFactory.CreateScoreBoardPanelSettings();
            _replaceElementManager.TopBarObjects = CreatePanelReplacementFactory.CreateTopBarPanelSettings();
            _replaceElementManager.SpawnFactionObjects = CreatePanelReplacementFactory.CreateFactionPanelSettings();
            
            _replaceElementManager.ReplaceScoreBoard();
            _replaceElementManager.ReplaceTopBar();
            _replaceElementManager.ReplaceFactionPanel();
        }
        
        public async void OnPlayerSpawnedAsync(FactionCountry factionCountry)
        {
            if (_replaceElementManager.PlayerInfoObjects == null)
            {
                TimeSpan ts = TimeSpan.FromMilliseconds(150); //seems needed otherwise the things wont get made
                await Task.Delay(ts);
                _replaceElementManager.PlayerInfoObjects = CreatePanelReplacementFactory.CreatePlayerInfoPanelSettings();
            }
            _replaceElementManager.ReplacePlayerInfoPanel(factionCountry);
        }
        public async void OnRoundEndFactionWinnerAsync(FactionCountry factionCountry, FactionRoundWinnerReason reason)
        {
            if (factionCountry == FactionCountry.None) { return;}
            
            TimeSpan ts = TimeSpan.FromMilliseconds(5); //seems needed otherwise the things wont get made
            await Task.Delay(ts);            
            _replaceElementManager.OnRoundEndFactionWinner(factionCountry);
            
            ts = TimeSpan.FromMilliseconds(18500); //seems needed otherwise the things wont get made
            await Task.Delay(ts);
            
            _replaceElementManager.MapVoting();
        }
        
        
        [System.Serializable]
        public class CustomFactionSettings
        {
            [Tooltip("This is the faction which will be replaced ingame. Make sure not to override the same faction twice")]
            public FactionCountry FactionToOverride = FactionCountry.British;

            [Tooltip("UI settings which will be applied to the faction")]
            public FactionUIOverride UIOverride;
            
            [Tooltip("Voice lines which get replaced for this faction")]
            public List<VoiceLineOverloadSetting> voiceLineSettings;
        }
    }
}