# SDK para VB Framework 6

##Implementacion

Se requiere agrega la biblioteca dll al proyecto que la implementará 
y se solicita agregar la librería  "FluentValidation" 11.2.2

Si se ocupa un archivo csproj se visualiza de esta manera

```xml
<ItemGroup>
    <Reference Include="SdkNet">
      <HintPath>........\SdkNet.dll</HintPath>
    </Reference>
  <PackageReference Include="FluentValidation" Version="11.2.2" />
  </ItemGroup>
```
  o en su defecto si ocupa la librería que se agrega
```xml
<ItemGroup>
    <Reference Include="FluentValidation">
      <HintPath>..\FluentValidation.dll</HintPath>
    </Reference>
    <Reference Include="SdkVisualBasic">
      <HintPath>..\SdkVisualBasic.dll</HintPath>
    </Reference>
  </ItemGroup>
```

 En la clase que se ocupará se necesita agregar
```cs
...
using SdkNet;
using SdkNet.Models;
...
```
Ejemplo de su ejecución
```cs
            WPPClient client = new WPPClient("https://sandboxpo.mit.com.mx/gen", "SNDBX123",
    "5DCC67393750523CD165F17E1EFADD21");
            UrlData urlData = new UrlData();
            urlData.reference = "reference001";
            urlData.amount = 10.0;
            urlData.moneda = UrlData.MonedaType.MXN;
            urlData.promotions = "C";
            urlData.stEmail = 1;
            urlData.expirationDate = DateTime.Now;
            BusinessData businessData = new BusinessData();
            businessData.idBranch = "01SNBXBRNCH";
            businessData.idCompany = "SNBX";
            businessData.user = "SNBXUSR0123";
            businessData.pwd = "SECRETO";
            PaymentData paymentData = new PaymentData();
            paymentData.business = businessData;
            paymentData.url = urlData;
            var responseURL = client.GetUrlPayment(paymentData);
```

