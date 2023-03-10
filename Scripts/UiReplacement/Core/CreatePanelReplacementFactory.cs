using TMPro;
using UnityEngine.UI;

namespace SceneChecker.Core
{
    public static class CreatePanelReplacementFactory
    {
        public static RoundEndObjects CreateRoundEndPanelSettings()
        {
            var roundEnd = GetRefenceFactory.GetRoundEndInfo("Faction Round Winner Panel");
            TextMeshProUGUI roundEnReasondText = GetRefenceFactory.GetRoundEndReasonText("Round End Reason Text");
            TextMeshProUGUI roundEndFactionWinnerText = GetRefenceFactory.GetRoundEndFactionWinnerText("Faction Winner Text");


            var panelDumb = GetRefenceFactory.GetRoundEndScoreBoard();

            //defo refactor this at some point. Horrible.
            return new RoundEndObjects(roundEnd.winningEmblem, roundEndFactionWinnerText,roundEnReasondText,
                panelDumb.attackingImage, panelDumb.attackingFlag, panelDumb.attackingText,
                panelDumb.defendingImage, panelDumb.defendingFlag, panelDumb.defendingText);
        }
        
        
        
        public static ScoreBoardObjects CreateScoreBoardPanelSettings()
        {
            string attackingName = "Attacking Faction - Faction Scores Section";
            string defendingName = "Defending Faction - Faction Scores Section";

            (Image img, TextMeshProUGUI txt) attacking = GetRefenceFactory.GetImageAndTextForScoreBoard(attackingName);
            (Image img, TextMeshProUGUI txt) defending = GetRefenceFactory.GetImageAndTextForScoreBoard(defendingName);
            
            return new ScoreBoardObjects(attacking.img, defending.img,
                attacking.txt, defending.txt);
        }
        
        public static SpawnFactionObjects CreateFactionPanelSettings()
        {
            string attackingName = "Attacking - Spawn Faction Panel";
            string defendingName = "Defending - Spawn Faction Panel";
            (Image main, Image disabled) attacker = GetRefenceFactory.GetSpawnMenuImage(attackingName);
            (Image main, Image disabled) defenders = GetRefenceFactory.GetSpawnMenuImage(defendingName);


            Image headerEmblem = GetRefenceFactory.GetSpawnMenuHeaderImage();
            
            return new SpawnFactionObjects(attacker.main, attacker.disabled,
                defenders.main, defenders.disabled,headerEmblem);
        }

        public static TopBarObjects CreateTopBarPanelSettings()
        {
            Image topBarAttacker = GetRefenceFactory.GetTopBarImage("Ally Flag Image");
            Image topBarDefender = GetRefenceFactory.GetTopBarImage("Enemy Flag Image");
            return new TopBarObjects(topBarAttacker, topBarDefender);
        }

        public static PlayerInfoObjects CreatePlayerInfoPanelSettings()
        {
            Image image = GetRefenceFactory.GetPlayerInfoImage("Player Health Panel");
            return new PlayerInfoObjects(image);
        }

        public static MapVotingObjects CreateMapVotingPanelSettings()
        {
            MapVotingObjects objects = new MapVotingObjects(GetRefenceFactory.GetMapVotingPanel());
            return objects;
        }
    }
}