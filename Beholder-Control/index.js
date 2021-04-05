const ipcRenderer = require('electron').ipcRenderer;
const Beholder = require('beholder-detection').default;

const { keyboard, Key } = require('@nut-tree/nut-js');
keyboard.config.autoDelayMs = 0;

// Code for if I can get key press working on front side
// const { keyboard, Key, mouse, left, right, up, down, screen } = require("@nut-tree/nut-js");
// In keyboard.class.js, I have set the keyboard delay to 0 Ms
// Attach listener in the main process with the given ID
// keyboard.config.autoDelayMs = 0;

// Delay after marker has been undetected before it reports that the marker is missing
const BUTTON_TIMEOUT = 250;
const BUTTON_LONG = 2000;

// Marker detection variables
let palm, netrixi, folkvar, iv, undo, go;
let markerCenter, markerCorner1, markerCorner2;
let xCenter, yCenter, markerRotation, pastLocation, pastRotation, pastDepth;
let cornerDistance, xCorner1 = 0, yCorner1 = 0, xCorner2 = 0, yCorner2 = 0;

// Detection for boundaries of markerButton1 movement
let xLattice1 = 0, xLattice2 = 220, xLattice3 = 420, xLattice4 = 640;
let yLattice1 = 0, yLattice2 = 170, yLattice3 = 310, yLattice4 = 480;

// Detection for rotations of markerButton1
let startPoint = 1.5, endPoint = 0;
let secondZone = (startPoint - endPoint) * (3/4), thirdZone = (startPoint - endPoint) * (2/4), fourthZone = (startPoint - endPoint) * (1/4);

let rotatingRight = false, rotatingLeft = false;

// Detection for depth of markerButton1
let near = 75, far = 50;

// Variable to help detection of changes in location, rotation, or depth
let deadzone = 5;

// Arrays for differentiating between marker inputs
let markerButton = [palm, netrixi, folkvar, iv, undo, go];
let keyOutput = ['H', 'Y', 'O', 'I', 'U', 'V'];
let wasMarkerPresent = [false, false, false, false, false, false];
let buttonTimer = [BUTTON_LONG, BUTTON_TIMEOUT, BUTTON_TIMEOUT, BUTTON_TIMEOUT, BUTTON_TIMEOUT, BUTTON_TIMEOUT];

// Hand overlay
let canvas, ctx;
let videoElement;
let hand, rhe, rhg, lhe, lhg;
let netrixiMarker, folkvarMarker, ivMarker, undoMarker;
let gridRow, gridColumn;

let videoLoaded = false;
let videoMargins = 10;

let handSize = 0.4;
let markerSize = 0.25;
let gridThickness = 0.25;


// code written in here will be executed once when the page loads
function init() {
  // Initialize beholder
  Beholder.init('#beholder-root', { feed_params: { flip: true , contrast: 100 }, overlay_params: { present: false } }, [855, 787, 907, 357, 683, 84]);

  // Change these values depending on what markers are being used
  markerButton[0] = Beholder.getMarker(855);
  markerButton[1] = Beholder.getMarker(787);
  markerButton[2] = Beholder.getMarker(907);
  markerButton[3] = Beholder.getMarker(357);
  markerButton[4] = Beholder.getMarker(683);
  markerButton[5] = Beholder.getMarker(84);
  
  // Initializes a video stream of the webcam
  canvas = document.querySelector("#canvas-overlay");
  ctx = canvas.getContext("2d");
  
  rhe = document.querySelector(".rightHandEye");
  rhg = document.querySelector(".rightHandGo");
  lhe = document.querySelector(".leftHandEye");
  lhg = document.querySelector(".leftHandGo");
  
  netrixiMarker = document.querySelector(".netrixiMarker");
  folkvarMarker = document.querySelector(".folkvarMarker");
  ivMarker = document.querySelector(".ivMarker");
  undoMarker = document.querySelector(".undoMarker");
  
  gridRow = document.querySelector(".gridRow");
  gridColumn = document.querySelector(".gridColumn");
  
  
  window.onresize = ()=>{
      
      // Changes the body window
      body = document.querySelector("body");
      body.style.margin = videoMargins + "px";
      
      body.style.backgroundColor = "black";
      body.style.overflow = "hidden";
      
      // Resizes the video width to match the height while retaining a 4:3 aspect ratio
      if (window.innerWidth < window.innerHeight*4/3) {
          canvas.width = window.innerWidth - (videoMargins*2);
          if (canvas.height != window.innerWidth*3/4) canvas.height = canvas.width*3/4;
      }

      // Resizes the video height to match the width while retaining a 4:3 aspect ratio
      if (window.innerHeight < window.innerWidth*3/4) {
          canvas.height = window.innerHeight - (videoMargins*2);
          if (canvas.width != window.innerHeight*4/3) canvas.width = canvas.height*4/3;
      }
  }
  
  window.onresize();
  
  Beholder.addVideoStreamListener((s)=>{
      videoLoaded = true;
      videoElement = document.querySelector("#beholder-video");
  });
  

  requestAnimationFrame(update);
}

