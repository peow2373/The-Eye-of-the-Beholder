const ipcRenderer = require('electron').ipcRenderer;
const Beholder = require('beholder-detection');

// Code for if I can get key press working on front side
// const { keyboard, Key, mouse, left, right, up, down, screen } = require("@nut-tree/nut-js");
// In keyboard.class.js, I have set the keyboard delay to 0 Ms
// Attach listener in the main process with the given ID
// keyboard.config.autoDelayMs = 1;

// Delay after marker has been undetected before it can be detected again
const BUTTON_TIMEOUT = 250;

// Marker detection variables
let palm, netrixi, folkvar, iv, undo, pause, extra;
let markerCenter, markerCorner1, markerCorner2;
let xCenter, yCenter, markerRotation, pastLocation, pastRotation, pastDepth;
let cornerDistance, xCorner1 = 0, yCorner1 = 0, xCorner2 = 0, yCorner2 = 0;

// Determines whether the player is right-handed or left-handed
let rightHand = true;

// Detection for boundaries of markerButton1 movement
let xLattice1 = 0, xLattice2 = 220, xLattice3 = 420, xLattice4 = 640;
let yLattice1 = 0, yLattice2 = 170, yLattice3 = 310, yLattice4 = 480;

// Detection for rotations of markerButton1
let startingPointR = 1.4, firstZoneR = 0.8, secondZoneR = 0.2, thirdZoneR = -0.4, fourthZoneR = -1, fifthZoneR = -1.6, resetR = -1.6;
let startingPointL = -1.4, firstZoneL = -0.8, secondZoneL = -0.2, thirdZoneL = 0.4, fourthZoneL = 1, fifthZoneL = 1.6, resetL = 1.6;

// Detection for depth of markerButton1
let near = 75, far = 50;

// Variable to help detection of changes in location, rotation, or depth
let deadzone = 5;

// Arrays for differentiating between marker inputs
let markerButton = [palm, netrixi, folkvar, iv, undo, pause, extra];
let keyOutput = ['H', 'Y', 'O', 'I', 'P', 'U', 'V'];
let wasMarkerPresent = [false, false, false, false, false, false, false];
let buttonTimer = [BUTTON_TIMEOUT, BUTTON_TIMEOUT, BUTTON_TIMEOUT, BUTTON_TIMEOUT, BUTTON_TIMEOUT, BUTTON_TIMEOUT, BUTTON_TIMEOUT];

// code written in here will be executed once when the page loads
function init() {
  // Initialize beholder
  Beholder.init('#beholder-root');

  // Change these values depending on what markers are being used 
  markerButton[0] = Beholder.getMarker(11);
  markerButton[1] = Beholder.getMarker(0);
  markerButton[2] = Beholder.getMarker(9);
  markerButton[3] = Beholder.getMarker(4);
  markerButton[4] = Beholder.getMarker(5);
  markerButton[5] = Beholder.getMarker(18);
  markerButton[6] = Beholder.getMarker(25);

  requestAnimationFrame(update);
}

