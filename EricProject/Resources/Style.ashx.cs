using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessObject;
namespace EricProject.Resources
{
    /// <summary>
    /// Summary description for Style
    /// </summary>
    public class Style : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/css";
            string CSS = "";
            bool isMobile = false;
            string sUA = HttpContext.Current.Request.UserAgent.Trim().ToLower();

            if (HttpContext.Current.Request.Browser.IsMobileDevice || sUA.Contains("ipod") || sUA.Contains("iphone") || sUA.Contains("android") || sUA.Contains("opera mobi") || (sUA.Contains("windows phone os") && sUA.Contains("iemobile")) || sUA.Contains("fennec"))
                isMobile = true;
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                isMobile = true;
            }
          //  isMobileBrowser();
            if (isMobileBrowser())
            {
               
                CSS = @"body { font-size: 1em ; font-family: Arial;}
.cuponBox .clr { clear:both; overflow: hidden; height: 0px; }
.cuponBox { margin:0 auto; width:80%; background:#dcdfe4;position:relative; line-height: 19px;}
.cuponBox h2 { text-align: center; padding: 0px; margin: 0px; font-size: 12px; line-height: 20px; color: #535456; font-weight: normal; border-bottom: none;}
.cuponBox .loginField {padding-bottom: 10px;}
.cuponBox .loginField .field { padding: 0 10px; background: url('..images/Coupon/field_bg.png') repeat-x scroll 0 0 transparent; border:1px solid #CDD1DB; height:28px; width:251px;}		
.cuponBox .loginField textarea { padding: 5px 10px; background: url('..images/Coupon/field_bg.png') repeat-x scroll 0 0 transparent; font-family:Arial, Helvetica, sans-serif; font-size:1em; border:1px solid #CDD1DB; height:50px; width:351px;}
.cuponBox .loginField .button { cursor:pointer; width:88px; height:37px; color:#ffffff; font-weight:bold; float:left; border:0px; background: url('http://dev.tapiton.com/images/Coupon/button_bg.png') repeat-x scroll 0 0 transparent; }
.cuponBox .loginField .button:hover { background: url('http://dev.tapiton.com/images/Coupon/button_bg_hover.png') repeat-x scroll 0 0 transparent;  }
.cuponBox .loginField a { line-height: 37px; float: right; font-family: Arial; color: #000000; font-size: 14px; text-decoration: none; padding-right: 28px;}}

.cuponBox .parentTabel { height: 100%; position: relative; border: 10px solid #f4f4f4}
.cuponBox .topbg { padding:5px 5px 0 5px;}
.cuponBox p {padding:0px; margin: 0px; text-align:center; font-size:12px; color:#535456;}
.cuponBox p span {	font-size:12px; color:#2e789f; font-weight:bold; }
.cuponBox .middlecont { padding:0px 5px 0px 5px;}
.cuponBox .logoImg { padding: 0 5px; text-align: center;}
.cuponBox .logoImg img { margin-top: 0px; width: 100%;}
.cuponBox .tabelPart { padding: 0px 0px 0px 0px;}
.cuponBox .innerTabel { padding-bottom: 1px; background: #fff; padding: 5px 5px 0px 5px; border: 1px solid #b2b3b7; border-bottom: 0px;}
.cuponBox .innerTabel td { padding: 3px 4px; background: #ffffff; font-size: 10px; line-height: 18px; color: #000000;}
.cuponBox .innerTabel .toprow td { background: #2d769c; font-size: 12px; line-height: 18px; color: #ffffff; font-weight: bold;}
.cuponBox .innerTabel td { padding: 3px 4px; background: #ffffff; font-size: 10px; line-height: 18px;  color: #000000;}
.cuponBox .innerTabel .grybg td { background:#F3F3F3;}
.cuponBox .botTabelbg { border-bottom: 1px solid #dcdfe4; background:#2e789f;}
.cuponBox .bordertxt { text-align: center; padding-bottom: 5px; border-bottom: 1px dashed #aabcc5;}
.cuponBox .botdata { padding-left: 16px; padding-top: 3px; font-size: 15px; color: #ffffff; vertical-align: middle;}
.cuponBox .botdata td {vertical-align: middle;}
.cuponBox .sharetxt { font-size:10px; color: #ffffff;}
.cuponBox .loginPopup {display: none; left: 17%; padding: 6px 15px; position: absolute; top: 15px; width: 401px; background: #fff; border: 5px solid #dddddd;}
.cuponBox .loginBg { width: 100%; float: left; padding: 0px; margin: 0px;}
.cuponBox .loginHd { border-bottom: 1px solid #BDBDBD; color: #000000; font-family: 'Trebuchet MS'; font-size: 18px; font-weight: normal; margin: 0; padding: 0 0 8px;}
.cuponBox .sharecupon { font-size:12px; color:#ffffff;}
.cuponBox .btmLink { float:left; background:#ffffff; border:1px solid #d6d7da; font-size:11px; color:#838080; text-decoration:none; padding:0 8px; height:17px;}
.cuponBox .bottomsec { padding: 0px 5px 5px 5px;}
.cuponBox .btmTxt { padding-left:10px; background:#dcdfe4; text-align:left; line-height:27px; font-size:12px; color:#848484;}
.cuponBox .social img { width:40%;}
.cuponBox .social img.msg { width:44%;}
.cuponBox .listimg img { width:84%;}
.cuponBox .loginHd .closeImg img { width:120%;}
.cuponBox .loginHd .closeImg a { position:absolute; top:-16px; right:-4px;}
.socialtrans{z-index: 2;background-color: White;position: fixed;top: 0;left: 0;width: 100%;height: 100%;border: none;-ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=50)';filter: alpha(opacity=50);-moz-opacity: 0.5;-khtml-opacity: 0.5;opacity: 0.5;}
.socialpopup{width: 100%;position: absolute;top: 15%;left: 0px;z-index: 3;height: 1px;}
.socialclosebtn{position: absolute;right: 0;margin-top: -10px;margin-right: -10px;cursor: pointer;}
.cuponBox input {padding: 1px; margin-bottom: 1px; color: #000000; font-size: 13px; font-family: Arial; border: inset 1px #EEEEEE}";

            }
            else
            {
                
                CSS = @"body { font-size: 1em ; font-family: Arial;}
.cuponBox .clr { clear:both; overflow: hidden; height: 0px; }
.cuponBox { margin:0 auto; width:60%; background:#dcdfe4;position:relative; line-height: 19px;}
.cuponBox h2 { text-align: center; padding: 0px; margin: 0px; font-size: 21px; line-height: 32px; color: #535456; font-weight: normal; border-bottom: none;}
.cuponBox .loginField {padding-bottom: 10px;}
.cuponBox .loginField .field { padding: 0 10px; background: url('..images/Coupon/field_bg.png') repeat-x scroll 0 0 transparent; border:1px solid #CDD1DB; height:28px; width:251px;}		
.cuponBox .loginField textarea { padding: 5px 10px; background: url('..images/Coupon/field_bg.png') repeat-x scroll 0 0 transparent; font-family:Arial, Helvetica, sans-serif; font-size:1em; border:1px solid #CDD1DB; height:50px; width:351px;}
.cuponBox .loginField .button { cursor:pointer; width:88px; height:37px; color:#ffffff; font-weight:bold; float:left; border:0px; background: url('http://dev.tapiton.com/images/Coupon/button_bg.png') repeat-x scroll 0 0 transparent; }
.cuponBox .loginField .button:hover { background: url('http://dev.tapiton.com/images/Coupon/button_bg_hover.png') repeat-x scroll 0 0 transparent;  }
.cuponBox .loginField a { line-height: 37px; float: right; font-family: Arial; color: #000000; font-size: 14px; text-decoration: none; padding-right: 28px;}}

.cuponBox .parentTabel { height: 100%; position: relative; border: 10px solid #f4f4f4}
.cuponBox .topbg { padding:5px 5px 0 5px;}
.cuponBox p {padding:0px; margin: 0px; text-align:center; font-size:19px; line-height:32px; color:#535456;}
.cuponBox p span {	font-size:27px; color:#2e789f; font-weight:bold; line-height:32px;}
.cuponBox .middlecont { padding:0px 5px 0px 5px;}
.cuponBox .logoImg { padding: 0 5px; text-align: center;}
.cuponBox .logoImg img { margin-top: 0px; width: 100%;}
.cuponBox .tabelPart { padding: 0px 0px 0px 0px;}
.cuponBox .innerTabel { padding-bottom: 1px; background: #fff; padding: 5px 5px 0px 5px; border: 1px solid #b2b3b7; border-bottom: 0px;}
.cuponBox .innerTabel td { padding: 3px 4px; background: #ffffff; font-size: 12px; line-height: 18px; color: #000000;}
.cuponBox .innerTabel .toprow td { background: #2d769c; font-size: 12px; line-height: 18px; color: #ffffff; font-weight: bold;}
.cuponBox .innerTabel td { padding: 3px 4px; background: #ffffff; font-size: 12px; line-height: 18px;  color: #000000;}
.cuponBox .innerTabel .grybg td { background:#F3F3F3;}
.cuponBox .botTabelbg { border-bottom: 1px solid #dcdfe4; background:#2e789f;}
.cuponBox .bordertxt { text-align: center; padding-bottom: 5px; border-bottom: 1px dashed #aabcc5;}
.cuponBox .botdata { padding-left: 16px; padding-top: 3px; font-size: 15px; color: #ffffff; vertical-align: middle;}
.cuponBox .botdata td {vertical-align: middle;}
.cuponBox .sharetxt { font-size:15px; color: #ffffff;}
.cuponBox .loginPopup {display: none; left: 17%; padding: 6px 15px; position: absolute; top: 15px; width: 401px; background: #fff; border: 5px solid #dddddd;}
.cuponBox .loginBg { width: 100%; float: left; padding: 0px; margin: 0px;}
.cuponBox .loginHd { border-bottom: 1px solid #BDBDBD; color: #000000; font-family: 'Trebuchet MS'; font-size: 18px; font-weight: normal; margin: 0; padding: 0 0 8px;}
.cuponBox .sharecupon { font-size:12px; color:#ffffff;}
.cuponBox .btmLink { float:left; background:#ffffff; border:1px solid #d6d7da; font-size:11px; color:#838080; text-decoration:none; padding:0 8px; height:17px;}
.cuponBox .bottomsec { padding: 0px 5px 5px 5px;}
.cuponBox .btmTxt { padding-left:10px; background:#dcdfe4; text-align:left; line-height:27px; font-size:12px; color:#848484;}
.cuponBox .social img { width:40%; min-width:20px;}
.cuponBox .social img.msg { width:44%;}
.cuponBox .listimg img { width:84%;}
.cuponBox .loginHd .closeImg img { width:120%;}
.cuponBox .loginHd .closeImg a { position:absolute; top:-16px; right:-4px;}
.socialtrans{z-index: 2;background-color: White;position: fixed;top: 0;left: 0;width: 100%;height: 100%;border: none;-ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=50)';filter: alpha(opacity=50);-moz-opacity: 0.5;-khtml-opacity: 0.5;opacity: 0.5;}
.socialpopup{width: 100%;position: absolute;top: 15%;left: 0px;z-index: 3;height: 1px;}
.socialclosebtn{position: absolute;right: 0;margin-top: -10px;margin-right: -10px;cursor: pointer;}
.cuponBox input {padding: 1px; margin-bottom: 1px; color: #000000; font-size: 13px; font-family: Arial; border: inset 1px #EEEEEE}";

            }
            context.Response.Write(CSS);

        }
        public static bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            ////FIRST TRY BUILT IN ASP.NT CHECK
            //if (context.Request.Browser.IsMobileDevice)
            //{
            //    return true;
            //}
            ////THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            //if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            //{
            //    return true;
            //}
            ////THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            //if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
            //    context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            //{
            //    return true;
            //}
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }
            string sUA = HttpContext.Current.Request.UserAgent.Trim().ToLower();

            //if (HttpContext.Current.Request.Browser.IsMobileDevice || sUA.Contains("ipod") || sUA.Contains("iphone") || sUA.Contains("android") || sUA.Contains("opera mobi") || (sUA.Contains("windows phone os") && sUA.Contains("iemobile")) || sUA.Contains("fennec"))
            //    return true;

            return false;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}