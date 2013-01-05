Thank you for purchasing Touch Roll!

Please do email any questions, comments, suggestions, or bugs to me at franktinsley@me.com. I want to make Touch Roll better all the time!

Also don't forget to leave a review on the Asset Store. :)

The how-to video for Touch Roll can be found at http://is.gd/touchrollvideos

------------
Introduction
------------

Touch Roll features an extremely easy-to-use component that grants game objects the ability to be touched, dragged, and even released with natural feeling momentum.

---------------
Getting Started
---------------

Just follow these few simple steps to get everything running:

1. After importing Touch Roll into your Unity project, open the Example scene inside the Example folder. This will automatically add two new tags to your project: "Ground" and "Auto Roll". (You can use these tags in your scene to set which objects will act as the surface you can touch and drag objects on top of and which objects will force the touch to end.)

2. Now close the example scene (or spend some time playing with it if you like) and open your own scene.

3. For Touch Roll to work you just need 3 things in a scene. An object you want to touch, an object for it to move over (i.e. the ground), and an extra object to add a manager component to. So go ahead and add the manager to an extra object in your scene like the main camera. Just select it in the Hierarchy, then click the Component menu and under Touch click Touchy Manager. This will add the Touchy Manager component to the object.

4. Now select the object that is acting as the ground and in the Inspector set its Tag to Ground.

5. Finally, select an object (preferably one placed above the ground) that you want to touch and in the Component menu, under Touch, click Touchy. This will add the Touchy component and if needed a Rigidbody. That's it! Now if you play with your scene on a mobile device by using the Unity Remote app or by building your project you'll be able to touch, drag, and roll your game object! Try adding the Touchy component to more objects. Touch Roll supports full multi-touch so you can grab and move lots of objects all at the same time!

-------------------
Main Asset Features
-------------------

---------------------------
1. Touchy Manager Component

Tracks touches and sends messages to "touchies". Contains 4 variables in the inspector you can customize to fit in with your project:

1. Ground Tag - Default value is "Ground". Game objects with colliders that have this tag will be used as the "ground", meaning that "touchies" will move along the camera-facing surface when touched and dragged. If your "touchies" aren't working make sure they're above an object with the tag set by this variable. If you decide to change this tag make sure you actually add the tag to your project in the tag manager and of course set objects acting as the ground to use it.

2. Auto Roll Tag - Default value is "Auto Roll". Game objects with colliders that have this tag will cause any "touchy" currently being touched and dragged to release and roll when the touch is over them. These a great for situations where you need to set a limit on how close players can drag objects towards a target. Like the Ground Tag, if you change this you'll need to add a tag with the same exact name to the tag manager.

3. Touchy Collision Layer - Default value is 12. This sets the layer that "touchies" will switch to at run time. This layer is actually ignored by touches thus allowing the colliders involved in physics collisions to be different than those that actually have to be touched. Any game objects you want "touchies" to collide with but not block touches (invisible walls for instance) should be set to this layer. Only objects that you want ignored by touches should use this layer so change this value if your project uses it already.

4. Touch Area Layer - Default value is 22. This sets the layer that the "touchies" will set their "touch areas" to. This is the layer that the touchy manager will see. Nothing else should use this layer so change this value if your project uses it already.

-------------------
2. Touchy Component

Component that when added to game objects allows them to be touched, dragged, and released with momentum. Contains 4 variables in the inspector you can customize to accommodate your specific game object:

1. Use Momentum - Default value is true. Uncheck this if you rather not use momentum and want the object to stay exactly where the touch ended.
2. Added Touch Area - Default value is 1. This controls how much larger this touchy's touch area should be than it's normal collider. If you want the touch area to be larger then change this value. The touch area is created at runtime at the start of the scene.

----------------
3. Example Scene

It's a good idea to open the included "Example" scene when you first import Touch Roll so that the two tags "Ground" and "Auto Roll" will automatically be added to your project. Otherwise you will need to add them manually.

The example scene is setup to demonstrate a number of game objects with the touchy component attached.
The main camera has the touchy manager component attached and the floor and walls of the testing facility are all tagged "Ground" so that all the touchies will move around on them when touched. The main camera has been made the child of a game object named "Camera Rotater". It has an iTween script to give the scene's camera a little bit of movement. This demonstrates Touch Roll's independence of camera position and motion. Finally you may notice that one of the children of "Testing Facility" is deactivated. This object is tagged "Auto Roll" and you can use it to experiment with the touchy manager's response to touches blocked by objects with said tag.


---------------
Version Changes
---------------

1.0
- Initial version.

1.5
- Simplified Touchy Component. Now there's no need to fine tune any momentum maximum or multiplier. It just feels right all the time regardless of all the various physics variables of the object. If though you do want to toy with the Momentum Maximum variable again simply change the line in Touchy.cs that looks like this "private float momentumMaximum = 150.0f;" so that "private" is "public". Save and all the touchy components will let you play with the Momentum Maximum again. But be careful of course. Going past 150 will likely lead to objects tossed much too quickly to be stopped by other objects in their path.