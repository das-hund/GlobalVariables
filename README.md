
# Introduction

Keeping values on different prefabs in sync is burdensome and error-prone. A way to solve this problem is by not storing a value on the prefab itself but to reference an asset in the project folder. Now, all prefabs that share this value can reference the asset instead, automatically keeping them in sync.

One problem that arises from this, however, is that you don't always know in advance if you want a simple value or a reference to an asset. In a naive implementation, this would lead to many .asset files cluttering your project. That is why the VariableReference can specify if it is local or global. 'Global' meaning that it is using a reference to an asset and 'local' meaning that it is storing the value itself.


Ryan Hipple from Shell Games shares further explains this concept and its benefits in the "Modular Data" portion of his excellent talk at Unite Austion 2017 ([timestamped link](https://youtu.be/WBJCr89I4XQ?t=15m25s)).

## Disclaimer
This repository is still very early in its development and fundamental changes *will* take place. I do not recommend building on this but rather to take take inspiration from some of the concepts.


## Getting Started

The Unity project contains three demos under Assets/Demos which show differnt ways of utilizing this project.

### 1 - Interaction With Unity Types

In this scene, StringVariable assets are used to store translated texts. When the player toggles a different language using the checkboxes at the top, the title and the welcome text are swapped out.

### 2 - Game Events

In this example, a number of cubes is moved around using the GameEvent functionality. Events can be raised during runtime by selecting the event in the project hierarchy and clicking "Raise Event".

### 3 - Functions

In the Pong scene, a ball bounces around in a box. This example illustrates the use of functions to update GlobalVariables.
`Ball_Pos_Max_Calculation` updates `Ball_Pos_Max`, depending on the `Box_Size` variable. `Ball_Pos_Min` and `Ball_Pos_Max` tell the ClampingFunction `Ball_Clamping_Calculation` how far the ball can move on the X, Y and Z coordinate before it hits a wall. This ClampingFunction ensures that the ball never leaves the box. The ClampingFunction raises events whenever it has to apply the clamping logic. The RestrictedBallMover listens to these events and mirrors the ball's direction appropriately.

## Contact

Responsible for the content in this repository is David Kahl.

E-Mail: davidcoding@mail.gmx 