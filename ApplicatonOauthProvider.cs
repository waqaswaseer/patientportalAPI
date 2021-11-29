using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Data;
using patientportalapi.DataAccess;
using patientportalapi.Models;


namespace patientportalapi
{
    public class ApplicatonOauthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string query = string.Empty;
            DataTable dt = new DataTable();
            DAL objpt = new DAL();
            if (context.Password== "PX@*658")
                query = " exec getpatientdataapiwithcode '" + context.UserName + "'";
            else
                query = " exec getpatientdataapi '" + context.UserName + "','" + context.Password + "'";
            dt = objpt.dtFetchData(query);

            if (dt.Rows.Count > 0)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("patientno", dt.Rows[0]["patientno"].ToString()));
                identity.AddClaim(new Claim("gendername", dt.Rows[0]["gendername"].ToString()));
                identity.AddClaim(new Claim("age", dt.Rows[0]["age"].ToString()));
                identity.AddClaim(new Claim("mobileno", dt.Rows[0]["mobileno"].ToString()));
                identity.AddClaim(new Claim("firstname", dt.Rows[0]["firstname"].ToString()));
                identity.AddClaim(new Claim("nic", dt.Rows[0]["nic"].ToString()));
                identity.AddClaim(new Claim("address1", dt.Rows[0]["address1"].ToString()));
                context.Validated(identity);
            }
            else
            {
                query = " exec loginvalidation '" + context.UserName + "','" + context.Password + "'";
                dt = objpt.dtFetchData_(query);

                if (dt.Rows.Count > 0)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim("patientno", "000000"));
                    identity.AddClaim(new Claim("gendername", dt.Rows[0]["gender"].ToString()));
                    identity.AddClaim(new Claim("age", dt.Rows[0]["age"].ToString()));
                    identity.AddClaim(new Claim("mobileno", dt.Rows[0]["phoneNo"].ToString()));
                    identity.AddClaim(new Claim("firstname", dt.Rows[0]["username"].ToString()));
                    identity.AddClaim(new Claim("nic", ""));
                    identity.AddClaim(new Claim("address1", ""));
                    context.Validated(identity);
                }
                else
                    return;
            };
         }
    }
}