# betfair-aping

[![russgray MyGet Build Status](https://www.myget.org/BuildSource/Badge/russgray?identifier=c4a0a48e-9cc9-4a71-bb52-9a8565e5b675)](https://www.myget.org/)

Client library for Betfair's API-NG (next gen).

Official API documentation: https://developer.betfair.com/

# Overview
This is a simple client for the new Betfair API, known as API-NG. THe legacy SOAP API (API6) was deprecated at the end of 2014 so API-NG is now required for programmatic access to the exchange.

This library uses the JSON REST access method rather than JSON RPC. It also provides a simple command-line application that can be useful for exploring the sometimes complicated requests and responses.
# Details
# Usage
## Installation
Install via nuget.
## Authentication
Authentication is significantly more complex than in API6. Interactive login requires an API key and a self-signed certificate in addition to your account username and password. Full instructions for creating a certificate and generating a key can be found in the official docs.

At this time, the project does not yet support OAUTH login.
## Basic Usage
## Command Line
The command-line app allows you to call any operation interactively.
### Windows Credential Manager
# Contributing
