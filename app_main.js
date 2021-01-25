const { app, BrowserWindow, ipcMain } = require('electron');

const path = require('path');
const url = require('url');

// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the JavaScript object is garbage collected.
let mainWindow;

function createWindow() {
  // Create the browser window.
  mainWindow = new BrowserWindow({
    //alwaysOnTop: true,
    x : 5,
    y : 5,
    
    width: 1200, // Set these to whatever is convenient
    height: 600,

    // Needed to include communication between render and main processes
    webPreferences: {
      nodeIntegration: true,
    },
  });

  // mainWindow.setMenu(null);
  mainWindow.webContents.openDevTools(); // Uncomment to open the DevTools automatically

  // and load the index.html of the app.
  mainWindow.loadURL(url.format({
    pathname: path.join(__dirname, 'index.html'),
    protocol: 'file:',
    slashes: true,
  }));

  // Emitted when the window is closed.
  mainWindow.on('closed', () => {
    // Dereference the window object, usually you would store windows
    // in an array if your app supports multi windows, this is the time
    // when you should delete the corresponding element.
    mainWindow = null;
  });
}

// This method will be called when Electron has finished
// initialization and is ready to create browser windows.
// Some APIs can only be used after this event occurs.
app.on('ready', createWindow);

// Quit when all windows are closed.
app.on('window-all-closed', () => {
  // On OS X it is common for applications and their menu bar
  // to stay active until the user quits explicitly with Cmd + Q
  app.quit();
});

app.on('activate', () => {
  // On OS X it's common to re-create a window in the app when the
  // dock icon is clicked and there are no other windows open.
  if (mainWindow === null) {
    createWindow();
  }
});

// Example code for sending messages here from the main process to press keys
const { keyboard, Key } = require('@nut-tree/nut-js');
keyboard.config.autoDelayMs = 1;





// Key presses when a certain marker is present

// Palm marker (H)
ipcMain.on("H_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.H)});
ipcMain.on("H_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.H)});

// Netrixi marker (Y)
ipcMain.on("Y_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.Y)});
ipcMain.on("Y_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.Y)});

// Folkvar marker (O)
ipcMain.on("O_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.O)});
ipcMain.on("O_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.O)});

// Iv marker (I)
ipcMain.on("I_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.I)});
ipcMain.on("I_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.I)});

// Pause marker (P)
ipcMain.on("P_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.P)});
ipcMain.on("P_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.P)});

// Undo marker (U)
ipcMain.on("U_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.U)});
ipcMain.on("U_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.U)});

// Extra marker (V)
ipcMain.on("V_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.V)});
ipcMain.on("V_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.V)});

// No marker (J)
ipcMain.on("J_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.J)});
ipcMain.on("J_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.J)});




// Key presses based on location of Palm marker

// Top left (Q)
ipcMain.on("1_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.Q)});
ipcMain.on("1_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.Q)});

// Top middle (W)
ipcMain.on("2_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.W)});
ipcMain.on("2_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.W)});

// Top right (E)
ipcMain.on("3_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.E)});
ipcMain.on("3_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.E)});


// Middle left (A)
ipcMain.on("4_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.A)});
ipcMain.on("4_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.A)});

// Middle middle (S)
ipcMain.on("5_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.S)});
ipcMain.on("5_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.S)});

// Middle right (D)
ipcMain.on("6_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.D)});
ipcMain.on("6_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.D)});


// Bottom left (Z)
ipcMain.on("7_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.Z)});
ipcMain.on("7_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.Z)});

// Bottom middle (X)
ipcMain.on("8_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.X)});
ipcMain.on("8_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.X)});

// Bottom right (C)
ipcMain.on("9_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.C)});
ipcMain.on("9_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.C)});




// Key presses based on rotation of palm marker

// Starting position (K)
ipcMain.on("K_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.K)});
ipcMain.on("K_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.K)});

// Extreme left (L)
ipcMain.on("L_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.L)});
ipcMain.on("L_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.L)});

// Left (G)
ipcMain.on("G_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.G)});
ipcMain.on("G_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.G)});

// Middle (B)
ipcMain.on("B_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.B)});
ipcMain.on("B_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.B)});

// Right (T)
ipcMain.on("T_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.T)});
ipcMain.on("T_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.T)});

// Extreme right (R)
ipcMain.on("R_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.R)});
ipcMain.on("R_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.R)});




// Key presses based on depth of palm marker

// Near (N)
ipcMain.on("N_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.N)});
ipcMain.on("N_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.N)});

// Middle (M)
ipcMain.on("M_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.M)});
ipcMain.on("M_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.M)});

// Far (F)
ipcMain.on("F_KEY_DOWN", async (event,arg) => {keyboard.pressKey(Key.F)});
ipcMain.on("F_KEY_UP", async (event,arg) => {keyboard.releaseKey(Key.F)});