let lastTime = Date.now();
// code written in here will be executed every frame
function update() {
    
  const currentTime = Date.now();
  let delta = currentTime - lastTime;
  lastTime = currentTime;
  
  // Limits delta to 50 to account for latency
  if (delta > 50) {
    delta = 50;
  }

  Beholder.update(); // comment this line to turn off detection
    
    // Displays the webcam as a video
    ctx.save();
    ctx.translate(canvas.width, 0);
    ctx.scale(-1, 1);
    if (videoLoaded) ctx.drawImage(videoElement, 0, 0, canvas.width, canvas.height);
    ctx.restore();
    
    // Draws grid overlay
    DrawGridOverlay();
    
    // Draws hand image
    ctx.save();
    DrawHandOverlay();
    ctx.restore();
    
    // Draws marker images
    DrawMarkerOverlay();

  requestAnimationFrame(update);

  // Call KeyPress function for each markerButton
  for (let i = 0; i < markerButton.length; i++) {

    // This is the logic for a single key press using a given marker
    let keyInput = KeyCode(keyOutput[i]);

    if (markerButton[i].present) {
      buttonTimer[i] = BUTTON_TIMEOUT;
      if (!wasMarkerPresent[i]) {
          
        // If it is the Go marker 
        if (i == 5) {
            if (!wasMarkerPresent[0]) {
                wasMarkerPresent[5] = true;

                setImmediate( async (event,arg) => {keyboard.pressKey(Key.V)});
                setImmediate( async (event,arg) => {keyboard.releaseKey(Key.V)});
                
                //rotatingRight = false;
                //rotatingLeft = false;
            }
        } else {
            
            wasMarkerPresent[i] = true;

            // Tells the main process that the marker is present
            setImmediate( async (event,arg) => {keyboard.pressKey(keyInput)});
            setImmediate( async (event,arg) => {keyboard.releaseKey(keyInput)});
        }
      }
    } else {
      // Start timer countdown once marker disappears
      buttonTimer[i] -= delta;
    }

    if (wasMarkerPresent[i] && !markerButton[i].present && buttonTimer[i] <= 0) {
      wasMarkerPresent[i] = false;

      // Tells the main process that the marker is no longer present
        // If it is the Palm marker
        if (keyOutput[i] == 'H') {
            setImmediate( async (event,arg) => {keyboard.pressKey(Key.L)});
            setImmediate( async (event,arg) => {keyboard.releaseKey(Key.L)});
        } else {
            setImmediate( async (event,arg) => {keyboard.pressKey(keyInput)});
            setImmediate( async (event,arg) => {keyboard.releaseKey(keyInput)});
        }

        return;
    }
  }

  // Retrieve information about the markers, such as location, rotation, and distance between corners
  if (markerButton[0].present) {
    MarkerLocation();
    MarkerRotation();
    MarkerDepth();
  }
}


