namespace BusLocationsApp.Models
{
    public class JourneysResponse
    {
        public string status { get; set; }
        public JourneyDatum[] data { get; set; }
        public object message { get; set; }
        public object usermessage { get; set; }
        public object apirequestid { get; set; }
        public string controller { get; set; }
        public object clientrequestid { get; set; }
        public object webcorrelationid { get; set; }
        public string correlationid { get; set; }
        public object parameters { get; set; }
    }

    public class JourneyDatum
    {
        public int id { get; set; }
        public int partnerid { get; set; }
        public string partnername { get; set; }
        public int routeid { get; set; }
        public string bustype { get; set; }
        public string bustypename { get; set; }
        public int totalseats { get; set; }
        public int availableseats { get; set; }
        public Journey journey { get; set; }
        public Feature[] features { get; set; }
        public string originLocation { get; set; }
        public string destinationLocation { get; set; }
        public bool isactive { get; set; }
        public int originlocationid { get; set; }
        public int destinationlocationid { get; set; }
        public bool ispromoted { get; set; }
        public int cancellationoffset { get; set; }
        public bool hasbusshuttle { get; set; }
        public bool disablesaleswithoutgovid { get; set; }
        public string displayoffset { get; set; }
        public float partnerrating { get; set; }
        public bool hasdynamicpricing { get; set; }
        public bool disablesaleswithouthescode { get; set; }
        public bool disablesingleseatselection { get; set; }
        public int changeoffset { get; set; }
        public int rank { get; set; }
        public bool displaycouponcodeinput { get; set; }
        public bool disablesaleswithoutdateofbirth { get; set; }
        public int? openoffset { get; set; }
        public object displaybuffer { get; set; }
        public bool allowsalesforeignpassenger { get; set; }
        public bool hasseatselection { get; set; }
        public object[] brandedfares { get; set; }
        public bool hasgenderselection { get; set; }
        public bool hasdynamiccancellation { get; set; }
        public object partnertermsandconditions { get; set; }
        public string partneravailablealphabets { get; set; }
        public int providerid { get; set; }
        public string partnercode { get; set; }
        public bool enablefulljourneydisplay { get; set; }
        public string providername { get; set; }
        public bool enableallstopsdisplay { get; set; }
        public bool isdestinationdomestic { get; set; }
        public object minlengovid { get; set; }
        public object maxlengovid { get; set; }
        public bool requireforeigngovid { get; set; }
        public bool iscancellationinfotext { get; set; }
        public object cancellationoffsetinfotext { get; set; }
        public bool istimezonenotequal { get; set; }
        public float markuprate { get; set; }
        public bool isprintticketbeforejourney { get; set; }
        public bool isextendedjourneydetail { get; set; }
        public bool ispaymentselectgender { get; set; }
        public bool shouldturkeyonthenationalitylist { get; set; }
        public float markupfee { get; set; }
        public object partnernationality { get; set; }
        public bool generatebarcode { get; set; }
        public bool isprintticketbeforejourneybadge { get; set; }
        public bool isprintticketbeforejourneymail { get; set; }
        public bool isshowfarerules { get; set; }
        public bool isdifferentseatprice { get; set; }
        public bool redirecttocheckout { get; set; }
        public bool isshowratingavarage { get; set; }
        public float partnerrouterating { get; set; }
        public int partnerrouteratingvotecount { get; set; }
        public int partnerratingvotecount { get; set; }
        public bool includedstationfee { get; set; }
        public bool isdomesticroute { get; set; }
        public bool isabroadroute { get; set; }
        public bool disablesaleswithoutpassportexpirationdate { get; set; }
        public int transfercount { get; set; }
        public bool isnationalidentitynumbervalidator { get; set; }
        public object nationalidentityvalidationrules { get; set; }
        public object internetpricerate { get; set; }
        public object journeydetailcarrierbase { get; set; }
        public bool hasshuttleselection { get; set; }
        public int originstationid { get; set; }
        public int destinationstationid { get; set; }
        public bool hasavailableseatinfo { get; set; }
        public bool hasseatselectionmethod { get; set; }
        public bool hasjourneyshuttle { get; set; }
        public object displayoffsetstarted { get; set; }
        public object displayoffsetaftermin { get; set; }
        public bool issendordercompletionpdf { get; set; }
    }

    public class Journey
    {
        public string kind { get; set; }
        public string code { get; set; }
        public Stop[] stops { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime departure { get; set; }
        public DateTime arrival { get; set; }
        public string currency { get; set; }
        public string duration { get; set; }
        public float originalPrice { get; set; }
        public float internetprice { get; set; }
        public float? providerinternetprice { get; set; }
        public object booking { get; set; }
        public string busname { get; set; }
        public Policy policy { get; set; }
        public string[] features { get; set; }
        public object featuresdescription { get; set; }
        public string description { get; set; }
        public object available { get; set; }
        public object partnerprovidercode { get; set; }
        public object peronno { get; set; }
        public object partnerproviderid { get; set; }
        public bool issegmented { get; set; }
        public object partnername { get; set; }
        public object providercode { get; set; }
        public float sortingprice { get; set; }
        public bool hasmultiplebrandedfareselection { get; set; }
        public bool hasavailableseatinfo { get; set; }
    }

    public class Policy
    {
        public object maxseats { get; set; }
        public int? maxsingle { get; set; }
        public int? maxsinglemales { get; set; }
        public int? maxsinglefemales { get; set; }
        public bool mixedgenders { get; set; }
        public bool govid { get; set; }
        public bool lht { get; set; }
    }

    public class Stop
    {
        public int id { get; set; }
        public int? kolayCarLocationId { get; set; }
        public string name { get; set; }
        public string station { get; set; }
        public DateTime? time { get; set; }
        public bool isorigin { get; set; }
        public bool isdestination { get; set; }
        public bool issegmentstop { get; set; }
        public int index { get; set; }
        public object obiletstationid { get; set; }
        public object mapurl { get; set; }
        public object stationphone { get; set; }
        public object stationaddress { get; set; }
    }

    public class Feature
    {
        public int id { get; set; }
        public int? priority { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool ispromoted { get; set; }
        public string backcolor { get; set; }
        public string forecolor { get; set; }
        public object partnernotes { get; set; }
    }
}
