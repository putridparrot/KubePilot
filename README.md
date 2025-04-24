THIS IS VERY MUCH UNDER DEVELOPMENT AND IS A BIT OF A MESS WHILST I TRY THINGS OUT.

_I'm developing this in a public repository so, until the notice above is removed one can assume this is still very much subject to lots of changes and I'm using this to learn more about Kubernetes, so some areas may need more attention to be of use._

[![KubePilot Build](https://github.com/putridparrot/KubePilot/actions/workflows/dotnet-desktop.yml/badge.svg?branch=main)](https://github.com/putridparrot/KubePilot/actions/workflows/dotnet-desktop.yml)

# KubePilot

A Blazor based Kubernetes UI along the lines of kubectl.

# Operating Systems

The application is built on Blazor Hybrid, so runs on Windows, Mac Catalyst and Android. 

I've not yet got it running on Mac Catalyst as I seem to have an XCode/.NET compatibility issues and I've not tested on Android. I'll amend this README if I confirm it working on these OS's, but my primary focus if the Windows client.

# Code Generation

As the moment, whilst I play with the AKS API's and KubernetesClient, the source code for the pages and NavBar are generated from the CodeGenerator project, so any changes to the general way things work should be changed in the YAML file and any relevent changes too the handlerbars template and/or actual code, should be changed in the YAML first, run the code generator and then copy the files to the relevent folder. 

It's done this way so it's easy to make changes across all generaic list pages and is not automatically overwriting the originals to allow the developer to copy across the bits they might wish to test.

Obviously the intention is to move generalised code into shared libraries, but for now this has been a successful way of making changes and testing.