function DrawGridOverlay() {
    
    let rowThickness = canvas.height*gridThickness
    let columnThickness = canvas.width*gridThickness

    // Draw grid rows
    ctx.drawImage(gridRow, 0, (canvas.height/3) - (rowThickness/2), canvas.width, rowThickness);
    ctx.drawImage(gridRow, 0, (canvas.height*2/3) - (rowThickness/2), canvas.width, rowThickness);

    // Draw grid columns
    ctx.drawImage(gridColumn, (canvas.width/3) - (rowThickness/2), 0, columnThickness, canvas.height);
    ctx.drawImage(gridColumn, (canvas.width*2/3) - (rowThickness/2), 0, columnThickness, canvas.height);
}


function DrawHandOverlay() {

    // Check to see if the Go marker is present
    if (markerButton[5].present && !markerButton[0].present) {
        markerCenter = markerButton[5].center;
        hand = rhg;
    } else {
        markerCenter = markerButton[0].center;
        hand = rhe;
    }
    
    let xHand, yHand;

    // Check to see if the Palm marker is present
    if (markerButton[0].present || markerButton[5].present) {
        xHand = markerCenter[Object.keys(markerCenter)[0]];
        yHand = markerCenter[Object.keys(markerCenter)[1]];
    } else {
        if (!wasMarkerPresent[0] && !wasMarkerPresent[5]) {
            //xHand = -1000;
            //yHand = -1000;
        }
    }
    
    let xStart = (xHand*canvas.width)/640;
    let yStart = (yHand*canvas.height)/480;
    let size = handSize * canvas.width;

    ctx.translate(xStart, yStart)
    ctx.rotate(0);
    
    // Check to see if the Palm marker is rotated
    if (pastRotation == -1 || pastRotation == -2) ctx.rotate(-Math.PI/2);
    if (pastRotation == 2 || pastRotation == -4) ctx.rotate(-Math.PI/2 * 3/4);
    if (pastRotation == 3 || pastRotation == -5) ctx.rotate(-Math.PI/2 * 2/4);
    if (pastRotation == 4 || pastRotation == -6) ctx.rotate(-Math.PI/2 * 1/4);
    
    ctx.translate(-xStart, -yStart)
    
    // Check to see if the Palm marker is far/near
    if (pastDepth == 1) size *= 1.65;
    if (pastDepth == 2) size *= 1.15;
    if (pastDepth == 3) size *= 0.75;

    ctx.drawImage(hand, xStart-(size/2), yStart-(size/2), size, size);
}


function DrawMarkerOverlay() {

    for (let i = 1; i < markerButton.length - 1; i++) {

        // Check to see if the marker is present
        markerCenter = markerButton[i].center;

        let xLoc, yLoc;

        // Check to see if the Palm marker is present
        if (markerButton[i].present) {
            xLoc = markerCenter[Object.keys(markerCenter)[0]];
            yLoc = markerCenter[Object.keys(markerCenter)[1]];
            
        } else {
            if (!wasMarkerPresent[i]) {
                xLoc = -1000;
                yLoc = -1000;
            }
        }

        let xStart = (xLoc*canvas.width)/640;
        let yStart = (yLoc*canvas.height)/480;
        let size = markerSize * canvas.width;

        if (i == 1) ctx.drawImage(netrixiMarker, xStart-(size/2), yStart-(size/2), size, size);
        if (i == 2) ctx.drawImage(folkvarMarker, xStart-(size/2), yStart-(size/2), size, size);
        if (i == 3) ctx.drawImage(ivMarker, xStart-(size/2), yStart-(size/2), size, size);
        if (i == 4) ctx.drawImage(undoMarker, xStart-(size/2), yStart-(size/2), size, size);
    }
}



