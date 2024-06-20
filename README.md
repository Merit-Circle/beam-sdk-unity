# Beam SDK for Unity

## Installation
To install Beam SDK for Unity you have to add following dependencies to your manifest.json:  

#### Option 1 - manifest.json

Open Packages/manifest.json and add these lines:

```
"beam.sdk.client": "https://github.com/Merit-Circle/beam-sdk-unity.git",
"com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask"
```

#### Option 2 - Package Manager Editor UI

Follow these instructions:

https://docs.unity3d.com/Manual/upm-ui-giturl.html

And add these urls:  
```
https://github.com/Merit-Circle/beam-sdk-unity.git
https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask
```


## Usage
To use the package, initialize `BeamClient`:
```csharp
var beamClient = gameObject.AddComponent<BeamClient>()
                .SetBeamApiKey("your-publishable-api-key") // set your Publishable(!) API key
                .SetEnvironment(BeamEnvironment.Testnet) // defaults to Testnet
                .SetDebugLogging(true) // optional, defaults to false
                .SetStorage(yourCustomStorageImplementation); // optional, defaults to PlayerPrefs storage;
```

### Checking for an active session

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
                chainId: 13337 // optional chainId, defaults to 13337
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
                chainId: 13337, // optional chainId, defaults to 13337
                secondsTimeout: 240 // timeout in seconds for getting a result of Session signing from the browser
            ));
```

### Signing an operation:
Once you get an operationId from Beam API, that requires signing by the user, you can call BeamClient.SignOperation() to sign and execute given operation:
```csharp
StartCoroutine(beamClient.SignOperation(
                "entityIdOfYourUser",
                operationId,    // operationId from Beam API
                actionResult =>
                {
                    if (actionResult.Status == BeamResultType.Success)
                    {
                        // you can now check for actual Status of the signing f.e.:
                        var isSigned = actionResult.Result == BeamOperationStatus.Signed;
                        var isExecuted = actionResult.Result == BeamOperationStatus.Executed;
                        var isRejected = actionResult.Result == BeamOperationStatus.Rejected;
                        // (...)
                    }
                },
                chainId: 13337, // optional chainId, defaults to 13337
                fallbackToBrowser: true, // if true, will automatically open browser for the user to sign the operation, if there is no valid session
                secondsTimeout: 240 // timeout in seconds for getting a result of message signing from the browser, used if there was no valid session
            ));
```

### Examples
You can find an example implementation using this demo in [beam-sdk-unity-example](https://github.com/Merit-Circle/beam-sdk-unity-example/tree/main)
