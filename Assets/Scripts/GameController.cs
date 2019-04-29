using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class GameController : DesignPatterns.MonoBehaviorSingleton<GameController>
{
    public string editLevelTemplate;
    public string level1;
    public string level2;
    public string level3;

    public GameObject boulderHeroPrefab;
    private GameObject heroObject;
    private GameObject flagObject;

    private Vector3 heroStartingPosition;

    public InputField levelSelectInputLevel;

    [Header("Brush")]
    public GameObject brushObject;
    public GameObject brushIndicator;
    public int currentBrush;

    public Sprite boulderSprite;
    public Sprite wallSprite;
    public Sprite laserSprite;
    public Sprite laserSpriteDown;
    public Sprite laserSpriteLeft;
    public Sprite laserSpriteUp;
    public Sprite flagSprite;
    public Sprite heroSprite;
    public Sprite eraseSprite;

    public GameObject boulderPrefab;
    public GameObject wallPrefab;
    public GameObject laserPrefab;
    public GameObject laserPrefabDown;
    public GameObject laserPrefabLeft;
    public GameObject laserPrefabUp;
    public GameObject flagPrefab;
    public GameObject heroPrefab;

    [Header("Level Creation")]
    public bool isInCreationBrushMode = false;
    public int livesToUse = 3;

    public int creditsAvailable = 225;

    public int currentCredits = 225;

    public GameObject grid;

    public bool levelIsRunning = false;

    public string levelLoaded;

    public GameObject tempGrid;

    public bool canPublish = false;

    public bool isTestingCreationLevel = false;
    public bool isPlayingActualLevel = false;

    public Button publishButton;

    [Header("Canvas")]
    public GameObject menuUI;
    public GameObject levelSelectionUI;
    public GameObject playGameUI;
    public GameObject playGameCreateUI;
    public GameObject creationUI;
    public GameObject livesCreationUI;
    public GameObject winUI;
    public GameObject loseUI;

    public GameObject publishUI;

    public InputField publishInputField;

    public int lives = 3;

    private Rewired.Player player;

    public InputField levelNameInput;

    public string levelName;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void onClickCreateLevels() {
        this.menuUI.SetActive(false);
        this.livesCreationUI.SetActive(true);
    }

    public void onClickPlayLevels() {
        this.menuUI.SetActive(false);
        this.levelSelectionUI.SetActive(true);
    }

    public void onClickQuitGame() {
        Application.Quit();
    }

    public void onClickBackFromLivesCreationUI() {
        this.livesCreationUI.SetActive(false);
        this.menuUI.SetActive(true);
    }

    public void onClickIncreaseLivesToUse() {
        if(this.livesToUse < 5) {
            this.livesToUse += 1;
            this.creditsAvailable += 75;
        }
    }

    public void onClickDecreaseLivesToUse() {
        if(this.livesToUse > 1) {
            this.livesToUse -= 1;
            this.creditsAvailable -= 75;
        }
    }

    public void onClickContinueToLevelCreation() {
        int actualLivesToUse = this.livesToUse;
        this.levelLoaded = this.editLevelTemplate;
        this.loadLevelString();
        this.cleanTempGrid();
        this.livesCreationUI.SetActive(false);
        this.creationUI.SetActive(true);
        this.isInCreationBrushMode = true;
        this.levelNameInput.text = "Untitled Level";
        this.brushObject.SetActive(true);
        this.currentCredits = this.creditsAvailable;
        this.livesToUse = actualLivesToUse;
        this.lives = this.livesToUse;
    }


    public void onClickWallBrush() {
        this.currentBrush = 1;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.wallSprite;
    }

    public void onClickBoulderBrush() {
        this.currentBrush = 2;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.boulderSprite;
    }

    public void onClickLaserBrush() {
        this.currentBrush = 3;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.laserSprite;
    }

    public void onClickLaserBrushDown() {
        this.currentBrush = 6;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.laserSpriteDown;
    }

    public void onClickLaserBrushLeft() {
        this.currentBrush = 7;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.laserSpriteLeft;
    }

    public void onClickLaserBrushUp() {
        this.currentBrush = 8;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.laserSpriteUp;
    }

    public void restartPlayableLevel() {
        this.loadLevelString();
        this.cleanTempGrid();
    }

    public void onClickFlagBrush() {
        this.currentBrush = 4;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.flagSprite;
        
    }

    public void onClickHeroBrush() {
        this.currentBrush = 5;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.heroSprite;
        
    }

    public void onClickEraseBrush() {
        this.currentBrush = 0;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.eraseSprite;
    }

    public void onClickBackFromCreationUI() {
        this.creationUI.SetActive(false);
        this.isInCreationBrushMode = false;
        this.brushObject.SetActive(false);
        this.livesCreationUI.SetActive(true);
    }

    private int tileCost(int i) {
        switch(i) {
            case 0:
                return 0;
            case 1:
                return 3;
            case 2:
                return 10;
            case 3:
                return 15;
            case 4:
                return 0;
            case 5:
                return 0;
            case 6:
                return 15;
            case 7:
                return 15;
            case 8:
                return 15;
            default:
                return 0;
        }
    }

    public void onClickPlayGame() {
        this.levelSelectionUI.SetActive(true);
        this.menuUI.SetActive(false);
    }

    public void onClickBackFromLevelSelectScreen() {
        this.levelSelectionUI.SetActive(false);
        this.menuUI.SetActive(true);
    }

    public void onClickBackFromPlayLevel() {
        this.levelSelectionUI.SetActive(true);
        this.playGameUI.SetActive(false);
        this.levelIsRunning = false;
        this.isPlayingActualLevel = false;
    }

    public void onClickWin() {
        this.winUI.SetActive(false);
        this.levelSelectionUI.SetActive(true);
    }

    public void onClickLose() {
        this.loseUI.SetActive(false);
        this.levelSelectionUI.SetActive(true);
    }

    private void loadSetupLevel() {
        this.loadLevelString();
        this.cleanTempGrid();
        this.levelSelectionUI.SetActive(false);
        this.playGameUI.SetActive(true);

        this.lives = this.livesToUse;
        this.levelIsRunning = true;
        this.isPlayingActualLevel = true;
    }

    public void onClickLoadLevel() {
        this.levelLoaded = this.levelSelectInputLevel.text;
        this.loadSetupLevel();
    }

    public void onClickLoadLevel1() {
        this.levelSelectInputLevel.text = this.level1;
    }

    public void onClickLoadLevel2() {
        this.levelSelectInputLevel.text = this.level2;
    }

    public void onClickLoadLevel3() {
        this.levelSelectInputLevel.text = this.level3;
    }

    public void onClickBrush() {
        if( this.brushObject.transform.position.x < -12.0f ||
            this.brushObject.transform.position.x > 4.0f ||
            this.brushObject.transform.position.y < -6.0f ||
            this.brushObject.transform.position.y > 2.0f) {
            return;
        }
        
        for(int i=0; i<this.grid.transform.childCount; i++) {
            Transform childTranform = this.grid.transform.GetChild(i).transform;
            if( Mathf.Abs(childTranform.position.x - this.brushObject.transform.position.x) < 0.5f &&
                Mathf.Abs(childTranform.position.y - this.brushObject.transform.position.y) < 0.5f) {
                GameObject.Destroy(childTranform.gameObject);
                this.currentCredits += this.tileCost(childTranform.gameObject.GetComponent<Stringer>().brushType);  
            }
        }
        if(this.currentCredits >= this.tileCost(this.currentBrush)) {  
            if(this.currentBrush == 1) {
                GameObject.Instantiate(this.wallPrefab, this.brushIndicator.transform.position, Quaternion.identity, this.grid.transform);
            } else if(this.currentBrush == 2) {
                GameObject.Instantiate(this.boulderPrefab, this.brushIndicator.transform.position, Quaternion.identity, this.grid.transform);
            } else if(this.currentBrush == 3) {
                GameObject.Instantiate(this.laserPrefab, this.brushIndicator.transform.position, Quaternion.identity, this.grid.transform);
            } else if(this.currentBrush == 4) {
                GameObject obj = GameObject.FindGameObjectWithTag("Flag");
                if(obj != null) {
                    GameObject.Destroy(obj);
                }
                GameObject.Instantiate(this.flagPrefab, this.brushIndicator.transform.position, Quaternion.identity, this.grid.transform);
            } else if(this.currentBrush == 5) {
                GameObject obj = GameObject.FindGameObjectWithTag("Hero");
                if(obj != null) {
                    GameObject.Destroy(obj);
                }
                GameObject.Instantiate(this.heroPrefab, this.brushIndicator.transform.position, Quaternion.identity, this.grid.transform);
            } else if(this.currentBrush == 6) {
                GameObject.Instantiate(this.laserPrefabDown, this.brushIndicator.transform.position, Quaternion.identity, this.grid.transform);
            } else if(this.currentBrush == 7) {
                GameObject.Instantiate(this.laserPrefabLeft, this.brushIndicator.transform.position, Quaternion.identity, this.grid.transform);
            } else if(this.currentBrush == 8) {
                GameObject.Instantiate(this.laserPrefabUp, this.brushIndicator.transform.position, Quaternion.identity, this.grid.transform);
            }
            
            this.currentCredits -= this.tileCost(this.currentBrush);
        }
        this.canPublish = false;
        this.publishButton.interactable = false;
    }

    private void genLevelString() {
        string levelString = "";
        this.levelName.Replace('#','_');
        levelString += this.levelName + "#";
        levelString += this.livesToUse + "#";
        for(int i=0; i<this.grid.transform.childCount; i++) {
            Transform childTranform = this.grid.transform.GetChild(i);
            levelString += childTranform.gameObject.GetComponent<Stringer>().getString();
            if(i<this.grid.transform.childCount - 1) {
                levelString += "/";
            }
        }
        byte[] bytesToEncode = Encoding.UTF8.GetBytes (levelString);
        string encodedText = Convert.ToBase64String (bytesToEncode);
        this.levelLoaded = encodedText;
    }

    private void loadLevelString() {
        for(int i=0; i<this.grid.transform.childCount; i++) {
            Transform childTranform = this.grid.transform.GetChild(i);
            GameObject.Destroy(childTranform.gameObject);
        }
        byte[] decodedBytes = Convert.FromBase64String (levelLoaded);
        string decodedText = Encoding.UTF8.GetString (decodedBytes);
        string[] levelMetaStrings = decodedText.Split('#');
        this.levelName = levelMetaStrings[0];
        this.levelNameInput.text = this.levelName;
        this.livesToUse = int.Parse(levelMetaStrings[1]);
        this.lives = this.livesToUse;
        string[] objStrings = levelMetaStrings[2].Split('/');
        for(int i=0; i<objStrings.Length; i++) {
            string[] objProperties = objStrings[i].Split(';');
            int objType = int.Parse(objProperties[0]);
            float objX = float.Parse(objProperties[1]);
            float objY = float.Parse(objProperties[2]);
            Vector3 pos = new Vector3(objX, objY, 0.0f);
            if(objType == 1) {
                GameObject.Instantiate(this.wallPrefab, pos, Quaternion.identity, this.grid.transform);
            } else if(objType == 2) {
                GameObject.Instantiate(this.boulderPrefab, pos, Quaternion.identity, this.grid.transform);
            } else if(objType == 3) {
                GameObject.Instantiate(this.laserPrefab, pos, Quaternion.identity, this.grid.transform);
            } else if(objType == 4) {
                this.flagObject = GameObject.Instantiate(this.flagPrefab, pos, Quaternion.identity, this.grid.transform);
            } else if(objType == 5) {
                this.heroObject = GameObject.Instantiate(this.heroPrefab, pos, Quaternion.identity, this.grid.transform);
                this.heroStartingPosition = this.heroObject.transform.position; 
            } else if(objType == 6) {
                GameObject.Instantiate(this.laserPrefabDown, pos, Quaternion.identity, this.grid.transform);
            } else if(objType == 7) {
                GameObject.Instantiate(this.laserPrefabLeft, pos, Quaternion.identity, this.grid.transform);
            } else if(objType == 8) {
                GameObject.Instantiate(this.laserPrefabUp, pos, Quaternion.identity, this.grid.transform);
            }
        }
    }

    public void backFromTryLevel() {
        this.levelIsRunning = false;
        this.brushObject.SetActive(true);
        this.isInCreationBrushMode = true;
        this.isTestingCreationLevel = false;
        this.loadLevelString();
        this.playGameCreateUI.SetActive(false);
        this.creationUI.SetActive(true); 
        this.cleanTempGrid();
    }

    public void tryLevel() {
        this.lives = this.livesToUse;
        this.levelIsRunning = true;
        this.brushObject.SetActive(false);
        this.isInCreationBrushMode = false;
        this.isTestingCreationLevel = true;
        this.levelName = this.levelNameInput.text;
        this.genLevelString();
        this.playGameCreateUI.SetActive(true);
        this.creationUI.SetActive(false);
        this.heroObject = GameObject.FindGameObjectWithTag("Hero");
        this.heroStartingPosition = this.heroObject.transform.position; 
    }

    public void onFlagReach() {
        if(this.isTestingCreationLevel) {
            this.canPublish = true;
            this.publishButton.interactable = true;
            this.backFromTryLevel();
        } else if(this.isPlayingActualLevel) {
            this.winUI.SetActive(true);
            this.playGameUI.SetActive(false);
            this.lives = this.livesToUse;
            this.levelIsRunning = false;
            this.isPlayingActualLevel = false;
        }
    }

    public void onClickPublish() {
        this.isInCreationBrushMode = false;
        this.brushObject.SetActive(false);
        this.creationUI.SetActive(false);
        this.publishUI.SetActive(true);
        this.publishInputField.text = this.levelLoaded;
    }

    public void onClickPublishBackToMainMenu() {
        this.publishUI.SetActive(false);
        this.menuUI.SetActive(true);
    }

    public void KillHero() {
        if(this.lives > 1) {
            this.lives -= 1;
            Debug.Log("a" + this.heroObject);
            Debug.Log("b" + this.boulderHeroPrefab);
            Debug.Log("c" + this.tempGrid);
            GameObject.Instantiate(this.boulderHeroPrefab, this.heroObject.transform.position, Quaternion.identity, this.tempGrid.transform);
            this.heroObject.transform.position = this.heroStartingPosition;
        } else {
            if(this.isTestingCreationLevel) {
                this.backFromTryLevel();
            } else if(this.isPlayingActualLevel) {
                this.loseUI.SetActive(true);
                this.playGameUI.SetActive(false);
                this.lives = this.livesToUse;
                this.levelIsRunning = false;
                this.isPlayingActualLevel = false;
            }
        }
    }

    public void cleanTempGrid() {
        for(int i=0; i<this.tempGrid.transform.childCount; i++) {
            Transform childTranform = this.tempGrid.transform.GetChild(i);
            GameObject.Destroy(childTranform.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10.0f;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.x = Mathf.Floor(mousePosition.x) + 0.5f;
        mousePosition.y = Mathf.Floor(mousePosition.y) + 0.5f;
        brushObject.transform.position = mousePosition;

        if(this.player.GetButtonDown("ClickMouse")) {
            if(this.isInCreationBrushMode) {
                this.onClickBrush();
            }
        }
        if(this.player.GetButtonDown("ClickMouseRight")) {
            if(this.isInCreationBrushMode) {
                int lastBrush = this.currentBrush;
                this.currentBrush = 0;
                this.onClickBrush();
                this.currentBrush = lastBrush;
            }
        }

        if(this.levelNameInput.text != this.levelName) {
            this.canPublish = false;
        }
    }

    public override void onAwake() {
        this.player = Rewired.ReInput.players.GetPlayer("Player0");
    }
}
