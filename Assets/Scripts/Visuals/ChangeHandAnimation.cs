using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHandAnimation : MonoBehaviour
{
    private SpriteRenderer handSR, markerSR, flipHandSR, rotateHandSR;
    private SpriteRenderer[] gridSR = new SpriteRenderer[4];

    public bool animation1, animation2, animation3;

    public GameObject hand, gridRow1, gridRow2, gridColumn1, gridColumn2;

    public Sprite rightHanded, leftHanded;
    public Sprite rightHandedFlipped, leftHandedFlipped;

    public GameObject marker;
    public Sprite netrixiMarker, folkvarMarker, ivMarker, undoMarker;

    public GameObject handIcons, flipHand;
    public Sprite flipRight, flipLeft;

    public GameObject rightHandMoveRight, rightHandMoveLeft;
    public GameObject leftHandMoveRight, leftHandMoveLeft;
    public GameObject moveLeft, moveRight;
    public static int handMovementOrder = 1;

    public GameObject rotateHandRight, rotateHandLeft;
    public GameObject rotateIcons;
    public Sprite startRotatingRight, continueRotatingRight;
    public Sprite startRotatingLeft, continueRotatingLeft;

    private Sprite frontHand, backHand;

    public static float optionXLocation;
    public static float optionYLocation1, optionYLocation2, optionYLocation3;
    public static float optionWidth, optionHeight;

    public static float optionPadding;

    private float scaleFactor = 1f;
    private float sizeOfHand;
    private float standardHandSize = 1.25f;
    private float largerHandSize = 0.95f;

    private float smallScale = 0.5f;
    private float largeScale = 1.5f;

    private float[] xPos, yPos;

    public static string animationName1 = "Flip Hand";
    public static string animationName2 = "Flip Hand";
    public static string animationName3 = "Flip Hand";
    
    public static bool restartAnimation;

    private float standardAnimationTime = 3f;

    public static bool shouldResetHand;

    // Start is called before the first frame update
    void Start()
    {
        handSR = hand.GetComponent<SpriteRenderer>();
        markerSR = marker.GetComponent<SpriteRenderer>();
        flipHandSR = flipHand.GetComponent<SpriteRenderer>();

        if (GameManagerScript.rightHanded) rotateHandSR = rotateHandRight.GetComponent<SpriteRenderer>();
        else rotateHandSR = rotateHandLeft.GetComponent<SpriteRenderer>();
        
        gridSR[0] = gridRow1.GetComponent<SpriteRenderer>();
        gridSR[1] = gridRow2.GetComponent<SpriteRenderer>();
        gridSR[2] = gridColumn1.GetComponent<SpriteRenderer>();
        gridSR[3] = gridColumn2.GetComponent<SpriteRenderer>();

        DisableGrids(true, true);

        if (GameManagerScript.rightHanded) frontHand = rightHanded;
        else frontHand = leftHanded;
        
        handSR.sprite = frontHand;
        sizeOfHand = largerHandSize;
        
        markerSR.enabled = false;
        
        flipHand.SetActive(false);

        rotateHandRight.SetActive(false);
        rotateHandLeft.SetActive(false);
            
        leftHandMoveLeft.SetActive(false);
        leftHandMoveRight.SetActive(false);
        rightHandMoveLeft.SetActive(false);
        rightHandMoveRight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Is the player right-handed?
        if (GameManagerScript.rightHanded)
        {
            frontHand = rightHanded;
            backHand = rightHandedFlipped;
        }
        else
        {
            frontHand = leftHanded;
            backHand = leftHandedFlipped;
        }

        ChangeLocation();
        ChangeScale();
        
        ChangeHandLocation();
        ChangeMarkerScale();
        ChangeGrids();
        
        // Check to see if the animation is restarted
        if (restartAnimation) StopAllCoroutines();

        
        // Play hand animation 1
        if (animation1 && HandAnimationManager.startAnimation)
        {
            StartCoroutine(PlayAnimation(animationName1, 1));
        }
        
        // Play hand animation 2
        if (animation2 && HandAnimationManager.startAnimation)
        {
            StartCoroutine(PlayAnimation(animationName2, 2));
        }
        
        // Play hand animation 3
        if (animation3 && HandAnimationManager.startAnimation)
        {
            StartCoroutine(PlayAnimation(animationName3, 3));
        }
        
        // Check to see if the animation is deactivated
        if (animation1 && !HandAnimationManager.animation1Playing) ResetHand(true);
        if (animation2 && !HandAnimationManager.animation2Playing) ResetHand(true);
        if (animation3 && !HandAnimationManager.animation3Playing) ResetHand(true);
    }


    private IEnumerator PlayAnimation(string animationName, int animationNumber)
    {
        float standardDelay = 1f;
        float endDelay = 1f;

        // The animation has started
        switch (animationNumber)
        {
            case 1: HandAnimationManager.animation1Playing = true;
                break;
            case 2: HandAnimationManager.animation2Playing = true;
                break;
            case 3: HandAnimationManager.animation3Playing = true;
                break;
        }

        //print("Animation " + animationNumber + " is playing");

        switch (animationName)
        {
            // Choose a certain marker to show
            case "Netrixi Marker":
                markerSR.sprite = netrixiMarker;
                markerSR.enabled = true;
                
                ResetHand(true);
                EndOfAnimation();
                break;
            
            case "Folkvar Marker":
                markerSR.sprite = folkvarMarker;
                markerSR.enabled = true;
                
                ResetHand(true);
                EndOfAnimation();
                break;
            
            case "Iv Marker":
                markerSR.sprite = ivMarker;
                markerSR.enabled = true;
                
                ResetHand(true);
                EndOfAnimation();
                break;
            
            case "Undo Marker":
                markerSR.sprite = undoMarker;
                markerSR.enabled = true;
                
                ResetHand(true);
                EndOfAnimation();
                break;
            
            
            // Flip Hand 180 degrees
            case "Flip Hand":
                ResetHand(true);
                EnableGrids();
                DisableGrids(false, true);
                sizeOfHand = largerHandSize;

                flipHand.SetActive(true);
                if (GameManagerScript.rightHanded) flipHandSR.sprite = flipRight;
                else flipHandSR.sprite = flipLeft;
                
                yield return new WaitForSeconds(standardDelay);

                float rotateHand = hand.transform.eulerAngles.y;

                float rotationAmount = 5;
                float rotationDelay = (standardAnimationTime - (standardDelay*2)) / (180 / rotationAmount);

                // Flip Hand animation
                for (int i = 0; i < (180 / rotationAmount); i++)
                {
                    if (GameManagerScript.rightHanded) rotateHand += rotationAmount;
                    else rotateHand -= rotationAmount;
                
                    Vector3 rotation = new Vector3(hand.transform.eulerAngles.x, rotateHand, hand.transform.eulerAngles.z);
                    
                    hand.transform.eulerAngles = rotation;
                    yield return new WaitForSeconds(rotationDelay);

                    if (i >= (int) (180 / rotationAmount) / 2 - 5)
                    {
                        handSR.sprite = backHand;
                        handSR.flipX = true;
                    }
                }
                
                yield return new WaitForSeconds(standardDelay - rotationDelay);
                
                ResetHand(false);

                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            
            // Move Left or Right
            case "Move Left":
                ResetHand(true);
                EnableGrids();

                GameObject move;
                
                float waitBeforeGrids = 0.5f;
                
                float distanceArrowMoves = 0.02f;
                float arrowMoveDelay = 0.025f;
                float waitBeforeArrow = 0.75f;

                DisableGrids(false, true);
                sizeOfHand = largerHandSize;

                if (GameManagerScript.rightHanded) move = rightHandMoveLeft;
                else move = leftHandMoveLeft;

                Camera camera = Camera.main;
                float cameraWidth = (2f * camera.orthographicSize) * camera.aspect;
                float distanceToMove = distanceArrowMoves * cameraWidth;

                Vector3 startLocation = new Vector3(moveLeft.transform.position.x + (distanceToMove / 2), move.transform.position.y, move.transform.position.z);
                move.transform.position = startLocation;

                // Move Arrow Animation
                yield return new WaitForSeconds(waitBeforeArrow);
                move.SetActive(true);
                
                for (int i = 0; i < 20; i++)
                {
                    Vector3 loc = new Vector3(move.transform.position.x - (distanceToMove / 20), move.transform.position.y, move.transform.position.z);
                    move.transform.position = loc;

                    yield return new WaitForSeconds(arrowMoveDelay);
                }
                
                yield return new WaitForSeconds(waitBeforeGrids);

                move.transform.position = startLocation;
                move.SetActive(false);

                yield return new WaitForSeconds(waitBeforeGrids);

                // Move Hand Animation
                EnableGrids();
                sizeOfHand = standardHandSize;
                
                yield return new WaitForSeconds(standardDelay);

                switch (handMovementOrder)
                {
                    case 1: hand.transform.position = new Vector3(xPos[0], yPos[0], 1);
                        break;
                    case 2: hand.transform.position = new Vector3(xPos[0], yPos[1], 1);
                        break;
                    case 3: hand.transform.position = new Vector3(xPos[0], yPos[2], 1);
                        break;
                }

                yield return new WaitForSeconds(standardDelay);
                
                hand.transform.position = new Vector3(xPos[1], yPos[1], 1);
                DisableGrids(true, true);

                yield return new WaitForSeconds(waitBeforeArrow);
                EndOfAnimation();
                break;
            
            case "Move Right":
                ResetHand(true);
                EnableGrids();
                
                waitBeforeGrids = 0.5f;
                
                distanceArrowMoves = 0.02f;
                arrowMoveDelay = 0.025f;
                waitBeforeArrow = 0.75f;

                DisableGrids(false, true);
                sizeOfHand = largerHandSize;
                hand.transform.position = new Vector3(xPos[0], yPos[1], 1);

                if (GameManagerScript.rightHanded) move = rightHandMoveRight;
                else move = leftHandMoveRight;

                camera = Camera.main;
                cameraWidth = (2f * camera.orthographicSize) * camera.aspect;
                distanceToMove = distanceArrowMoves * cameraWidth;

                startLocation = new Vector3(moveRight.transform.position.x - (distanceToMove / 2), move.transform.position.y, move.transform.position.z);
                move.transform.position = startLocation;

                // Move Arrow Animation
                yield return new WaitForSeconds(waitBeforeArrow);
                move.SetActive(true);
                
                for (int i = 0; i < 20; i++)
                {
                    Vector3 loc = new Vector3(move.transform.position.x + (distanceToMove / 20), move.transform.position.y, move.transform.position.z);
                    move.transform.position = loc;

                    yield return new WaitForSeconds(arrowMoveDelay);
                }
                
                yield return new WaitForSeconds(waitBeforeGrids);

                move.transform.position = startLocation;
                move.SetActive(false);

                yield return new WaitForSeconds(waitBeforeGrids);

                // Move Hand Animation
                EnableGrids();
                sizeOfHand = standardHandSize;
                hand.transform.position = new Vector3(xPos[1], yPos[1], 1);
                
                yield return new WaitForSeconds(standardDelay);

                switch (handMovementOrder)
                {
                    case 1: hand.transform.position = new Vector3(xPos[2], yPos[0], 1);
                        break;
                    case 2: hand.transform.position = new Vector3(xPos[2], yPos[1], 1);
                        break;
                    case 3: hand.transform.position = new Vector3(xPos[2], yPos[2], 1);
                        break;
                }

                yield return new WaitForSeconds(standardDelay);
                
                hand.transform.position = new Vector3(xPos[1], yPos[1], 1);
                DisableGrids(true, true);

                yield return new WaitForSeconds(waitBeforeArrow);
                EndOfAnimation();
                break;


            // Netrixi Fireball + Iv Block
            case "Mid to Far":
                ResetHand(true);
                EnableGrids();
                
                sizeOfHand = largerHandSize;
                scaleFactor = 1f;

                float waitBeforeScaling = 1.25f;
                float scaleTime = (standardAnimationTime - waitBeforeScaling - standardDelay);
                float timesScaled = 50;
                float scaledifference = smallScale / timesScaled;

                yield return new WaitForSeconds(standardDelay);
                
                // Resize hand Animation
                for (int i = 0; i < timesScaled; i++)
                {
                    scaleFactor -= scaledifference;
                    
                    ChangeScale();

                    yield return new WaitForSeconds(scaleTime/timesScaled);
                }
                
                yield return new WaitForSeconds(waitBeforeScaling);

                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            case "Far to Near":
                ResetHand(true);
                EnableGrids();
                
                sizeOfHand = largerHandSize;
                scaleFactor = smallScale;

                waitBeforeScaling = 1.25f;
                scaleTime = (standardAnimationTime - waitBeforeScaling - standardDelay);
                timesScaled = 50;
                scaledifference = (largeScale - smallScale) / timesScaled;

                yield return new WaitForSeconds(standardDelay);
                
                // Resize hand Animation
                for (int i = 0; i < timesScaled; i++)
                {
                    scaleFactor += scaledifference;
                    
                    ChangeScale();

                    yield return new WaitForSeconds(scaleTime/timesScaled);
                }
                
                yield return new WaitForSeconds(waitBeforeScaling);

                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            
            // Netrixi Lightning + Iv Empower
            case "Start Rotating":
                ResetHand(true);
                EnableGrids();
                DisableGrids(false, true);
                sizeOfHand = largerHandSize;
                
                GameObject startRotating;
                
                float waitBeforeRotating = 1.25f;
                float rotateTime = (standardAnimationTime - waitBeforeRotating - standardDelay);
                float timesRotated = 50;
                float rotateDifference;

                if (GameManagerScript.rightHanded)
                {
                    startRotating = rotateHandRight;
                    rotateDifference = 90 / timesRotated;
                    rotateHandSR.sprite = startRotatingRight;
                }
                else
                {
                    startRotating = rotateHandLeft;
                    rotateDifference = -90 / timesRotated;
                    rotateHandSR.sprite = startRotatingLeft;
                    
                    hand.transform.position = new Vector3(xPos[0], yPos[1], 1);
                }

                startRotating.SetActive(true);
                rotateIcons.transform.eulerAngles = new Vector3(0,0,0);

                yield return new WaitForSeconds(standardDelay);

                rotateHand = hand.transform.eulerAngles.z;
                
                // Rotate Hand animation
                for (int i = 0; i < timesRotated; i++)
                {
                    rotateHand += rotateDifference;
                
                    Vector3 rotation = new Vector3(hand.transform.eulerAngles.x, hand.transform.eulerAngles.y, rotateHand);
                    
                    hand.transform.eulerAngles = rotation;
                    rotateIcons.transform.eulerAngles = new Vector3(0,0,0);
                    yield return new WaitForSeconds(rotateTime/timesRotated);
                }
                
                yield return new WaitForSeconds(waitBeforeRotating);
                
                DisableGrids(true, true);
                startRotating.SetActive(false);

                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            case "Continue Rotating":
                ResetHand(true);
                EnableGrids();
                DisableGrids(false, true);
                sizeOfHand = largerHandSize;

                GameObject continueRotating;
                
                float waitBeforeFlippingHand = 0.75f;
                float waitAfterFlippingHand = 0.5f;
                float startingRotation;
                
                rotateTime = standardAnimationTime - waitBeforeFlippingHand - waitAfterFlippingHand - standardDelay;
                timesRotated = 5;

                if (GameManagerScript.rightHanded)
                {
                    continueRotating = rotateHandRight;
                    rotateDifference = 90 / (timesRotated);
                    rotateHandSR.sprite = continueRotatingRight;
                    startingRotation = 90;
                    
                    rotateIcons.transform.eulerAngles = new Vector3(0,0,-90);
                }
                else
                {
                    continueRotating = rotateHandLeft;
                    rotateDifference = -90 / (timesRotated);
                    rotateHandSR.sprite = continueRotatingLeft;
                    startingRotation = -90;
                    
                    rotateIcons.transform.eulerAngles = new Vector3(0,0,90);
                    hand.transform.position = new Vector3(xPos[0], yPos[1], 1);
                }

                continueRotating.SetActive(true);

                Vector3 startRotation = new Vector3(hand.transform.eulerAngles.x, hand.transform.eulerAngles.y, startingRotation);
                hand.transform.eulerAngles = startRotation;
                
                yield return new WaitForSeconds(standardDelay);

                rotateHand = hand.transform.eulerAngles.z;
                
                // Rotate Hand one way animation
                for (int i = 0; i < timesRotated; i++)
                {
                    rotateHand -= rotateDifference;

                    Vector3 rotation = new Vector3(hand.transform.eulerAngles.x, hand.transform.eulerAngles.y, rotateHand);
                    
                    hand.transform.eulerAngles = rotation;
                    yield return new WaitForSeconds(rotateTime/timesRotated);
                }
                
                yield return new WaitForSeconds(waitBeforeFlippingHand*2/5);
                
                flipHand.SetActive(true);
                if (GameManagerScript.rightHanded) flipHandSR.sprite = flipRight;
                else flipHandSR.sprite = flipLeft;

                yield return new WaitForSeconds(waitBeforeFlippingHand*3/5);

                handSR.sprite = backHand;
                continueRotating.SetActive(false);
                
                yield return new WaitForSeconds(waitAfterFlippingHand);
                
                flipHand.SetActive(false);
                DisableGrids(true, true);

                yield return new WaitForSeconds(endDelay); 
                EndOfAnimation();
                break;
            
            
            // Netrixi Transmutate
            case "D to X":
                ResetHand(true);
                EnableGrids();

                float waitAfterMove = 1.25f;
                float moveTime = standardAnimationTime - standardDelay - waitAfterMove;
                float timesMoved = 50;

                // Start at position 6
                Vector3 currLoc; 
                if (GameManagerScript.rightHanded) currLoc = new Vector3(xPos[2], yPos[1], 1);
                else currLoc = new Vector3(xPos[0], yPos[1], 1);
                hand.transform.position = currLoc;

                // End at position 8
                Vector3 destination;
                destination = new Vector3(xPos[1], yPos[2], 1);

                float travelDistanceX = destination.x - currLoc.x;
                float travelDistanceY = destination.y - currLoc.y;
                
                yield return new WaitForSeconds(standardDelay);
                
                // Move Hand animation
                for (int i = 0; i < timesMoved; i++)
                {
                    Vector3 loc = new Vector3(hand.transform.position.x + (travelDistanceX/timesMoved), hand.transform.position.y + (travelDistanceY/timesMoved), hand.transform.position.z);
                    hand.transform.position = loc;

                    yield return new WaitForSeconds(moveTime/timesMoved);
                }
                
                yield return new WaitForSeconds(waitAfterMove);
                
                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            case "X to A":
                ResetHand(true);
                EnableGrids();
                
                waitAfterMove = 1.25f;
                moveTime = standardAnimationTime - standardDelay - waitAfterMove;
                timesMoved = 50;
                
                // Start at position 8
                currLoc = new Vector3(xPos[1], yPos[2], 1);
                hand.transform.position = currLoc;
                
                // End at position 4
                if (GameManagerScript.rightHanded) destination = new Vector3(xPos[0], yPos[1], 1);
                else destination = new Vector3(xPos[2], yPos[1], 1);

                travelDistanceX = destination.x - currLoc.x;
                travelDistanceY = destination.y - currLoc.y;
                
                yield return new WaitForSeconds(standardDelay);
                
                // Move Hand animation
                for (int i = 0; i < timesMoved; i++)
                {
                    Vector3 loc = new Vector3(hand.transform.position.x + (travelDistanceX/timesMoved), hand.transform.position.y + (travelDistanceY/timesMoved), hand.transform.position.z);
                    hand.transform.position = loc;

                    yield return new WaitForSeconds(moveTime/timesMoved);
                }
                
                yield return new WaitForSeconds(waitAfterMove);
                
                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            case "A to W":
                ResetHand(true);
                EnableGrids();
                
                waitAfterMove = 1.25f;
                moveTime = standardAnimationTime - standardDelay - waitAfterMove;
                timesMoved = 50;
                
                // Start at position 4
                if (GameManagerScript.rightHanded) currLoc = new Vector3(xPos[0], yPos[1], 1);
                else currLoc = new Vector3(xPos[2], yPos[1], 1);
                hand.transform.position = currLoc;
                
                // End at position 2
                destination = new Vector3(xPos[1], yPos[0], 1);

                travelDistanceX = destination.x - currLoc.x;
                travelDistanceY = destination.y - currLoc.y;
                
                yield return new WaitForSeconds(standardDelay);
                
                // Move Hand animation
                for (int i = 0; i < timesMoved; i++)
                {
                    Vector3 loc = new Vector3(hand.transform.position.x + (travelDistanceX/timesMoved), hand.transform.position.y + (travelDistanceY/timesMoved), hand.transform.position.z);
                    hand.transform.position = loc;

                    yield return new WaitForSeconds(moveTime/timesMoved);
                }
                
                yield return new WaitForSeconds(waitAfterMove);
                
                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            
            // Folkvar Swing Sword
            case "Q to C":
                ResetHand(true);
                EnableGrids();
                
                waitAfterMove = 1.25f;
                moveTime = standardAnimationTime - standardDelay - waitAfterMove;
                timesMoved = 50;
                
                // Start at position 1
                currLoc = new Vector3(xPos[0], yPos[0], 1);
                hand.transform.position = currLoc;
                
                // End at position 9
                destination = new Vector3(xPos[2], yPos[2], 1);

                travelDistanceX = destination.x - currLoc.x;
                travelDistanceY = destination.y - currLoc.y;
                
                yield return new WaitForSeconds(standardDelay);
                
                // Move Hand animation
                for (int i = 0; i < timesMoved; i++)
                {
                    Vector3 loc = new Vector3(hand.transform.position.x + (travelDistanceX/timesMoved), hand.transform.position.y + (travelDistanceY/timesMoved), hand.transform.position.z);
                    hand.transform.position = loc;

                    yield return new WaitForSeconds(moveTime/timesMoved);
                }
                
                yield return new WaitForSeconds(waitAfterMove);
                
                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            
            // Folkvar Smite
            case "Q to W":
                ResetHand(true);
                EnableGrids();
                
                waitAfterMove = 1.25f;
                moveTime = standardAnimationTime - standardDelay - waitAfterMove;
                timesMoved = 50;
                
                // Start at position 1
                currLoc = new Vector3(xPos[0], yPos[0], 1);
                hand.transform.position = currLoc;
                
                // End at position 2
                destination = new Vector3(xPos[1], yPos[0], 1);

                travelDistanceX = destination.x - currLoc.x;
                travelDistanceY = destination.y - currLoc.y;
                
                yield return new WaitForSeconds(standardDelay);
                
                // Move Hand animation
                for (int i = 0; i < timesMoved; i++)
                {
                    Vector3 loc = new Vector3(hand.transform.position.x + (travelDistanceX/timesMoved), hand.transform.position.y + (travelDistanceY/timesMoved), hand.transform.position.z);
                    hand.transform.position = loc;

                    yield return new WaitForSeconds(moveTime/timesMoved);
                }
                
                yield return new WaitForSeconds(waitAfterMove);
                
                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            case "W to X":
                ResetHand(true);
                EnableGrids();
                
                waitAfterMove = 1.25f;
                moveTime = standardAnimationTime - standardDelay - waitAfterMove;
                timesMoved = 50;
                
                // Start at position 2
                currLoc = new Vector3(xPos[1], yPos[0], 1);
                hand.transform.position = currLoc;
                
                // End at position 8
                destination = new Vector3(xPos[1], yPos[2], 1);

                travelDistanceX = destination.x - currLoc.x;
                travelDistanceY = destination.y - currLoc.y;
                
                yield return new WaitForSeconds(standardDelay);
                
                // Move Hand animation
                for (int i = 0; i < timesMoved; i++)
                {
                    Vector3 loc = new Vector3(hand.transform.position.x + (travelDistanceX/timesMoved), hand.transform.position.y + (travelDistanceY/timesMoved), hand.transform.position.z);
                    hand.transform.position = loc;

                    yield return new WaitForSeconds(moveTime/timesMoved);
                }
                
                yield return new WaitForSeconds(waitAfterMove);
                
                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            

            // Folkvar Grand Slam
            case "Z to W":
                ResetHand(true);
                EnableGrids();
                
                waitAfterMove = 1.25f;
                moveTime = standardAnimationTime - standardDelay - waitAfterMove;
                timesMoved = 50;
                
                // Start at position 7
                currLoc = new Vector3(xPos[0], yPos[2], 1);
                hand.transform.position = currLoc;
                
                // End at position 2
                destination = new Vector3(xPos[1], yPos[0], 1);

                travelDistanceX = destination.x - currLoc.x;
                travelDistanceY = destination.y - currLoc.y;
                
                yield return new WaitForSeconds(standardDelay);
                
                // Move Hand animation
                for (int i = 0; i < timesMoved; i++)
                {
                    Vector3 loc = new Vector3(hand.transform.position.x + (travelDistanceX/timesMoved), hand.transform.position.y + (travelDistanceY/timesMoved), hand.transform.position.z);
                    hand.transform.position = loc;

                    yield return new WaitForSeconds(moveTime/timesMoved);
                }
                
                yield return new WaitForSeconds(waitAfterMove);
                
                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            case "W to C":
                ResetHand(true);
                EnableGrids();
                
                waitAfterMove = 1.25f;
                moveTime = standardAnimationTime - standardDelay - waitAfterMove;
                timesMoved = 50;
                
                // Start at position 2
                currLoc = new Vector3(xPos[1], yPos[0], 1);
                hand.transform.position = currLoc;
                
                // End at position 9
                destination = new Vector3(xPos[2], yPos[2], 1);

                travelDistanceX = destination.x - currLoc.x;
                travelDistanceY = destination.y - currLoc.y;
                
                yield return new WaitForSeconds(standardDelay);
                
                // Move Hand animation
                for (int i = 0; i < timesMoved; i++)
                {
                    Vector3 loc = new Vector3(hand.transform.position.x + (travelDistanceX/timesMoved), hand.transform.position.y + (travelDistanceY/timesMoved), hand.transform.position.z);
                    hand.transform.position = loc;

                    yield return new WaitForSeconds(moveTime/timesMoved);
                }
                
                yield return new WaitForSeconds(waitAfterMove);
                
                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;
            
            
            // Iv Heal
            case "D to A":
                ResetHand(true);
                EnableGrids();
                
                waitAfterMove = 1.25f;
                moveTime = standardAnimationTime - standardDelay - waitAfterMove;
                timesMoved = 50;
                
                // Start at position 6
                if (GameManagerScript.rightHanded) currLoc = new Vector3(xPos[2], yPos[1], 1);
                else currLoc = new Vector3(xPos[0], yPos[1], 1);
                hand.transform.position = currLoc;
                
                // End at position 3
                if (GameManagerScript.rightHanded) destination = new Vector3(xPos[0], yPos[1], 1);
                else destination = new Vector3(xPos[2], yPos[1], 1);

                travelDistanceX = destination.x - currLoc.x;
                travelDistanceY = destination.y - currLoc.y;
                
                yield return new WaitForSeconds(standardDelay);
                
                // Move Hand animation
                for (int i = 0; i < timesMoved; i++)
                {
                    Vector3 loc = new Vector3(hand.transform.position.x + (travelDistanceX/timesMoved), hand.transform.position.y + (travelDistanceY/timesMoved), hand.transform.position.z);
                    hand.transform.position = loc;

                    yield return new WaitForSeconds(moveTime/timesMoved);
                }
                
                yield return new WaitForSeconds(waitAfterMove);
                
                DisableGrids(true, true);
                
                yield return new WaitForSeconds(endDelay);
                EndOfAnimation();
                break;


            default:
                ResetHand(true);
                EnableGrids();
                EndOfAnimation();
                break;
        }

        yield return new WaitForSeconds(standardDelay);
        
        
        void EndOfAnimation()
        {
            // The animation has ended
            switch (animationNumber)
            {
                case 1: HandAnimationManager.animation1Playing = false;
                    break;
                case 2: HandAnimationManager.animation2Playing = false;
                    break;
                case 3: HandAnimationManager.animation3Playing = false;
                    break;
            }
        }
    }


    void DisableGrids(bool handDisabled, bool gridsDisabled)
    {
        if (gridsDisabled)
        {
            for (int i = 0; i < gridSR.Length; i++)
            {
                gridSR[i].enabled = false;
            }
        }

        if (handDisabled) handSR.enabled = false;
    }

    void EnableGrids()
    {
        handSR.enabled = true;
        markerSR.enabled = false;
            
        for (int i = 0; i < gridSR.Length; i++)
        {
            gridSR[i].enabled = true;
        }
    }


    void ChangeLocation()
    {
        float xLoc = optionXLocation;
        float yLoc = 0;
        
        if (animation1) yLoc = optionYLocation1;
        if (animation2) yLoc = optionYLocation2;
        if (animation3) yLoc = optionYLocation3;
        
        this.transform.position = new Vector3(xLoc, yLoc, 1);
    }
    
    
    void ChangeScale()
    {
        float currHeight = handSR.sprite.bounds.extents.y * 2;
        float maxHeight = (optionHeight - (2*optionPadding)) / sizeOfHand;

        if (maxHeight != currHeight)
        {
            float scale = maxHeight / currHeight;

            hand.transform.localScale = new Vector3(scale * scaleFactor, scale * scaleFactor, 1);
            
            handIcons.transform.localScale = hand.transform.localScale;
        }
    }


    void ChangeHandLocation()
    {
        handIcons.transform.position = hand.transform.position;
        
        xPos = new[]
        {
            this.transform.position.x - optionWidth / 3, 
            this.transform.position.x,
            this.transform.position.x + optionWidth / 3,
        };
        
        yPos = new[]
        {
            this.transform.position.y + optionHeight / 3, 
            this.transform.position.y,
            this.transform.position.y - optionHeight / 3
        };
    }


    void ChangeMarkerScale()
    {
        float markerHeight = markerSR.sprite.bounds.extents.y * 2;
        float maxHeight = optionHeight - (3*optionPadding);
        float scale = maxHeight / markerHeight;
        
        marker.transform.localScale = new Vector3(scale, scale, 1);
    }


    void ChangeGrids()
    {
        float gridThickness = 0.375f;
        
        // Change scale of the grid
        float rowWidth = gridSR[0].sprite.bounds.extents.x * 2;
        float rowThickness = gridSR[0].sprite.bounds.extents.y * 2;
        float columnHeight = gridSR[2].sprite.bounds.extents.x * 2;
        float columnThickness = gridSR[2].sprite.bounds.extents.y * 2;
        
        float maxWidth = optionWidth - (2*optionPadding);
        float maxHeight = optionHeight - (2*optionPadding);
        
        float rowThick = ((optionHeight * gridThickness)/rowThickness);
        float columnThick = ((optionHeight * gridThickness)/columnThickness);

        // Change scale of the grid rows
        gridRow1.transform.localScale = new Vector3( maxWidth / rowWidth, rowThick, 1);
        gridRow2.transform.localScale = new Vector3( maxWidth / rowWidth, rowThick, 1);
        
        // Change scale of the grid columns
        gridColumn1.transform.localScale = new Vector3( columnThick, maxHeight / columnHeight, 1);
        gridColumn2.transform.localScale = new Vector3( columnThick, maxHeight / columnHeight, 1);
        
        // Position grid rows
        gridRow1.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + optionHeight/2/3, 1);
        gridRow2.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - optionHeight/2/3, 1);
        
        // Position grid columns
        gridColumn1.transform.position = new Vector3(this.transform.position.x + optionWidth/2/3, this.transform.position.y, 1);
        gridColumn2.transform.position = new Vector3(this.transform.position.x - optionWidth/2/3, this.transform.position.y, 1);
    }
    
    
    
    void ResetHand(bool resetIcons)
    {
        DisableGrids(true, true);
        
        hand.transform.eulerAngles = new Vector3(0,0,0);
        this.transform.position = new Vector3(xPos[1], yPos[1], 1);

        handSR.sprite = frontHand;
        handSR.flipX = false;

        if (resetIcons)
        {
            sizeOfHand = standardHandSize;
            flipHand.SetActive(false);
            
            hand.transform.position = this.transform.position;
            scaleFactor = 1f;
            
            rotateHandRight.SetActive(false);
            rotateHandLeft.SetActive(false);
            
            leftHandMoveLeft.SetActive(false);
            leftHandMoveRight.SetActive(false);
            rightHandMoveLeft.SetActive(false);
            rightHandMoveRight.SetActive(false);

            leftHandMoveLeft.transform.position = moveLeft.transform.position;
            rightHandMoveLeft.transform.position = moveLeft.transform.position;

            leftHandMoveRight.transform.position = moveRight.transform.position;
            rightHandMoveRight.transform.position = moveRight.transform.position;
        }
    }
}
