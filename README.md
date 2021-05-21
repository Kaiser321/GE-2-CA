# Games Engine 2 Assignment: Starwars Space Battle

Name: Kai O'Leary
Student Number: C15354951
Course Code: TU858/4

## Story
> "A long time ago in a galaxy far, far away..."

The crew onboard of the Millennium Falcon was making way to the ice planet Hoth at the Anoat sector. However they were unaware that they were followed by an small squadron of elite imperial TIE fighters and was then ambushed.  The falcon crew performed quick evasive maneuvers in order to shake off the pursuers. Responding to the stress signal a squad of rebel X-Wings has arrived to the battle to fend off the imperial starfighters. With the combined forces of rebels they successfully won the skirmish.

# Video
[![YouTube](http://img.youtube.com/vi/ksTK8cmIPF0/0.jpg)](https://youtu.be/ksTK8cmIPF0)



## Elements
### Boids
All the ships movements are implemented using several steering behaviors. Behaviors such as Pursuing, Fleeing and Offset Pursue was used. Path Finding using A Star algorithm and Obstacle Avoidance was used for traversing through the asteroid field. Each fighter squad consists of 3 ships with 1 leader. Offset Pursue was used for the rest of the ships in the squad to follow the leader and move in formation.

### Steering Behaviors
Steering Behaviors used for the ships are:
* Seek
* Pursue
* Flee
* Follow Path
* Obstacle Avoidance
* Path Finding
* Noise Wander

### Custom Scripts
These following scripts are custom made scripts for this project:
* ShipStates - States for the ship behaviors
* SceneStates - States for playing out each scene
* ShipController - Controller for X-Wing and TIE-Fighter ships
* FalconController - Controller for the Falcon
* SceneController - Controller to play out each scene and moving cameras
* BulletController - Controller for laser bullets and detect on hit
* Spawner - Spawns squads of X-Wings and TIE-Fighters at an given location


### Finite State Machine
A state machine was implemented the behaviors of the ships. Each ship has 6 states.
* FollowingLeader - Ship follows squad leader using offset pursue.
* TraverseAsteroidCluster - Enable path finding.
* FindFalcon - Ship seeks Falcon's location while constantly seek for enemies close by. If in range of enemy, enable `Pursuing` state and attack them. The target that's been pursured will enable `Fleeing` state.
* Pursuing - Ship pursuing and target and shoots while in range.
* Fleeing - Ship flees and target if getting pursued. If too far from falcon it will enable `FindFalcon` state.

Each ship has a controller script `ShipController` attached with contains it's health, rate of fire and the range of attack.

A state machine was also used to play the scenes. Each scene is a state and when an event triggers in a scene the next scene is played. This is contained in the `SceneController` script.

### Trail Renderer
Trail renderer was used to add effects to the bullets to recreate the laser effect from the movie. It was also added to the Falcon and X-Wings for additional flying effects.

## Storyboard
### Scene 1
The Falcon flies towards the camera.
![alt text](https://github.com/Kaiser321/GE-2-CA/blob/main/Storyboard/1.png)
### Scene 2
Shot of the falcon flies away.
![alt text](https://github.com/Kaiser321/GE-2-CA/blob/main/Storyboard/2.png)
### Scene 3
Three TIE-Fighters appears and flies towards the falcon with a tracking shot.
![alt text](https://github.com/Kaiser321/GE-2-CA/blob/main/Storyboard/3.png)
### Scene 4
The Falcon flees from the TIE-Fighters while getting shot at.
![alt text](https://github.com/Kaiser321/GE-2-CA/blob/main/Storyboard/4.png)
### Scene 5
The Falcon flies towards an asteroid field while evading attack. 
![alt text](https://github.com/Kaiser321/GE-2-CA/blob/main/Storyboard/5.png)
### Scene 6
The Falcon traverses through the asteroid field with multiple camera angles.
![alt text](https://github.com/Kaiser321/GE-2-CA/blob/main/Storyboard/6.png)
### Scene 7
A squad of X-Wings appear and flies towards to Falcon.
![alt text](https://github.com/Kaiser321/GE-2-CA/blob/main/Storyboard/7.png)
### Scene 8
More X-Wings and TIE-Fighters appear and a big battle happens.
![alt text](https://github.com/Kaiser321/GE-2-CA/blob/main/Storyboard/8.png)
### Scene 9
The Rebels appears victorious from the skirmish and flies away. 
![alt text](https://github.com/Kaiser321/GE-2-CA/blob/main/Storyboard/9.png)


## Inspirations
[![YouTube](http://img.youtube.com/vi/mSvPxNopdHs/0.jpg)](https://youtu.be/mSvPxNopdHs)
[![YouTube](http://img.youtube.com/vi/8sarFZJl3h0/0.jpg)](https://youtu.be/8sarFZJl3h0)
[![YouTube](http://img.youtube.com/vi/c8deRYotdng/0.jpg)](https://youtu.be/c8deRYotdng)

## Resources
"Millennium Falcon" (https://skfb.ly/6qLJP) by Stym is licensed under Creative Commons Attribution-ShareAlike (http://creativecommons.org/licenses/by-sa/4.0/).

"X-Wing - by Greta Pastorino" (https://skfb.ly/QFnr) by AIV is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).

"TIE-fighter from STAR WARS" (https://skfb.ly/6QZPZ) by Ilyas.King is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).

"asteroid" (https://skfb.ly/COwN) by admone is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).

All sound effect from "STAR WARS SOUND EFFECTS" from soundboard.com (https://www.soundboard.com/sb/starwarsfx).