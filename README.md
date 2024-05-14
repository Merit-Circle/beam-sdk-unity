# Beam SDK Client for Unity

##
# Dependencies
This package only depends upon:

`"com.unity.nuget.newtonsoft-json": "3.2.1"`

## Installation
To install Beam Client SDK for Unity you have to add following dependencies to your manifest.json:
Option 1 - manifest.json

Open Packages/manifest.json and add these lines:

`"beam.sdk.client": "https://github.com/Merit-Circle/beam-client-unity.git"`

Option 2 - Package Manager Editor UI

Follow these instructions:

https://docs.unity3d.com/Manual/upm-ui-giturl.html

And add this url:

`https://github.com/Merit-Circle/beam-client-unity.git`


## Usage
To use the package, initalize `BeamBrowserClient`:
```csharp
var beamBrowserClient = gameObject.AddComponent<BeamBrowserClient>()
                .SetPublishableBeamApiKey("RwNv...bb5Vt")   // set your Publishable(!) Game API Key
                .SetEnvironment(BeamEnvironment.Testnet);   // pick Environment, defaults to Testnet
```
BeamBrowserClient exposes methods that should be wrapped in Coroutines to not block UI thread.

### Signing a session:
```csharp
StartCoroutine(beamBrowserClient.SignSession("entityIdOfTheUser", result =>
            {
                print($"Got Beam SignSession result: {result.Status} {result.Error}");
            }));
```

### Signing a transaction:
```csharp
StartCoroutine(beamBrowserClient.SignTransaction("entityIdOfTheUser", "tin_xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxxx", result =>
            {
                print($"Got Beam SignTransaction result: {result.Status} {result.Error}");
            }));
```