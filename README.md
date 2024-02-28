# FreeSR - Server Software Reimplementation for a Certain Turn-Based Anime Game

**FreeSR** is an open-source server software reimplementation for a certain turn-based anime game, specifically version 1.6.5 developed by xeondev. This project aims to provide a platform for running private servers, enabling players to enjoy the game in a controlled environment with modified or additional features. The open-source version of FreeSR missing some and has empty classes, inviting contributors to actively participate by raising issues or submitting pull requests.

## Introduction

The "FreeSR" project is an initiative to recreate the server-side functionality of a certain turn-based anime game, version 1.6.5, which was originally developed by xeondev.

## Features

- Server software reimplementation for a certain turn-based anime game (version 1.6.5)
- Open-source with permissive licensing
- Extensible architecture allowing for custom modifications and additional features
- Regular updates and maintenance to address issues and improve functionality

## Getting Started

To get started with FreeSR, follow the steps below:

1. Clone the repository to your local machine.
2. Build project with dotnet7.0.
3. Create configuration files for Dispatch and Gateserver. (just rename the .example.json file in your build folder to [ServerName].json and configure it for your machine)
4. Run FreeSR.Dispatch and FreeSR.Gateserver
5. Enjoy the game!

## Connecting

To connect to your server, just compile and run `FreeSR.Tool.Proxy`, it will redirect all HTTP requests to your Dispatch.

## Contributing

We welcome contributions from the community to make FreeSR even better. If you find any issues, have suggestions for improvements, or want to add new features, feel free to create an issue or submit a pull request. Please ensure that your contributions align with the project's goals and adhere to the code of conduct.

## Contact

Feel free to join our [Discord server](https://discord.gg/reversedrooms) for discussions and community interactions.


