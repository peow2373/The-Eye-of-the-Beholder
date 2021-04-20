const { screen, app, BrowserWindow, ipcMain } = require('electron');

const path = require('path');
const url = require('url');

let width = 640;
let height = 480;

// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the JavaScript object is garbage collected.
let mainWindow;

function createWindow() {
  // Create the browser window.
  const screenData = screen.getPrimaryDisplay().workAreaSize
  
  mainWindow = new BrowserWindow({
    
    alwaysOnTop: true,
    
    x : screenData.width - width, // xOffset from where the window is loaded
    y : 0, // yOffset from where the window is loaded

    // Set these to change window size
    width: width,
    height: height,
    
    //frame: false, 
    icon: __dirname + '/images/Electron Icon.ico',

    // Needed to include communication between render and main processes
    webPreferences: {
      nodeIntegration: true,
    },
  });

  mainWindow.setMenu(null);
  //mainWindow.webContents.openDevTools(); // Uncomment to open the DevTools automatically
  
  

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
keyboard.config.autoDelayMs = 0;


// Key presses when a certain marker is present

// Palm marker appears (H)
ipcMain.on("H_KEY_DOWN", async (event,arg) => {keyboard.type(Key.H)});

// Palm marker disappears (L)
ipcMain.on("L_KEY_DOWN", async (event,arg) => {keyboard.type(Key.L)});

// Netrixi marker (Y)
ipcMain.on("Y_KEY_DOWN", async (event,arg) => {keyboard.type(Key.Y)});

// Folkvar marker (O)
ipcMain.on("O_KEY_DOWN", async (event,arg) => {keyboard.type(Key.O)});

// Iv marker (I)
ipcMain.on("I_KEY_DOWN", async (event,arg) => {keyboard.type(Key.I)});

// Undo marker (U)
ipcMain.on("U_KEY_DOWN", async (event,arg) => {keyboard.type(Key.U)});

// Go marker (V)
ipcMain.on("V_KEY_DOWN", async (event,arg) => {keyboard.type(Key.V)});




// Key presses based on location of Palm marker

// Top left (Q)
ipcMain.on("1_KEY_DOWN", async (event,arg) => {keyboard.type(Key.Q)});

// Top middle (W)
ipcMain.on("2_KEY_DOWN", async (event,arg) => {keyboard.type(Key.W)});

// Top right (E)
ipcMain.on("3_KEY_DOWN", async (event,arg) => {keyboard.type(Key.E)});


// Middle left (A)
ipcMain.on("4_KEY_DOWN", async (event,arg) => {keyboard.type(Key.A)});

// Middle middle (S)
ipcMain.on("5_KEY_DOWN", async (event,arg) => {keyboard.type(Key.S)});

// Middle right (D)
ipcMain.on("6_KEY_DOWN", async (event,arg) => {keyboard.type(Key.D)});


// Bottom left (Z)
ipcMain.on("7_KEY_DOWN", async (event,arg) => {keyboard.type(Key.Z)});

// Bottom middle (X)
ipcMain.on("8_KEY_DOWN", async (event,arg) => {keyboard.type(Key.X)});

// Bottom right (C)
ipcMain.on("9_KEY_DOWN", async (event,arg) => {keyboard.type(Key.C)});




// Key presses based on rotation of palm marker

// Starting position rightHanded (K)
ipcMain.on("K_KEY_DOWN", async (event,arg) => {keyboard.type(Key.K)});

// Left (G)
ipcMain.on("G_KEY_DOWN", async (event,arg) => {keyboard.type(Key.G)});

// Middle (B)
ipcMain.on("B_KEY_DOWN", async (event,arg) => {keyboard.type(Key.B)});

// Right (T)
ipcMain.on("T_KEY_DOWN", async (event,arg) => {keyboard.type(Key.T)});

// End (R)
ipcMain.on("R_KEY_DOWN", async (event,arg) => {keyboard.type(Key.R)});

// Starting position leftHanded (J)
ipcMain.on("J_KEY_DOWN", async (event,arg) => {keyboard.type(Key.J)});




// Key presses based on depth of palm marker

// Near (N)
ipcMain.on("N_KEY_DOWN", async (event,arg) => {keyboard.type(Key.N)});

// Middle (M)
ipcMain.on("M_KEY_DOWN", async (event,arg) => {keyboard.type(Key.M)});

// Far (F)
ipcMain.on("F_KEY_DOWN", async (event,arg) => {keyboard.type(Key.F)});
