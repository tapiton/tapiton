using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;
namespace BusinessObject
{
    public class Twitter
    {
        public string GetRequestToken(string consumerKey, string consumerSecret, string callbackUrl)
        {
            return OAuthUtility.GetRequestToken(consumerKey, consumerSecret, callbackUrl).Token;
        }
        public Uri BuildAuthorizationUri(string requestToken)
        {
            return OAuthUtility.BuildAuthorizationUri(requestToken);
        }
        
        public OAuthTokenResponse GetAccessToken(string consumerKey, string consumerSecret, string oauth_token, string oauth_verifier)
        {
            return OAuthUtility.GetAccessToken(consumerKey, consumerSecret, oauth_token, oauth_verifier);
        }
    }
}
