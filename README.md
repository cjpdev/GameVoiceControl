# GameVoiceControl

Control games using your voice.

  As of 03/02/2020 this software support Fornite with some support for Starcraft. 
  This software was mainly created as a bit of fun while in Covid-19 lockdown. 
  Thus, why it is now open source. 
  However, as I don't really play these games much I will not be maintaining this code.
  
**Is it safe to use?**

It is safe to use this software with the above games, as this is not a hack it does not interfere with
any part of these games. It only onverts voice command into key/mouse inputs and behaviors that
are relevent(useful) in the context of the running games/software. It interacts directly with Windows OS
not the game.

  NO WITHOUT WARRANTY OF ANY KIND, and I ACCEPT NO LIABLITY OF ANY KIND.
  **So use a your on risk**, 

Lower level windows API is used to handle keyboard, DirectX and mouse input. Voice control is
handled by the Microsoft voice recognition API.

https://support.microsoft.com/en-us/windows/use-voice-recognition-in-windows-10-83ff75bd-63eb-0b6c-18d4-6fae94050571

**Games supported & configuration**

Functionality has been add to handle use cases specific to "Fornite" and "StarCraft".
Each game requires a specific XML configuration that must be loaded at the start.

```Fornite-extra.gvc``` or ```StarCarft-extra.gvc```

https://github.com/cjpdev/GameVoiceControl/blob/master/GameVoiceControl/Configuration/Fornite-extra.gvc

It is possible modifiying these XNL file to create new configuration/actions.

**Application configuration: App.config**

MouseClickSpeed
KeyPressSpeed
AutoJumpSpeed
AutoDodgeSpeed
WordConfidenceAdjust: raise or lower the word confidence detection of all setting in the actual game configuration file.
AutoFireSpeed
AutoBuildSpeed
TopMost
WordConfidenceAdjustMin
WordConfidenceAdjustMax


***Voice recognition***

Headphones and mic are required, but application will not start if no mic is detected.
The Microsoft voice recognistion seems to be very susceptible to backgournd noise, so headphones should be used to reduce ingame sounds effecting recognition accuracy.
Also any other background talking will effect recognition accuracy. You can try use the Microsoft voice recognition training setup to improve detection accuracy. (This can be found in the Window control panel).



