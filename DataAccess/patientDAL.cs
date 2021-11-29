using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using patientportalapi.DataAccess;
using patientportalapi.Models;
using System.Data;
using System.IO;
using System.Net;
using System.Xml;

namespace patientportalapi.DataAccess
{
    public class patientDAL :DAL
    {
        public int maxID(string tablename, string columnname)
        {
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "select max(isnull(" + columnname + ",0)) as lastid from " + tablename;
            dt = dtFetchData_(query);
            if (dt != null && dt.Rows[0]["lastid"].ToString() != "")
                return Convert.ToInt32(dt.Rows[0]["lastid"].ToString());
            else
                return 0;
        }
        public int getuserID(string username)
        {
            int result = 0;
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec getuserid '" + username.ToString() + "'";
            dt = dtFetchData_(query);
            result = Convert.ToInt32(dt.Rows[0]["userID"]);
            return result;
        }

        public IEnumerable<patientallvisits> GetAllPatientVisits(string patientno)
        {
            List<patientallvisits> lstvisit = new List<patientallvisits>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec getallpatientvisitapi '" + patientno.ToString() + "'";
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    patientallvisits ptvisit = new patientallvisits();

                    ptvisit.labno = dt.Rows[index]["labno"].ToString();
                    ptvisit.sampledate = dt.Rows[index]["sampledate"].ToString();
                    ptvisit.totalbill = Convert.ToInt32(dt.Rows[index]["totalbill"].ToString());
                    ptvisit.discount = Convert.ToInt32(dt.Rows[index]["discount"].ToString());
                    ptvisit.receivedamount = Convert.ToInt32(dt.Rows[index]["receivedamount"].ToString());
                    ptvisit.duereceived = Convert.ToInt32(dt.Rows[index]["duereceived"].ToString());
                    ptvisit.balance = Convert.ToInt32(dt.Rows[index]["balance"].ToString());
                    ptvisit.companyname = dt.Rows[index]["companyname"].ToString();
                    ptvisit.pdfbill = dt.Rows[index]["billurl"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public int usersignup(signup sdata)
        {
            try
            {
                DataTable dt = new DataTable();
                string query = string.Empty;
                int maxId_ = maxID("userSignup", "userID")+1;
                query = " insert into userSignup(userID,username,age,gender,phoneNo,emailAdd,password,uaddres) values (" + maxId_ + ",'" + sdata.username + "','" + sdata.age + "','" + sdata.gender + "','" + sdata.mobileno +
                    "','" + sdata.email + "','" + sdata.password + "','" + sdata.address +  "') ";
                if (exeQuery_(query))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<signup> UserProfileData(string name)
        {
            List<signup> lstvisit = new List<signup>();
            DataTable dt = new DataTable();
            string query = string.Empty;
            var userID_ = getuserID(name);
            query = "exec userorder " + userID_;
            dt = dtFetchData_(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    signup ptvisit = new signup();
                    ptvisit.username = dt.Rows[index]["username"].ToString();
                    ptvisit.age = dt.Rows[index]["age"].ToString();
                    ptvisit.gender = dt.Rows[index]["gender"].ToString();
                    ptvisit.mobileno = dt.Rows[index]["phoneNo"].ToString();
                    ptvisit.address = dt.Rows[index]["uaddres"].ToString();
                    ptvisit.password = dt.Rows[index]["Total"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;            
        }


        public int Testbooking(orderdetail sdata)
        {
            int maxId_ = maxID("bookedorders", "orderid") + 1;
            try
            {
                DataTable dt = new DataTable();
                string query = string.Empty;
                query = " exec checkbasket'" + sdata.username + "'," + sdata.testcode;
                dt = dtFetchData_(query);
                if (dt.Rows.Count > 0 )
                    return 0;
                else
                {              
                    int maxId = maxID("bookedorders", "orderid") + 1;
                    int getuserid = getuserID(sdata.username);
                    query = "insert into bookedorders (orderid, timedate, userID, orderstatus) values ("+ maxId + ", getdate() ,"+ getuserid + " , 'p')";
                    if (exeQuery_(query))
                    {
                        query = "insert into orderdetails (orderid, testcode , testname, rate, status ) values ("+ maxId + "," + sdata.testcode +",'" + sdata.testname + "'," + sdata.price+", 'p' )";
                        if (exeQuery_(query))
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                    
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Labtests> GetAllTests()
        {
            List<Labtests> lstvisit = new List<Labtests>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "select * from labtest";
            dt = dtFetchData_(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    Labtests ptvisit = new Labtests();
                    ptvisit.testcode = dt.Rows[index]["testcode"].ToString();
                    ptvisit.testname = dt.Rows[index]["testname"].ToString();
                    ptvisit.price = dt.Rows[index]["rate"].ToString();
                    ptvisit.specimen = dt.Rows[index]["specimen"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<userbasket> GetPendingBasket(string name)
        {
            List<userbasket> lstvisit = new List<userbasket>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec userbasket '"+ name +"'" ;
            dt = dtFetchData_(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    userbasket ptvisit = new userbasket();
                    //ptvisit.username = dt.Rows[index]["username"].ToString();
                    ptvisit.testcode = dt.Rows[index]["testcode"].ToString();
                    ptvisit.testname = dt.Rows[index]["testname"].ToString();
                    ptvisit.price = dt.Rows[index]["rate"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<currentvisitdata> GetCurrentVisitData(string patientno)
        {
            List<currentvisitdata> lstvisit = new List<currentvisitdata>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec lastvisitdata '" + patientno.ToString() + "'";
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    currentvisitdata ptvisit = new currentvisitdata();
                    ptvisit.labno = dt.Rows[index]["labno"].ToString();
                    ptvisit.sampledate = dt.Rows[index]["sampledate"].ToString();
                    ptvisit.testname = dt.Rows[index]["testname"].ToString();
                    ptvisit.rptstatus = dt.Rows[index]["rptstatus"].ToString();
                    ptvisit.pdfurl = dt.Rows[index]["pdfurl"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<currentvisitdata> GetLabNoData(string labno)
        {
            List<currentvisitdata> lstvisit = new List<currentvisitdata>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec labtestdetailapi '" + labno.ToString() + "'";
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    currentvisitdata ptvisit = new currentvisitdata();
                    ptvisit.labno = dt.Rows[index]["labno"].ToString();
                    ptvisit.sampledate = dt.Rows[index]["sampledate"].ToString();
                    ptvisit.testname = dt.Rows[index]["testname"].ToString();
                    ptvisit.rptstatus = dt.Rows[index]["rptstatus"].ToString();
                    ptvisit.pdfurl = dt.Rows[index]["pdfurl"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<abnormaltestresults> GetAbnormalResults(string labno,int testtype)
        {
            List<abnormaltestresults> lstvisit = new List<abnormaltestresults>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec currentvisitabnormalresults '" + labno.ToString() + "'," + testtype;
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    abnormaltestresults ptvisit = new abnormaltestresults();
                    ptvisit.labno = dt.Rows[index]["labno"].ToString();
                    ptvisit.testname = dt.Rows[index]["testname"].ToString();
                    ptvisit.parametername = dt.Rows[index]["parametername"].ToString();
                    ptvisit.result = dt.Rows[index]["result"].ToString();
                    ptvisit.comments = dt.Rows[index]["comments"].ToString();
                    ptvisit.resultstatus = dt.Rows[index]["resultstatus"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<labnoList> GetAllLabnos(string labno,string testcode)
        {
            List<labnoList> lstvisit = new List<labnoList>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec getalllabno '" + labno.ToString() + "','" + testcode.ToString() + "'";
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    labnoList ptvisit = new labnoList();
                    ptvisit.computerno = dt.Rows[index]["computerno"].ToString();
                    ptvisit.visitno = dt.Rows[index]["visitno"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<labPreviousResults> GetAllTestResults(string labno,string testcode)
        {
            List<labPreviousResults> lstvisit = new List<labPreviousResults>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec loadpreviousresult '" + labno.ToString() + "','" + testcode.ToString() + "'";
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    labPreviousResults ptvisit = new labPreviousResults();
                    ptvisit.testcode = dt.Rows[index]["testcode"].ToString();
                    ptvisit.parametercode = dt.Rows[index]["parametercode"].ToString();
                    ptvisit.sortorder = dt.Rows[index]["sortorder"].ToString();
                    ptvisit.testname = dt.Rows[index]["testname"].ToString();
                    ptvisit.parametername = dt.Rows[index]["parametername"].ToString();
                    ptvisit.result1 = dt.Rows[index]["result1"].ToString();
                    ptvisit.rem1 = dt.Rows[index]["rem1"].ToString();
                    ptvisit.result2 = dt.Rows[index]["result2"].ToString();
                    ptvisit.rem2 = dt.Rows[index]["rem2"].ToString();
                    ptvisit.result3 = dt.Rows[index]["result3"].ToString();
                    ptvisit.rem3 = dt.Rows[index]["rem3"].ToString();
                    ptvisit.result4 = dt.Rows[index]["result4"].ToString();
                    ptvisit.rem4 = dt.Rows[index]["rem4"].ToString();
                    ptvisit.result5 = dt.Rows[index]["result5"].ToString();
                    ptvisit.rem5 = dt.Rows[index]["rem5"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<AllTestNames> GetAllTestNames(string labno)
        {
            List<AllTestNames> lstvisit = new List<AllTestNames>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec gettestnameonly '" + labno.ToString() + "'";
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    AllTestNames ptvisit = new AllTestNames();
                    ptvisit.testcode = dt.Rows[index]["testcode"].ToString();
                    ptvisit.testname = dt.Rows[index]["testname"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<SingleTestResult> SingleTestResults(string labno,string parametercode)
        {
            List<SingleTestResult> lstvisit = new List<SingleTestResult>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "exec singleparamallresult '" + labno.ToString() + "','" + parametercode.ToString() + "'";
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    SingleTestResult ptvisit = new SingleTestResult();
                    ptvisit.computerno = dt.Rows[index]["computerno"].ToString();
                    ptvisit.fulllabno = dt.Rows[index]["fulllabno"].ToString();
                    ptvisit.result = dt.Rows[index]["result"].ToString();
                    ptvisit.minivalue = dt.Rows[index]["minivalue"].ToString();
                    ptvisit.maxvalue = dt.Rows[index]["maxvalue"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<Getonlinecode> GetCode(string pno)
        {
            List<Getonlinecode> lstvisit = new List<Getonlinecode>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "select top 1 onlinecode,mobileno,firstname,encpatientno+'C'+cast(computerno as varchar)+'Ct' encpatientno from patientregistration where patientno = '" + pno.ToString() + "' order by computerno desc ";
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    Getonlinecode ptvisit = new Getonlinecode();
                    ptvisit.onlinecode = dt.Rows[index]["onlinecode"].ToString();
                    ptvisit.mobileno = dt.Rows[index]["mobileno"].ToString();
                    ptvisit.firstname = dt.Rows[index]["firstname"].ToString();

                    string smsmsg = string.Empty;
                    smsmsg = "Dear Patient your onlinecode is " + ptvisit.onlinecode.ToString() + " you can also click on link http://patients.cmahospital.org:3689/x.aspx?id=" + dt.Rows[index]["onlinecode"].ToString();
                    string smsmasking = string.Empty;
                    smsmasking = "NextStep";
                    string apiurl = string.Empty;
                    //apiurl = "http://weblogin.premiumsms.pk/sendsms_url.html?Username=03238828807&Password=next123&From=" + smsmasking.ToString() + "&To=" + ptvisit.mobileno.ToString() + "&Message=" + smsmsg.ToString() + "&type=ur";
                    apiurl = "http://weblogin.premiumsms.pk/sendsms_url.html?Username=03238828807&Password=next123&From=" + smsmasking.ToString() + "&To=" + ptvisit.mobileno.ToString() + "&Message=" + smsmsg.ToString();
                    System.Net.WebRequest wrURL;
                    Stream objStream;
                    string strURL;
                    wrURL = WebRequest.Create(apiurl.ToString());
                    objStream = wrURL.GetResponse().GetResponseStream();
                    StreamReader objSReader = new StreamReader(objStream);
                    strURL = objSReader.ReadToEnd().ToString();
                    XmlTextReader reader = new XmlTextReader(new StringReader(strURL));

                    //reader.Read();
                    //byte[] buffer = new byte[1];

                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;


        }

        public string getonlinecode(string patientno ,string onlinecode)
        {
            List<Getonlinecode> lstvisit = new List<Getonlinecode>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            query = "select computerno from patientregistration where patientno = '" + patientno.ToString() + "' and onlinecode='" + onlinecode.ToString() + "'";
            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                return "success";
            }
            else
            {
                return "invalid";
            }
            


        }

        public IEnumerable<PatientData> patientData(string patientno)
        {
            List<PatientData> lstvisit = new List<PatientData>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            //query = "exec singleparamallresult '" + labno.ToString() + "','" + parametercode.ToString() + "'";
            query = "exec mobilereportview  '" + patientno.ToString() + "'";

            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    PatientData ptvisit = new PatientData();
                    ptvisit.computerno = dt.Rows[index]["computerno"].ToString();
                    ptvisit.labno = dt.Rows[index]["labno"].ToString();
                    ptvisit.patientno = dt.Rows[index]["patientno"].ToString();
                    ptvisit.firstname = dt.Rows[index]["firstname"].ToString();
                    ptvisit.sampledate = dt.Rows[index]["sampledate"].ToString();
                    ptvisit.testname = dt.Rows[index]["testname"].ToString();
                    ptvisit.billclear = dt.Rows[index]["billclear"].ToString();
                    ptvisit.samplerec = dt.Rows[index]["samplerec"].ToString();
                    ptvisit.pdfurl = dt.Rows[index]["pdfurl"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

        public IEnumerable<CovidPatientData> covidPatientData(string labno)
        {
            List<CovidPatientData> lstvisit = new List<CovidPatientData>();
            string query = string.Empty;
            DataTable dt = new DataTable();
            //query = "exec singleparamallresult '" + labno.ToString() + "','" + parametercode.ToString() + "'";
            query = "exec covidPatientData  '" + labno.ToString() + "'";

            dt = dtFetchData(query);
            if (dt.Rows.Count > 0)
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    CovidPatientData ptvisit = new CovidPatientData();
                    ptvisit.firstname = dt.Rows[index]["firstname"].ToString();
                    ptvisit.agegender = dt.Rows[index]["agegender"].ToString();
                    ptvisit.nic = dt.Rows[index]["nic"].ToString();
                    ptvisit.mobileno = dt.Rows[index]["mobileno"].ToString();
                    ptvisit.sampledate = dt.Rows[index]["sampledate"].ToString();
                    ptvisit.reportdate = dt.Rows[index]["reportdate"].ToString();
                    ptvisit.flightno = dt.Rows[index]["flightno"].ToString();
                    ptvisit.flightdate = dt.Rows[index]["flightdate"].ToString();
                    ptvisit.destination = dt.Rows[index]["destination"].ToString();
                    ptvisit.passport = dt.Rows[index]["passport"].ToString();
                    ptvisit.pnrno = dt.Rows[index]["pnrno"].ToString();
                    ptvisit.airline = dt.Rows[index]["airline"].ToString();
                    ptvisit.result = dt.Rows[index]["result"].ToString();
                    lstvisit.Add(ptvisit);
                }
            }

            return lstvisit;

        }

    }
}