function KeyCode(keyOutput) {

  // if Palm marker is visible
  if (keyOutput == 'H') return Key.H;
  
  // if Netrixi marker is visible
  if (keyOutput == 'Y') return Key.Y;
  
  // if Folkvar marker is visible
  if (keyOutput == 'O') return Key.O;

  // if Iv marker is visible
  if (keyOutput == 'I') return Key.I;
  
  // if Undo marker is visible
  if (keyOutput == 'U') return Key.U;
  
  // if Go marker is visible
  if (keyOutput == 'V') return Key.V;
}



function MarkerLocation() {

  // Retrieve marker location
  markerCenter = markerButton[0].center;

  if (markerButton[0].present) {
    xCenter = markerCenter[Object.keys(markerCenter)[0]];
    yCenter = markerCenter[Object.keys(markerCenter)[1]];
  } else {
    xCenter = 0;
    yCenter = 0;
  }

  // Is the marker in the left section of the grid?
  if (xCenter > xLattice1 +deadzone/2 && xCenter <= xLattice2 -deadzone/2) {

    // Is the marker in the upper left section of the grid?
    if (yCenter > yLattice1 +deadzone/2 && yCenter <= yLattice2 -deadzone/2) {
      if (pastLocation !== 1) {
        pastLocation = 1;
          setImmediate( async (event,arg) => {keyboard.pressKey(Key.Q)});
          setImmediate( async (event,arg) => {keyboard.releaseKey(Key.Q)});
        return;
      }
    }
    // Is the marker in the middle left section of the grid?
    if (yCenter > yLattice2 +deadzone/2 && yCenter <= yLattice3 -deadzone/2) {
      if (pastLocation !== 4) {
        pastLocation = 4;
          setImmediate( async (event,arg) => {keyboard.pressKey(Key.A)});
          setImmediate( async (event,arg) => {keyboard.releaseKey(Key.A)});
        return;
      }
    }
    // Is the marker in the lower left section of the grid?
    if (yCenter > yLattice3 +deadzone/2 && yCenter <= yLattice4 -deadzone/2) {
      if (pastLocation !== 7) {
        pastLocation = 7;
          setImmediate( async (event,arg) => {keyboard.pressKey(Key.Z)});
          setImmediate( async (event,arg) => {keyboard.releaseKey(Key.Z)});
        return;
      }
    }
  }

  // Is the marker in the middle section of the grid?
  if (xCenter > xLattice2 +deadzone/2 && xCenter <= xLattice3 -deadzone/2) {

    // Is the marker in the upper middle section of the grid?
    if (yCenter > yLattice1 -deadzone/2 && yCenter <= yLattice2 -deadzone/2) {
      if (pastLocation !== 2) {
        pastLocation = 2;
          setImmediate( async (event,arg) => {keyboard.pressKey(Key.W)});
          setImmediate( async (event,arg) => {keyboard.releaseKey(Key.W)});
        return;
      }
    }
    // Is the marker in the center section of the grid?
    if (yCenter > yLattice2 +deadzone/2 && yCenter <= yLattice3 -deadzone/2) {
      if (pastLocation !== 5) {
        pastLocation = 5;
          setImmediate( async (event,arg) => {keyboard.pressKey(Key.S)});
          setImmediate( async (event,arg) => {keyboard.releaseKey(Key.S)});
        return;
      }
    }
    // Is the marker in the lower middle section of the grid?
    if (yCenter > yLattice3 +deadzone/2 && yCenter <= yLattice4 -deadzone/2) {
      if (pastLocation !== 8) {
        pastLocation = 8;
          setImmediate( async (event,arg) => {keyboard.pressKey(Key.X)});
          setImmediate( async (event,arg) => {keyboard.releaseKey(Key.X)});
        return;
      }
    }
  }

  // Is the marker in the right section of the grid?
  if (xCenter > xLattice3 +deadzone/2 && xCenter <= xLattice4 -deadzone/2) {

    // Is the marker in the upper right section of the grid?
    if (yCenter > yLattice1 +deadzone/2 && yCenter <= yLattice2 -deadzone/2) {
      if (pastLocation !== 3) {
        pastLocation = 3;
          setImmediate( async (event,arg) => {keyboard.pressKey(Key.E)});
          setImmediate( async (event,arg) => {keyboard.releaseKey(Key.E)});
        return;
      }
    }
    // Is the marker in the middle right section of the grid?
    if (yCenter > yLattice2 +deadzone/2 && yCenter <= yLattice3 -deadzone/2) {
      if (pastLocation !== 6) {
        pastLocation = 6;
          setImmediate( async (event,arg) => {keyboard.pressKey(Key.D)});
          setImmediate( async (event,arg) => {keyboard.releaseKey(Key.D)});
        return;
      }
    }
    // Is the marker in the lower right section of the grid?
    if (yCenter > yLattice3 +deadzone/2 && yCenter <= yLattice4 -deadzone/2) {
      if (pastLocation !== 9) {
        pastLocation = 9;
          setImmediate( async (event,arg) => {keyboard.pressKey(Key.C)});
          setImmediate( async (event,arg) => {keyboard.releaseKey(Key.C)});
      }
    }
  }
}



