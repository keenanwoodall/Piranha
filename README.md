# Piranha
A simple tool to make rigidbodies swarm a mesh in Unity.

Works on Mesh Filters and Skinned Mesh Renderers.

![1](https://i.imgur.com/5wLtUS5.gif)

## Setup
1. Add the `PirahnaTarget` component to a game object with a Mesh Filter or Skinned Mesh Renderer. 
2. Drag any rigidbodies you want to swarm the target into the `Pirahnas` list.
3. Hit play!

## Options

- Increase `Force` to change how attracted the rigidbodies are to the target.
![2](https://i.imgur.com/xOgaggq.gif)
- Change the `Attraction Mode` to `Distance` to prevent rigidbodies from swarming the target unless they are within the `Attraction Distance`.
![3](https://i.imgur.com/Ow8BHMi.gif)
