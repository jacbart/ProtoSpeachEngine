# ProtoSpeechEngine

This prototype speech engine for Microsoft windows sends commands to Dictationbridge, to control NVDA. This document acts as a manual on how to build and run this product, and offers instructions on what to say.
This document does not serve as a manual for dictationbridge, and this product does not replace dictationbridges commandset, it simply is a prototype so we can do user testing on how to edit with speech and a screen reader. This work will probably be rewritten in c++ and integrated into core once a stable commandset is designed.

## development:

The prototype speech engine compiles with visual studio 2017.
Make sure System.Speech is available for c#, which it should be if you select desktop development. 
You need to select all the .net core for windows desktop development in order to compile this app.
When you press build, files will be placed in bin/debug or bin/release. Place NVDAControllerClient32.dll and vocab.grxml in that file.
To run the program, simply click the executable, or tell visual studio to run. The program should start up, and present a form.

## Running the program.

Once you get the form, you should see an enable speech recognition button. Click this button to start recognition. Make sure speech recognition on windows is listening, and say "start listening" or press control+windows.
Now, ensure NVDA is running, and make sure you have installed [ Dictationbridge](https://dictationbridge.com).
When you give a command, anywhere in windows, the log updates to reflect what you said, and the command being fired, then NVDA will do something.

## commands

You can say any of the following commands. They are split up into categories.

### Modes

| speech | description |
| ---- | ---- |
| edit mode | Enable edit mode. This allows editing with speech. |
| review mode | Enable review mode. This simply allows reviewing text. |

### commands in edit and review mode

These first commands allow setting the program up to move by various units. When the unit is selected, that unit is spoken.

| speech | description | 
| ---- | ---- | 
| paragraph | sets the program up to move by paragraph.
| Sentence | Sets the program up to move by sentence.
| line | sets the program up to move by line.
| word | sets the program up to move by word.
| Char or Character | sets the program up to move by character.

These next commands control movement by that unit.

| command | description |
| ---- | ---- |
| next | Move forward by the specified unit. The cursor doesn't move in review mode, and currently doesn't in edit mode either, although that might change with later prototypes. |
| prev | Move backward by the specified unit. The cursor doesn't move in review mode, and currently doesn't in edit mode either, although that might change with later prototypes. |
| previous | Move backward by the specified unit. The cursor doesn't move in review mode, and currently doesn't in edit mode either, although that might change with later prototypes. |
| root | Route the cursor to this location (in edit mode only). There is no cursor idea in non edit modes currently, and routing the mouse is already built into dictationbridge. |

