# GameVoiceControl

Control games using your voice.

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



