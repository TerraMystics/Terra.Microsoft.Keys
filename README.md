
<p align="center">
    <a href="https://github.com/TheArchitect123"><img src="CIcon.png" align="center" width=150/></a>
</p>

<p align="center">
A lightweight library to generate keys commonly used for the terra ecosystem. RawKey, Mnemonic & CLI Key
</p>
<br/>

<p align="center">
  <a href="https://github.com/TerraMystics/Terra.Microsoft.Keys/blob/main/LICENSE">
  <img alt="GitHub" src="https://img.shields.io/github/license/terra-money/terra.js">
  </a>

  <a href="https://www.nuget.org/packages/Terra.Microsoft.Keys/1.0.0">
    <img alt="GitHub" src="https://img.shields.io/nuget/v/Terra.Microsoft.Keys">
  </a>
  
  <a href="https://www.nuget.org/packages/Terra.Microsoft.Keys/1.0.0">
    <img alt="GitHub" src="https://img.shields.io/nuget/dt/terra.Microsoft.Keys?color=red">
  </a>
</p>

<p align="center">
  <a href="https://docs.terra.money/develop/feather-js/keys"><strong>Explore the Docs »</strong></a>
  <br />
</p>

<br/>
<p align="center">
 <a href="https://www.buymeacoffee.com/dangenius" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" style="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>
</p>

## Features

- **Written in C#**, with type definitions
- Easy key management for anyone building on the Terra Ecosystem
- Works with all .Net Environments

## Installation & Configuration

Grab the latest version off [Nuget](https://www.nuget.org/packages/Terra.Microsoft.Keys/#readme-body-tab)

```sh
dotnet add package Terra.Microsoft.Keys
```

Please make sure to add the following nuget Packages into your .csproj file
```sh
<ItemGroup>
    <PackageReference Include="Cryptography.ECDSA.Secp256k1" Version="1.1.3" />
    <PackageReference Include="NBitcoin" Version="7.0.23" />
    <PackageReference Include="RandomStringCreator" Version="2.0.0" />
    <PackageReference Include="System.Security.Cryptography.Algorithms" Version="4.3.1" />
    <PackageReference Include="Terra.Microsoft.Extensions" Version="1.0.1" />
    <PackageReference Include="Terra.Microsoft.ProtoBufs" Version="1.0.1" />
  </ItemGroup>
```

## Usage

Terra.Microsoft.Keys can be used for Mobile & Web Developers, or SDK Developers looking to extend the Terra Platform. Supports all Microsoft Technologies from Xamarin, MAUI, ASP & Unity.

### Generating a Mnemonic Key
```cs
MnemonicKey GenerateMnemonic() {

  // Create a key out of a mnemonic string (recovery words)
  string recoveryWords = "notice oak worry limit wrap speak medal online prefer cluster roof addict wrist behave treat actual wasp year salad speed social layer crew genius";
  
  // If creating a random mnemonic, don't pass the recovery words
  return new MnemonicKey(recoveryWords);
}

//ONLY FOR TESTING & DEVELOPMENT PURPOSES: DO NOT EXPOSE PRIVATE KEY, IT COULD RISK EXPOSING THE WALLET FUNDS IF LOST
MnemonicKey GenerateMnemonicWithPrivateKey() {

  string recoveryWords = "notice oak worry limit wrap speak medal online prefer cluster roof addict wrist behave treat actual wasp year salad speed social layer crew genius";
  
  // Create a key out of a mnemonic string (recovery words)
  var mnmonic =  new MnemonicKey(recoveryWords, exposePrivateKey: true);
    
  Console.WriteLine($"PrivateKey: {mnmonic.privateKeyExposed}");
    
  return mnmonic;
}
```

## Terra.Microsoft.Keys For Unity Developers

If you are using Terra.Microsoft.Keys for Unity, please make sure to install the [following asset](https://github.com/TerraMystics/NuGetForUnity) in your project, and follow the installation instructions above

## License

This software is licensed under the MIT license. See [LICENSE](https://github.com/TerraMystics/Terra.Microsoft.Keys/blob/main/LICENSE) for full disclosure.

© 2022 TerraMystics.