let lastTime = Date.now();
// code written in here will be executed every frame
function update() {
  const currentTime = Date.now();
  const delta = currentTime - lastTime;
  lastTime = currentTime;

  Beholder.update(); // comment this line to turn off detection

  requestAnimationFrame(update);

  // Call KeyPress function for each markerButton
  for (let i = 0; i < markerButton.length; i++) {

    buttonTimer[i] -= delta;

    // This is the logic for a single key press using a given marker
    let keyInput1 = KeyCode(keyOutput[i], 1);
    let keyInput2 = KeyCode(keyOutput[i], 2);

    if (markerButton[i].present) {
      buttonTimer[i] = BUTTON_TIMEOUT;
      if (!wasMarkerPresent[i]) {
        wasMarkerPresent[i] = true;

        // Tells the main process that the marker is present
        ipcRenderer.send(keyInput1);
        ipcRenderer.send(keyInput2);
      }
    }

    if (wasMarkerPresent[i] && !markerButton[i].present && buttonTimer[i] <= 0) {
      wasMarkerPresent[i] = false;

      // Tells the main process that the marker is no longer present
      ipcRenderer.send(keyInput1);
      ipcRenderer.send(keyInput2);
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



function KeyCode(keyOutput, binary) {

  // if Palm marker is visible
  if (keyOutput == 'H') {
    if (binary == 1) return 'H_KEY_DOWN';
    if (binary == 2) return 'H_KEY_UP';
  }
  // if Netrixi marker is visible
  if (keyOutput == 'Y') {
    if (binary == 1) return 'Y_KEY_DOWN';
    if (binary == 2) return 'Y_KEY_UP';
  }
  // if Folkvar marker is visible
  if (keyOutput == 'O') {
    if (binary == 1) return 'O_KEY_DOWN';
    if (binary == 2) return 'O_KEY_UP';
  }
  // if Iv marker is visible
  if (keyOutput == 'I') {
    if (binary == 1) return 'I_KEY_DOWN';
    if (binary == 2) return 'I_KEY_UP';
  }
  // if Pause marker is visible
  if (keyOutput == 'P') {
    if (binary == 1) return 'P_KEY_DOWN';
    if (binary == 2) return 'P_KEY_UP';
  }
  // if Undo marker is visible
  if (keyOutput == 'U') {
    if (binary == 1) return 'U_KEY_DOWN';
    if (binary == 2) return 'U_KEY_UP';
  }
  // if Extra marker is visible
  if (keyOutput == 'V') {
    if (binary == 1) return 'V_KEY_DOWN';
    if (binary == 2) return 'V_KEY_UP';
  }
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
      if (pastLocation != 1) {
        ipcRenderer.send('1_KEY_DOWN');
        pastLocation = 1;
        ipcRenderer.send('1_KEY_UP');
        return;
      }
    }
    // Is the marker in the middle left section of the grid?
    if (yCenter > yLattice2 +deadzone/2 && yCenter <= yLattice3 -deadzone/2) {
      if (pastLocation != 4) {
        ipcRenderer.send('4_KEY_DOWN');
        pastLocation = 4;
        ipcRenderer.send('4_KEY_UP');
        return;
      }
    }
    // Is the marker in the lower left section of the grid?
    if (yCenter > yLattice3 +deadzone/2 && yCenter <= yLattice4 -deadzone/2) {
      if (pastLocation != 7) {
        ipcRenderer.send('7_KEY_DOWN');
        pastLocation = 7;
        ipcRenderer.send('7_KEY_UP');
        return;
      }
    }
  }

  // Is the marker in the middle section of the grid?
  if (xCenter > xLattice2 +deadzone/2 && xCenter <= xLattice3 -deadzone/2) {

    // Is the marker in the upper middle section of the grid?
    if (yCenter > yLattice1 -deadzone/2 && yCenter <= yLattice2 -deadzone/2) {
      if (pastLocation != 2) {
        ipcRenderer.send('2_KEY_DOWN');
        pastLocation = 2;
        ipcRenderer.send('2_KEY_UP');
        return;
      }
    }
    // Is the marker in the center section of the grid?
    if (yCenter > yLattice2 +deadzone/2 && yCenter <= yLattice3 -deadzone/2) {
      if (pastLocation != 5) {
        ipcRenderer.send('5_KEY_DOWN');
        pastLocation = 5;
        ipcRenderer.send('5_KEY_UP');
        return;
      }
    }
    // Is the marker in the lower middle section of the grid?
    if (yCenter > yLattice3 +deadzone/2 && yCenter <= yLattice4 -deadzone/2) {
      if (pastLocation != 8) {
        ipcRenderer.send('8_KEY_DOWN');
        pastLocation = 8;
        ipcRenderer.send('8_KEY_UP');
        return;
      }
    }
  }

  // Is the marker in the right section of the grid?
  if (xCenter > xLattice3 +deadzone/2 && xCenter <= xLattice4 -deadzone/2) {

    // Is the marker in the upper right section of the grid?
    if (yCenter > yLattice1 +deadzone/2 && yCenter <= yLattice2 -deadzone/2) {
      if (pastLocation != 3) {
        ipcRenderer.send('3_KEY_DOWN');
        pastLocation = 3;
        ipcRenderer.send('3_KEY_UP');
        return;
      }
    }
    // Is the marker in the middle right section of the grid?
    if (yCenter > yLattice2 +deadzone/2 && yCenter <= yLattice3 -deadzone/2) {
      if (pastLocation != 6) {
        ipcRenderer.send('6_KEY_DOWN');
        pastLocation = 6;
        ipcRenderer.send('6_KEY_UP');
        return;
      }
    }
    // Is the marker in the lower right section of the grid?
    if (yCenter > yLattice3 +deadzone/2 && yCenter <= yLattice4 -deadzone/2) {
      if (pastLocation != 9) {
        ipcRenderer.send('9_KEY_DOWN');
        pastLocation = 9;
        ipcRenderer.send('9_KEY_UP');
        return;
      }
    }
  }
}



function MarkerRotation() {

  // Retrieve marker rotation
  markerRotation = markerButton[0].rotation;

  // is the person using the palm marker with their right-hand?
  if (rightHand) {

    // if the player starts by facing the Palm marker to the right
    if (markerRotation <= startingPointR -deadzone/200) {
      if (pastRotation != -1) {
        ipcRenderer.send('K_KEY_DOWN');
        pastRotation = -1;
        ipcRenderer.send('K_KEY_UP');
      }
    }

    // if the Palm marker is in the starting position

    // is the marker then rotated to the first zone?
    if (markerRotation > startingPointR +deadzone/200 && markerRotation <= firstZoneR -deadzone/200) {
      if (pastRotation != 1) {
        ipcRenderer.send('L_KEY_DOWN');
        pastRotation = 1;
        ipcRenderer.send('L_KEY_UP');
        return;
      }
    }

    // is the marker then rotated to the second zone?
    if (markerRotation > firstZoneR +deadzone/200 && markerRotation <= secondZoneR -deadzone/200) {
      if (pastRotation != 2) {
        ipcRenderer.send('G_KEY_DOWN');
        pastRotation = 2;
        ipcRenderer.send('G_KEY_UP');
        return;
      }
    }

    // is the marker then rotated to the third zone?
    if (markerRotation > secondZoneR +deadzone/200 && markerRotation <= thirdZoneR -deadzone/200) {
      if (pastRotation != 3) {
        ipcRenderer.send('B_KEY_DOWN');
        pastRotation = 3;
        ipcRenderer.send('B_KEY_UP');
        return;
      }
    }

    // is the marker then rotated to the fourth zone?
    if (markerRotation > thirdZoneR +deadzone/200 && markerRotation <= fourthZoneR -deadzone/200) {
      if (pastRotation != 4) {
        ipcRenderer.send('T_KEY_DOWN');
        pastRotation = 4;
        ipcRenderer.send('T_KEY_UP');
        return;
      }
    }

    // is the marker then rotated to the fifth zone?
    if (markerRotation > fourthZoneR +deadzone/200 && markerRotation <= fifthZoneR) {
      if (pastRotation != 5) {
        ipcRenderer.send('R_KEY_DOWN');
        pastRotation = 5;
        ipcRenderer.send('R_KEY_UP');
        return;
      }
    }
  }

  // is the person using the palm marker with their left-hand?
  else {

    // if the player starts by facing the Palm marker to the left
    if (markerRotation <= startingPointL -deadzone/200) {
      if (pastRotation != -1) {
        ipcRenderer.send('K_KEY_DOWN');
        pastRotation = -1;
        ipcRenderer.send('K_KEY_UP');
      }
    }

    // if the Palm marker is in the starting position
    
    // is the marker then rotated to the first zone?
    if (markerRotation > startingPointL +deadzone/200 && markerRotation <= firstZoneL -deadzone/200) {
      if (pastRotation != 1) {
        ipcRenderer.send('L_KEY_DOWN');
        pastRotation = 1;
        ipcRenderer.send('L_KEY_UP');
        return;
      }
    }

    // is the marker then rotated to the second zone?
    if (markerRotation > firstZoneL +deadzone/200 && markerRotation <= secondZoneL -deadzone/200) {
      if (pastRotation != 2) {
        ipcRenderer.send('G_KEY_DOWN');
        pastRotation = 2;
        ipcRenderer.send('G_KEY_UP');
        return;
      }
    }

    // is the marker then rotated to the third zone?
    if (markerRotation > secondZoneL +deadzone/200 && markerRotation <= thirdZoneL -deadzone/200) {
      if (pastRotation != 3) {
        ipcRenderer.send('B_KEY_DOWN');
        pastRotation = 3;
        ipcRenderer.send('B_KEY_UP');
        return;
      }
    }

    // is the marker then rotated to the fourth zone?
    if (markerRotation > thirdZoneL +deadzone/200 && markerRotation <= fourthZoneL -deadzone/200) {
      if (pastRotation != 4) {
        ipcRenderer.send('T_KEY_DOWN');
        pastRotation = 4;
        ipcRenderer.send('T_KEY_UP');
        return;
      }
    }

    // is the marker then rotated to the fifth zone?
    if (markerRotation > fourthZoneL +deadzone/200 && markerRotation <= fifthZoneL) {
      if (pastRotation != 5) {
        ipcRenderer.send('R_KEY_DOWN');
        pastRotation = 5;
        ipcRenderer.send('R_KEY_UP');
        return;
      }
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
  if (cornerDistance >= near +deadzone/3) {
    if (pastDepth != 1) {
      ipcRenderer.send('N_KEY_DOWN');
      pastDepth = 1;
      ipcRenderer.send('N_KEY_UP');
      return;
    }
  }

  // Is the marker in the middle from the camera?
  if (cornerDistance > far +deadzone/3 && cornerDistance < near -deadzone/3) {
    if (pastDepth != 2) {
      ipcRenderer.send('M_KEY_DOWN');
      pastDepth = 2;
      ipcRenderer.send('M_KEY_UP');
      return;
    }
  }

  // Is the marker farthest from the camera?
  if (cornerDistance <= far -deadzone/3) {
    if (pastDepth != 3) {
      ipcRenderer.send('F_KEY_DOWN');
      pastDepth = 3;
      ipcRenderer.send('F_KEY_UP');
      return;
    }
  }
}

window.onload = init;