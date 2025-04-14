This repository is for implemented code examples from https://www.gabrielgambetta.com/computer-graphics-from-scratch/ using C#. Everything here is for learning purposes, thats why I am trying to have as little dependencies as possible. Because of that I am implemeting my own Point3D, Vector3D types, and everything else which may become needed at some point later.

Main types:

ICanvas - type that represents a Canvas object. As this is an interface from the C# Library project, platform-specific implementation is required for each of the platforms. In this repo for now only an AppKit app is used for testing.

Global - public static class for all the logic.

In order to add a platform, create a platform-specific project, create a type which implements ICanvas, and set it via "Global.SetPlatformCanvas" on the initizlization of the project-specific code.
