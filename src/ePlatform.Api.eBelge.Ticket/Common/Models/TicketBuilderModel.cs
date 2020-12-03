using System;
using System.Collections.Generic;
using ePlatform.Api.eBelge.Ticket.Common.Enums;

namespace ePlatform.Api.eBelge.Ticket.Common.Models
{
    public class TicketBuilderModel
    {
        public Guid Ettn { get; set; }

        /// <summary>
        /// 0 Taslak, 20 Kaydet ve Gönder
        /// </summary>
        public TicketStatus Status { get; set; }

        /// <summary>
        /// Bilet Numarası
        /// </summary>
        public string TicketNumber { get; set; }

        /// <summary>
        /// Ön ek.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Referans numarası.
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// Bilet tipi.
        /// </summary>
        public TicketType TicketType { get; set; }

        /// <summary>
        /// Bilet tarihi.
        /// </summary>
        public DateTime TicketDate { get; set; }

        /// <summary>
        /// Belge tipi.
        /// </summary>
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// Para birimi.
        /// </summary>
        public Currency CurrencyCode { get; set; }

        /// <summary>
        /// Para birimi tl değilse kur bilgisi.
        /// </summary>
        public decimal? ExchangeRate { get; set; }

        /// <summary>
        /// Müşteri VKN/TCKN.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Müşteri ad.
        /// </summary>
        public string CustomerFirstName { get; set; }

        /// <summary>
        /// Müşteri soyad.
        /// </summary>
        public string CustomerLastName { get; set; }

        /// <summary>
        /// Müşteri bulvar/cadde/sokak.
        /// </summary>
        public string CustomerStreet { get; set; }

        /// <summary>
        /// Müşteri bina adı.
        /// </summary>
        public string CustomerBuildingName { get; set; }

        /// <summary>
        /// Müşteri bina no.
        /// </summary>
        public string CustomerBuildingNo { get; set; }

        /// <summary>
        /// Müşteri kapı no.
        /// </summary>
        public string CustomerDoorNo { get; set; }

        /// <summary>
        /// Müşteri kasaba/köy.
        /// </summary>
        public string CustomerTown { get; set; }

        /// <summary>
        /// Müşteri mahalle/semt/ilçe.
        /// </summary>
        public string CustomerDistrict { get; set; }

        /// <summary>
        /// Müşteri il.
        /// </summary>
        public string CustomerCity { get; set; }

        /// <summary>
        /// Müşteri posta kodu.
        /// </summary>
        public string CustomerPostCode { get; set; }

        /// <summary>
        /// Müşteri ülke.
        /// </summary>
        public string CustomerCountry { get; set; }

        /// <summary>
        /// Müşteri telefon.
        /// </summary>
        public string CustomerTelephone { get; set; }

        /// <summary>
        /// Müşteri email.
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Müşteriye e-mail gönderilsin mi.
        /// </summary>
        public bool IsEmailSend { get; set; }

        /// <summary>
        /// Mütşeri vergi dairesi.
        /// </summary>
        public string CustomerTaxCenter { get; set; }

        /// <summary>
        /// Toplam Tutar
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Toplam Vergi
        /// </summary>
        public decimal TotalVat { get; set; }

        /// <summary>
        /// Ödeme şekli.
        /// </summary>
        public PaymentType PaymentType { get; set; }

        /// <summary>
        /// Ödeme şekli açıklama.
        /// </summary>
        public string PaymentDescription { get; set; }

        /// <summary>
        /// Ödenecek Toplam Tutar
        /// </summary>
        public decimal PayableAmount { get; set; }

        /// <summary>
        /// Taşıt plakası.
        /// </summary>
        public string VehiclePlate { get; set; }

        /// <summary>
        /// Sefer zamanı.
        /// </summary>
        public DateTime ExpeditionTime { get; set; }

        /// <summary>
        /// Sefer numarası.
        /// </summary>
        public string ExpeditionNumber { get; set; }

        /// <summary>
        /// Hareket zamanı.
        /// </summary>
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Hareket yeri.
        /// </summary>
        public string DepartureLocation { get; set; }

        /// <summary>
        /// Koltuk numarası.
        /// </summary>
        public string SeatNumber { get; set; }

        /// <summary>
        /// Aracı işleten VKN/TCKN.
        /// </summary>
        public string VehicleOperatingVknTckn { get; set; }

        public string VehicleOperatingTitle { get; set; }
        public string VehicleOperatingCountry { get; set; }
        public string VehicleOperatingCity { get; set; }
        public string VehicleOperatingDistrict { get; set; }
        public string VehicleOperatingTown { get; set; }
        public string VehicleOperatingStreet { get; set; }
        public string VehicleOperatingBuildingName { get; set; }
        public string VehicleOperatingBuildingNo { get; set; }
        public string VehicleOperatingDoorNo { get; set; }
        public string VehicleOperatingMersisNo { get; set; }
        public string VehicleOperatingRegisterNo { get; set; }
        public string VehicleOperatingPostCode { get; set; }
        public string VehicleOperatingTelephone { get; set; }
        public string VehicleOperatingEmail { get; set; }
        public string VehicleOperatingTaxCenter { get; set; }

        /// <summary>
        /// Komisyon tutarı.
        /// </summary>
        public decimal CommissionAmount { get; set; }

        /// <summary>
        /// Komisyon kdv oranı.
        /// </summary>
        public decimal CommissionTaxAmount { get; set; }

        /// <summary>
        /// Gider göstern VKN/TCKN.
        /// </summary>
        public string RecordExpensesVknTckn { get; set; }

        /// <summary>
        /// Gider gösteren ünvan.
        /// </summary>
        public string RecordExpensesTitle { get; set; }

        /// <summary>
        /// Etkinlik zamanı.
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// Etkinlik adı.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Etkinlik yeri.
        /// </summary>
        public string EventLocation { get; set; }

        /// <summary>
        /// Etkinliğin yapıldığı il.
        /// </summary>
        public string EventCity { get; set; }

        /// <summary>
        /// Etkinliğin yapıldığı ilin Plaka kodu
        /// </summary>
        public int EventCityId { get; set; }

        /// <summary>
        /// Etkinliğin yapılacağı belediye.
        /// </summary>
        public string EventMunicipality { get; set; }

        /// <summary>
        /// Etkinlik açıklama.
        /// </summary>
        public string EventDescription { get; set; }

        /// <summary>
        /// Organizatör VKN/TCKN.
        /// </summary>
        public string EventOrganizerVknTckn { get; set; }

        /// <summary>
        /// XsltCode
        /// </summary>
        public string XsltCode { get; set; }

        public List<TicketLine> TicketLines { get; set; }

        public List<NoteModel> Notes { get; set; }
    }
}
