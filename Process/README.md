https://drive.google.com/drive/folders/1OxZEvi-7nbnCbyYGuNcMXI67CbYC7kKu?usp=sharing

^ Link to our unstructured working documents, we will be posting pictures, updates, and developments in a new document each week. 

This README will be a more structured overview of future goals and progress made throughout each week

# 2/23/2021 Update

- Completed all content for dialogue system. All scenes are created and linked with GameManager.
- Player sprites now visually move left/right in combat
- Enemy AI chooses moves randomly from list of options
- If Enemy AI already chooses a duplicate move, choose a different move

- For Hunter next week: Make Barkeeper dialogue, make Jester dialogue, starting to work on game sound
- For Perry next week: Health bar + damage system (Make combat winnable), make Brute sprite

- Look into "Darkest Dungeon" as a good example of what the Combat Scene will end up looking like
- Change dialogue scene text so it is more legible
- Replace Input.GetKeyDown with Input.GetKeyPress

# 2/16/2021 Update

- Dialogue system works and has been integrated with a Game Manager Script! Players can now change from a tutorial scene, to castle dialogue, to combat scene
- Combat updates: Movement capabilities, divided characters so they can be attacked individually, 10 movement squares(5 for player, 5 for enemy), Combat demo including enemies
- Controls will flip appropriately for right/left handed people
- Standardized print-out marker PDF

# 2/9/2021 Update

- Beholder software now has a configuration option (Allows the player to flip the camera so the image is mirrored, creating better visual feedback for hand gestures)
- There are still latency issues affecting the response time within the game, these need to be fixed to create a better gameplay experience
- Combat demo now has the ability to change rounds once a player has locked in their choice for both of their attacks
- Created a spreadsheet to plan out the order/type of scenes in the game (Outlines the basic structure that will be implemented into the GameManagerScript)
- Implemented Cradle into Unity to iterate through the Twine story
- Decided that Cradle's customization features are too limited for what we were planning 
- Shifted focus towards trying Ink to implement the story (Hopefully will allow us to trigger events in the game when a certain point in the story is reached)
- If Ink fails, we will shift to hard-coding the entire story (Least ideal solution, but it gives us the most control over variables and event triggers)
- Enemy combat AI wasn't worked on this week, the main focus was fixing errors in Beholder-detection and dialogue parsing
- Plans for next week include: Enemy combat AI, GameManagerScript, Ink+Unity Dialogue interaction, fixing latency issues with Beholder marker-detection

# 2/2/2021 Update

- Completed combat demo interaction in Unity
- Players can now perform specific hand gestures to select a certain attack/action for their character to perform
- Players can now choose both attacks to be made during one round of combat
- Hand gestures are currently not working due to an error with the Electron detection app (Keyboard inputs can simulate hand gesture movement in the meantime)
- Also updated sprites this week, implementing standard size for all sprites, and created standalone files for the walking + idle animations of the main characters
- Cradle has been prototyped with a shorter Twine story, but the links need to be arranged in a horizontal layout. Right now, they are stuck to a vertical layout
- Reached out to the creator or Cradle for implementing horizontal layout and text parsing questions
- To Do Next Week: Enemy combatant AI, establishing a random selection system so the AI can choose its moves to make against the player
- For playtesting: Two players can manually choose moves to verse each other in combat, while a third moderator records player and enemy health, damage dealt, time taken, and # of rounds to complete the battle
- Have links react to button presses rather than clicks
- Solve horizontal layout issue
- Ensure that the body text always fits on the UI

# 1/26/2021 Update

- Using the same parsing technique that we used in last semester's feasibility tests, we found lots of complications with keeping track of variables and triggering external   events/scenes. So, we are instead going to import our Twine story to Unity using a Plugin called Cradle
- Designed character moves in combat (how much damage they do, what gestures players will do to activate them, etc)
- Completed gesture/hand mapping. Now, users can see a graphic of their hand reflected on Unity that depicts their hand's current postion (on a 9x9 grid), rotation, and distance from the camera
- Unity script can now recognize a chain of gestures as a spell. Must apply same process to 8 other attacks for the combat scene. 
- Paper prototyped a combat scene where we went back and forth making attacks on each other while keeping track of health

# 1/19/2021 Update

Goals

30%
1. Standardize marker sizes and setup, design printout page
2. Code gestures to cause unity inputs (simulate combat gestures and make sure they work)
3. Focus on combat scene itself - implement 2 turn system and randomized enemy attacks. No movement, primitive graphics. Maybe include health bar
4. Draft dialogue interface
5. Test combat

50%
1. Link external events to dialogue and pair character graphics with each node
2. Design basic sound effects
3. Animate characters and attacks
4. Finish combat system with basic animations/feedback
5. Figure out webcam setup with Unity
6. Test combat + story

90% 
1. Full story is playable
2. Refine UI
3. Complete cut scene animation
4. Adding particle effects
5. Implement background environments
6. Test visuals and environment

100%
1. Finishing touches

Documentation
1. Complete Website
2. Refine documentation