function MarkerRotation() {

  // Retrieve marker rotation
  markerRotation = markerButton[0].rotation;
  
  let deadZoneDivided = 0.05;

    // RIGHT HANDED
  
    // if the player starts by facing the Palm marker to the Left
    if (markerRotation >= startPoint +deadZoneDivided) {
        if (pastRotation !== -1) {
            pastRotation = -1;
              setImmediate( async (event,arg) => {keyboard.pressKey(Key.K)});
              setImmediate( async (event,arg) => {keyboard.releaseKey(Key.K)});
            
            rotatingRight = true;
            rotatingLeft = false;
            return;
        }
    }

    // is the marker then rotated to the second zone?
    if (markerRotation < startPoint -deadZoneDivided && markerRotation >= secondZone +deadZoneDivided) {
        if (pastRotation !== 2) {
            pastRotation = 2;
            if (rotatingRight) {
                setImmediate( async (event,arg) => {keyboard.pressKey(Key.G)});
                setImmediate( async (event,arg) => {keyboard.releaseKey(Key.G)});
            }
            return;
        }
    }

    // is the marker then rotated to the third zone?
    if (markerRotation < secondZone -deadZoneDivided && markerRotation >= thirdZone +deadZoneDivided) {
        if (pastRotation !== 3) {
            pastRotation = 3;
            if (rotatingRight) {
                setImmediate( async (event,arg) => {keyboard.pressKey(Key.B)});
                setImmediate( async (event,arg) => {keyboard.releaseKey(Key.B)});
            }
            return;
        }
    }

    // is the marker then rotated to the fourth zone?
    if (markerRotation < thirdZone -deadZoneDivided && markerRotation >= fourthZone +deadZoneDivided) {
        if (pastRotation !== 4) {
            pastRotation = 4;
            if (rotatingRight) {
                setImmediate( async (event,arg) => {keyboard.pressKey(Key.T)});
                setImmediate( async (event,arg) => {keyboard.releaseKey(Key.T)});
            }
            return;
        }
    }

    // is the marker then rotated to the fifth zone?
    if (markerRotation < fourthZone -deadZoneDivided && markerRotation >= endPoint +deadZoneDivided) {
        if (pastRotation !== 5) {
            pastRotation = 5;
            if (rotatingRight) {
                setImmediate( async (event,arg) => {keyboard.pressKey(Key.R)});
                setImmediate( async (event,arg) => {keyboard.releaseKey(Key.R)});
            }
            return;
        }
    }

    // LEFT HANDED  

    // if the player starts by facing the Palm marker to the Right
    if (markerRotation <= (-startPoint) -deadZoneDivided) {
        if (pastRotation !== -2) {
            pastRotation = -2;
              setImmediate( async (event,arg) => {keyboard.pressKey(Key.J)});
              setImmediate( async (event,arg) => {keyboard.releaseKey(Key.J)});

            rotatingRight = false;
            rotatingLeft = true;
            return;
        }
    }

    // is the marker then rotated to the second zone?
    if (markerRotation > (-startPoint) +deadZoneDivided && markerRotation <= (-secondZone) -deadZoneDivided) {
        if (pastRotation !== -4) {
            pastRotation = -4;
            if (rotatingLeft) {
                setImmediate( async (event,arg) => {keyboard.pressKey(Key.G)});
                setImmediate( async (event,arg) => {keyboard.releaseKey(Key.G)});
            }
            return;
        }
    }

    // is the marker then rotated to the third zone?
    if (markerRotation > (-secondZone) +deadZoneDivided && markerRotation <= (-thirdZone) -deadZoneDivided) {
        if (pastRotation !== -5) {
            pastRotation = -5;
            if (rotatingLeft) {
                setImmediate( async (event,arg) => {keyboard.pressKey(Key.B)});
                setImmediate( async (event,arg) => {keyboard.releaseKey(Key.B)});
            }
            return;
        }
    }

    // is the marker then rotated to the fourth zone?
    if (markerRotation > (-thirdZone) +deadZoneDivided && markerRotation <= (-fourthZone) -deadZoneDivided) {
        if (pastRotation !== -6) {
            pastRotation = -6;
            if (rotatingLeft) {
                setImmediate( async (event,arg) => {keyboard.pressKey(Key.T)});
                setImmediate( async (event,arg) => {keyboard.releaseKey(Key.T)});
            }
            return;
        }
    }

    // is the marker then rotated to the fifth zone?
    if (markerRotation > (-fourthZone) +deadZoneDivided && markerRotation <= endPoint -deadZoneDivided) {
        if (pastRotation !== -7) {
            pastRotation = -7;
            if (rotatingLeft) {
                setImmediate( async (event,arg) => {keyboard.pressKey(Key.R)});
                setImmediate( async (event,arg) => {keyboard.releaseKey(Key.R)});
            }
            return;
        }
    }
}



