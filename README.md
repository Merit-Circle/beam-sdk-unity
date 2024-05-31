# Beam SDK Client for Unity

##
# Dependencies
This package only depends upon:

`"com.unity.nuget.newtonsoft-json": "3.2.1"`

## Installation
To install Beam Client SDK for Unity you have to add following dependencies to your manifest.json:  

Option 1 - manifest.json

Open Packages/manifest.json and add these lines:

```
"com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask#2.5.4",
"beam.sdk.client": "https://github.com/Merit-Circle/beam-client-unity.git"
```

Option 2 - Package Manager Editor UI

Follow these instructions:

https://docs.unity3d.com/Manual/upm-ui-giturl.html

And add these urls:
`https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask#2.5.4`
`https://github.com/Merit-Circle/beam-client-unity.git`


## Usage
To use the package, initalize `BeamBrowserClient`:
```csharp
var beamClient = gameObject.AddComponent<BeamClient>()
                .SetBeamApiGame("your-game-id", "your-publishable-api-key")
                .SetEnvironment(BeamEnvironment.Testnet);
```
BeamClient exposes methods that should be wrapped in Coroutines to not block UI thread.

### Creating a session:
```csharp
StartCoroutine(beamClient.CreateSession("entityIdOfYourUser", result =>
                {
                    // handle the result here
                },
                chainId: 13337, // defaults to 13337
                secondsTimeout: 240 // timeout in seconds for getting a result of Session signing from the browser
            ));
```

### Signing a transaction:
```csharp
StartCoroutine(beamClient.SignOperation("entityIdOfYourUser", "operationIdFromBeamAPI", result =>
                {
                    // handle the result here
                },
                chainId: 13337, // defaults to 13337
                secondsTimeout: 240, // timeout in seconds for getting a response in case browser flow was chosen
                fallBackToBrowser: true // if true, opens a browser for the user to sign the operation if Session was not started
            ));
```