using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using patientportalapi.Models;
using patientportalapi.DataAccess;
using System.IO;


namespace patientportalapi.Controllers
{
    public class patientController : ApiController
    {
        patientDAL objpt = new patientDAL();

        [HttpGet]
        [Route("api/GetUserClaims")]
        public patientlogin GetUserClaims()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            patientlogin model = new patientlogin()
            {
                firstname = identityClaims.FindFirst("firstname").Value,
                patientno = identityClaims.FindFirst("patientno").Value,
                age = identityClaims.FindFirst("age").Value,
                gendername = identityClaims.FindFirst("gendername").Value,
                nic = identityClaims.FindFirst("nic").Value,
                address1 = identityClaims.FindFirst("address1").Value,
                mobileno = identityClaims.FindFirst("mobileno").Value,

            };
            return model;
        }

        [HttpPost]
        [Route("api/signup")]
        [AllowAnonymous]
        public int useraccount(signup resultinfo)
        {
            return objpt.usersignup(resultinfo);
        }
        [HttpPost]
        [Route("api/newaddress")]
        [AllowAnonymous]
        public int newaddress(updateadress resultinfo)
        {
            return objpt.newaddress(resultinfo);
        }

        [HttpPost]
        [Route("api/Bookorder")]
        [AllowAnonymous]
        public int patientorderbooking(orderdetail resultinfo)
        {
            return objpt.Testbooking(resultinfo);
        }
        [HttpGet]
        [Route("api/getuserProfile/{username}")]
        [AllowAnonymous]
        public IEnumerable<signup> getuserProfile(string username)
        {
            return objpt.UserProfileData(username);
        }

        [HttpGet]
        [Route("api/labtests")]
        [AllowAnonymous]
        public IEnumerable<Labtests> GetTests()
        {
            return objpt.GetAllTests();
        }
        [HttpGet]
        [Route("api/pendingbasket/{username}")]
        [AllowAnonymous]
        public IEnumerable<userbasket> Getpendingtests(string username)
        {
            return objpt.GetPendingBasket(username);
        }

        // GET: api/GetAllVisits/54654654
        [HttpGet]
        [Route("{patientno}")]
        [AllowAnonymous]
        public IEnumerable<patientallvisits> Get(string patientno)
        {
            return objpt.GetAllPatientVisits(patientno);
        }

        [HttpGet]
        [Route("api/LastVisit/{patientno}")]
        [AllowAnonymous]
        public IEnumerable<currentvisitdata> LastVisit(string patientno)
        {
            return objpt.GetCurrentVisitData(patientno);
        }

        [HttpGet]
        [Route("api/LabNoData/{labno}")]
        [AllowAnonymous]
        public IEnumerable<currentvisitdata> LabNoData(string labno)
        {
            return objpt.GetLabNoData(labno);
        }

        [HttpGet]
        [Route("api/ProfileTestResults/{labno}")]
        [AllowAnonymous]
        public IEnumerable<abnormaltestresults> ProfileTestResults(string labno)
        {
            return objpt.GetAbnormalResults(labno,1);
        }

        [HttpGet]
        [Route("api/SingleTestResults/{labno}")]
        [AllowAnonymous]
        public IEnumerable<abnormaltestresults> SingleTestResults(string labno)
        {
            return objpt.GetAbnormalResults(labno, 0);
        }

        [HttpGet]
        [Route("api/GetAllLabno/{labno}/{testcode}")]
        [AllowAnonymous]
        public IEnumerable<labnoList> GetAllLabno(string labno,string testcode)
        {
            return objpt.GetAllLabnos(labno,testcode);
        }

        [HttpGet]
        [Route("api/GetPreviousResults/{labno}/{testcode}")]
        [AllowAnonymous]
        public IEnumerable<labPreviousResults> GetPreviousResults(string labno,string testcode)
        {
            return objpt.GetAllTestResults(labno,testcode);
        }

        [HttpGet]
        [Route("api/GetAllTestName/{labno}")]
        [AllowAnonymous]
        public IEnumerable<AllTestNames> GetAllTestName(string labno)
        {
            return objpt.GetAllTestNames(labno);
        }


        [HttpGet]
        [Route("api/SingleTestResult/{labno}/{parametercode}")]
        [AllowAnonymous]
        public IEnumerable<SingleTestResult> SingleTestResult(string labno,string parametercode)
        {
            return objpt.SingleTestResults(labno,parametercode);
        }


        [HttpGet]
        [Route("api/getonlinecode/{pno}")]
        [AllowAnonymous]
        public IEnumerable<Getonlinecode> getonlinecode(string pno)
        {
            return objpt.GetCode(pno);
        }



        [HttpGet]
        [Route("api/loginWOToken/{patientno}/{onlinecode}")]
        [AllowAnonymous]
        public string getonlinecode(string patientno, string onlinecode)
        {
            return objpt.getonlinecode(patientno, onlinecode);
        }

        [HttpGet]
        [Route("api/patientData/{patientno}")]
        [AllowAnonymous]
        public IEnumerable<PatientData> patientData(string patientno)
        {
            return objpt.patientData(patientno);
        }

        [HttpGet]
        [Route("api/covidPatientData/{labno}")]
        [AllowAnonymous]
        public IEnumerable<CovidPatientData> covidPatientData(string labno)
        {
            return objpt.covidPatientData(labno);
        }
    }
}