function MarkerDepth() {

  // Retrieve marker corners and determine distance between them
  markerCorner1 = markerButton[0].corners[0];
  markerCorner2 = markerButton[0].corners[1];

  if (markerButton[0].present) {
    xCorner1 = markerCorner1[Object.keys(markerCorner1)[0]];
    yCorner1 = markerCorner1[Object.keys(markerCorner1)[1]];
    xCorner2 = markerCorner2[Object.keys(markerCorner2)[0]];
    yCorner2 = markerCorner2[Object.keys(markerCorner2)[1]];
  }

  let xDistance = xCorner2 - xCorner1;
  let yDistance = yCorner2 - yCorner1;

  cornerDistance = Math.sqrt( xDistance*xDistance + yDistance*yDistance );

  // Is the marker nearest to the camera?
  if (cornerDistance >= near +deadzone/2) {
    if (pastDepth !== 1) {
      pastDepth = 1;
        setImmediate( async (event,arg) => {keyboard.pressKey(Key.N)});
        setImmediate( async (event,arg) => {keyboard.releaseKey(Key.N)});
      return;
    }
  }

  // Is the marker in the middle from the camera?
  if (cornerDistance > far +deadzone/3 && cornerDistance < near -deadzone/2) {
    if (pastDepth !== 2) {
      pastDepth = 2;
        setImmediate( async (event,arg) => {keyboard.pressKey(Key.M)});
        setImmediate( async (event,arg) => {keyboard.releaseKey(Key.M)});
      return;
    }
  }

  // Is the marker farthest from the camera?
  if (cornerDistance <= far -deadzone/2) {
    if (pastDepth !== 3) {
      pastDepth = 3;
        setImmediate( async (event,arg) => {keyboard.pressKey(Key.F)});
        setImmediate( async (event,arg) => {keyboard.releaseKey(Key.F)});
    }
  }
}

window.onload = init;
