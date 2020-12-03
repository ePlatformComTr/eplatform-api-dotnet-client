# ePlatform-api-dotnet-client

## Turkcell e-Şirket e-Belge uygulamaları için .Net Client


### Nuget Paketleri

* [ePlatform.Api.Core](https://www.nuget.org/packages/ePlatform.Api.Core/)
    * Authentication ve diğer temel işlemler için gerekli olan paket
* [ePlatform.Api.eBelge.Ticket](https://www.nuget.org/packages/ePlatform.Api.eBelge.Ticket/)
    * e-Bilet entegrasyonu için gerekli olan paket
* [ePlatform.Api.eBelge.Invoice](https://www.nuget.org/packages/ePlatform.Api.eBelge.Invoice/)
    * e-Fatura/e-Arşiv entegrasyonu için gerekli olan paket

Paket sürümleri netstandard2.0 ve .Net 4.6.1 üstünü desteklemektedir.

### Kullanım

.NetCore projesinizde, startup.cs dosyasında, ConfigureServices kısmına, ilgili Clientlar için gerekli kısımları eklemelisiniz.

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDistributedMemoryCache(); //Token cache için gerekli
        services.AddePlatformTicketClients(Configuration); //e-Bilet clientlari için gereli
        services.AddePlatformInvoiceClients(Configuration); //e-Fatura/e-Arşiv clientlari için gereli
    }

Uygulamanızın AppSettings.json dosyasında, Client için gerekli ilgili model yer almalıdır.

    "ePlatformClientOptions": {
        "AuthServiceUrl": "https://coretest.isim360.com",
        "TicketServiceUrl": "https://ebiletservicetest.isim360.com",  // e-Bilet için gerekli olan url
        "InvoiceServiceUrl": "https://efaturaservicetest.isim360.com", // e-Fatura/e-Arşiv için gereli olan url
        "Auth": {
        //Test için aşağıdaki kullanıcı bilgilerini kullanabilir yada özel kullanıcı talep edebilirsiniz.
        "Username": "serviceuser01@isim360.com",
        "Password": "ePlatform123+",
        "ClientId": "serviceApi"
        }
    }



Sample projesinden yer alan Controller'lar içerisinde görebileceğiniz şekilde, ilgili servisleri inject edip kullanmanız gerekmektedir.

## Teknik dökümantasyon
[https://developer.turkcellesirket.com/](https://developer.turkcellesirket.com/)

## Soru ve yorumlarınız için
    entegrasyon@eplatform.com.tr