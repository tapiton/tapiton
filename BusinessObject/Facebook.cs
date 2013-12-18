﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessObject
{
    /// <summary>
    /// Gigs entity
    /// </summary>
    [Serializable]
    public class Facebook
    {
        public int Id { get; set; }        
        public string FacebookAccessToken { get; set; }
        public string FacebookId { get; set; }
    }

    public class FacebookPhoto
    {
        public string id { get; set; }
        public From from { get; set; }
        public Tags tags { get; set; }
        public string picture { get; set; }
        public string source { get; set; }
        public string height { get; set; }
        public string width { get; set; }
        public string link { get; set; }
        public string icon { get; set; }
        public string created_time { get; set; }
        public string updated_time { get; set; }
    }

    public class FacebookAlbum
    {
        public string id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string privacy { get; set; }
        public int count { get; set; }
        public string type { get; set; }
        public string created_time { get; set; }
        public string updated_time { get; set; }
    }

    public class FacebookAlbums
    {
        public List<FacebookAlbum> data { get; set; }
        public Paging paging { get; set; }
    }

    public class FacebookPhotos
    {
        public List<FacebookPhoto> data { get; set; }
        public Paging paging { get; set; }
    }

    public class FacebookLike
    {
        public string name { get; set; }
        public string category { get; set; }
        public string id { get; set; }
        public string created_time { get; set; }
    }

    public class FacebookLikes
    {
        public List<FacebookLike> data { get; set; }
    }

    public class FacebookFriendCount
    {
        public string name { get; set; }
        public string category { get; set; }
        public string id { get; set; }
        public string created_time { get; set; }
    }

    public class FacebookFriendCounts
    {
        public List<FacebookFriendCount> data { get; set; }
    }
    public class FacebookProfile
    {
        public string id { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string link { get; set; }
        public string birthday { get; set; }
        // public string work { get; set; }
        public string timezone { get; set; }
        public string local { get; set; }
        public string verified { get; set; }
        public string updated_time { get; set; }
        public string Gender { get; set; }
    }

    public class Paging
    {
        public string next { get; set; }
        public string previous { get; set; }
    }

    public class From
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Tags
    {
        public List<Tag> data { get; set; }
    }

    public class Tag
    {
        public string id { get; set; }
        public string name { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public DateTime created_time { get; set; }
    }
    public class postStatus
    {
        public string id { get; set; }
    }
}

