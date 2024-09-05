# Cedeira.Essentials.NET <!-- omit in toc -->

<div align="center">
  <a href="https://github.com/cedeirasf/Cedeira.Essentials.NET/blob/main/LICENSE">
    <img alt="License: MIT" src="https://img.shields.io/badge/License-MIT-yellow.svg">
  </a>

  <a href="https://github.com/cedeirasf/Cedeira.Essentials.NET/blob/main/CODE_OF_CONDUCT.md">
    <img alt="Contributor covenant: 2.1" src="https://img.shields.io/badge/Contributor%20Covenant-2.1-4baaaa.svg">
  </a>

  <a href="https://semver.org/">
    <img alt="Semantic Versioning: 2.0.0" src="https://img.shields.io/badge/Semantic--Versioning-2.0.0-a05f79?logo=semantic-release&logoColor=f97ff0">
  </a>

  <br />

  <a href="https://github.com/cedeirasf/Cedeira.Essentials.NET/issues/new/choose">Report Bug</a>
  ·
  <a href="https://github.com/cedeirasf/Cedeira.Essentials.NET/issues/new/choose">Request Feature</a>
</div>

&nbsp;

# Content <!-- omit in toc -->
- [:wave: Introducing Library](#wave-introducing-library)
- [Install](#install)
	- [Via NuGet Package Manager](#via-nuget-package-manager)
	- [Via .NET CLI](#via-net-cli)
- [Documentation](#documentation)
- [:rocket: Upcomming Features](#rocket-upcoming-features)
- [:handshake: Contributing to `Cedeira Essentials NET`](#handshake-contributing-to-Cedeira-Essentials)


&nbsp;
# :wave: Introducing Library
`Cedeira.Essentials.NET` is a library for .NET with common and reusable functionalities.

For the moment, this project is in early development phase, so that all version are inestables.

You can take a look to the [upcoming features](#rocket-upcomming-features) to know more about `Cedeira.Essentials.NET` future.

Hey! don't be discouraged, you can help me to carry out this project in many ways, contributing with new features, reporting bugs, sharing in your social networks or supporting with a :star:

Please, look at [Contributing to `Cedeira Essentials NET`](#handshake-contributing-to-Cedeira-Essentials) to choose the way to collaborate that with you feel better.

&nbsp;
# Install

## Via NuGet Package Manager

To install the latest version of Cedeira.Essentials.NET via the NuGet Package Manager, run the following command in the NuGet Package Manager Console:

```
Install-Package Cedeira.Essentials.NET
```

## Via .NET CLI

To install the latest version using the .NET CLI, run this command:

```
dotnet add package Cedeira.Essentials.NET
```

Or, to install a specific version:

```
dotnet add package Cedeira.Essentials.NET --version X.X.X
```

&nbsp;
# Documentation

To generate static documentation using docfx locally, follow these steps:

```
cd "path to the repository"
cd ./docs
docfx docfx.json
```

If you want to run a server to host the documentation, use the --serve flag with the docfx command:

```
cd "path to the repository"
cd ./docs
docfx docfx.json --serve
```

The documentation will be generated as a website in the docs/_site directory. The process is configured to copy the docs/api folder, generated by docfx when mapping the projects, into the site along with the artifacts inside the docs/ folder that allow for the inclusion of an index and a welcome page.
![NOTE] Running docfx ./docs/docfx.json --serve from the root of the repository yields the same result locally.

To generate the image:

```
docker build -t <image name>:latest -f ./docs/Dockerfile .
```

&nbsp;
# :rocket: Upcoming Features

Upcoming..

&nbsp;
# :handshake: Contributing to `Cedeira Essentials`

Any kind of positive contribution is welcome! Please help us to grow by contributing to the project.

If you wish to contribute, you can work on any [issue](https://github.com/cedeirasf/Cedeira.Essentials.NET/issues/new/choose) or create one on your own. After adding your code, please send us a Pull Request.

> Please read [`CONTRIBUTING`](CONTRIBUTING.md) for details on our [`CODE OF CONDUCT`](CODE_OF_CONDUCT.md), and the process for submitting pull requests to us.

&nbsp;
# :pray: Support

We all need support and motivation. `Cedeira.Essentials.NET` is not an exception. Please give this project a :star: start to encourage and show that you liked it. Don't forget to leave a :star: star before you move away.
