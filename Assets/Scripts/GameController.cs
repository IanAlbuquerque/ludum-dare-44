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
        this.swapToUIBGM();
        this.bgmIntroPiano.Play();
        this.bgmIntroSax.Play();
        this.bgmLoopBass.Play();

        coroutine = playLoop(this.bgmIntroPiano.clip.length - 1.7f);
        StartCoroutine(coroutine);
        // this.bgmLoopBass.PlayDelayed(this.bgmLoopBass.clip.length - 1.0f);
    }

    private IEnumerator coroutine;

    private IEnumerator playLoop(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.bgmLoopPiano.Play();
        this.bgmLoopSax.Play();
    }    

    public GameObject muteAudioButton;
    public GameObject unMuteAudioButton;

    public void muteAudio() {
        this.bgmIntroPiano.mute = true;
        this.bgmIntroSax.mute = true;
        this.bgmIntroBass.mute = true;
        this.bgmLoopPiano.mute = true;
        this.bgmLoopSax.mute = true;
        this.bgmLoopBass.mute = true;
        this.muteAudioButton.SetActive(false);
        this.unMuteAudioButton.SetActive(true);
    }

    public void unmuteAudio() {
        this.bgmIntroPiano.mute = false;
        this.bgmIntroSax.mute = false;
        this.bgmIntroBass.mute = false;
        this.bgmLoopPiano.mute = false;
        this.bgmLoopSax.mute = false;
        this.bgmLoopBass.mute = false;
        this.muteAudioButton.SetActive(true);
        this.unMuteAudioButton.SetActive(false);
    }

    public void swapToUIBGM() {
        this.bgmIntroPiano.volume = 0.0f;
        this.bgmIntroSax.volume = 1.0f;
        this.bgmIntroBass.volume = 0.0f;
        this.bgmLoopPiano.volume = 0.0f;
        this.bgmLoopSax.volume = 1.0f;
        this.bgmLoopBass.volume = 0.0f;
    }

    public void swapToCreateBGM() {
        this.bgmIntroPiano.volume = 1.0f;
        this.bgmIntroSax.volume = 0.0f;
        this.bgmIntroBass.volume = 0.0f;
        this.bgmLoopPiano.volume = 1.0f;
        this.bgmLoopSax.volume = 0.0f;
        this.bgmLoopBass.volume = 0.0f;
    }

    public void swapToPlayBGM() {
        this.bgmIntroPiano.volume = 0.0f;
        this.bgmIntroSax.volume = 0.0f;
        this.bgmIntroBass.volume = 1.0f;
        this.bgmLoopPiano.volume = 0.0f;
        this.bgmLoopSax.volume = 0.0f;
        this.bgmLoopBass.volume = 1.0f;
    }

    public AudioSource bgmIntroSax;
    public AudioSource bgmIntroPiano;
    public AudioSource bgmIntroBass;
    public AudioSource bgmLoopSax;
    public AudioSource bgmLoopPiano;
    public AudioSource bgmLoopBass;
    public AudioSource sfxDeath;
    public AudioSource sfxClick;
    public AudioSource sfxPlacement;
    public AudioSource sfxBrushSelect;

    public void onClickCreateLevels() {
        this.menuUI.SetActive(false);
        this.livesCreationUI.SetActive(true);
        this.livesToUse = 3;
        this.creditsAvailable = this.livesToCredits(this.livesToUse);
        this.sfxClick.Play();
    }

    public void onClickPlayLevels() {
        this.menuUI.SetActive(false);
        this.levelSelectionUI.SetActive(true);
        this.sfxClick.Play();
    }

    public void onClickQuitGame() {
        this.sfxClick.Play();
        Application.Quit();
    }

    public void onClickBackFromLivesCreationUI() {
        this.sfxClick.Play();
        this.livesCreationUI.SetActive(false);
        this.menuUI.SetActive(true);
    }

    public void onClickIncreaseLivesToUse() {
        if(this.livesToUse < 5) {
            this.sfxClick.Play();
            this.livesToUse += 1;
            this.creditsAvailable = this.livesToCredits(this.livesToUse);
        }
    }

    public void onClickDecreaseLivesToUse() {
        if(this.livesToUse > 1) {
            this.sfxClick.Play();
            this.livesToUse -= 1;
            this.creditsAvailable = this.livesToCredits(this.livesToUse);
        }
    }

    public void onClickContinueToLevelCreation() {
        this.sfxClick.Play();
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
        this.creditsAvailable = this.livesToCredits(this.livesToUse);
        this.swapToCreateBGM();
    }

    private int livesToCredits(int lives) {
        return 50 * lives + 100;
    }

    public void onClickWallBrush() {
        this.sfxBrushSelect.Play();
        this.currentBrush = 1;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.wallSprite;
    }

    public void onClickBoulderBrush() {
        this.sfxBrushSelect.Play();
        this.currentBrush = 2;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.boulderSprite;
    }

    public void onClickLaserBrush() {
        this.sfxBrushSelect.Play();
        this.currentBrush = 3;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.laserSprite;
    }

    public void onClickLaserBrushDown() {
        this.sfxBrushSelect.Play();
        this.currentBrush = 6;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.laserSpriteDown;
    }

    public void onClickLaserBrushLeft() {
        this.sfxBrushSelect.Play();
        this.currentBrush = 7;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.laserSpriteLeft;
    }

    public void onClickLaserBrushUp() {
        this.sfxBrushSelect.Play();
        this.currentBrush = 8;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.laserSpriteUp;
    }

    public void restartPlayableLevel() {
        this.sfxClick.Play();
        this.loadLevelString();
        this.cleanTempGrid();
    }

    public void onClickFlagBrush() {
        this.sfxBrushSelect.Play();
        this.currentBrush = 4;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.flagSprite;
        
    }

    public void onClickHeroBrush() {
        this.sfxBrushSelect.Play();
        this.currentBrush = 5;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.heroSprite;
        
    }

    public void onClickEraseBrush() {
        this.sfxBrushSelect.Play();
        this.currentBrush = 0;
        this.brushIndicator.GetComponent<SpriteRenderer>().sprite = this.eraseSprite;
    }

    public void onClickBackFromCreationUI() {
        this.sfxClick.Play();
        this.creationUI.SetActive(false);
        this.isInCreationBrushMode = false;
        this.brushObject.SetActive(false);
        this.livesCreationUI.SetActive(true);
        this.swapToUIBGM();
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
                return 20;
            case 4:
                return 0;
            case 5:
                return 0;
            case 6:
                return 20;
            case 7:
                return 20;
            case 8:
                return 20;
            default:
                return 0;
        }
    }

    public void onClickPlayGame() {
        this.sfxClick.Play();
        this.levelSelectionUI.SetActive(true);
        this.menuUI.SetActive(false);
    }

    public void onClickBackFromLevelSelectScreen() {
        this.sfxClick.Play();
        this.levelSelectionUI.SetActive(false);
        this.menuUI.SetActive(true);
    }

    public void onClickBackFromPlayLevel() {
        this.sfxClick.Play();
        this.levelSelectionUI.SetActive(true);
        this.playGameUI.SetActive(false);
        this.levelIsRunning = false;
        this.isPlayingActualLevel = false;
        this.swapToUIBGM();
    }

    public void onClickWin() {
        this.sfxClick.Play();
        this.winUI.SetActive(false);
        this.levelSelectionUI.SetActive(true);
        this.swapToUIBGM();
    }

    public void onClickLose() {
        this.sfxClick.Play();
        this.loseUI.SetActive(false);
        this.levelSelectionUI.SetActive(true);
        this.swapToUIBGM();
    }

    private void loadSetupLevel() {
        this.loadLevelString();
        this.cleanTempGrid();
        this.levelSelectionUI.SetActive(false);
        this.playGameUI.SetActive(true);

        this.lives = this.livesToUse;
        this.levelIsRunning = true;
        this.isPlayingActualLevel = true;
        this.swapToPlayBGM();
    }

    public void onClickLoadLevel() {
        this.sfxClick.Play();
        this.levelLoaded = this.levelSelectInputLevel.text;
        this.loadSetupLevel();
    }

    public void onClickLoadLevel1() {
        this.sfxClick.Play();
        this.levelSelectInputLevel.text = this.level1;
    }

    public void onClickLoadLevel2() {
        this.sfxClick.Play();
        this.levelSelectInputLevel.text = this.level2;
    }

    public void onClickLoadLevel3() {
        this.sfxClick.Play();
        this.levelSelectInputLevel.text = this.level3;
    }

    public void onClickBrush() {
        if( this.brushObject.transform.position.x < -12.0f ||
            this.brushObject.transform.position.x > 4.0f ||
            this.brushObject.transform.position.y < -6.0f ||
            this.brushObject.transform.position.y > 2.0f) {
            return;
        }
        
        this.sfxPlacement.Play();
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
        this.sfxClick.Play();
        this.levelIsRunning = false;
        this.brushObject.SetActive(true);
        this.isInCreationBrushMode = true;
        this.isTestingCreationLevel = false;
        this.loadLevelString();
        this.playGameCreateUI.SetActive(false);
        this.creationUI.SetActive(true); 
        this.cleanTempGrid();
        this.swapToCreateBGM();
    }

    public void tryLevel() {
        this.sfxClick.Play();
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
        this.swapToPlayBGM();
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
        this.sfxClick.Play();
        this.isInCreationBrushMode = false;
        this.brushObject.SetActive(false);
        this.creationUI.SetActive(false);
        this.publishUI.SetActive(true);
        this.publishInputField.text = this.levelLoaded;
    }

    public void onClickPublishBackToMainMenu() {
        this.sfxClick.Play();
        this.publishUI.SetActive(false);
        this.menuUI.SetActive(true);
        this.swapToUIBGM();
    }

    public bool killedHero = false;

    private void LateUpdate() {
        killedHero = false;
    }

    public void KillHero() {
        if(this.killedHero == true) {
            return;
        }
        this.killedHero = true;
        this.sfxDeath.Play();
        if(this.lives > 1) {
            this.lives -= 1;
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
