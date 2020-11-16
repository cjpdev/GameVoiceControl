# GameVoiceControl

Control games using your voice.

**Games supported & configuration**

Functionality has been add to handle use cases specific to "Fornite" and "StarCraft".
Each game requires a specific XML configuration that must be loaded at the start.

```Fornite-extra.gvc``` or ```StarCarft-extra.gvc```

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
The Microsoft voice recognistion is very susceptible to backgournd noise, so headphones should be used. so game sounds do not effect recognition accuracy.
Also any other back talking will effect recognition accuracy. You try use the Microsoft voice recognition training setup to improve how it detects you voice. (This can be fould in the Window control panel).



