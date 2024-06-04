# Beam SDK Client for Unity

## Installation
To install Beam Client SDK for Unity you have to add following dependencies to your manifest.json:  

#### Option 1 - manifest.json

Open Packages/manifest.json and add this line:

```
"beam.sdk.client": "https://github.com/Merit-Circle/beam-client-unity.git"
```

#### Option 2 - Package Manager Editor UI

Follow these instructions:

https://docs.unity3d.com/Manual/upm-ui-giturl.html

And add this url:  
```
https://github.com/Merit-Circle/beam-client-unity.git
```


## Usage
To use the package, initialize `BeamClient`:
```csharp
var beamClient = gameObject.AddComponent<BeamClient>()
                .SetBeamApiGame("your-game-id", "your-publishable-api-key")
                .SetEnvironment(BeamEnvironment.Testnet) // defaults to Testnet
                .SetDebugLogging(true) // optional, defaults to false
                .SetStorage(yourCustomStorageImplementation); // optional, defaults to PlayerPrefs storage;
```
BeamClient exposes methods that should be wrapped in Coroutines to not block UI thread.

### Checking for active session

```csharp
StartCoroutine(beamClient.GetActiveSession(
                "entityIdOfYourUser",
                actionResult =>
                {
                    if (actionResult.Status == BeamResultType.Success)
                    {
                        var session = actionResult.Result;
                        // you have an active session that can sign operations
                    }
                    else
                    {
                        // you need to create a session using CreateSession(), or User will sign operations using browser
                    }
                },
                chainId: 1337 // optional chainId, defaults to 13337
            ));
```

### Creating a session:
```csharp
StartCoroutine(beamClient.CreateSession(
                "entityIdOfYourUser",
                actionResult =>
                {
                    if (actionResult.Status == BeamResultType.Success)
                    {
                        var session = actionResult.Result;
                        // you now have an active session that can sign operations
                    }
                },
                chainId: 1337, // optional chainId, defaults to 13337
                secondsTimeout: 240 // timeout in seconds for getting a result of Session signing from the browser
            ));
```

### Signing an operation:
Once you get an operationId from Beam API, that requires signing by the user, you can call BeamClient.SignOperation() to sign and execute given operation:
```csharp
StartCoroutine(m_BeamClient.SignOperation(
                "entityIdOfYourUser",
                operationId,    // operationId from Beam API
                actionResult =>
                {
                    if (actionResult.Status == BeamResultType.Success)
                    {
                        // you can now check for actual Status of the signing f.e.:
                        var isApproved = actionResult.Result == BeamOperationStatus.Executed;
                        var isRejected = actionResult.Result == BeamOperationStatus.Rejected;
                        // (...)
                    }
                },
                chainId: 1337, // optional chainId, defaults to 13337
                fallbackToBrowser: true, // if true, will automatically open browser for the user to sign the operation, if there is no valid session
                secondsTimeout: 240 // timeout in seconds for getting a result of message signing from the browser, used if there was no valid session
            ));
```