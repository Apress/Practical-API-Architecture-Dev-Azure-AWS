using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Auth.AccessControlPolicy;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace MassRoverAPI.Authorizer
{
    public class Function
    {
        public Policy FunctionHandler(APIGatewayCustomAuthorizerRequest authRequest, ILambdaContext context)
        {
            var token = authRequest.AuthorizationToken;

            Policy policy;

            if (ValidateToken(token))
            {
                var statement = new Statement(Statement.StatementEffect.Allow);
                var policyStatements = new List<Statement> { statement };
                policy = new Policy("TokenValidationPassed", policyStatements);
            }
            else
            {
                var statement = new Statement(Statement.StatementEffect.Deny);
                var policyStatements = new List<Statement> { statement };
                policy = new Policy("TokenValidationFailed", policyStatements);
            }
            return policy;
        }

        private bool ValidateToken(string token)
        {
            // JWT token validation here 
            return true;
        }

    }
}
