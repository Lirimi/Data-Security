using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;


namespace ds
{
    public class TokenStatus
    {
        DatabaseConnection DB = new DatabaseConnection();
        public void Status(String jwtInput)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
           

            //Check if it's readable token (string is in a JWT format)
            var readableToken = jwtHandler.CanReadToken(jwtInput);

            if (!readableToken) throw new Exception("Gabim: Vlera e dhene nuk eshte token valid.");
           var token = jwtHandler.ReadJwtToken(jwtInput);

            //Extract the payload of the JWT
            var claims = token.Claims;
             
            var expireClaim = claims.FirstOrDefault(x =>
                x.Type.ToString().Equals("expire", StringComparison.InvariantCultureIgnoreCase));
            var nameClaim = claims.FirstOrDefault(x =>
                x.Type.ToString().Equals("Name", StringComparison.InvariantCultureIgnoreCase));
            
            var username = nameClaim.Value;
            var expireDate = expireClaim.Value;
            string valid = "";


            if (nameClaim != null)
            {
                Console.WriteLine("User: " + username);
            }
            
            DB.Open();
            
            string query = "Select * FROM users WHERE USER =" + "'" + username + "'";
            
            DataSet ds;
            ds = DB.DataSet(query);
            
            Console.Write("Valid: ");
            DateTime issued = DateTime.Now;
            DateTime expire = DateTime.Parse(expireDate);
            
            if (issued < expire)
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    valid = "po";
                }
                else
                {
                    valid = "jo";
                }
            }
            else
            {
                valid = "jo";
            }

            Console.WriteLine(valid);
            
            if (expireClaim != null)
            {
                Console.WriteLine("Skadimi: " + expire.ToString("dd/MM/yyyy HH:mm"));
            }
            
            DB.Close();
        }
    }
}