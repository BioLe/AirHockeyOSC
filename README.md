# AirHockeyOSC

An air hockey 2D game made in Unity, where each player controls a striker with their mobile devices. 
There's also the possiblity to save at any time the game information to a CSV file, which exports to CSV's one for each player in the session.

Based on Thomas Fredericks - UnityOSC.

Code-object association:

2 Emptys (OSC Receivers)            - OSC.cs, with a port for each player
2 Images (Players)                  - oscTeste.cs
2 Box Colliders (Goals)             - baliza.cs
1 Image (Puck)                      - bola.cs

There are the minimum requirements for the project to work. 

----------------------------------------------------------------------------------------------------

In the DataAnalysis folder, there's data captured from several games and a Jupyter Notebook with most of the data.

Header:

Time.time,PlayerName,accelerometerRawX,accelerometerRawY,playerPosX,playerPosY,playerVelocity,puckPosX,puckPosY,puckVelocity,scoreBlue,scoreRed

----------------------------------------------------------------------------------------------------

Troubleshooting: If using W10 and neither of the players is moving try turning off the 'Public Network' firewall.
