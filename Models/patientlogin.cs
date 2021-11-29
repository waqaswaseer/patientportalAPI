using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace patientportalapi.Models
{
    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////
    
    public class signup
    {
        public string username { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string mobileno { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
    }

    public class Labtests
    {
        public string testcode { get; set; }
        public string testname { get; set; }
        public string price { get; set; }
        public string specimen { get; set; }
    }

    public class userbasket
    {        
        public string testcode { get; set; }
        public string testname { get; set; }
        public string price { get; set; }
    }

    public class orderdetail
    {
        public string username { get; set; }
        public string testname { get; set; }
        public string testcode { get; set; }
        public string price { get; set; }
    }



    /// ////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public class patientlogin
    {
        public string firstname { get; set; }
        public string patientno { get; set; }
        public string mobileno { get; set; }
        public string age { get; set; }
        public string gendername { get; set; }
        public string nic { get; set; }
        public string address1 { get; set; }
    }

    public class patientallvisits
    {
        public string labno { get; set; }
        public string sampledate { get; set; }
        public string companyname { get; set; }
        public int totalbill { get; set; }
        public int receivedamount { get; set; }
        public int discount { get; set; }
        public int duereceived { get; set; }
        public int balance { get; set; }
        public string pdfbill { get; set; }
    }

    public class currentvisitdata
    {
        public string labno { get; set; }
        public string sampledate { get; set; }
        public string testname { get; set; }
        public string rptstatus { get; set; }
        public string pdfurl { get; set; }
    }

    public class abnormaltestresults
    {
        public string labno { get; set; }
        public string testname { get; set; }
        public string parametername { get; set; }
        public string result { get; set; }
        public string comments { get; set; }
        public string resultstatus { get; set; }
    }

    public class labnoList
    {
        public string computerno { get; set; }
        public string visitno { get; set; }
        
    }

    public class AllTestNames
    {
        public string testcode { get; set; }
        public string testname { get; set; }

    }

    public class labPreviousResults
    {
        public string testcode { get; set; }
        public string parametercode { get; set; }
        public string sortorder { get; set; }
        public string testname { get; set; }
        public string parametername { get; set; }
        public string result1 { get; set; }
        public string rem1 { get; set; }
        public string result2 { get; set; }
        public string rem2 { get; set; }
        public string result3 { get; set; }
        public string rem3 { get; set; }
        public string result4 { get; set; }
        public string rem4 { get; set; }
        public string result5 { get; set; }
        public string rem5 { get; set; }

    }
    

    public class SingleTestResult
    {
        public string computerno { get; set; }
        public string fulllabno { get; set; }
        public string result { get; set; }
        public string minivalue { get; set; }
        public string maxvalue { get; set; }
    }

    public class Getonlinecode
    {
        public string onlinecode { get; set; }
        public string mobileno { get; set; }
        public string firstname { get; set; }
        
    }

    public class PatientData
    {
        public string mobileno { get; set; }
        public string patientno { get; set; }
        public string firstname { get; set; }
        public string computerno { get; set; }
        public string sampledate { get; set; }
        public string labno { get; set; }
        public string samplerec { get; set; }
        public string testname { get; set; }
        public string pdfurl { get; set; }
        public string billclear { get; set; }
    }

    public class CovidPatientData
    {
        public string firstname { get; set; }
        public string agegender { get; set; }
        public string nic { get; set; }
        public string mobileno { get; set; }
        public string sampledate { get; set; }
        public string reportdate { get; set; }
        public string flightno { get; set; }
        public string flightdate { get; set; }
        public string destination { get; set; }
        public string passport { get; set; }
        public string pnrno { get; set; }
        public string airline { get; set; }
        public string result { get; set; }
    }


}