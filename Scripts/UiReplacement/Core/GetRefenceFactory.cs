using System.Linq;
using SceneChecker.HierarchyTool;
using SceneChecker.Scripts.Logging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneChecker.Core
{
    public static class GetRefenceFactory
    {
        private static readonly ILog Logger = LogFactory.GetLogger(typeof(GetRefenceFactory), LogLevelsEnum.Information);


        public static MapVotingCard[] GetMapVotingPanel(string panelName = "Map Voting Panel")
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);
            GameObject mapVotingPanel = null;
            foreach (GameObject working in allObjects)
            {
                if (working.name == panelName)
                {
                    mapVotingPanel = working;
                    break;
                }
            }
            if (mapVotingPanel == null)
            {
                Logger.LogError($"Map voting panel is null");
                return null;
            }

            Transform canvasGroup = mapVotingPanel.transform.GetChild(1);
            Transform horizontalLayout = canvasGroup.GetChild(2);
            
            Transform votingCard1 = horizontalLayout.GetChild(0);
            Transform votingCard2 = horizontalLayout.GetChild(1);
            Transform votingCard3 = horizontalLayout.GetChild(2);
            Transform votingCard4 = horizontalLayout.GetChild(3);
            MapVotingCard[] cards = new[]
            {
                GetMapVotingCardReference(votingCard1),
                GetMapVotingCardReference(votingCard2),
                GetMapVotingCardReference(votingCard3),
                GetMapVotingCardReference(votingCard4)
            };
            return cards;
        }
        
        public static MapVotingCard GetMapVotingCardReference(Transform mapVotingCard)
        {
            Transform attackingFaction = mapVotingCard.GetChild(4);
            Image attackingImage = attackingFaction.GetComponent<Image>();
            
            Transform defendingFaction = mapVotingCard.GetChild(6);
            Image defendingImage = defendingFaction.GetComponent<Image>();
            return new MapVotingCard(attackingImage, defendingImage);
        }

        public static (Image attackingImage, Image attackingFlag, Image defendingImage, Image defendingFlag,
            TextMeshProUGUI attackingText, TextMeshProUGUI
            defendingText)
            GetRoundEndScoreBoard(string objectName = "End of Match Final Panel")
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);
            GameObject endOfMatchPanel = null;
            foreach (GameObject working in allObjects)
            {
                if (working.name == objectName)
                {
                    endOfMatchPanel = working;
                    break;
                }
            }

            if (endOfMatchPanel == null)
            {
                Logger.LogError($"could not find {objectName}");
                return (null, null, null, null, null, null);
            }

            Transform VerticalLayout = endOfMatchPanel.transform.GetChild(3);
            Transform scoreContainer = VerticalLayout.GetChild(1);
            
            Transform leftSide = scoreContainer.GetChild(0);
            var leftData = GetSideInfo(leftSide);
            Transform rightSide = scoreContainer.GetChild(2);

            var rightData = GetSideInfo(rightSide);

            (Image squareFactionImage, Image backgroundFlagImage, TextMeshProUGUI text) GetSideInfo(
                Transform sideTransform)
            {
                Transform headerFaction = sideTransform.GetChild(0);
                
                Transform image = headerFaction.GetChild(1);
                Image imageComponent = image.GetComponent<Image>();
                
                Transform flagBackGround = headerFaction.GetChild(3);
                Image flagImage = flagBackGround.GetComponent<Image>();

                Transform factionName = headerFaction.GetChild(4);
                TextMeshProUGUI factionTextComponent = factionName.GetComponent<TextMeshProUGUI>();
                return (imageComponent, flagImage, factionTextComponent);
            }

            return (leftData.squareFactionImage, leftData.backgroundFlagImage, rightData.squareFactionImage,
                rightData.backgroundFlagImage, leftData.text, rightData.text);
        }


        public static (Image winningEmblem, TextMeshProUGUI factionWinner) GetRoundEndInfo(string objectName)
        {
            GameObject factionRoundWinner = GameObject.Find(objectName);
            if (factionRoundWinner == null)
            {
                Logger.LogError($"We could not find {objectName}");
                return (null, null);
            }

            Transform baseTransform = factionRoundWinner.transform;
            
            TextMeshProUGUI factionWinnerLabel = null;
            Image factionEmblem = null;


            var winnerText = baseTransform.GetChild(0);
            factionWinnerLabel = winnerText.GetComponent<TextMeshProUGUI>();

            var emblemHolder = baseTransform.GetChild(2);
            factionEmblem = emblemHolder.GetComponent<Image>();
            
            if (factionWinnerLabel == null)
            {
                Logger.LogError("faction winner label is null");
            }

            if (factionEmblem == null)
            {
                Logger.LogError("factionEmblem is null");
            }

            return (factionEmblem, factionWinnerLabel);
        }

        public static TextMeshProUGUI GetRoundEndFactionWinnerText(string objectName)
        {
            GameObject winningFactionText = GameObject.Find(objectName);
            if (winningFactionText == null)
            {
                Logger.LogError($"We could not find {objectName}");
                return null;
            }

            TextMeshProUGUI textComponent = winningFactionText.GetComponent<TextMeshProUGUI>();
            if (textComponent == null) { return null; }
            return textComponent;
        }

        public static TextMeshProUGUI GetRoundEndReasonText(string objectName)
        {
            GameObject roundEndReasonText = GameObject.Find(objectName);
            if (roundEndReasonText == null)
            {
                Logger.LogError($"We could not find {objectName}");
                return null;
            }
            TextMeshProUGUI textComponent = roundEndReasonText.GetComponent<TextMeshProUGUI>();
            if (textComponent == null) { return null; }
            return textComponent;
        }

        public static Image GetPlayerInfoImage(string objectName)
        {
            GameObject playerInfoPanel = GameObject.Find(objectName);
            if (playerInfoPanel == null)
            {
                Logger.LogError($"could not find {objectName}");
                return null;
            }

            Transform factionImage = playerInfoPanel.transform.GetChild(1);
            if (factionImage == null)
            {
                Logger.LogError($"could not find factionImage");
                return null;
            }

            Image imageComponent = factionImage.GetComponent<Image>();
            return imageComponent;
        }


        public static Image GetTopBarImage(string parantName)
        {
            GameObject go = GameObject.Find(parantName);
            if (go == null)
            {
                Logger.LogError($"could not find {parantName}");
                return (null);
            }

            Image imageComponent = go.GetComponent<Image>();
            return imageComponent;
        }

        public static (Image emblem, TextMeshProUGUI factionName) GetImageAndTextForScoreBoard(string parantName)
        {
            GameObject go = GameObject.Find(parantName);
            if (go == null)
            {
                Logger.LogError($"could not find {parantName}");
                return (null, null);
            }

            TextMeshProUGUI factionName = null;
            Image factionEmblemn = null;

            Transform verticalLayout = go.transform.GetChild(0);
            Transform factionHeader = verticalLayout.transform.GetChild(0);

            for (int i = 0; i < factionHeader.childCount; i++)
            {
                Transform t = factionHeader.transform.GetChild(i);

                string tName = t.name;
                if (tName == "Faction Name")
                {
                    TextMeshProUGUI text = t.GetComponent<TextMeshProUGUI>();
                    if (text == null)
                    {
                        Logger.LogError("Faction name is null for text component");
                    }
                    else
                    {
                        factionName = text;
                    }
                }

                if (tName == "Emblem (Image)")
                {
                    Image imageComponent = t.GetComponent<Image>();
                    if (imageComponent == null)
                    {
                        Logger.LogError("Faction Image is null for image component");
                    }
                    else
                    {
                        factionEmblemn = imageComponent;
                    }
                }
            }

            return (factionEmblemn, factionName);
        }

        public static (Image main, Image disabled) GetSpawnMenuImage(string attackingName)
        {
            //must search for disabled objects aswell

            /*
            GameObject[] SpawnFactionPanelArray =
                GameObject.FindObjectsOfType<GameObject>().Where(t => t.name == attackingName) as GameObject[];
                */
            GameObject spawnFactionPanel;

            spawnFactionPanel = GameObject.Find(attackingName);
            if (spawnFactionPanel == null)
            {
                Logger.LogError($"could not find {attackingName}");
                return (null, null);
            }

            Image emblem = null;
            Image emblemDisabled = null;

            Transform emblemImages = spawnFactionPanel.transform.GetChild(1);
            for (int i = 0; i < emblemImages.childCount; i++)
            {
                Transform t = emblemImages.transform.GetChild(i);
                string tName = t.name;
                if (tName == "Emblem Image")
                {
                    Image text = t.GetComponent<Image>();
                    emblem = text;
                }

                if (tName == "Emblem Image (Disabled)")
                {
                    Image text = t.GetComponent<Image>();
                    emblemDisabled = text;
                }
            }

            return (emblem, emblemDisabled);
        }
        
        public static Image GetSpawnMenuHeaderImage(string panelName = "Spawn Panel")
        {
            
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);
            GameObject spawnPanel = null;
            foreach (GameObject working in allObjects)
            {
                if (working.name == panelName)
                {
                    spawnPanel = working;
                    break;
                }
            }
            if (spawnPanel == null)
            {
                Logger.LogError($"could not find {panelName}");
                return null;
            }
            Transform header = spawnPanel.transform.GetChild(8);
            Transform emblem = header.GetChild(0);
            if (emblem == null)
            {
                Logger.LogError($"emblem is null");
                return null;
            }
            Image imageComponent = emblem.GetComponent<Image>();
            return imageComponent;
        }
    }
}