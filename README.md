SDK para .NET Framework 6

Implementacion:

Se requiere agrega la biblioteca dll al proyecto que la implementará 
y se solicita agregar la librería  "FluentValidation" 11.2.2

si se ocupa un archivo csproj se visualiza de esta manera

  <ItemGroup>
    <Reference Include="SdkNet">
      <HintPath>........\SdkNet.dll</HintPath>
    </Reference>
	  <PackageReference Include="FluentValidation" Version="11.2.2" />
  </ItemGroup>


  En la clase que se ocupará se necesita agregar 
...
using SdkNet;
using SdkNet.Models;
...

ejemplo de su ejecución
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
