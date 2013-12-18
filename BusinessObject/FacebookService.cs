using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessObject
{
    /// <summary>
    /// Summary description for FacebookService
    /// </summary>
    public class FacebookService
    {
        public bool Save(Facebook entity)
        {
            // do you own saving stuff in here, cause am not doing it for you!!

            System.Web.HttpContext.Current.Session["FacebookAccessToken"] = entity.FacebookAccessToken;
            System.Web.HttpContext.Current.Session["FacebookId"] = entity.FacebookId;

            return true;
        }

        private string GetSessionVariable(string key)
        {
            var value = System.Web.HttpContext.Current.Session[key];
            if (value != null)
                return value.ToString();
            else
                return string.Empty;
        }

        public Facebook GetById(int id)
        {
            // this is just going to be a fake'd object get your's from the database
            Facebook fb = new Facebook();
            fb.FacebookAccessToken = GetSessionVariable("FacebookAccessToken"); // facebook will give you this
            fb.FacebookId = GetSessionVariable("FacebookId");  // get this from facebook
            fb.Id = 99; // the database row id, it's not really though

            return fb;
        }

        public FacebookProfile User_GetDetails(string accessToken)
        {
            string url = "https://graph.facebook.com/me?access_token=" + accessToken;
            return CallUrl<FacebookProfile>(url);
        }

        public FacebookFriendCounts Friend_GetDetails(string accessToken)
        {
            string url = "https://graph.facebook.com/me/friends?access_token=" + accessToken;
            return CallUrl<FacebookFriendCounts>(url);
        }
        public FacebookPhotos Photos_FetchAllOfMe(string userName, string accessToken)
        {
            string url = "https://graph.facebook.com/" + userName + "/photos?access_token=" + accessToken;
            return CallUrl<FacebookPhotos>(url);
        }

        public FacebookAlbums Albums_FetchAll(string userName, string accessToken)
        {
            string url = "https://graph.facebook.com/" + userName + "/albums?access_token=" + accessToken;
            return CallUrl<FacebookAlbums>(url);
        }

        public FacebookPhotos Photos_FetchAllFromAlbum(string albumId, string accessToken)
        {
            string url = "https://graph.facebook.com/" + albumId + "/photos?access_token=" + accessToken;
            return CallUrl<FacebookPhotos>(url);
        }

        public FacebookLikes Pages_FetchAllILike(string userName, string accessToken)
        {
            string url = "https://graph.facebook.com/" + userName + "/likes?access_token=" + accessToken;
            return CallUrl<FacebookLikes>(url);
        }
        public postStatus Post_Feed(string ProfileID, string accessToken, string Link, string Caption, string Name, string Message, string Description)
        {
            try
            {
            string url = "https://graph.facebook.com/" + ProfileID + "/feed?link=" + Link + "&caption=" + Caption + "&name=" + Name + "&message=" + Message + "&description=" + Description + "&access_token=" + accessToken;
            return CallUrl<postStatus>(url, "");
            }
            catch { return new postStatus(); }
        }
        public postStatus Post_Feed(string ProfileID, string accessToken, string Link, string Picture, string Caption, string Name, string Message, string Description)
        {
            try
            {
                string url = "https://graph.facebook.com/" + ProfileID + "/feed?link=" + Link + "&picture=" + Picture + "&caption=" + Caption + "&name=" + Name + "&message=" + Message + "&description=" + Description + "&access_token=" + accessToken;
                return CallUrl<postStatus>(url, "");
            }
            catch { return new postStatus(); }
        }
        private T CallUrl<T>(string url)
        {
            try
            {
                string result = Tools.CallUrl(url);
                T items = result.FromJson<T>();
                return items;
            }
            catch
            {
                return default(T);
            }
        }
        private T CallUrl<T>(string url, string method)
        {
            try
            {
                string result = Tools.CallUrl(url, method);
                T items = result.FromJson<T>();
                return items;
            }
            catch
            {
                return default(T);
            }
        }
    }
}